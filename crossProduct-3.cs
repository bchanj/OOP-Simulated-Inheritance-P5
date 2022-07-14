// Brandon Chan
// crossProduct.cs
// 11/9/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019

using System;
using System.Collections.Generic;
using System.Text;

/* CLASS INVARIANT 
 * - Following construction of a crossProduct object, there is an internal construction of a dataFilter and beacon type object
 *   according to the first 2 integer parameters of the crossProduct constructor. Object is on by default and will encapsulate
 *   an integer sequence and integer number.
 * - Following scramble(), the encapsulated sequence of both internal objects and the crossProduct object itself will be altered,
 *   merging data.
 * - Following signal(), an integer signal will be returned based on the common encapsulated sequence between crossProduct and its
 *   internal object, merging functionality.
 * - toggleOn() will alternate the state of the object between on and off and will allow/inhibit use of the object.
 * - crossProduct utilizes the beacon and dataFilter interfaces as a guideline for its own functionality.
 * - Following a call to recharge(), the charge of the internal beacon object will be returned to its initial value.
 * - A crossProduct object has-a beacon and dataFilter object.
 * - crossProduct is not a beacon object nor a dataFilter object, but a combination of both that merges their capabilities and data.
 * - The state of the object and its internal objects is not changed through a call to filter()
 * - toggleMode() will alternate the object through large and small modes as documented in the dataFilter class hierarchy   
 * - Following construction of a crossProduct object using an array of an invalid size, an exception will be thrown.
 */

/* INTERFACE INVARIANT
 * - Sending in a 0, 1, 2 for parameter 1 will create a dataFilter, dataMod, or dataCut object respectively. 
 * - If a value besides 0, 1, or 2 is entered, it will result in the internal construction of a dataCut object.
 * - Sending in a 0, 1, 2 for parameter 2 will create a beacon, strobeBeacon, or quirkyBeacon object respectively. 
 * - If a value besides 0, 1, or 2 is entered, it will result in the internal construction of a quirkyBeacon object.
 * - Negative values not supported and will be converted to positive values.
 * - Calling any crossProduct method on a crossProduct object constructed through null argument will cause an exception
 *   to be thrown.
 * - Calling signal() on an object that is off will return 0.
 * - Arrays of all zero values not supported and will cause an exception to be thrown.
 * - Calling scramble() or filter() on a crossProduct object that is off will return an array of size 1 encapsulating a -1.
 * - Arrays of crossProduct objects allowed but must be individually instantiated with a valid construction of a crossProduct object
 *   (not made by null argument constructor).
 * - An off object will prevent all functionality except the ability to turn it back on (toggleOn())
 * - Must pass in an array of size 4 or greater or else an exception will be thrown.
 */

namespace P5
{
    public class crossProduct: beaconInterface, dfInterface
    {
        dataFilter dfObj;
        beacon bObj;
        int[] seq;
        int filterMode;
        int beaconMode;
        bool on;
        const int MINSEQSIZE = 4;

        // POST: Will create an invalid object that is used to instantiate arrays of crossProduct objects
        public crossProduct()
        {
            seq = null;
            filterMode = -1;
            beaconMode = -1;
            on = false;
            dfObj = null;
            bObj = null;
        }

        // PRE: Must not pass in an array of size 0, an array with negative values, or an array of all zeroes
        //      Parameters 1 and 2 must be values of 0 to 2. Negative encapsulated numbers not allowed. Sequence must be of size 4
        //      or greater.
        // POST: Negative values (array or number) will be made positive, may throw exception if null array passed in.
        //       Will create a crossProduct object which represents a combination of the dataFilter and beacon objects.
        //       If invalid number given for parameter 1, a dataCut object will be created.
        //       If invalid number given for parameter 2, a quirkyBeacon object will be created. 
        public crossProduct(int d, int b, int[] arr, int num)
        {
            if (arr.Length < MINSEQSIZE) 
            { throw new System.ArgumentException("Parameter may not be null or of size 4 or greater: 'arr'"); }

            seq = arr;

            int sum = 0;
            for (int i = 0; i < seq.Length; i++) { sum += seq[i]; }
            if (sum == 0) { throw new System.ArgumentException("Sequence must be not all zeroes", "seq"); }

            filterMode = d;
            beaconMode = b;
            on = true;

            if (filterMode == 0) { dfObj = new dataFilter(num, arr); }
            else if (filterMode == 1) { dfObj = new dataMod(num, arr); }
            else { dfObj = new dataCut(num, arr); }

            if (beaconMode == 0) { bObj = new beacon(arr); }
            else if (beaconMode == 1) { bObj = new strobeBeacon(arr); }
            else { bObj = new quirkyBeacon(arr); }
        }

