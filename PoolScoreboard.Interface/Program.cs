using System;
using System.CodeDom;
using System.Collections.Generic;
using PoolScoreboard.Application;

namespace PoolScoreboard.Interface
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GameController controller = new GameController();
            
            Console.Write("Enter player 1: ");
            var player1 = Console.ReadLine();
            Console.Write("Enter player 2: ");
            var player2 = Console.ReadLine();

            var team1 = controller.CreateTeam(new[] {player1});
            var team2 = controller.CreateTeam(new[] {player2});

            var game = controller.CreateEightBallPoolGame(team1, team2);
            
            Console.Write(game.ToString());

            while (!game.GameOver)
            {
                Console.WriteLine($"{Table.CurrentShooter.ThisShooter.Name}'s turn, " +
                                  $"shooting {Table.CurrentShooter.Shooting.ToString()}");
                Console.Write("Enter first ball hit or x to finish turn: ");
                var objectBall = Console.ReadLine();
                var sunk = new List<string>();
                string entered = string.Empty;
                while (entered != "x")
                {
                    Console.Write("Enter sunk ball or x to finish turn: ");
                    entered = Console.ReadLine();
                    if (!entered.Equals("x")) sunk.Add(entered);
                }
                Console.WriteLine(game.PlayShot(objectBall, sunk).ToString());
            }
            Console.Read();
        }
    }
}