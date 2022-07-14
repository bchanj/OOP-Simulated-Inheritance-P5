// Brandon Chan
// dataCut.cs - Child object of dataFilter.
// 10/3/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019

/* CLASS INVARIANT 
 * - This will create the object, dataCut, which is a child to the dataFilter parent object.
 * - Any negative values in the encapsulated sequence will be made positive, as well as negative encapsulated numbers.
 *   The dataFilter objects and its children are not built to handle negative values.
 * - If the number passed in for encapsulation is not prime, it will be changed to the nearest upper prime value.
 * - When constructing a dataFilter object or any of its children, an exception will be thrown if a null sequence or one with
 *   any invalid size is passed in.
 * - If given appropriate values, the constructor will create a dataFilter object or child object set to Large mode (true) by default,
 *   it will have an encapsulated prime number 'p' and an encapsulated sequence 'seq', with exclusively positive values.
 * - dataCut inherits all of dataFilters resources and member functions.
 * - The only way to modify the encapsulated sequence of a dataCut object is through the scramble method which orders n/2 pairs in a sequence 
 *   according to the current mode. This ordered sequence is then returned. The filter method will only return a product of the sequence,
 *   which is a subset of the encapsulated sequence, but will not alter the sequence itself.
 * - The toggleMode method is the only way to swap between large and small modes.
 * - Sequences passed into scramble or constructor but be of valid size (minimum length of 4).
 * - Following calls to scramble or filter on a dataCut object, the encapsulated sequence may be altered and may become illegal
 *   (length 3 or less) and will be prone to triggering exception throws on further calls to filter or scramble.
 * - Calls to scramble will change the encapsulated sequence to a modified version of the passed in sequence, which removes
 *   all values in common with the sequence passed into the most recent call to scramble.
 * - Calls to filter will filter the sequence according to mode, while also removing the max value (if large) or removing the lowest
 *   value (if small). However, this function does not alter the encapsulated sequence itself.
 * - The first call to scramble will only do the original dataFilter scramble and has the main 
 *   purpose of setting the previousScramble array up for future usage.  
 */

