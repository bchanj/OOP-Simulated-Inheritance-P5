using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using P5;

namespace CrossProductTests
{
    [TestClass]
    public class crossProductTests
    {
        readonly int[] testArr = { 1, 4, 70, 22, 3 };
        readonly int[] testArr2 = { 4, 5, 70, 1, 4 };
        readonly int[] invalidArr = { 1, 2, 3 };
        const int TESTARRSIGNAL = 20;
        const int TESTQUIRKYSIGNAL = 108;

        [TestMethod]
        public void testMergedFunctionalityAndScramble_on_signalWillChange()
        {
            int[] arr = testArr;
            int[] arr2 = testArr2;
            crossProduct obj1 = new crossProduct(0, 0, testArr, 7);
            int prevSignal = obj1.signal();
            obj1.scramble(arr2);
            Assert.AreNotEqual(prevSignal, obj1.signal());
        }

        [TestMethod]
        public void testMergedFunctionality_off_willDisableFunctionality()
        {
            int[] arr = testArr;
            crossProduct obj1 = new crossProduct(0, 0, arr, 7);
            Assert.IsTrue(obj1.toggleOn() == false);
            Assert.IsTrue(obj1.signal() == 0);
            Assert.IsTrue(obj1.scramble(arr)[0] == -1);
            Assert.IsTrue(obj1.filter()[0] == -1);
        }

        [TestMethod]
        public void testErrorHandling_invalidArr_exceptionThrown()
        {
            int[] arr = invalidArr;
            crossProduct obj;
            Assert.ThrowsException<ArgumentException>(() => obj = new crossProduct(0, 0, arr, 7));
        }

        [TestMethod]
        public void testSignal_on_returnsCorrectInteger()
        {
            int[] arr = testArr;
            crossProduct obj1 = new crossProduct(0, 0, arr, 7);
            Assert.AreEqual(obj1.signal(), TESTARRSIGNAL);
        }
        
        [TestMethod]
        public void testFilter_on_returnsCorrectArray()
        {
            int[] arr = testArr;
            crossProduct obj = new crossProduct(0, 0, arr, 7);
            Assert.IsTrue(obj.filter().Length == 2);
        }

        [TestMethod]
        public void testToggleMode_on_filterWillReturnDifferentResults()
        {
            int[] arr = testArr;
            crossProduct obj = new crossProduct(0, 0, arr, 7);
            int[] temp = obj.filter();
            obj.toggleMode();
            Assert.AreNotEqual(obj.filter(), temp);
            Assert.IsTrue(obj.filter().Length == 3);
            Assert.IsTrue(temp.Length == 2);
        }

        [TestMethod]
        public void testRecharge_noCharge_chargedCorrectResponseFromSignal()
        {
            int[] arr = testArr;
            crossProduct obj = new crossProduct(0, 0, arr, 7);
            for (int i = 0; i < 5; i++) { obj.signal(); }
            Assert.AreEqual(obj.signal(), 0);
            obj.recharge();
            Assert.AreEqual(obj.signal(), TESTARRSIGNAL);
        }

        [TestMethod]
        public void testCrossProductRepresentation_on_correctResponseFromSignalAndScramble()
        {
            int[] arr = testArr;
            int[] arr2 = testArr2;
            crossProduct obj = new crossProduct(1, 2, arr, 7);
            Assert.AreEqual(obj.signal(), TESTQUIRKYSIGNAL);
            obj.scramble(arr2);
            Assert.AreNotEqual(obj.signal(), TESTQUIRKYSIGNAL);
        }
    }
}
