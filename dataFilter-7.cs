// Brandon Chan
// dataFilter.cs - Parent object to dataMod and dataCut child objects.
// 10/3/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019

/* CLASS INVARIANT 
 * - This will create the object, dataFilter, which is a parent to the dataMod and dataCut objects.
 * - Any negative values in the encapsulated sequence will be made positive, as well as negative encapsulated numbers.
 *   The dataFilter objects and its children are not built to handle negative values.
 * - If the number passed in for encapsulation is not prime, it will be changed to the nearest upper prime value.
 * - When constructing a dataFilter object or any of its children, an exception will be thrown if a null sequence or one with
 *   any invalid size is passed in.
 * - If given appropriate values, the constructor will create a dataFilter object or child object set to Large mode (true) by default,
 *   it will have an encapsulated prime number 'p' and an encapsulated sequence 'seq', with exclusively positive values.
 * - All parts of dataFilter will be inherited by the dataMod and dataCut child objects. There is no suppression of data involved.
 * - The only way to modify the encapsulated sequence of a dataFilter object is through the scramble method which orders n/2 pairs in a sequence 
 *   according to the current mode. This ordered sequence is then returned. The filter method will only return a product of the sequence,
 *   which is a subset of the encapsulated sequence, but will not alter the sequence itself.
 * - The toggleMode method is the only way to swap between large and small modes.
 * - Sequences passed into scramble or constructor but be of valid size (minimum length of 4).
 * - When dataFilter scramble is used by the dataCut child object, it may be used on an illegal sequence and throw an exception.
 */

/* INTERFACE INVARIANT
 * - dataFilter objects will provide correct functionality assuming they were constructed with valid sequences.
 * - filter method will only provide correct functionality given that a prime value within the range of values was provided.
 *   For example, 7 is a number within the range of an array of values: 3, 24, 4, 81, 92
 * - scramble method will only provide correct functionality if the encapsulated array is of size 4 or greater.
 * - Calling filter on a null sequence will yield the encapsulated prime number 'p' stored with in an array of size 1.
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace P5
{
    interface dfInterface
    {
        int[] scramble(int[] arr);
        int[] filter();
        void toggleMode();
    }
    public class dataFilter: dfInterface
    {
        protected readonly int p;
        protected bool sizeToggle;
        protected int[] seq;
        protected const int MINSEQSIZE = 4;

        // PRE: Must pass in an array of size 4 or greater with non-negative values. Number passed in must be prime and non-negative. 
        // POST: Object will be created, encapsulating the array and prime number passed in, mode will be set to Large by default.
        public dataFilter(int num, int[] arr)
        {
            if (arr.Length < MINSEQSIZE) { throw new System.ArgumentException("Parameter may not be null or of size 4 or greater: 'arr'"); }
            if (num < 0) { num = num * (-1); }
            if (!isAPrimeNum(num)) { p = findNearestPrime(num); }
            else { p = num; }
            for (int i = 0; i < arr.Length; i++) { if (arr[i] < 0) { arr[i] = arr[i] * (-1); } }
            sizeToggle = true;
            seq = arr;
        }

        // PRE: Must be called on an object with a valid encapsulated sequence (size 4 or greater)
        // POST: Encapsulated sequence may be replaced by reordered version of the sequence passed in. 
        //       The client given sequence may be altered. A reordered sequence may be returned.
        //       Reordering occurs by swapping n/2 pairs of values within the sequence according to mode.
        public virtual int[] scramble(int[] arr)
        {
            int temp;
            if (seq.Length < MINSEQSIZE) { throw new System.ArgumentException("Parameter may not be null or invalid length: 'arr'"); }
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 == 0 && i != arr.Length-1)
                {
                    if ((sizeToggle && arr[i] < arr[i + 1]) || (!sizeToggle && arr[i] > arr[i + 1]))
                    {
                        temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                    }
                }
            }
            seq = arr;
            return arr;
        }

        // PRE: Must be called on an object with a valid sequence (must not be null)
        // POST: Will return a subset of the sequence according to the current mode. If p is equal to a value
        //       in the sequence, the resulting sequence will exclude it. May returned p encapsulated in a sequence if
        //       size of sequence is invalid. May return an empty array.
        public virtual int[] filter()
        {
            int[] filtered;
            int sizeFiltered = 0;
            int seqCount = 0;
            if (seq.Length <= 0) {
                int[] checkNull = new int[1];
                checkNull[0] = p;
                return checkNull;
            }
            if (sizeToggle)
            {
                for (int i = 0; i < seq.Length; i++) {
                    if (seq[i] > p) { sizeFiltered++; }
                }
                filtered = new int[sizeFiltered];
                for (int i = 0; i < seq.Length; i++)
                {
                    if (seq[i] > p) {
                        filtered[seqCount] = seq[i];
                        seqCount++;
                    }
                }
                return filtered;
            } else {
                for (int i = 0; i < seq.Length; i++) {
                   if (seq[i] < p) { sizeFiltered++; }
                }
                filtered = new int[sizeFiltered];
                for (int i = 0; i < seq.Length; i++)
                {
                    if (seq[i] < p)
                    {
                        filtered[seqCount] = seq[i];
                        seqCount++;
                    }
                }
                return filtered;
            }
        }

        // POST: The mode of the object will be toggled between Large (true) and Small (false).
        public void toggleMode()
        {
            sizeToggle = !sizeToggle;
        }

        // POST: Returns a bool depending on if the passed in value is prime or not.
        protected bool isAPrimeNum(int val)
        {
            int count = 2;
            bool prime = true;
            int squareRoot = (int) Math.Sqrt(val);
            while (count < squareRoot+1 && prime)
            {
                if (val % count == 0) { prime = false; }
                else { count++; }
            }
            return prime;
        }

        // POST: Returns the upper nearest prime number.
        private int findNearestPrime(int num)
        {
            int upper = num;
            upper++;
            while (!isAPrimeNum(upper)) { upper++; }
            return upper;
        }
    }
}

/* IMPLEMENTATION INVARIANT 
 * - Chose to make the use of negative numbers illegal in seq because -1 is used to flag items for removal in the dataCut child object. 
 * - Chose to not allow negative encapsulated prime numbers because all sequence values are positive.
 * - If sequence is null or of an invalid size, chose to throw exceptions for the constructor and scramble method for easier readability 
 *   and transparency.
 * - p and seq are used in tandem in the filter method, p is compared to indexes withtin the encapsulated sequence.
 * - Required all sequences to have a length of 4 or greater to make sure that scramble and filter can be called with proper functionality,
 *   even for dataCut objects.
 */