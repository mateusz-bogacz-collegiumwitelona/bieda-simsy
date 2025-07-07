using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bieda_simsy
{
    internal class Game : PlayerStats
    {
        public void SetupGame()
        {
            string choice;

            do
            {
                ShowMainMenu();
                choice =  Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SetName();
                        ShowName();
                        break;
                    case "2":
                        Console.WriteLine("Loading game...");
                        break;
                    case "0":
                        Console.WriteLine("Exiting the game. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Are you a idiot? Please enter a valid option (0-2).");
                        break;

                }

            } while (choice != "0");
        }


        private void ShowMainMenu()
        {
            Console.WriteLine("Welcome to Bieda Simsy!");
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }


        
    }
}