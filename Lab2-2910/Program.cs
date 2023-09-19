namespace Lab2_2910
{
    internal class Program
    {
        static void Main()
        {
            
            Dictionary<string, List<VideoGame>> platformGames = ReadAndGroupGamesByPlatform();

            
            foreach (var kvp in platformGames)
            {
                Console.WriteLine($"Platform: {kvp.Key}");
                DisplayTop5GamesByGlobalSales(kvp.Value);
            }
            foreach (var kvp in platformGames)
            {
                Console.WriteLine($"Platform: {kvp.Key}");
                List<VideoGame> top5Games = GetTop5GamesByGlobalSales(kvp.Key, kvp.Value);
                foreach (var game in top5Games)
                {
                    Console.WriteLine($"{game.Title} - Global Sales: {game.GlobalSales}");
                }
            }
        }

        static Dictionary<string, List<VideoGame>> ReadAndGroupGamesByPlatform()
        {
            Dictionary<string, List<VideoGame>> platformGames = new Dictionary<string, List<VideoGame>>();
            string[] lines = File.ReadAllLines("videogame.csv");

            foreach (string line in lines.Skip(1)) 
            {
                string[] columns = line.Split(',');
                if (columns.Length >= 5) 
                {
                    VideoGame game = new VideoGame
                    {
                        Title = columns[0],
                        Platform = columns[1],
                        Genre = columns[3],
                        Publisher = columns[4],
                    };

                    
                    if (double.TryParse(columns[9], out double globalSales))
                    {
                        game.GlobalSales = globalSales;
                    }
                    else
                    {
                        game.GlobalSales = 0.0;
                    }

                    if (!platformGames.ContainsKey(game.Platform))
                    {
                        platformGames[game.Platform] = new List<VideoGame>();
                    }

                    platformGames[game.Platform].Add(game);
                }
                else
                {
                   
                }
            }

            return platformGames;
        }


        static void DisplayTop5GamesByGlobalSales(List<VideoGame> games)
        {
            var top5Games = games
                .OrderByDescending(game => game.GlobalSales)
                .Take(5);

            foreach (var game in top5Games)
            {
                Console.WriteLine($"{game.Title} - Global Sales: {game.GlobalSales}");
            }
        }
        static Func<string, List<VideoGame>, List<VideoGame>> GetTop5GamesByGlobalSales =
        (platform, games) => games
        .Where(game => game.Platform == platform)
        .OrderByDescending(game => game.GlobalSales)
        .Take(5)
        .ToList();

    }
}