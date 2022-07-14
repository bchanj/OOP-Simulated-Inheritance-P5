using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;
using System;

namespace BeaconTests
{
    [TestClass]
    public class strobeBeaconTests
    {
        readonly int[] testArr = { 1, 4, 70, 22, 3 };

        [TestMethod]
        public void testSignal_threeCalls_willAlternate()
        {
            int[] arr = testArr;
            strobeBeacon obj = new strobeBeacon(arr);
            int before = obj.signal();
            Assert.IsTrue(before == (obj.signal() * -1));
            Assert.IsTrue(before == obj.signal());
        }
    }
}
