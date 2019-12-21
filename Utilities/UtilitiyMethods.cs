using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BehaviourDrivenParllelPrjt.Utilities
{
    public class UtilitiyMethods
    {
        private readonly IWebDriver _driver;
        public UtilitiyMethods(IWebDriver driver)
        {
            this._driver = driver;
        }

       
    }
}
