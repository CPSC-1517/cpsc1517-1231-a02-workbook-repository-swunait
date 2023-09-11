// See https://aka.ms/new-console-template for more information
using NhlClassLibrary;

// Create a new instance an NhLPlayer with the following values:
//      Name:   Connor McDavid
//      Position:   Center
//      JerseyNumber:   97
//      Goals: 2
//      Assists: 5
NhlPlayer currentNhlPlayer = new NhlPlayer("Connor McDavid", 97, PlayerPosition.Center,2,5);
Console.WriteLine(currentNhlPlayer);
//Console.WriteLine($"Name: {currentNhlPlayer.Name}");
//Console.WriteLine($"Jersey Number:{currentNhlPlayer.JerseyNumber}");