﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;

namespace Notepad.Tests
{
    public class NotepadSession
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723"; //for WinAppDriver.exe
        //protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/wd/hub"; //for Appium
        private const string NotepadAppId = @"C:\Windows\System32\notepad.exe";

        protected static WindowsDriver<WindowsElement> session;
        protected static WindowsElement editBox;

        public static void Setup(TestContext context)
        {
            // Launch a new instance of Notepad application
            if (session == null)
            {
                // Create a new session to launch Notepad application
                DesiredCapabilities appCapabilities = new DesiredCapabilities();
                appCapabilities.SetCapability("app", NotepadAppId);
                appCapabilities.SetCapability("platformName", "Windows");
                appCapabilities.SetCapability("deviceName", "WindowsPC");
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);
                Assert.IsNotNull(session);
                Assert.IsNotNull(session.SessionId);

                // Verify that Notepad is started with untitled new file
                Assert.AreEqual("Untitled - Notepad", session.Title);

                // Set implicit timeout to 1.5 seconds to make element search to retry every 500 ms for at most three times
                session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1.5);

                // Keep track of the edit box to be used throughout the session
                editBox = session.FindElementByClassName("Edit");
                Assert.IsNotNull(editBox);
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Close();

                try
                {
                    // Dismiss Save dialog if it is blocking the exit
                    session.FindElementByName("Don't Save").Click();
                }
                catch { }

                session.Quit();
                session = null;
            }
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Select all text and delete to clear the edit box
            editBox.SendKeys(Keys.Control + "a" + Keys.Control);
            editBox.SendKeys(Keys.Delete);
            Assert.AreEqual(string.Empty, editBox.Text);
        }

    }
}
