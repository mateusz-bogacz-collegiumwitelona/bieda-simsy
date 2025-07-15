using bieda_simsy.GameMechanics.Interfaces;
using bieda_simsy.GameMechanics.Models;
using bieda_simsy.Saved.Interfaces;

namespace bieda_simsy.GameMechanics
{
    internal class PlayerManager : ISaved, IStats
    {
        private Player _player;
        private StatModifier _modifier;
        private const int BASE_STATS = 10; //base statistic value
        public bool IsAlive => _player.IsAlive;
        
        /// <summary>
        /// name for save 
        /// </summary>
        public string FileName =>
            string.IsNullOrEmpty(_player.Name)
            ? "default_save"
            : _player.Name.ToLower().Replace(" ", "_");

        /// <summary>
        /// basic constructor
        /// </summary>
        public PlayerManager()
        {
            _player = new Player();
            _modifier = new StatModifier();
        }

        /// <summary>
        /// constructor implements PlayerDefaults
        /// PlayerDefaults base stats for new game
        /// </summary>
        /// <param name="defaults"></param>
        public PlayerManager(PlayerDefaults defaults)
        {
            _player = new Player(defaults);
            _modifier = new StatModifier();
        }

        /// <summary>
        /// geters and setters
        /// </summary>
        public string Name
        {
            get => _player.Name;
            set => _player.Name = value;
        }

        public int Live => _player.Live;
        public int Money => _player.Money;
        public int Hungry => _player.Hungry;
        public int Sleep => _player.Sleep;
        public int Happiness => _player.Happiness;
        public int Purity => _player.Purity;
        public bool Alive => _player.IsAlive;


        /// <summary>
        /// set player name
        /// </summary>
        public string SetName()
        {
            Console.Write("Enter your name: ");
            _player.Name = Console.ReadLine() ?? "Unnamed";
            return _player.Name;
        }

        /// <summary>
        /// manual setting of the name from the parameter
        /// </summary>
        public void SetName(string name)
        {
            _player.Name = string.IsNullOrEmpty(name)
                ? "Unnamed"
                : name;
        }

        /// <summary>
        /// reduce statistics after each action simulating the passage of time
        /// </summary>
        public void DecayStats(bool isAlive, int happiness, int hungry, int sleep, int purity)
        {
            if (!_player.IsAlive)
            {
                ShowDeadScreen();
                return;
            }

            int decayValue = 5;

            _player.Happiness = _modifier.OddStats(_player.Happiness, decayValue);
            _player.Hungry= _modifier.OddStats(_player.Hungry, decayValue);
            _player.Sleep = _modifier.OddStats(_player.Sleep, decayValue);
            _player.Purity = _modifier.OddStats(_player.Purity, decayValue);

            _player.Live = _modifier.LiveChanged(_player);

            if (_modifier.IsDead(_player.Live))
            {
                _player.IsAlive = false;
                Console.WriteLine($"\n{_player.Name} is critically ill!");
            }
        }


        /// <summary>
        /// increst happiness stat
        /// </summary>
        public void PlayWith(int value)
        {
            if (!_player.IsAlive)
            {
                ShowDeadScreen();
                return;
            }

            Console.Clear();
            int oldHappiness = _player.Happiness;
            _player.Happiness = _modifier.AddStats(_player.Happiness, value);
            int happinessGained = _player.Happiness - oldHappiness;
            Console.WriteLine($"You played with {_player.Name}. Happiness increased by {happinessGained}");

            PostAction();
        }

        /// <summary>
        /// increst food stat
        /// </summary>
        public void Feed(int value)
        {
            if (!_player.IsAlive)
            {
                ShowDeadScreen();
                return;
            }

            Console.Clear();
            int oldHungry = _player.Hungry;
            _player.Hungry = _modifier.AddStats(_player.Hungry, value);
            int hungryGained = _player.Hungry - oldHungry;
            Console.WriteLine($"You fed {_player.Name}. Hunger increased by {hungryGained}");
            PostAction();
        }

        /// <summary>
        /// increst money but with cost of other stats
        /// </summary>
        public void YouMustWork(int value)
        {
            if (!_player.IsAlive)
            {
                ShowDeadScreen();
                return;
            }

            Console.Clear();

            int moneyFromWork = _modifier.AddOddMoney();
            int oldHappiness = _player.Happiness;
            int oldHungry = _player.Hungry;
            int oldSleep = _player.Sleep;
            int oldPurity = _player.Purity;

            _player.Money += moneyFromWork;

            _player.Happiness = _modifier.OddStats(_player.Happiness, value);
            _player.Hungry = _modifier.OddStats(_player.Hungry, value);
            _player.Sleep = _modifier.OddStats(_player.Sleep, value);
            _player.Purity = _modifier.OddStats(_player.Purity, value);

            Console.WriteLine($"You worked and earned {moneyFromWork} money. Current money: {_player.Money}");
            Console.WriteLine($"But work is exhausting and you have:");
            Console.WriteLine($" {_player.Name} has lost {oldHappiness - _player.Happiness} happiness,\n " +
                              $"{oldHungry - _player.Hungry} hunger,\n " +
                              $"{oldSleep - _player.Sleep} sleep,\n " +
                              $"{oldPurity - _player.Purity} purity.");

            PostAction();
        }

