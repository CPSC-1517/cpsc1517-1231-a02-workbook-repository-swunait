using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace NhlClassLibrary
{
    // A static class contains only static(class-level) methods and 
    // can be instianted (cannot create objects of this class)
    public static class Utilities
    {
        public static bool IsPositiveOrZero(int value)
        {
            return value >= 0;
        }
        //public static bool IsPositiveOrZero(int value) => value >= 0;

        // Add a static (class-level) to determine if a number is even
        // An even number is a number that is divible by 2
        public static bool IsEvenNumber(int number)
        {
            return number % 2 == 0;
        }

        // Add a static (class-level) to determine if a number is odd
        public static bool IsOddNumber(int number)
        {
            return number % 2 != 0;
        }
    }

}
