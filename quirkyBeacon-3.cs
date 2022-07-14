// Brandon Chan
// quirkyBeacon.cs
// 11/9/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019
using System;
using System.Collections.Generic;
using System.Text;

/* CLASS INVARIANT 
 * - Following construction of a quirkyBeacon object, it will have a non-negative, non-zero array of integers encapsulated
 *   and will be on and fully charged by default.
 * - Following a call to setSeq() the encapsulated array will be replaced assuming it is valid, therefore changing the
 *   state of the object.
 * - A call to signal() will return an integer based on the encapsulated sequence and an obscure equation but does not 
 *   alter it. However, the charge will decrement.
 * - toggleOn() will change the object between on/off states and increments an internal counter which, when hits the max,
 *   will prevent further toggling between on/off.
 * - recharge() will return the charge of the object back to the fully charged state from when it was constructed
 * - Attempting to encapsulate negative values will lead them to be converted to positive values.
 * - Attempting to construct a beacon object with an all-zero array will lead to an exception to be thrown.
 * - quirkyBeacon is a child object to the beacon object.
 */

/* INTERFACE INVARIANT 
 * - Calling signal() on an object that is off will return 0.
 * - Arrays of all zero values not supported and will cause an exception to be thrown.
 * - Negative values not supported and will be converted to positive values.
 * - An off object will prevent all functionality except the ability to turn it back on (toggleOn())
 * - signal() will return an integer signal resulting from on an obscure equation.
 * - setSeq() implemented to allow varying of the signal returned from signal() whenever.
 * - recharge() returns the charge of the object back to its initial value, allowing further signal() calls.
 * - a call to signal() will decrement the charge integer.
 * - An object without any charge left cannot emit a signal.
 * - toggleOn() has limited usage and when that limit is hit the object mode can no longer be changed.
 */

namespace P5
{
    public class quirkyBeacon: beacon, beaconInterface
    {
        const int DIVIDEND = 4;
        const int TOGGLEMAX = 3;
        int toggleCount;
        public quirkyBeacon(int[] arr) : base(arr) { toggleCount = 0; }

        // PRE: Object must be on
        // POST: Will return an integer which is the base class signal modified
        public override int signal()
        {
            int signal = base.signal();
            if (signal == 0) { return signal; }
            signal = signal * (signal / DIVIDEND) + (DIVIDEND + DIVIDEND);
            return signal;
        }

        // PRE: Object must not have reached its toggleMax
        // POST: Object will toggle between on/off, if toggleMax was reached already then object can no longer change state
        public override bool toggleOn()
        {
            if (toggleCount < TOGGLEMAX)
            {
                toggleCount++;
                return base.toggleOn();
            }
            return on;
        }

        // POST: Object will have its charge reset to its initial value
        public override void recharge()
        {
            base.recharge();
        }

        // PRE: User must not pass in an array of all zeroes or an array of size 0 (null). Negative array values
        //      not supported. Object must be on
        // POST: An array of positive values will be encapsulated.
        public override void setSeq(int[] arr)
        {
            base.setSeq(arr);
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
 * - Chose the equation above to change the signal because it was hard to decipher yet simple to write and debug.
 * - Chose to make the max number of mode toggling to be an odd value so that the object would be stuck in off mode assuming it
 *   hit the toggleMax.
 */