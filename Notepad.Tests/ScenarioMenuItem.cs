using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Notepad.Tests
{
    [TestClass]
    public class ScenarioMenuItem : NotepadSession
    {
        [TestMethod]
        public void MenuItemEdit()
        {
            // Select Edit -> Time/Date to get Time/Date from Notepad
            Assert.AreEqual(string.Empty, editBox.Text);
            session.FindElementByName("Edit").Click();
            session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Time/Date\")]").Click();
            string timeDateString = editBox.Text;
            Assert.AreNotEqual(string.Empty, timeDateString);

            // Select all text, copy, and paste it twice using MenuItem Edit -> Select All, Copy, and Paste
            session.FindElementByName("Edit").Click();
            session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Select All\")]").Click();
            session.FindElementByName("Edit").Click();
            session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Copy\")]").Click();
            session.FindElementByName("Edit").Click();
            session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Paste\")]").Click();
            session.FindElementByName("Edit").Click();
            session.FindElementByXPath($"//MenuItem[starts-with(@Name, \"Paste\")]").Click();

            // Verify that the Time/Date string is duplicated
            Assert.AreEqual(timeDateString + timeDateString, editBox.Text);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }
    }
}
