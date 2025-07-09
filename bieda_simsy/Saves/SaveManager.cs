using bieda_simsy.Saved.Interfaces;
using bieda_simsy.Saved.Models;
using System.Text.Json;

namespace bieda_simsy
{
    internal class SaveManager
    {
        private static readonly string SaveDirectory = "saves";
        private static readonly string SaveExtension = ".json";
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public SaveManager()
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }

        public void SaveGame(ISaved game)
        {
            try
            {
                var data = game.GetData();
                var saveModel = new SaveModel
                {
                    Name = data["name"]?.ToString() ?? "Unknown",
                    Live = Convert.ToInt32(data["live"]),
                    Money = Convert.ToInt32(data["money"]),
                    Happiness = Convert.ToInt32(data["happiness"]),
                    Hungry = Convert.ToInt32(data["hungry"]),
                    Sleep = Convert.ToInt32(data["sleep"]),
                    IsAlive = Convert.ToBoolean(data["isAlive"])
                };

                string filePath = Path.Combine(SaveDirectory, $"{game.FileName}{SaveExtension}");
                string json = JsonSerializer.Serialize(saveModel, JsonOptions);
                File.WriteAllText(filePath, json);

                Console.WriteLine($"Game saved successfully to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public void LoadGame(ISaved game)
        {
            try
            {
                string filePath = Path.Combine(SaveDirectory, $"{game.FileName}{SaveExtension}");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Save file not found: {filePath}");
                    return;
                }

                string json = File.ReadAllText(filePath);
                var saveModel = JsonSerializer.Deserialize<SaveModel>(json, JsonOptions);

                if (saveModel == null)
                {
                    Console.WriteLine("Failed to deserialize save file.");
                    return;
                }

                var data = new Dictionary<string, object>
                {
                    ["name"] = saveModel.Name,
                    ["live"] = saveModel.Live,
                    ["money"] = saveModel.Money,
                    ["happiness"] = saveModel.Happiness,
                    ["hungry"] = saveModel.Hungry,
                    ["sleep"] = saveModel.Sleep,
                    ["isAlive"] = saveModel.IsAlive
                };

                game.LoadData(data);
                Console.WriteLine($"Game loaded successfully from: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
            }
        }

        public List<SavedInfo> GetAllSaves()
        {
            var saves = new List<SavedInfo>();

            try
            {
                if (!Directory.Exists(SaveDirectory))
                    return saves;

                var files = Directory.GetFiles(SaveDirectory, $"*{SaveExtension}");

                foreach (var file in files)
                {
                    try
                    {
                        string json = File.ReadAllText(file);
                        var saveModel = JsonSerializer.Deserialize<SaveModel>(json, JsonOptions);

                        if (saveModel != null)
                        {
                            saves.Add(new SavedInfo
                            {
                                FileName = Path.GetFileNameWithoutExtension(file),
                                PlayerName = saveModel.Name,
                                SaveDate = saveModel.SaveDate,
                                IsAlive = saveModel.IsAlive
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error reading save file {file}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading saves directory: {ex.Message}");
            }

            return saves.OrderByDescending(s => s.SaveDate).ToList();
        }

        public void DeleteSave(string fileName)
        {
            try
            {
                string filePath = Path.Combine(SaveDirectory, $"{fileName}{SaveExtension}");

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine($"Save file deleted: {filePath}");
                }
                else
                {
                    Console.WriteLine($"Save file not found: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting save file: {ex.Message}");
            }
        }
    }
}
