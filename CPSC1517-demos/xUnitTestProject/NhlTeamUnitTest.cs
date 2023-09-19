using FluentAssertions;
using NhlClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTestProject
{
    public class NhlTeamUnitTest
    {
        [Fact]
        public void AddPlayer_TeamFull_ThrowsInvalidOperationException()
        {
            // Given - Arrage
            var player1 = new NhlPlayer("Connor McDavid", 97, PlayerPosition.Center, 65, 80);
            var player2 = new NhlPlayer("Leon D", 96, PlayerPosition.LeftWing, 65, 80);
            var player3 = new NhlPlayer("Ryan NH", 95, PlayerPosition.RightWing, 65, 80);
            var player4 = new NhlPlayer("Mattias Ekholm", 96, PlayerPosition.Defense, 65, 80);
            var team1 = new NhlTeam();
            team1.AddPlayer(player1);
            team1.AddPlayer(player2);
            team1.AddPlayer(player3);

            // When - Act
            var action = () => team1.AddPlayer(player4);
            
            // Then - Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
