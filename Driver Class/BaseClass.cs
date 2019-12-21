using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviourDrivenParllelPrjt.Utilities;
using OpenQA.Selenium.Remote;

namespace BeheviourDrivenDevelopment.Driver_Class
{
   public  class BaseClass
    {
        public IWebDriver Driver;
        public BaseClass() {

        }

        public IWebDriver GetWebdriver() {
            if (1 == 1)
            {
                Driver = InitilizeWebDriver();
            }
            
            return Driver;
        }

        public IWebDriver InitilizeWebDriver()
        {
            //if (ConfigurationManager.AppSettings["browser"].Equals("chrome")) {

            //    DesiredCapabilities cap = new DesiredCapabilities();
            //    cap.SetCapability("browserName","firefox");

            //    IWebDriver _driver = new RemoteWebDriver(new System.Uri("http://172.17.185.81:4444/wd/hub/"), cap);
            //    return _driver;


            //}

            try {
                IWebDriver _driver = SelectBrowser(Driver);
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Manage().Window.Maximize();
                return _driver;
            }
            catch(WebDriverException e)
            {
                Assert.Fail("failed to load driver:"+e.Message);
                return null;
            }
            
        }

        public IWebDriver SelectBrowser(IWebDriver _driver)
        {
                    var BrowserType =ConfigurationManager.AppSettings["browser"];

            switch (BrowserType.ToLower())
            {
                case "chrome":
                    _driver = new ChromeDriver();
                    break;

                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
            }

            return _driver;

        }

        public void NavigateToUrl(string url) {

            Driver.Navigate().GoToUrl(url);
        }

        public void CloseInstance(IWebDriver Driver) {
            Driver.Quit();
        }
    }
}
