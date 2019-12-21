using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BehaviourDrivenParllelPrjt.pages;
using BeheviourDrivenDevelopment.Driver_Class;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BeheviourDrivenDevelopment
{
    [Binding]
    public  sealed class Hooks:BaseClass
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        public IWebDriver _driver;
        public static string RunID;
        public readonly string screenshotPath = @"" + AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", ConfigurationManager.AppSettings["ScreenshotFolderPath"]);
        private   ExtentTest featureName;
       // [ThreadStatic]
        private   ExtentTest scenario;
        private   static ExtentReports extent;
        //private static KlovReporter klov;
        private static readonly IDictionary<string,ExtentTest> featureNames =new Dictionary<string,ExtentTest>();
        private readonly IObjectContainer _iObjectContainer;
        private readonly TestContext _testContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        //public Hooks() { }

        public Hooks(IObjectContainer obj, TestContext testContext, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            this._iObjectContainer = obj;
            this._featureContext = featureContext;
            this._scenarioContext = scenarioContext;
            this._testContext = testContext;
        }
        [BeforeTestRun]
        public  static void SetupRun()
        {
            var htmlReporter = new ExtentHtmlReporter(@""+AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", ConfigurationManager.AppSettings["rPath"]));
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            RunID= Guid.NewGuid().ToString();
        }
        [AfterTestRun]
        public static void StopRun()
        {
            extent.Flush();
        }

        [BeforeFeature]
        public static void extentreport()
        {
            
        }
        [BeforeScenario]
        public void SetupTest()
        {
            string text = _featureContext.FeatureInfo.Title;
            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            //featureNames.Add(featureName);
       
            if (featureNames != null && featureNames.Any(f=>f.Key.Contains(text)))
            {
                extent.RemoveTest(featureName);
               // featureNames.Remove(featureName);
                scenario = featureNames.Where(f => f.Key.Contains(text)).Select(f => f.Value.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title).AssignCategory(_scenarioContext.ScenarioInfo.Tags.FirstOrDefault())).FirstOrDefault();
            }
            else
            {
                    scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title).AssignCategory(_scenarioContext.ScenarioInfo.Tags.FirstOrDefault());
                featureNames.Add(text,featureName);
            }

            //extent.StartedReporterList.Contains(featureName);
            _driver = GetWebdriver();
                _driver.Manage().Window.Size = new Size(1280, 1240);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
               _iObjectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            GooglePage gpage = new GooglePage(_driver);
            _iObjectContainer.RegisterInstanceAs<GooglePage>(gpage);
            //NavigateToUrl("https://www.testandquiz.com/selenium/testing.html");
            //webtable
            // NavigateToUrl("http://demo.automationtesting.in/WebTable.html");
            //register page
            NavigateToUrl("http://demo.automationtesting.in/Register.html");
            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterStep]
        public void afterStep()
        {
            if (_scenarioContext.TestError != null)
            {
                string testName = _scenarioContext.ScenarioInfo.Title + "_" + RunID;
                TakeScreenshot(testName);

                switch ((Gerkin)Enum.Parse(typeof(Gerkin), _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString()))
                {
                    case Gerkin.Given:
                scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(SafeText(testName)).Build()).AddScreenCaptureFromPath(SafeText(testName));
                        break;
                    case Gerkin.Then:
                        scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message).AddScreenCaptureFromPath(SafeText(testName));
                        break;
                    case Gerkin.When:
                        scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message).AddScreenCaptureFromPath(SafeText(testName));
                        break;
                    case Gerkin.And:
                        scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message).AddScreenCaptureFromPath(SafeText(testName));
                        break;
                }

                
            }
            else
            {
                switch ((Gerkin)Enum.Parse(typeof(Gerkin),_scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString()))
                {
                    case Gerkin.Given:
                        scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case Gerkin.Then:
                        scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case Gerkin.When:
                        scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case Gerkin.And:
                        scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                }
            }
        }
        [AfterScenario]
        public void TearDown()
        {
            if (_scenarioContext.TestError != null)
            {
                Console.WriteLine(_scenarioContext.TestError.Message);               
            }
                //TODO: implement logic that has to run after executing each scenario
                CloseInstance(Driver);
        }

        public enum Gerkin { Given,Then,And,When}

        public string SafeText(string testname)
        {
            return screenshotPath+testname
                   .Replace("\"", "")
                   .Replace("(", "")
                   .Replace(")", "")
                   .Replace(":", "")
                   .Replace(",", "")
                   .Replace(" ", "")
                   .Replace("_", "")
                   .Replace("~", "")+ ".png";
       
        }

        public void TakeScreenshot(string testName)
        {
            try
            {
                var ss = ((ITakesScreenshot)_driver).GetScreenshot();
                var screencapPath = SafeText(testName);
                Directory.CreateDirectory(screenshotPath);

                byte[] imageBytes = Convert.FromBase64String(ss.ToString());
                using (BinaryWriter bw = new BinaryWriter(new FileStream(screencapPath, FileMode.Append, FileAccess.Write)))
                {
                    bw.Write(imageBytes);
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
           }
        }

    }
    
}


