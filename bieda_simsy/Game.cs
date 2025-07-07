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
                        Console.Clear();
                        ChoicePlayerOptions();
                        break;
                    case "2":
                        ChoicePlayerOptions();
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

        private void ShowPlayerOptionsMenu()
        {
            Console.WriteLine("1. Play with " + ShowName);
            Console.WriteLine("2. Feed " + ShowName);
            Console.WriteLine("3. Work " + ShowName);
            Console.WriteLine("4. Shop " + ShowName);
            Console.WriteLine("0. Exit to Main Menu");
            Console.WriteLine("What is your choice?");
        }

        private void ChoicePlayerOptions()
        {
            Console.Clear();

            SetMinusStats();
            GetInfo();
            Console.WriteLine("");
            ShowPlayerOptionsMenu();

            string choice = Console.ReadLine();

            do { 
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        PlayWith();
                        break;
                    case "2":
                        Console.Clear();
                        Feed();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("You chose to work for " + ShowName);
                        break;
                    case "4":
                        Console.WriteLine("You chose to shop for " + ShowName);
                        break;
                    case "0":
                        SetupGame();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != "0");
        }
    }
}