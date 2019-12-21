using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviourDrivenParllelPrjt.pages
{
    public class GooglePage
    {
        private IWebDriver _driver;
       public  GooglePage(IWebDriver driver) {
            _driver = driver;
        }
        //for google.com
        //private static By searchbox = By.XPath("//input[@name='q']");
        //for dropdown javapoint
        //     private static By dropdown = By.XPath("//*[@id='testingDropdown']");
        //dropdwon automationtesting.in 
        private static By dropdown = By.XPath("//select");
        // automationtesting.in
        private static By maleRadio = By.XPath("//input[@value='Male']");

        public string enterTextIntoSearch()
        {
            // wait.Until(ExpecteConditions)
            IList<IWebElement> options=new List<IWebElement>();
            if (IsElementDisplayed(maleRadio))
            {

                _driver.FindElement(maleRadio).Click();
                //_driver.FindElement(searchbox).SendKeys("Rajesh ");
                Thread.Sleep(3000);
            }
            return null;
        }

        public void ScrollIntoView()
        {
            IList<IWebElement> options = new List<IWebElement>();
            if (IsElementDisplayed(dropdown))
            {
                SelectElement select = new SelectElement(_driver.FindElement(dropdown));
                //select.SelectByText("Manual Testing");
                select.SelectByText("30");
                //select.SelectByValue("Database");
                options = select.Options;
                EventFiringWebDriver fire = new EventFiringWebDriver(_driver);
                fire.ExecuteScript("document.querySelector('div[role=\"rowgroup\"][class*=\"ui-grid-viewport\"]').scrollTop=800");


            }
        }
        public bool IsElementDisplayed(By element)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            bool elt;
            try
            {
                elt = wait.Until<bool>(driver =>
                {

                    var elementTobeDisplayed = driver.FindElement(element);
                    return (elementTobeDisplayed.Displayed) ? true : false;

                });
            }
            catch(WebDriverTimeoutException )
            {
                return false;
            }
            //catch (TimeoutException)
            //{
            //    return false;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
            return elt;
        }
    }
}

