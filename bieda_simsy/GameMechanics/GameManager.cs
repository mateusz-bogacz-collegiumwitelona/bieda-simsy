using bieda_simsy.GameMechanics.Enums;
using bieda_simsy.GameMechanics.Interfaces;
using bieda_simsy.Saved.Interfaces;
using System.Xml.Linq;

namespace bieda_simsy.GameMechanics
{
    internal class GameManager : ISaved
    {
        private PlayerManager? _player;
        private SaveManager _saveManager;
        private GameState _gameState;

        public GameManager()
        {
            _saveManager = new SaveManager();
            _gameState = GameState.NotStarted;
            _player = null;
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
                        StartNewGame();
                        Console.Clear();
                        GameLoop();
                        break;
                    case "2":
                        LoadGameMenu();
                        break;
                    case "0":
                        Console.WriteLine("Exiting the game. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option (0-2).");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }

            } while (choice != "0");
        }

        private void StartNewGame()
        {
            _player = new PlayerManager();
            _gameState = GameState.Started;
            _player.SetName();
        }

        private void GameLoop()
        {
            while (_gameState == GameState.Started && _player != null && _player.IsAlive)
            {
                ShowPlayerOptionsMenu();
                ChoicePlayerOptions();
            }

            if (_player != null && !_player.IsAlive)
            {
                Console.WriteLine($"\n{_player.Name} has died. Game over.");
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                _gameState = GameState.NotStarted;
            }
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

            if (saves.Count == 0)
            {
                Console.WriteLine("No saves found");
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < saves.Count; i++)
            {
                string status = saves[i].IsAlive ? "Alive" : "Dead";
                Console.WriteLine($"{i + 1}. {saves[i].PlayerName} ({status}) - {saves[i].SaveDate:yyyy-MM-dd HH:mm}");
            }

            Console.WriteLine("Select a save to load (or press 0 to return): ");

            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int index) && index > 0 && index <= saves.Count)
            {
                var selectedSave = saves[index - 1];

                _player = new PlayerManager();
                _saveManager.LoadGame(this);
                _gameState = GameState.Started;

                Console.Clear();
                Console.WriteLine($"Game loaded successfully! Welcome back, {_player.Name}!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                GameLoop();
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
            if (_player == null || !_player.IsAlive)
                return;

            _player.DecayStats(
                _player.Alive,
                _player.Happiness,
                _player.Hungry,
                _player.Sleep,
                _player.Purity
            );

            Console.Clear();

            _player.MustPayTax();

            Console.WriteLine("");
            Console.WriteLine($"Name: {_player.Name}");
            Console.WriteLine($"Live: {_player.Live}");
            Console.WriteLine($"Money: {_player.Money}");
            Console.WriteLine($"Hungry: {_player.Hungry}");
            Console.WriteLine($"Happiness: {_player.Happiness}");
            Console.WriteLine($"Purity: {_player.Purity}");
            Console.WriteLine($"Sleep: {_player.Sleep} \n");

            Console.WriteLine($"1. Play with {_player.Name}");
            Console.WriteLine($"2. Feed {_player.Name}");
            Console.WriteLine($"3. Work {_player.Name}");
            Console.WriteLine($"4. Sleep {_player.Name}");
            Console.WriteLine($"5. Wash {_player.Name}");
            Console.WriteLine("6. Go to shop");
            Console.WriteLine("7. Save Game");
            Console.WriteLine("0. Exit to Main Menu");
            Console.WriteLine("What is your choice?");
        }

        private void ChoicePlayerOptions()
        {
            if (_player == null || !_player.IsAlive)
                return;

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _player.PlayWith(20);
                    break;
                case "2":
                    _player.Feed(20);
                    break;
                case "3":
                    _player.YouMustWork(20);
                    break;
                case "4":
                    _player.GoToSleep(20);
                    break;
                case "5":
                    _player.WashYourself(20);
                    break;
                case "6":
                    BuyItems();
                    break;
                case "7":
                    SaveGame();
                    break;
                case "0":
                    _gameState = GameState.NotStarted;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private void SaveGame()
        {
            if (_player == null)
            {
                Console.WriteLine("No game to save!");
                return;
            }

            _saveManager.SaveGame(this);
            Console.WriteLine("Game saved successfully!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ShopAssortment()
        {
            if (_player == null)
                return;

            Console.Clear();
            Console.WriteLine($"Money: {_player.Money}");
            Console.WriteLine("");
            Console.WriteLine("Welcome to the shop!");
            Console.WriteLine("1. Buy Food - 10 coins");
            Console.WriteLine("2. Buy Toys - 10 coins");
            Console.WriteLine("3. Buy Coffee - 10 coins");
            Console.WriteLine("4. Buy Soup - 10 coins");
            Console.WriteLine("5. Info about items");
            Console.WriteLine("0. Exit Shop");
            Console.Write("What would you like to buy? ");
        }

        private void BuyItems()
        {
            if (_player == null || !_player.IsAlive)
                return;

            Console.Clear();
            string choice;

            do
            {
                ShopAssortment();
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _player.BuySomething("Food", 1, 10, 25);
                        break;
                    case "2":
                        _player.BuySomething("Toys", 2, 10, 25);
                        break;
                    case "3":
                        _player.BuySomething("Coffee", 3, 10, 25);
                        break;
                    case "4":
                        _player.BuySomething("Soup", 4, 10, 25);
                        break;
                    case "5":
                        ShowInfoAboutProducts();
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            } while (choice != "0");
        }

        public void ShowInfoAboutProducts()
        {
            Console.Clear();
            Console.WriteLine("Product Information:");
            Console.WriteLine("1. Food - Restores hunger");
            Console.WriteLine("2. Toys - Restores happiness");
            Console.WriteLine("3. Coffee - Restores energy/sleep");
            Console.WriteLine("4. Soup - Restores purity\n");
            Console.WriteLine("Press any key to return to the shop.");
            Console.ReadKey();
        }

        public string FileName => _player?.Name ?? "default_save";

        public Dictionary<string, object> GetData()
        {
            if (_player == null)
            {
                return new Dictionary<string, object>();
            }

            return new Dictionary<string, object>
            {
                { "name", _player.Name },
                { "live", _player.Live },
                { "money", _player.Money },
                { "happiness", _player.Happiness },
                { "hungry", _player.Hungry },
                { "sleep", _player.Sleep },
                { "purity", _player.Purity },
                { "isAlive", _player.Alive }
            };
        }

        public void LoadData(Dictionary<string, object> data)
        {
            if (_player != null)
            {
                _player.LoadData(data);
            }
        }
    }
}