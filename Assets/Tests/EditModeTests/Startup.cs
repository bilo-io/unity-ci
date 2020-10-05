using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Startup
    {
        [Test]
        public void StartupSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator StartupWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [Test]
        [UnityPlatform(RuntimePlatform.WindowsPlayer)]
        public void TestWindowsPlayer()
        {
            Assert.AreEqual(Application.platform, RuntimePlatform.WindowsPlayer);
        }

        [Test]
        [UnityPlatform(RuntimePlatform.WindowsEditor)]
        public void TestWindowsEditor()
        {
            Assert.AreEqual(Application.platform, RuntimePlatform.WindowsEditor);
        }

        [Test]
        [UnityPlatform(RuntimePlatform.OSXPlayer)]
        public void TestOSXPlayer()
        {
            Assert.AreEqual(Application.platform, RuntimePlatform.OSXPlayer);
        }

        [Test]
        [UnityPlatform(RuntimePlatform.OSXEditor)]
        public void TestOSXEditor()
        {
            Assert.AreEqual(Application.platform, RuntimePlatform.OSXEditor);
        }

        [Test]
        [UnityPlatform(exclude = new[] { RuntimePlatform.WindowsEditor })]
        public void PlayerIsNotEditor()
        {
            Assert.AreNotEqual(Application.platform, RuntimePlatform.WindowsEditor);
        }

        [Test]
        public void LogAssertExample()
        {
            //Expect a regular log message
            LogAssert.Expect(LogType.Log, "Log message");
            //A log message is expected so without the following line
            //the test would fail
            Debug.Log("Log message");

            //Without expecting an error log, the test would fail
            LogAssert.Expect(LogType.Error, "Error message");
            //An error log is printed
            Debug.LogError("Error message");
        }
    }
}

