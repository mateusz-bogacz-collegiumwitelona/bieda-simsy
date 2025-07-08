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
                choice = Console.ReadLine();

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
                        Dispose();
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
            Console.Clear();
            GetInfo();
            Console.WriteLine("");

            Console.WriteLine($"1. Play with {ShowName}");
            Console.WriteLine($"2. Feed {ShowName}");
            Console.WriteLine($"3. Work {ShowName}");
            Console.WriteLine($"4. Sleep {ShowName}");
            Console.WriteLine($"5. Shop {ShowName}");
            Console.WriteLine("0. Exit to Main Menu");
            Console.WriteLine("What is your choice?");
        }

        private void ChoicePlayerOptions()
        {
            string choice;

            do
            {
                if(!IsAlive)
                {
                    Console.WriteLine("You are dead. Game over.");
                    return;
                }

                ShowPlayerOptionsMenu();

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PlayWith();
                        break;
                    case "2":
                        Feed();
                        break;
                    case "3":
                        YouMustWork();
                        break;
                    case "4":
                        break;
                    case "5":
                        BuyItems();
                        break;
                    case "0":
                        Console.Clear();
                        SetupGame();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != "0");
        }

        private void ShopAssortment()
        {
            Console.Clear();
            GetMoney();
            Console.WriteLine("");
            Console.WriteLine("Welcome to the shop!");
            Console.WriteLine("1. Buy Food - 10 coins");
            Console.WriteLine("2. Buy Toys - 10 coins");
            Console.WriteLine("3. Info about items");
            Console.WriteLine("0. Exit Shop");
            Console.Write("What would you like to buy? ");
        }


        private void BuyItems()
        {
            Console.Clear();

            string choice;

            do
            {
                if (!IsAlive)
                {
                    Console.WriteLine("You are dead. Game over.");
                    return;
                }

                ShopAssortment();
                
                choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        BuySomeFood();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        BuyAToy();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "3":
                        ShowInfoAboutProducts();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;

                }
            } while (choice != "0");
        }


        public void ShowInfoAboutProducts()
        {
            Console.Clear();
            Console.WriteLine("Product Information:");
            Console.WriteLine("1. Food - Restores 10 pkt of hunger");
            Console.WriteLine("2. Toys - Increases 10 pkt happiness");
            Console.WriteLine("Press any key to return to the shop.");
            Console.ReadKey();
        }
    }
}