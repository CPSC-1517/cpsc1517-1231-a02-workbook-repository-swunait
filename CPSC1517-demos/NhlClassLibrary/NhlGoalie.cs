using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NhlClassLibrary
{
    public class NhlGoalie : NhlPlayer
    {
        // Define data fields
        private double _saveValuePercentage;

        // Define properties with a backing field
        public double SaveValuePercentage
        {
            get => _saveValuePercentage;

            set
            {
                // Throw an ArgumentOutRangeException if new value is not between 0 and 1
                if (value < 0 || value > 1) 
                {
                    throw new ArgumentOutOfRangeException("SaveValuePercentage must be a value between 0 and 1.");
                }
                // Assigned new value to backing field
                _saveValuePercentage = value;
            }
        }

        // Define auto-implemented properties without a backing field
        public double GoalsAgainstAverage { get; set; }
        // Define auto-implemented properties that can only be changed from within the class
        public int Shutouts { get; private set; } 

        // Define instance-level methods
        public void AddShutout()
        {
            Shutouts++;
        }

        // Define constructor to initialize the object
        public NhlGoalie(
            string name, 
            int jerseyNumber, 
            int goals,
            int assists) : base(name,jerseyNumber, PlayerPosition.Goalie, goals, assists) 
        {

        }
        public NhlGoalie(
            string name,
            int jerseyNumber,
            double svp,
            double gav) : base(name, jerseyNumber, PlayerPosition.Goalie, 0, 0)
        {
            SaveValuePercentage = svp;
            GoalsAgainstAverage = gav;
        }

        // override to ToString() with a string representation of the current object
        // to include the NhlPlayer properties and NhlGoalie properties



    }
}