        // PRE: Must be used on an object not created by the null argument constructor, object must be on
        // POST: Will return an integer signal. 
        public int signal()
        {
            if (bObj == null) { throw new System.NullReferenceException("Cannot use functionality of an invalid object."); }
            if (!on) { return 0; }
            return bObj.signal();
        }

        // PRE: Must be used on an object not created by the null argument constructor
        // POST: Will toggle on object on/off and will return a bool reflecting the state
        public bool toggleOn()
        {
            if (bObj == null) { throw new System.NullReferenceException("Cannot use functionality of an invalid object."); }
            return (on = bObj.toggleOn());
        }

        // PRE: Must be used on an object not created by the null argument constructor, object must be on
        // POST: Object will be recharged
        public void recharge()
        {
            if (bObj == null) { throw new System.NullReferenceException("Cannot use functionality of an invalid object."); }
            if (on)
            {
                bObj.recharge();
            }
        }

        // PRE: Must be used on an object not created by the null argument constructor, object must be on
        // POST: Will toggle the object between large and small modes
        public void toggleMode()
        {
            if (bObj == null) { throw new System.NullReferenceException("Cannot use functionality of an invalid object."); }
            if (on) { dfObj.toggleMode(); }
        }

        // PRE: Must be used on an object not created by the null argument constructor, object must be on
        // POST: Will alter the sequences of both internal objects and the sequence of the crossProduct object itself.
        //       An integer array will be returned based on the encapsulated sequence (and potentially encapsulated number)
        public int[] scramble(int [] arr)
        {
            if (bObj == null || dfObj == null || !on) {
                int[] nullArr = { -1 };
                return nullArr;
            }

            int[] temp = dfObj.scramble(arr);
            if (beaconMode == 0) { bObj.setSeq(temp); }
            else if (beaconMode == 1) { bObj.setSeq(temp); }
            else { bObj.setSeq(temp); }
            seq = temp;
            return temp;
        }

        // PRE: Must be used on an object not created by the null argument constructor, object must be on
        // POST: Will return an integer array based on the encapsulated sequence and number
        public int[] filter()
        {
            if (bObj == null || dfObj == null || !on) {
                int[] nullArr = { -1 };
                return nullArr;
            }
            return dfObj.filter();
        }
    }
}

/* IMPLEMENTATION INVARIANT
 * - setSeq() from the beacon class hierarchy was not implemented because mutating the sequence is not allowed in dataFilter
 *   objects.
 * - Decided to merge functionality and data through signal() and scramble() in combination because they easily affect each other
 *   and are both reliant on the encapsulated sequence. 
 * - The on mode from the beacon objects was reflected in a crossProduct object because it made sense that if a crossProduct object is
 *   off, then all functionality including that of dataFilter should be disabled.
 * - Negative values and arrays of all zeroes not supported to allow -1 and 0 as error responses.
 * - Decided to construct dataFilter and beacon objects internally to reduce client responsibilities and to better simulate
 *   multiple inheritance. 
 * - Implemented a null argument constructor to allow the creation of arrays of crossProduct objects.
 * - Decided to only track encapsulated sequence and on state of object within crossProduct itself, other actions are done on 
 *   the internal objects.
 * - Chose to reuse the minimum sequence size requirement from dataFilter locally to ensure that there was proper construction 
 *   of all objects and to improve code efficiency through an early exception throw.
 */