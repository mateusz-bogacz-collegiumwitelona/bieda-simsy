using bieda_simsy.Saved;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bieda_simsy.GameMechanics
{
    internal class GameManager : PlayerManager
    {
        private SaveManager _saveManager;
        
        public GameManager()
        {
            _saveManager = new SaveManager();
        }

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
                        LoadGameMenu();
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
            Console.Clear();
            Console.WriteLine("Welcome to Bieda Simsy (Tamagotchi)!");
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");
        }

        private void LoadGameMenu()
        {
            Console.Clear();
            Console.WriteLine("Load Game:\n");

            var saves = _saveManager.GetAllSaves();

            if (saves.Count() == 0)
            {
                Console.WriteLine("No saves found");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < saves.Count(); i++)
            {
                string status = saves[i].IsAlive ? "Alive" : "Dead";
                Console.WriteLine($"{i + 1}. {saves[i].PlayerName} ({status}) - {saves[i].SaveDate:yyyy-MM-dd HM:mm}");
            }

            Console.WriteLine("Select a save to load (or press 0 to return): ");

            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int index) && index > 0 && index <= saves.Count)
            {
                var selectedSave = saves[index - 1];
                SetName(selectedSave.PlayerName);
                _saveManager.LoadGame(this);
                Console.Clear();
                ChoicePlayerOptions();
            }
            else if (choice != "0")
            {
                Console.WriteLine("Invalid choice. Please try again.");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
            }
        }



        private void ShowPlayerOptionsMenu()
        {
            Console.Clear();

            MustPayTax();
            
            Console.WriteLine("");
            
            Console.WriteLine($"Name: {GetName()}");
            Console.WriteLine($"Live: {GetLive()}");
            Console.WriteLine($"Money: {GetMoney()}");
            Console.WriteLine($"Hungry: {GetHungry()}");
            Console.WriteLine($"Happiness: {GetHappiness()}");
            Console.WriteLine($"Purity: {GetPurity()}");
            Console.WriteLine($"Sleep: {GetSleep()} \n");

            Console.WriteLine($"1. Play with {GetName()}");
            Console.WriteLine($"2. Feed {GetName()}");
            Console.WriteLine($"3. Work {GetName()}");
            Console.WriteLine($"4. Sleep {GetName()}");
            Console.WriteLine($"5. Wash {GetName()}");
            Console.WriteLine("6. Go to shop");
            Console.WriteLine("7. Save Game");
            Console.WriteLine("0. Exit to Main Menu");
            Console.WriteLine("What is your choice?");
        }

        private void ChoicePlayerOptions()
        {
            string choice;

            do
            {
                if (!IsAlive)
                {
                    Console.WriteLine("You are dead. Game over.");
                    return;
                }

                Random random = new Random();
                int rand = random.Next(1, 5);

                ShowPlayerOptionsMenu();

                choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        PlayWith(10, rand);
                        break;
                    case "2":
                        Feed(10, rand);
                        break;
                    case "3":
                        YouMustWork(10, rand);
                        break;
                    case "4":
                        Sleep(10, rand);
                        break;
                    case "5":
                        WashYourself(10, rand);
                        break;
                    case "6":
                        BuyItems();
                        break;
                    case "7":
                        SaveGame();
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

        private void SaveGame()
        {
            _saveManager.SaveGame(this);
            Console.WriteLine("Game saved successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
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
                Random random = new Random();
                int rand = random.Next(1, 5);
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BuySomething("Food", 1, 10, 5, rand);
                        break;
                    case "2":
                        BuySomething("Toys", 2, 10, 5, rand);
                        break;
                    case "3":
                        BuySomething("Coffe", 3, 10, 5, rand);
                        break;
                    case "4":
                        BuySomething("Soup", 4, 10, 5, rand);
                        ShowInfoAboutProducts();
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
            Console.WriteLine("1. Food - Restores 10 of hunger");
            Console.WriteLine("2. Toys - Restores 10 happiness");
            Console.WriteLine("3. Coffe - Restores 10 of sleep");
            Console.WriteLine("4. Soup - Restores 10 of purity\n");
            Console.WriteLine("Press any key to return to the shop.");
            Console.ReadKey();
        }
    }
}