/* INTERFACE INVARIANT
 * - dataCut objects will provide correct functionality assuming they were constructed with valid sequences.
 * - filter method will only provide correct functionality given that a prime value within the range of values was provided.
 *   For example, 7 is a number within the range of an array of values: 3, 24, 4, 81, 92
 * - scramble method will only provide correct functionality if the encapsulated array is of size 4 or greater.
 * - Passing in the same array into two separate calls to scramble ensures that an dataCut object will have an illegal sequence,
 *   specifically null. Likewise passing an array with all but 3 similarities will also produce an illegal sequence because it will
 *   be cut below the legal array length of 4.
 * - Calling filter on a null sequence will yield the encapsulated prime number 'p' stored with in an array of size 1.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace P5
{
    public class dataCut: dataFilter, dfInterface
    {
        protected int[] previousScramble;
        private bool firstScramble;

        // PRE: Must pass in an array of size 4 or greater with non-negative values. Number passed in must be prime and non-negative. 
        // POST: Object will be created, encapsulating the array and prime number passed in, mode will be set to Large by default. 
        //       firstScramble will be set to true to indicate that the previousScramble array has not been initialzed yet.
        public dataCut(int num, int[] arr) : base(num, arr) { firstScramble = true; }

        // PRE: Must be called on an object with a sequence of length greater than 3. Encapsulated prime number must be within the range
        //      of numbers encapsulated in the sequence.
        // POST: Will return a filtered sequence in addition to removing the max/min, according to the mode.
        //       Mode set to large will return values larger than p and max will be removed from returned sequence.
        //       Mode set to small will return values smaller than p and min will be removed from returned sequence.
        //       Filter does NOT alter the encapsulated sequence itself.
        //       Filter may return an empty array.
        //       Reordering occurs by swapping n/2 pairs of values within the sequence according to mode.
        //       May return an empty array.
        public override int[] filter()
        {
            int target;
            int targetLoc = 0;
            int temp;
            int[] dataCutArr = base.filter();
            if (seq.Length <= 0 || dataCutArr.Length <= 0) {
                int[] checkNull = new int[1];
                checkNull[0] = p;
                return checkNull;
            }
            if (sizeToggle) {
                target = dataCutArr[0];
                for (int i = 1; i < dataCutArr.Length; i++) {
                    if (dataCutArr[i] > target) { 
                        target = dataCutArr[i];
                        targetLoc = i;
                    }
                }
            } else {
                target = dataCutArr[0];
                for (int i = 1; i < dataCutArr.Length; i++) {
                    if (dataCutArr[i] < target) { 
                        target = dataCutArr[i];
                        targetLoc = i;
                    }
                }
            }
            temp = dataCutArr[0];
            dataCutArr[0] = dataCutArr[targetLoc];
            dataCutArr[targetLoc] = temp;
            int[] finishedArr = new int[dataCutArr.Length - 1];
            for (int i = 0; i < finishedArr.Length; i++) { finishedArr[i] = dataCutArr[i + 1]; }
            return finishedArr;
        }

        // PRE: Must be called on an object with a valid encapsulated sequence (size 4 or greater)
        // POST: Encapsulated sequence will decrease in size and may become invalid for further usage of the object.
        public override int[] scramble(int[] arr)
        {
            if (seq.Length < MINSEQSIZE) { throw new System.ArgumentException("Sequence of dataCut object must be greater than length 3"); }
            int subtractSize = 0;
            int finalArrCount = 0;
            int finalArrSize;
            int[] storeNextPrev;

            if (firstScramble) {
                firstScramble = false;
                int[] firstRunArr = base.scramble(arr);
                previousScramble = new int[firstRunArr.Length];
                previousScramble = firstRunArr;
                return firstRunArr;
            }
            int[] dataCutArr = arr;
            storeNextPrev = new int[dataCutArr.Length];
            for (int i = 0; i < storeNextPrev.Length; i++) { storeNextPrev[i] = dataCutArr[i]; }
            for (int i = 0; i < dataCutArr.Length; i++) {
                for (int j = 0; j < previousScramble.Length; j++) {
                    if (dataCutArr[i] == previousScramble[j]) { dataCutArr[i] = -1; subtractSize++; }
                }
            }
            previousScramble = storeNextPrev;
            if (dataCutArr.Length > subtractSize) { finalArrSize = dataCutArr.Length - subtractSize; }
            else { finalArrSize = subtractSize - dataCutArr.Length; }
            int[] finalArr = new int[finalArrSize];
            for (int i = 0; i < dataCutArr.Length; i++) {
                if (dataCutArr[i] != -1) {
                    finalArr[finalArrCount] = dataCutArr[i];
                    if (finalArrCount != finalArr.Length - 1) { finalArrCount++; }
                }
            }
            finalArr = base.scramble(finalArr);
            return finalArr;
        }
    }
}

/* IMPLEMENTATION INVARIANT 
 * - Chose to flag objects for removal with -1 in the scramble function.
 * - Chose to make the use of negative numbers illegal in seq because -1 is used to flag items for removal in the dataCut child object. 
 * - Chose to not allow negative encapsulated prime numbers because all sequence values are positive.
 * - If sequence is null or of an invalid size, chose to throw exceptions for the constructor and scramble method for easier readability 
 *   and transparency.
 * - p and seq are used in tandem in the filter method, p is compared to indexes withtin the encapsulated sequence.
 * - Required all sequences to have a length of 4 or greater to make sure that scramble and filter can be called with proper functionality,
 *   even for dataCut objects.
 * - Chose to also check if sequence is a valid size and to throw an exception if not because of the nature of the method (can delete large
 *   amounts of data from a sequence). 
 * - Stored the passed in sequence from the previous call to scramble for future comparisons. Therefore the first call to scramble will 
 *   only do the dataFilter scramble and has the main purpose of setting the previousScramble array.
 */