// Brandon Chan
// dataMod.cs - Child object of dataFilter.
// 10/3/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019

/* CLASS INVARIANT 
 * - This will create the object, dataMod, which is a child to the dataFilter parent object.
 * - Any negative values in the encapsulated sequence will be made positive, as well as negative encapsulated numbers.
 *   The dataFilter objects and its children are not built to handle negative values.
 * - If the number passed in for encapsulation is not prime, it will be changed to the nearest upper prime value.
 * - When constructing a dataFilter object or any of its children, an exception will be thrown if a null sequence or one with
 *   any invalid size is passed in.
 * - If given appropriate values, the constructor will create a dataFilter object or child object set to Large mode (true) by default,
 *   it will have an encapsulated prime number 'p' and an encapsulated sequence 'seq', with exclusively positive values.
 * - dataMod inherits all of dataFilters resources and member functions.
 * - The only way to modify the encapsulated sequence of a dataMod object is through the scramble method which orders n/2 pairs in a sequence 
 *   according to the current mode. This ordered sequence is then returned. The filter method will only return a product of the sequence,
 *   which is a subset of the encapsulated sequence, but will not alter the sequence itself.
 * - The toggleMode method is the only way to swap between large and small modes.
 * - Sequences passed into scramble or constructor but be of valid size (minimum length of 4).
 * - Following calls to scramble on a dataMod object, some values in the encapsulated sequence may be altered (set to 2).
 * - Calls to filter on dataMod will return nearly the same result as on a dataFilter object except subset values may be
 *   incremented/decremented depending on mode (large/small).
 * - Calling scramble from dataMod will never make the sequence invalid because it doesn't alter the size of the sequence.
 */

/* INTERFACE INVARIANT
 * - dataMod objects will provide correct functionality assuming they were constructed with valid sequences.
 * - filter method will only provide correct functionality given that a prime value within the range of values was provided.
 *   For example, 7 is a number within the range of an array of values: 3, 24, 4, 81, 92
 * - scramble method will only provide correct functionality if the encapsulated array is of size 4 or greater.
 * - Calling filter on a null sequence will yield the encapsulated prime number 'p' stored with in an array of size 1.
 */

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace P5
{
    public class dataMod: dataFilter, dfInterface
    {
        // PRE: Must pass in an array of size 4 or greater with non-negative values. Number passed in must be prime and non-negative. 
        // POST: Object will be created, encapsulating the array and prime number passed in, mode will be set to Large by default.
        public dataMod(int num, int [] arr): base(num, arr) {}

        // PRE: Must be called on an object with a valid sequence (must not be null)
        // POST: Will return a subset of the sequence with incremented/decremented values according to the current mode. 
        //       If p is equal to a value in the sequence, the resulting sequence will exclude it. 
        //       May returned p encapsulated in a sequence if size of sequence is invalid.
        //       Reordering occurs by swapping n/2 pairs of values within the sequence according to mode.
        //       May return an empty array.
        public override int[] filter()
        {
            int[] dataModArr = base.filter();
            if (sizeToggle) { for (int i = 0; i < dataModArr.Length; i++) { dataModArr[i]++; }
            } else { for (int i = 0; i < dataModArr.Length; i++) { dataModArr[i]--; } }
            return dataModArr;
        }

        // POST: Encapsulated sequence may be replaced by reordered version of the sequence passed in. 
        //       The client given sequence may be altered. All prime numbers will be replaced with 2 in
        //       encapsulated sequence and the sequence will be returned.
        public override int[] scramble(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (isAPrimeNum(arr[i])) { arr[i] = 2; }
            }
            int[] dataModArr = base.scramble(arr);
            return dataModArr;
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
