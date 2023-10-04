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

        /// <summary>
        /// Return total goals for all players in the team
        /// </summary>
        public int TotalGoals
        {
            //get
            //{
            //    int total = 0;
            //    foreach(NhlPlayer player in Players)
            //    {
            //        total += player.Goals;
            //    }
            //    return total;
            //}
            get => Players.Sum(p => p.Goals);
        }

        /// <summary>
        /// Remove the first player with the jerseyNumber from the team
        /// and return the player removed from the team.
        /// </summary>
        /// <param name="jerseyNumber">The jerseyNumber of the player remove</param>
        /// <returns>The player that was removed.</returns>
        public NhlPlayer RemovePlayer(int jerseyNumber)
        {
            //NhlPlayer playerRemoved = null;

            //for (int index = 0; index < Players.Count; index++)
            //{
            //    NhlPlayer currentPlayer = Players[index];
            //    if (currentPlayer.JerseyNumber == jerseyNumber)
            //    {
            //        // Stop the loop
            //        index = Players.Count;
            //        // Assign the currentPlayer as the playerRemoved
            //        playerRemoved = currentPlayer;
            //    }
            //}
            //if (playerRemoved == null) 
            //{
            //    throw new ArgumentException($"There is no player with jersey number {jerseyNumber}");
            //}

            //return playerRemoved;

            NhlPlayer? playerRemoved = Players
                    .FirstOrDefault(currentPlayer => currentPlayer.JerseyNumber == jerseyNumber);
            if (playerRemoved == null) 
            {
                throw new ArgumentException($"There is no player with jersey number {jerseyNumber}");
            }

            Players.Remove(playerRemoved);

            return playerRemoved;
        }

        // Define methods to:
        // 1) Add a NhlPlayer
        //      - jerseyNumber must unique
        //      - cannot have more than 23 players
        public void AddPlayer(NhlPlayer newPlayer)
        {
            // Throw an ArgumentNullException if newPlayer is null
            if (newPlayer == null)
            //if (newPlayer is null)
            {
                throw new ArgumentNullException(nameof(newPlayer), 
                    "The newPlayer parameter cannot be null.");
            }

            // Throw an ArgumentException if there is another player with the same jerseyNumber
            // Technique #1: Search for existing player by jerseyNumber with sequential search
            //foreach (NhlPlayer player in Players)
            //{
            //    if (player.JerseyNumber == newPlayer.JerseyNumber)
            //    {
            //        throw new ArgumentException(
            //            $"There is already another player with jerseyNumber {newPlayer.JerseyNumber}",
            //            nameof(newPlayer));
            //    }
            //}
            // Technique #2: Use the Exists() method of the List<T> class.
            //bool foundPlayer = Players.Exists(
            //    currentPlayer => currentPlayer.JerseyNumber == newPlayer.JerseyNumber);
            // Technique #3: Use the Any() method of the Enumerable class.
            bool foundPlayer = Players.Any(
                currentPlayer => currentPlayer.JerseyNumber == newPlayer.JerseyNumber);

            if (foundPlayer)
            {
                throw new ArgumentException(
                        $"There is already another player with jerseyNumber {newPlayer.JerseyNumber}",
                        nameof(newPlayer));
            }
            
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
