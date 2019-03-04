using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Notepad.Tests
{
    [TestClass]
    public class ScenarioEditor : NotepadSession
    {
        [TestMethod]
        public void EditorEnterText()
        {
            // Type mixed text and apply shift modifier to 7890_ to generate corresponding symbols
            Thread.Sleep(TimeSpan.FromSeconds(2));
            editBox.SendKeys("abcdeABCDE 12345" + Keys.Shift + "7890-" + Keys.Shift + @"!@#$%");
            Assert.AreEqual(@"abcdeABCDE 12345&*()_!@#$%", editBox.Text);
        }

        [TestMethod]
        public void EditorKeyboardShortcut()
        {
            // Type a known text sequence, select, copy, and paste it three times
            editBox.SendKeys("789");
            editBox.SendKeys(Keys.Control + "a" + Keys.Control); // Select all using Ctrl + A keyboard shortcut
            editBox.SendKeys(Keys.Control + "c" + Keys.Control); // Copy using Ctrl + C keyboard shortcut
            editBox.SendKeys(Keys.Control + "vvv" + Keys.Control); // Paste 3 times using Ctrl + V keyboard shortcut
            Assert.AreEqual("789789789", editBox.Text);
        }

        [TestMethod]
        public void EditorNonPrintableShortcutKey()
        {
            // Press F5 to get Time/Date from Notepad
            Assert.AreEqual(string.Empty, editBox.Text);
            editBox.SendKeys(Keys.F5);
            Assert.AreNotEqual(string.Empty, editBox.Text);
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
