using FokinClicker.Domain;
using FokinClicker.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FokinClicker.Initializer;
public class DbContextInitialiser
{

    public static void AddAppDbContext(IServiceCollection services)
    {
        var pathToDbFile = GetPathToDbFile();
        services
            .AddDbContext<AppDbContext>(options => options
                .UseSqlite($"Data Source={pathToDbFile}"));
        string GetPathToDbFile()
        {
            var applicationFolder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "FokinClicker");
            if (!Directory.Exists(applicationFolder))
            {
                Directory.CreateDirectory(applicationFolder);
            }
            return Path.Combine(applicationFolder, "FokinClicker.db");
        }
    }

    public static void InitializeDbContext(AppDbContext appDbContext)
    {
        const string PassivBoost1 = "раб";
        const string PassivBoost2 = "шахта_варпа";
        const string PassivBoost3 = "Лаборатория";

        const string ActiveBoost1 = "кланокрысы";
        const string ActiveBoost2 = "штормокрысы";
        const string ActiveBoost3 = "крысоогр";
        const string ActiveBoost4 = "варп_пушка";

        const string BoostSupport1 = "надзиратель";
        const string BoostSupport2 = "вожак";
        const string BoostSupport3 = "загонщик";
        const string BoostSupport4 = "инженер";

        var support_to_boost = new Dictionary<string, string> {
            { BoostSupport1, ActiveBoost1},
            { BoostSupport2, ActiveBoost2},
            { BoostSupport3, ActiveBoost3},
            { BoostSupport4, ActiveBoost4}
        };
        appDbContext.Database.Migrate();

        var existingBoosts = appDbContext.Boosts.ToArray();
        var existingSupports = appDbContext.Supports.ToArray();

        AddBoostIfNotExist(PassivBoost1, price: 50, profit: 1, isAuto: true);
        AddBoostIfNotExist(PassivBoost2, price: 2000, profit: 50, isAuto: true);
        AddBoostIfNotExist(PassivBoost3, price: 5000, profit: 120, isAuto: true);

        AddBoostIfNotExist(ActiveBoost1, price: 100, profit: 5);
        AddBoostIfNotExist(ActiveBoost2, price: 400, profit: 20);
        AddBoostIfNotExist(ActiveBoost3, price: 1600, profit: 80);
        AddBoostIfNotExist(ActiveBoost4, price: 6400, profit: 320);

        AddSupportIfNotExist(BoostSupport1, price: 1000, multiplier: 2);
        AddSupportIfNotExist(BoostSupport2, price: 4000, multiplier: 2);
        AddSupportIfNotExist(BoostSupport3, price: 16000, multiplier: 2);
        AddSupportIfNotExist(BoostSupport4, price: 64000, multiplier: 2);

        appDbContext.SaveChanges();

        void AddBoostIfNotExist(string name, long price, long profit, bool isAuto = false)
        {
            if (!existingBoosts.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "BoostImages", $"{name}.png");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                appDbContext.Boosts.Add(new Boost
                {
                    Title = name,
                    Price = price,
                    Profit = profit,
                    IsAuto = isAuto,
                    Image = memoryStream.ToArray(),
                });
            }
        }
        void AddSupportIfNotExist(string name, int multiplier, long price)
        {
            if (!existingSupports.Any(eb => eb.Title == name))
            {
                var pathToImage = Path.Combine(".", "Resources", "BoostImages", $"{name}.png");
                using var fileStream = File.OpenRead(pathToImage);
                using var memoryStream = new MemoryStream();
                fileStream.CopyTo(memoryStream);
                appDbContext.Supports.Add(new Supports
                {
                    Title = name,
                    Multiplier = multiplier,
                    Price = price,
                    Image = memoryStream.ToArray(),
                    BoostId = appDbContext.Boosts.Where(b => support_to_boost[name] == b.Title).First().Id,
                });
            }
        }
    }
}
