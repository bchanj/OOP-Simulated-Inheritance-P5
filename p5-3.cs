// Brandon Chan
// p5.cs
// 11/9/20
// CPSC 3200, Professor Dingle, Fall Quarter 2020
// Developed in C# on Visual Studio Enterprise 2019

/* PURPOSE: This driver acts to use and test the beacon parent object and its children, strobeBeacon and quirkyBeacon, alongside
 *          the compositive crossProduct object which merges functionality and data of a dataFilter and beacon object. 
 *          It wil test the following:
 *          - toggleOn() which will prevent/allow beacon functionality
 *          - signal() which will return an integer based on the encapsulated signal within a beacon or crossProduct object
 *          - toggleMode() which swaps between large/small modes
 *          - scramble() which alters the encapsulated sequence
 *          - recharge() which resets the charge of the object to its initial value
 *          - filter() which returns an integer array based on the encapsulated sequence and mode
 *          - General functionality of crossProduct and beacon objects
 *          - Heterogeneous collections (beacon)
 *          - Randomly instantiated crossProduct objects
 *          - Confirm merged functionality and data of the dataFilter and beacon class hierarchies in crossProduct objects
 */

/* Revisions: 
*  - Version 1.0 - 10/8/20 - Intitial driver for crossProduct, beacon, strobeBeacon, and quirkyBeaocn alongside the many
*                            representations of the cross product of the beacon and dataFilter objects within crossProduct objects
*/


using System;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace P5
{
    class p5
    {
        const int ARRAYSIZE = 10;
        const int RANDARR_LOWBOUND= 5;
        const int RANDARR_UPBOUND = 100;
        const int RANDNUM_LOWBOUND = 30;
        const int RANDNUM_UPBOUND = 70;
        const int BASE = 0;
        const int DMOD_STROBE = 1;
        const int DCUT_QUIRKY = 2;
        const int TYPECOUNT = 3;

        // Returns a crossProduct object according randomly instantiated with a combination of the beacon and dataFilter objects.
        static crossProduct getCP()
        {
            int filterID;
            int beaconID;
            int num;

            Random rand = new Random();

            int[] randArr = new int[ARRAYSIZE];
            for (int i = 0; i < randArr.Length; i++)
            {
                randArr[i] = rand.Next(RANDARR_LOWBOUND, RANDARR_UPBOUND);
            }

            num = rand.Next(RANDNUM_LOWBOUND, RANDNUM_UPBOUND);

            filterID = rand.Next(BASE, DCUT_QUIRKY+1);
            beaconID = rand.Next(BASE, DCUT_QUIRKY+1);

            return new crossProduct(filterID, beaconID, randArr, num);
        }

        // Prints out the welcome message and the contents of the arrays used for the driver and their contents.
        static void printWelcome()
        {
            Console.WriteLine("=========================");
            Console.WriteLine("=== DATABEACON DRIVER ===");
            Console.WriteLine("=========================");
            Console.WriteLine();
        }

        // Tests merged functionality and data within a crossProduct object (filter, signal, scramble)
        static void testCP(crossProduct obj)
        {
            int[] retArr;
            Random rand = new Random();

            int[] randArr = new int[ARRAYSIZE];
            for (int i = 0; i < randArr.Length; i++) { randArr[i] = rand.Next(RANDARR_LOWBOUND, RANDARR_UPBOUND);}

            Console.Write("Returned from filter (Large mode): ");
            retArr = obj.filter();
            for (int i = 0; i < retArr.Length; i++)
            {
                Console.Write(retArr[i] + " ");
            }
            Console.WriteLine();

            Console.Write("Returned from filter (Small Mode): ");
            obj.toggleMode();
            retArr = obj.filter();
            for (int i = 0; i < retArr.Length; i++)
            {
                Console.Write(retArr[i] + " ");
            }
            Console.WriteLine();

            Console.WriteLine("Toggling mode back to large...");

            Console.WriteLine("SIGNAL (PRE-SCRAMBLE): " + obj.signal());
            retArr = obj.scramble(randArr);

            Console.Write("Returned from Scramble(): ");
            for (int i = 0; i < retArr.Length; i++)
            {
                Console.Write(retArr[i] + " ");
            }
            Console.WriteLine();
            
            Console.WriteLine("SIGNAL (POST-SCRAMBLE): " + obj.signal());
            Console.WriteLine();
        }

        // Returns a beacon object based on the integer passed in
        static beacon getBeacon(int place)
        {
            Random rand = new Random();
            int[] arr = new int[ARRAYSIZE];

            for (int i = 0; i < arr.Length; i++) { arr[i] = rand.Next(RANDARR_LOWBOUND, RANDARR_UPBOUND); }
            if (place % TYPECOUNT == BASE)
            {
                return new beacon(arr);
            }
            else if (place % (DCUT_QUIRKY + 1) == DMOD_STROBE)
            {
                return new strobeBeacon(arr);
            }
            else
            {
                return new quirkyBeacon(arr);
            }
        }

        // Tests the beacon objects functionality (Signal, toggleOn, recharge)
        static void testBeacon(beaconInterface[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % TYPECOUNT == BASE) { Console.WriteLine("*** Object " + (i+1) + " Beacon"); }
                else if (i % TYPECOUNT == DMOD_STROBE) { Console.WriteLine("*** Object " + (i+1) + " strobeBeacon"); }
                else { Console.WriteLine("*** Object " + (i+1) + " quirkyBeacon"); }
                Console.WriteLine("Signal (on): " + arr[i].signal());
                Console.WriteLine("Turning beacon off...");
                arr[i].toggleOn();
                Console.WriteLine("Signal (off): " + arr[i].signal());
                Console.WriteLine("Turning beacon back on...");
                Console.WriteLine("Recharging...");
                arr[i].recharge();
                Console.WriteLine();
            }
        }

        static void Main()
        {
            printWelcome();
            crossProduct[] hetGen = new crossProduct[ARRAYSIZE];
            for (int i = 0; i < hetGen.Length; i++) { hetGen[i] = getCP(); }

            Console.WriteLine("*** TESTING RANDOMLY INSTANTIATED CROSS PRODUCTS ***");
            for (int i = 0; i < hetGen.Length; i++)
            {
                Console.WriteLine("OBJECT " + (i+1));
                testCP(hetGen[i]);
            }

            Console.WriteLine("*** TESTING HETEROGENOUS COLLECTION OF BEACONS ***");
            beaconInterface[] beaconHetCol = new beaconInterface[ARRAYSIZE];
            for (int i = 0; i < beaconHetCol.Length; i++) { beaconHetCol[i] = getBeacon(i); }

            testBeacon(beaconHetCol);
        }
    }
}
