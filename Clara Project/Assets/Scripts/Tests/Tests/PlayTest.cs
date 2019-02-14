using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayTest {

    [Test]
    public void PlayTestSimplePasses() {
        // Use the Assert class to test conditions.
    }

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator _Move_Path_Test() {
        // Use the Assert class to test conditions.
        // yield to skip a frame

        //Assert.AreEqual()
        yield return null;
    }
}
