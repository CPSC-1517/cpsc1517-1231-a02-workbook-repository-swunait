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
        public void AddPlayer_DuplicateJerseyNumber_ThrowsArgumentException()
        {
            // Given - Arrange
            var team1 = new NhlTeam();
            var player1 = new NhlPlayer("Connor McDavid", 97, PlayerPosition.Center, 0, 0);
            var player2 = new NhlPlayer("Evander Kane", 91, PlayerPosition.LeftWing, 0, 0);
            var player3 = new NhlPlayer("Connor Brown", 28, PlayerPosition.Center, 0, 0);
            team1.AddPlayer(player1);
            team1.AddPlayer(player2);

            // When - Act
            Action act = () => team1.AddPlayer(player3);    
            
            // Ten - Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithParameterName("newPlayer")
                .WithMessage("There is already another player with jerseyNumber*");
        }

        [Fact]
        public void AddPlayer_NullParameter_ThrowsArgumentNullException()
        {
            // Given - Arrange
            var team1 = new NhlTeam();

            // When - Act
            Action act = () => team1.AddPlayer(null);

            // Then - Assert
            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("newPlayer")
                .WithMessage("*cannot be null*");
                //.Where(e => e.Message.Contains("cannot be null"));
        }

        [Fact]
        public void AddPlayer_TeamFull_ThrowsInvalidOperationException()
        {
            // Given - Arrage
            var player1 = new NhlPlayer("Connor McDavid", 97, PlayerPosition.Center, 65, 80);
            var player2 = new NhlPlayer("Leon Draisaitl", 29, PlayerPosition.LeftWing, 65, 80);
            var player3 = new NhlPlayer("Ryan NH", 93, PlayerPosition.RightWing, 65, 80);
            var player4 = new NhlPlayer("Mattias Ekholm", 14, PlayerPosition.Defense, 65, 80);
            var team1 = new NhlTeam();
            team1.AddPlayer(player1);
            team1.AddPlayer(player2);
            team1.AddPlayer(player3);

            // When - Act
            var action = () => team1.AddPlayer(player4);
            
            // Then - Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(56,4)]
        [InlineData(91,6)]
        public void RemovePlayer_MaintainOrder_PlayerRemoved(int jerseyNumber, int removeIndex)
        {
            // Given - Arrange
            // Create and initialize a list of players
            List<NhlPlayer> expectedPlayers = new()
            {
                new NhlPlayer("Connor McDavid", 97, PlayerPosition.Center, 64, 89),
                new NhlPlayer("Leon Draisaitl", 29, PlayerPosition.Center, 52, 76),
                new NhlPlayer("Ryan Nugent-Hopkins", 93, PlayerPosition.Center, 37, 67),
                new NhlPlayer("Zach Hyman", 18, PlayerPosition.LeftWing, 36, 47),
                new NhlPlayer("Kailer Yamamoto", 56, PlayerPosition.RightWing, 10, 15),
                new NhlPlayer("Mattias Ekholm", 14, PlayerPosition.Defense, 4, 10),
                new NhlPlayer("Evander Kane", 91, PlayerPosition.LeftWing, 16, 12),
            };

            // Create a new NhlTeam and add each NhlPlayer in expectedPlayers
            NhlTeam currentTeam = new();
            foreach(var currentPlayer in expectedPlayers)
            {
                currentTeam.AddPlayer(currentPlayer);
            }

            // When - Act
            var actualPlayer = currentTeam.RemovePlayer(jerseyNumber);
            expectedPlayers.RemoveAt(removeIndex);

            // Then - Assert
            actualPlayer.JerseyNumber.Should().Be(jerseyNumber);
            currentTeam.Players.Should().ContainInConsecutiveOrder(expectedPlayers);
        }
    }
}
