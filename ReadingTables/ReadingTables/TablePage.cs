using System;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace ReadingTables
{
    public class TablePage : Base
    {
        public TablePage()
        {
            PageFactory.InitElements(Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//table")]
        public IWebElement Table
        {
            get;
            set;
        }
    }
}
