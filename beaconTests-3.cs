using Microsoft.VisualStudio.TestTools.UnitTesting;
using P5;
using System;

namespace BeaconTests
{
    [TestClass]
    public class beaconTests
    {
        readonly int[] testArr = { 1, 4, 70, 22, 3 };
        readonly int[] negativeArr = { -1, 4, 70, -22, -3 };
        const int TESTARRSIGNAL = 20;
        readonly int[] zeroArr = { 0, 0, 0, 0, 0 };
        const int CHARGEDMAX = 5;

        [TestMethod]
        public void testConstruction_arrOfZeros_throwsException()
        {
            int[] arr = zeroArr;
            beacon obj;
            Assert.ThrowsException<ArgumentException>(() => obj = new beacon(zeroArr));
        }

        [TestMethod]
        public void testConstruction_negativeArr_turnsPositive()
        {
            int[] arr = negativeArr;
            beacon obj = new beacon(arr);
            Assert.AreEqual(obj.signal(), TESTARRSIGNAL);
        }

        [TestMethod]
        public void testSignal_on_returnsAvg()
        {
            int[] arr = testArr;
            beacon obj = new beacon(arr);
            Assert.AreEqual(obj.signal(), TESTARRSIGNAL);
        }

        [TestMethod]
        public void testSignal_off_returnsZero()
        {
            int[] arr = testArr;
            beacon obj = new beacon(arr);
            obj.toggleOn();
            Assert.AreEqual(obj.signal(), 0);
        }

        [TestMethod]
        public void testRechargeAndSignal_noCharge_fullyCharged()
        {
            int[] arr = testArr;
            beacon obj = new beacon(arr);
            for (int i = 0; i < CHARGEDMAX; i++)
            {
                obj.signal();
            }
            Assert.AreEqual(obj.signal(), 0);
            obj.recharge();
            Assert.AreEqual(obj.signal(), TESTARRSIGNAL);
        }

        [TestMethod]
        public void testRecharge_off_noChange()
        {
            int[] arr = testArr;
            beacon obj = new beacon(arr);
            for (int i = 0; i < CHARGEDMAX; i++)
            {
                obj.signal();
            }
            obj.recharge();
            obj.toggleOn();
            Assert.AreEqual(obj.signal(), 0);
        }

        [TestMethod]
        public void testToggleOn_startsOn_returnsFalse()
        {
            int[] arr = testArr;
            beacon obj = new beacon(arr);
            Assert.IsTrue(obj.toggleOn() == false);
        }

        [TestMethod]
        public void testToggleOn_startsOff_returnsTrue()
        {
            int[] arr = testArr;
            beacon obj = new beacon(arr);
            obj.toggleOn();
            Assert.IsTrue(obj.toggleOn() == true);
        }

        [TestMethod]
        public void testSetSeq_allZeroSeq_throwsException()
        {
            int[] arr = testArr;
            int[] invalidArr = zeroArr;
            beacon obj = new beacon(arr);
            Assert.ThrowsException<ArgumentException>(() => obj.setSeq(zeroArr));
        }
    }
}
