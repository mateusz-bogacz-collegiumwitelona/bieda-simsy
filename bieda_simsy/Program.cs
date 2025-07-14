using bieda_simsy;
using bieda_simsy.GameMechanics;
using bieda_simsy.GameMechanics.Settings;
using System.Text.Json;



try
{
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
    var json = File.ReadAllText(path);

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var appSettings = JsonSerializer.Deserialize<AppSettings>(json, options);


    if (appSettings is null)
    {
        Console.WriteLine("Loading settings error");
        return;
    }

    var playerDefaults = appSettings.PlayerDefaults;
    var savedSettings = appSettings.SaveSettings;

    GameManager game = new GameManager(playerDefaults, savedSettings);

    game.SetupGame();
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
    Console.WriteLine(e.StackTrace);
    Console.ReadKey();
}
