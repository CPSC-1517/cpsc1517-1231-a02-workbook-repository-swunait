using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlClassLibrary
{
    public class NhlPlayer
    {
        // A class the following components:
        // 1) Fields - for storing data
        // 2) Properties - for encapulating access (read or write) to the data fields
        // 3) Constructors
        //      - for initialize fields/properties to meaningful values
        //      - It is a method with no return type and same name as the Class name
        // 4) Instance-Level Methods
        //      - methods that needs to access fields/properties of the object
        //      - To access a instance method you need to create an instance of the class 
        // 5) Class-level (static) Methods
        //      - methods that does not neeed to access fields/properties of the object
        //      - To access a class-level (static) method you specify the ClassName.MethodName()

        // Define data fields for a NhlPlayer
        //private string _name;
        private int _jerseyNumber;
        //private PlayerPosition _position;
        //private int _goals;
        //private int _assists;

        // Define properties to encapsulate access to the data fields
        // In C# there are three different ways to define a property
        //  1) Fully-implemented properties
        //      - use this when you need validate the new values being assigned
        //  2) Auto-implemented properties
        //      - use this when there is no need to validate the new value being assigned
        //  3) Expression-body members
        //      - use this syntax when there is only ONE statement 

        // Define fully-implemented properties for _firstName, _lastName, _jerseyNumber, _goals, _assists
        public int JerseyNumber
        {
            //get
            //{
            //    return _jerseyNumber;
            //}
            get => _jerseyNumber;

            set
            {
                // Throw an ArgumentOfRangeException if the new value is not between 0-97
                if (value < 0 || value > 97)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(JerseyNumber)} must be a value between 0 and 97. ");
                }
                // Assign a new value to the backing data field
                _jerseyNumber = value;

            }
        }

        // Define auto-implemented properties for _position and add new property to Points
        public string Name { get; private set; }
        public PlayerPosition Position { get; set; }
        public int Goals { get; private set; }
        public int Assists { get; private set; }

        // Define a computed (derived) property for Points (Goals + Assist)
        //public int Points
        //{
        //    get
        //    {
        //        return Goals + Assists;
        //    }
        //}
        public int Points => Goals + Assists;

        // Define an greedy constructor with parameters for Name, JerseyNumber, Position, Goals, Assists
        // Validate Name must contain text and contain at least 5 or more characters
        //      1) Throw an ArgumentNullException if the name does not contain text
        //      2) Throw an ArgumentException if the name does not contain 5 or more characters
        // Validate Goals and Assists as a postive or zero number
        //      1) Throw an ArugmentException if goals/assists is not a positive or zero number

        public NhlPlayer(string name, int jerseyNumber,  PlayerPosition position, int goals, int assists)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException($"Name cannot be blank.");
            }
            if (name.Trim().Length < 5)
            {
                throw new ArgumentException("Name must contain 5 or more characters.");
            }
            //if (goals < 0)
            if (Utilities.IsPositiveOrZero(goals) == false)
            {
                throw new ArgumentException("Goals must be a positive or zero number.");
            }
            //if (assists < 0)
            if (!Utilities.IsPositiveOrZero(assists) )
            {
                throw new ArgumentException("Assists must be a positive or zero number.");
            }
            Name = name;
            JerseyNumber = jerseyNumber;
            Position = position;    
            Goals = goals;
            Assists = assists;
        }

        // Create a instance-level method to add goals
        public void AddGoal()
        {
            Goals++;
        }
        // Create an instance-elvel method to add assists
        public void AddAssists()
        {
            Assists++;
        }

        public override string ToString()
        {
            //return base.ToString();
            // return a string with value for Name,Position,JerseyNumber,Goals,Assists, and Points
            return $"{Name},{Position},{JerseyNumber},{Goals},{Assists},{Points}";
        }

        /// <summary>
        /// Converts a comma separated value of player info into a NhlPlayer object
        /// </summary>
        /// <param name="csvLine">The line of text to parse</param>
        /// <returns>NHLPlayer with info from csvLine</returns>
        public static NhlPlayer Parse(string csvLine)
        {
            // csvLine = Connor McDavid,Center,97,3,2,5
            // The order of values are:
            // 0 - Name
            // 1 - Postion
            // 2 - JerseyNumber
            // 3 - Goals
            // 4 - Assists
            // 5 - Points
            // Validate csvLine is not null
            if (string.IsNullOrWhiteSpace(null))
            {
                throw new ArgumentNullException("csvLine", "csvLine cannot be blank.");
            }
            string[] tokens = csvLine.Split(',');
            // Validate the number of values in tokens is exactly 6
            if (tokens.Length != 6) 
            {
                throw new FormatException($"{csvLine} does not contain exactly 6 values");
            }
            // Return an NhlPlayer
            string name = tokens[0];
            PlayerPosition position = Enum.Parse<PlayerPosition>(tokens[1]);
            int jerseyNumber = int.Parse(tokens[2]);
            int goals = int.Parse(tokens[3]);
            int assists = int.Parse(tokens[4]);
            return new NhlPlayer(name,jerseyNumber,position,goals,assists);
        }

        public static bool TryParse(string csvLine, out NhlPlayer player)
        {
            bool result = false;
            try
            {
                player = Parse(csvLine);
                result = true;
            }
            catch
            {
                player = null!;
            }

            return result;
        }
    }
}
