using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Linq;

namespace SeleniumFirst
{
    [TestFixture]
    public class UnitTest1
    {
        private IWebDriver driver;
        //private ListInfo users;
        //private DataReader user;
        private const string email = "nazar.l135@gmail.com";
        private const string password = "lol123";

        [OneTimeSetUp]
        public void BeforeAllMethods()
        {
            //users = DataReader.LoadJson();
            driver = new ChromeDriver();           
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            NUnit.Framework.Assert.AreEqual(LogIn(email, password), true );
        }

        [Test,Order(1)]
        public void Add_newAddress()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/address");

            driver.FindElement(By.CssSelector("a.btn.btn-primary")).Click();
            FillData();

            string actual = driver.FindElement(By.CssSelector("div.alert.alert-success")).Text;
            string expected = "Your address has been successfully inserted";
            NUnit.Framework.Assert.AreEqual(actual, expected);

        }

        [Test, Order(2)]
        public void Edite_Address()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/address");
            driver.FindElement(By.XPath("//tbody/tr[last()]/td[@class = 'text-right']/a[text()='Edit']")).Click();
            FillData();
            string actual = driver.FindElement(By.CssSelector("div.alert.alert-success")).Text;
            string expected = "Your address has been successfully updated";

            NUnit.Framework.Assert.AreEqual(actual, expected);
        }

        [Test,Order(3)]
        public void Delete_Address()
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/address");
            driver.FindElement(By.XPath("//tbody/tr[last()]/td[@class = 'text-right']/a[text()='Delete']")).Click();
            string actual = driver.FindElement(By.CssSelector("div.alert.alert-success")).Text;
            string expected = "Your address has been successfully deleted";
            NUnit.Framework.Assert.AreEqual(expected, actual);
        }

        private bool LogIn(string email, string password)
        {
            driver.Navigate().GoToUrl("http://atqc-shop.epizy.com/index.php?route=account/login");

            driver.FindElement(By.Id("input-email")).Click();
            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys(email);

            driver.FindElement(By.Id("input-password")).Click();
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys(password);

            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            try
            {
                if (driver.FindElement(By.CssSelector("div.alert.alert-danger")).Text.Equals("Warning: No match for E-Mail Address and/or Password"))
                {
                    return false;
                }
            }
            //try
            //{
            //    driver.FindElement(By.XPath("//a[contains(@href,'logout')]"));
            //}
            catch (NoSuchElementException)
            {
                return true;
            }
            return true;
        }

        private void FillData()
        {
            driver.FindElement(By.Id("input-firstname")).Click();
            driver.FindElement(By.Id("input-firstname")).Clear();
            driver.FindElement(By.Id("input-firstname")).SendKeys("Nazar");

            driver.FindElement(By.Id("input-lastname")).Click();
            driver.FindElement(By.Id("input-lastname")).Clear();
            driver.FindElement(By.Id("input-lastname")).SendKeys("Lobodynskyi");

            driver.FindElement(By.Id("input-address-1")).Click();
            driver.FindElement(By.Id("input-address-1")).Clear();
            driver.FindElement(By.Id("input-address-1")).SendKeys("Stryjska");

            driver.FindElement(By.Id("input-city")).Click();
            driver.FindElement(By.Id("input-city")).Clear();
            driver.FindElement(By.Id("input-city")).SendKeys("Lviv");

            driver.FindElement(By.Id("input-country")).SendKeys("Ukraine");
            Thread.Sleep(2000);
            driver.FindElement(By.Id("input-zone")).SendKeys("Kyiv");

            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
        }

        private void LogOut()
        {
            driver.FindElement(By.XPath("//a[@title ='My Account']")).Click();
            //Thread.Sleep(2000);
            driver.FindElement(By.XPath("//ul[@class='dropdown-menu dropdown-menu-right']/li[last()]")).Click();
            Thread.Sleep(2000);
        }

        [OneTimeTearDown]
        public void AfterAllMethods()
        {
            LogOut();
            driver.Quit();
            driver.Dispose();
        }

    }
}
