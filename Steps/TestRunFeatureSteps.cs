using BehaviourDrivenParllelPrjt.pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace BeheviourDrivenDevelopment.Steps
{
    [Binding]
    public class TestRunFeatureSteps
    {
        private readonly IWebDriver _driver;
       private readonly GooglePage _google;
        public TestRunFeatureSteps(IWebDriver driver,GooglePage google) {
            _driver = driver;
            _google = google;
        }
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            //ScenarioContext.Current.Pending();
           // IWebElement element= _driver.FindElement(By.XPath("//input[@name='q']"));
            // options= select.Options;
        
            if (p0 != 6)
            {
               var text= _google.enterTextIntoSearch();
                Assert.Multiple(()=>{
                    Assert.IsNotEmpty(text);
                    Assert.AreEqual(text,null);
                });
            }
                
            else
                Assert.Fail("unknown exception occured");
        }
        
        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            //ScenarioContext.Current.Pending();
            Assert.IsTrue(true);
        }
        
        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
            //ScenarioContext.Current.Pending();
            Assert.IsTrue(true);
        }
    }
}
