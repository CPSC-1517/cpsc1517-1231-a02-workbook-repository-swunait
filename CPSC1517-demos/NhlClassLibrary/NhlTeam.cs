using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlClassLibrary
{
    public class NhlTeam
    {
        // Define properties for:
        // 1) name - cannot be blank and must contain 5 or more characters
        // 2) city - cannot be blank or vegas
        // 3) arena - cannot be blank
        // 4) list of players 
        private string _name;
        private string _city;
        private string _arena;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be blank.");
                }
                if (value.Trim().Length < 5)
                {
                    throw new ArgumentException("Name must contain at least 5 characters");
                }
                _name = value;
            }
        }
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("City cannot be blank.");
                }
                if(value.Equals("vegas", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new ArgumentException("City cannot be VEGAS.");
                }
                _city = value;
            }
        }
        public string Arena
        {
            get => _arena;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Arena cannot be blank.");
                }
                _arena = value;
            }
        }


        // Define an auto-implemented property with a private for Players
        public List<NhlPlayer> Players { get; private set; } = new List<NhlPlayer>();
        public List<NhlGoalie> Goalies { get; private set; } = new List<NhlGoalie>();

        // Define methods to:
        // 1) Add a NhlPlayer
        //      - jerseyNumber must unique
        //      - cannot have more than 23 players
        public void AddPlayer(NhlPlayer newPlayer)
        {
            if (Players.Count >= 23) // double-check logic
            {
                throw new InvalidOperationException("Team is full. Cannot add anymore players.");
            }


            Players.Add(newPlayer);
        }
        // 2) Remove a NhlPlayer by jerseyNumber
        //      - jerseyNumber must exists
        // 3) Add a NhlGoalie
        //      - jerseyNumber must unique
        //      - maximum of 2 goalies per team
        // 4) Remove NhlGoalie from team by goalie name
        //      - goalie name must exists
        //      - team must have at least one goalie
    }
}
