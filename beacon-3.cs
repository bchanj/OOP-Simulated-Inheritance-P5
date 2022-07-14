// Brandon Chan
// beacon.cs 
// 11/9/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019
using System;
using System.Collections.Generic;
using System.Text;

/* CLASS INVARIANT 
 * - Following construction of a beacon object, it will have a non-negative, non-zero array of integers encapsulated
 *   and will be on and fully charged by default.
 * - Following a call to setSeq() the encapsulated array will be replaced assuming it is valid, therefore changing the
 *   state of the object.
 * - A call to signal() will return an integer based on the encapsulated sequence but does not alter it. However, the
 *   charge will decrement.
 * - toggleOn() will change the object between on/off states.
 * - recharge() will return the charge of the object back to the fully charged state from when it was constructed
 * - Attempting to encapsulate negative values will lead them to be converted to positive values.
 * - Attempting to construct a beacon object with an all-zero array will lead to an exception to be thrown.
 * - beacon is the parent object to the strobeBeacon and quirkyBeacon child objects.
 */

/* INTERFACE INVARIANT 
 * - Calling signal() on an object that is off will return 0.
 * - Arrays of all zero values not supported and will cause an exception to be thrown.
 * - Negative values not supported and will be converted to positive values.
 * - An off object will prevent all functionality except the ability to turn it back on (toggleOn())
 * - signal() will return the average of all values within the array as an integer signal.
 * - setSeq() implemented to allow varying of the signal returned from signal() whenever
 * - recharge() returns the charge of the object back to its initial value, allowing further signal() calls
 * - a call to signal() will decrement the charge integer
 * - An object without any charge left cannot emit a signal
 */

namespace P5
{
    interface beaconInterface {
        int signal();
        bool toggleOn();
        void recharge();
    }
    public class beacon: beaconInterface
    {
        protected int[] seq;
        protected int charged;
        protected bool on;
        protected const int CHARGEDMAX = 5;

        // PRE: User must not pass in an array of all zeroes or an array of size 0 (null). Negative array values
        //      not supported.
        // POST: Construction of beacon object with an sequence of positive values.
        public beacon(int[] arr)
        {
            int sum = 0;
            
            for (int i = 0; i < arr.Length; i++) { sum += arr[i]; }
            if (sum == 0) { throw new System.ArgumentException("Sequence must be not all zeroes", "seq"); }

            seq = arr;

            for (int i = 0; i < seq.Length; i++)
            {
                if (seq[i] < 0) { seq[i] = seq[i] * -1; }
            }

            charged = CHARGEDMAX;
            on = true;
        }

        // PRE: Array must not be all zeros. Object must be on
        // POST: The average of all values in the sequence will be returned, value will be non-zero
        public virtual int signal()
        {
            int avg = 0;
            int sum = 0;

            if (charged > 0 && on)
            {
                for (int i = 0; i < seq.Length; i++) { sum += seq[i]; }
                avg = (sum / seq.Length);
                charged--;
            }
            return avg;
        }

        // PRE: User must not pass in an array of all zeroes or an array of size 0 (null). Negative array values
        //      not supported. Object must be on
        // POST: An array of positive values will be encapsulated.
        public virtual void setSeq(int[] arr)
        {
            if (on)
            {
                int sum = 0;
                for (int i = 0; i < arr.Length; i++) { sum += arr[i]; }
                if (sum == 0) { throw new System.ArgumentException("Sequence must be not all zeroes", "seq"); }

                seq = arr;

                for (int i = 0; i < seq.Length; i++)
                {
                    if (seq[i] < 0) { seq[i] = seq[i] * -1; }
                }
            }
            
        }

        // POST: Object will toggle between on/off and a boolean to signify the state will be returned.
        public virtual bool toggleOn()
        {
            return on = !on;
        }

        // PRE: Object must be on
        // POST: Object will have its charge set back to it's initial value
        public virtual void recharge()
        {
            if (on) { charged = CHARGEDMAX; }
        }
    }
}

/* IMPLEMENTATION INVARIANT 
 * - Decided to implement setSeq() to allow the client to change the array and vary the signal over the lifetime
 *   of the object.
 * - Chose not to allow the use of all-zero arrays or to support negative values because 0 is used as an error response
 *   and to indicate that signal() was called on an object that is currently off.
 * - Chose to have fully charged objects that are automatically on to not interfere with functionality of child objects
 *   and to maintain simplicity alongside decreasing client responsibility.
 * - Decided to use an integer to represent the charge rather than a boolean to reduce the amount of recharging the client
 *   has to do to keep calling signal().
 * - Decided to not allow any functionality of the object if it is off besides toggleOn() because it seems more intuitive.
 */