        /// <summary>
        /// shop increst food stat
        /// </summary>
        public void BuyFood(string itemName, int choice, int price, int value)
        {
            CanIByu(_player.Money, price);
            _player.Money = _modifier.PayForSomething(_player.Money, price);
            _player.Hungry = _modifier.AddStats(_player.Hungry, value);
            Console.WriteLine($"You bought {itemName}. {_player.Name} now has {_player.Hungry} hunger");
            PostAction();
        }

        /// <summary>
        /// shop increst happiness stat
        /// </summary>
        public void BuyHappiness(string itemName, int choice, int price, int value)
        {
            CanIByu(_player.Money, price);
            _player.Happiness = _modifier.AddStats(_player.Happiness, value);
            Console.WriteLine($"You bought {itemName}. {_player.Name} now has {_player.Happiness} happiness");
            PostAction();
        }

        /// <summary>
        /// shop increst sleep stat
        /// </summary>
        public void BuySleep(string itemName, int choice, int price, int value)
        {
            CanIByu(_player.Money, price);
            _player.Sleep = _modifier.AddStats(_player.Sleep, value);
            Console.WriteLine($"You bought {itemName}. {_player.Name} now has {_player.Sleep} energy");
            PostAction();
        }

        /// <summary>
        /// shop increst purity stat
        /// </summary>
        public void BuyPurity(string itemName, int choice, int price, int value)
        {
            CanIByu(_player.Money, price);
            _player.Purity = _modifier.AddStats(_player.Purity, value);
            Console.WriteLine($"You bought {itemName}. {_player.Name} now has {_player.Purity} purity");
            PostAction();
        }

        /// <summary>
        /// increst sleep stat
        /// </summary>
        public void GoToSleep(int value)
        {
            if (!_player.IsAlive)
            {
                ShowDeadScreen();
                return;
            }

            Console.Clear();

            int oldSleep = _player.Sleep;
            _player.Sleep = _modifier.AddStats(_player.Sleep, value);
            int sleepGained = _player.Sleep - oldSleep;
            Console.WriteLine($"You slept. Sleep increased by {sleepGained}");

            PostAction();
        }

        /// <summary>
        /// increst purity stat
        /// </summary>
        public void WashYourself(int value)
        {
            if (!_player.IsAlive)
            {
                ShowDeadScreen();
                return;
            }

            Console.Clear();

            int oldPurity = _player.Purity;
            _player.Purity = _modifier.AddStats(_player.Purity, value);
            int purityGained = _player.Purity - oldPurity;
            Console.WriteLine($"You washed yourself. Purity increased by {purityGained}");

            PostAction();
        }

        /// <summary>
        /// every 5 rounds a random amount of coins is deducted from the player
        /// </summary>
        public void MustPayTax()
        {
            if (!_player.IsAlive)
            {
                ShowDeadScreen();
                return;
            }


            if (_player.ActionToBill >= 5)
            {
                int tax = _modifier.AddOddMoney();
                CanIByu(_player.Money, tax);
                Console.WriteLine($"You must pay tax - {tax} coins");
                _player.Money  = _modifier.PayForSomething(_player.Money, tax);
                _player.ActionToBill = 0;
            }
        }

        /// <summary>
        /// loads the player's state from the specified data dictionary 
        /// assigning values to the fields of the PlayerManager object
        /// </summary>
        public void LoadData(Dictionary<string, object> data)
        {
            _player.LoadData(data);
        }

        /// <summary>
        /// returns the current state of the player as a key-value dictionary
        /// </summary>
        public Dictionary<string, object> GetData()
        {
            return _player.GetData();
        }

        /// <summary>
        /// generates a random event (good or bad) and applies its effects 
        /// to the player's current stats
        /// </summary>
        protected void GenerateRandomEvent()
        {
            RandomEvent randomEvent = new RandomEvent();
            randomEvent.GenerateEvent(_player);
        }

        /// <summary>
        /// Updates the player's statistics based on the results of the event, 
        /// trimming the values to an acceptable range. 
        /// </summary>


        /// <summary>
        /// triggers a random event, 
        /// increments the countdown value to call the tax function 
        /// and holds the game until the button is not pressed
        /// </summary>
        private void PostAction()
        {
            GenerateRandomEvent();
            _player.ActionToBill++;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// function to check if a player can afford
        /// </summary>
        private void CanIByu(int money, int prince)
        {
            if (!_modifier.CanAfford(money, prince))
            {
                throw new Exception("You don't have enought money");
                return;
            }
        }

        /// <summary>
        /// basic dead screen 
        /// </summary>
        public void ShowDeadScreen()
        {
            Console.Clear();
            Console.WriteLine("<---- GAME OVER ---->");
            Console.WriteLine($"{_player.Name} has died...");
            Console.WriteLine($"Final Stats");
            Console.WriteLine($"Money: {_player.Money}");
            Console.WriteLine($"Days survived: {Math.Ceiling((decimal)_player.ActionToBill / 5)}");
        }
    }
}