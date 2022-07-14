// Brandon Chan
// beacon.cs 
// 11/9/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019

using System;
using System.Collections.Generic;
using System.Text;

/* CLASS INVARIANT 
 * - Following construction of a strobeBeacon object, it will have a non-negative, non-zero array of integers encapsulated
 *   and will be on and fully charged by default.
 * - Following a call to setSeq() the encapsulated array will be replaced assuming it is valid, therefore changing the
 *   state of the object.
 * - A call to signal() will return an integer based on the encapsulated sequence but does not alter it. 
 *   This signal will alternate between positive and negative with each call to signal. The charge will decrement
 *   with each call.
 * - toggleOn() will change the object between on/off states.
 * - recharge() not supported.
 * - Attempting to encapsulate negative values will lead them to be converted to positive values.
 * - Attempting to construct a beacon object with an all-zero array will lead to an exception to be thrown.
 * - strobeBeacon object is a child object of beacon
 */

/* INTERFACE INVARIANT 
 * - Calling signal() on an object that is off will return 0.
 * - Arrays of all zero values not supported and will cause an exception to be thrown.
 * - Negative values not supported and will be converted to positive values.
 * - An off object will prevent all functionality except the ability to turn it back on (toggleOn())
 * - signal() will return the average of all values within the array as an integer signal and will alternate between
 *   positive and negative with each call.
 * - setSeq() implemented to allow varying of the signal returned from signal() whenever
 * - recharge() is not implemented in strobeBeacon unlike its relative objects.
 * - An object without any charge left cannot emit a signal
 */

namespace P5
{
    public class strobeBeacon: beacon, beaconInterface
    {
        bool positive;
        public strobeBeacon(int[] arr): base(arr) { positive = true; }

        // PRE: Array must not be all zeros, object must be on
        // POST: The average of all values in the sequence will be returned, value will be non-zero.
        //       Each call to signal will alternate the signal between positive and negative
        public override int signal()
        {   
            if (positive) {
                positive = !positive;
                return base.signal(); 
            }
            positive = !positive;
            return (base.signal() * -1);
        }

        // PRE: User must not pass in an array of all zeroes or an array of size 0 (null). Negative array values
        //      not supported. Object must be on
        // POST: An array of positive values will be encapsulated.
        public override void setSeq(int[] arr)
        {
            base.setSeq(arr);
        }

        // POST: Object will toggle between on/off
        public override bool toggleOn()
        {
            return base.toggleOn();
        }

        // POST: Will not change charge of object
        public override void recharge() {}
    }
}

/* IMPLEMENTATION INVARIANT 
 * - Decided to implement setSeq() to allow the client to change the array and vary the signal over the lifetime
 *   of the object.
 * - Chose not to allow the use of all-zero arrays or to support negative values because 0 is used as an error response
 *   and to indicate that signal() was called on an object that is currently off.
 * - Chose to have objects that are automatically on to maintain simplicity.
 *   and to maintain simplicity alongside decreasing client responsibility.
 * - Decided to use an integer to represent the charge rather than a boolean to reduce the amount of recharging the client
 *   has to do to keep calling signal().
 * - Decided to not allow any functionality of the object if it is off besides toggleOn() because it seems more intuitive.
 */