using GridGame.Service.Impl.Minesweeper;
using GridGame.Service.Interface;
using GridGame.Service.Interface.Minesweeper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


class Program
{
    public static void Main()
    {

        var serviceProvider = RegisterServices();
        var gameService = serviceProvider.GetRequiredService<IGame>();

        gameService.StartGame();

    }

    public static ServiceProvider RegisterServices()
    {
        var configuration = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .Build();

        return new ServiceCollection()
                        .AddSingleton<IConfiguration>(configuration)
                        .AddSingleton<IMinesweeperGrid, MinesweeperGrid>()
                        .AddSingleton<IGame, MinesweeperGame>()
                        .AddSingleton<INavigationHandler, MinesweeperNavigationHandler>()
                        .AddSingleton<IPlayer, MinesweeperPlayer>()
                        .BuildServiceProvider();
    }

}

