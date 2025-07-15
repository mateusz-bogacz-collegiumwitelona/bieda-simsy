namespace bieda_simsy.GameMechanics.Models
{
    /// <summary>
    /// represents player stats and name
    /// </summary>
    internal class Player
    {
        public string Name { get; set; }
        public int Live { get; set; }
        public int Money { get; set; }
        public int Happiness { get; set; }
        public int Hungry { get; set; }
        public int Sleep { get; set; }
        public bool IsAlive { get; set; }
        public int ActionToBill { get; set; }
        public int Purity { get; set; }
    

        /// <summary>
        /// basic constructor
        /// </summary>
        public Player()
        {
            Name = string.Empty;
            Live = 100;
            Money = 10;
            Happiness = 100;
            Hungry = 100;
            IsAlive = true;
            Sleep = 100;
            ActionToBill = 0;
            Purity = 100;
        }

        /// <summary>
        /// constructor implements PlayerDefaults
        /// PlayerDefaults base stats for new game
        /// </summary>
        /// <param name="defaults"></param>
        public Player(PlayerDefaults defaults)
        {
            Name = defaults.Name;
            Live = defaults.Live;
            Money = defaults.Money;
            Happiness = defaults.Happiness;
            Hungry = defaults.Hungry;
            Sleep = defaults.Sleep;
            Purity = defaults.Purity;
            ActionToBill = 0;
            IsAlive = defaults.IsAlive;
        }

        public Player Clone()
        {
            return new Player
            {
                Name = this.Name,
                Live = this.Live,
                Money = this.Money,
                Happiness = this.Happiness,
                Hungry = this.Hungry,
                Sleep = this.Sleep,
                Purity = this.Purity,
                ActionToBill = this.ActionToBill,
                IsAlive = this.IsAlive
            };
        }

        /// <summary>
        /// returns the current state of the player as a key-value dictionary
        /// </summary>
        public Dictionary<string, object> GetData()
        {
            return new Dictionary<string, object>
            {
                { "name", Name },
                { "live", Live },
                { "money", Money },
                { "happiness", Happiness },
                { "hungry", Hungry },
                { "sleep", Sleep },
                { "purity", Purity },
                { "isAlive", IsAlive }
            };
        }

        /// <summary>
        /// loads the player's state from the specified data dictionary 
        /// assigning values to the fields of the PlayerManager object
        /// </summary>
        public void LoadData(Dictionary<string, object> data)
        {
            Name = data["name"]?.ToString() ?? "Unnamed";
            Live = Convert.ToInt32(data["live"]);
            Money = Convert.ToInt32(data["money"]);
            Happiness = Convert.ToInt32(data["happiness"]);
            Hungry = Convert.ToInt32(data["hungry"]);
            Sleep = Convert.ToInt32(data["sleep"]);
            Purity = Convert.ToInt32(data["purity"]);
            IsAlive = Convert.ToBoolean(data["isAlive"]);
        }

    }
}

