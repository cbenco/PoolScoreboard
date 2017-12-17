using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using PoolScoreboard.Application;

namespace PoolScoreboard.Interface
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            GameController controller = new GameController();

            int id = 0;
            string nameEntered = string.Empty;
            var players = new List<IPlayer>();
            while (nameEntered != "GO")
            {
                Console.Write("Enter player 1 or type GO to continue: ");
                nameEntered = Console.ReadLine();
                if (nameEntered != "GO")
                {
                    players.Add(new Player
                    {
                        Name = nameEntered,
                        Id = id
                    });
                }
                id++;
            }
            
            var team1 = controller.CreateTeam(players.Where(p => p.Id % 2 != 0));
            var team2 = controller.CreateTeam(players.Where(p => p.Id % 2 == 0));

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
            Console.WriteLine("Game over!");
            Console.Read();
        }
    }
}