using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;
using System;

namespace BeaconTests
{
    [TestClass]
    public class quirkyBeaconTests
    {
        readonly int[] testArr = { 1, 4, 70, 22, 3 };
        const int TESTSIGNALPOST = 108;
        const int TOGGLEMAX = 3;

        [TestMethod]
        public void testSignal_oneCall_returnsCorrectInteger()
        {
            int[] arr = testArr;
            quirkyBeacon obj = new quirkyBeacon(testArr);
            Assert.AreEqual(obj.signal(), TESTSIGNALPOST);
        }

        [TestMethod]
        public void toggleOn_hitsToggleMax_NoMoreToggling()
        {
            int[] arr = testArr;
            quirkyBeacon obj = new quirkyBeacon(testArr);
            for (int i = 0; i < TOGGLEMAX; i++)
            {
                obj.toggleOn();
            }
            Assert.IsTrue(obj.toggleOn() == false);
        }
    }
}
