using Npgsql;
using System.Text;
using LoremNET;
using Npgsql.PostgresTypes;

internal class Program
{
    static readonly Random rnd = new();
    private static async Task Main(string[] args)
    {
        string[] namesFor = { "Набор", "Инструмент", "Средство", "Комплекс" };
        string[] pretext = { "для", "от", "чтобы", "затем", "временного", "переводного", "постоянного" };
        string[] tatooContext = { "тату", "эскиз", "пирсинга", "набросок", "краски" };
        string[] addingInfo = { "змея", "кролик", "самолёт", "пират", "птица", "цитата", "стих", "капибара", "зеркало", "снег" };
        HashSet<string> generetatedTitles = new();

        while (generetatedTitles.Count < 500)
        {
            StringBuilder title = new();
            title.Append(namesFor[rnd.Next(namesFor.Length - 1)]);
            title.Append(' ');
            title.Append(pretext[rnd.Next(pretext.Length - 1)]);
            title.Append(' ');
            title.Append(tatooContext[rnd.Next(tatooContext.Length - 1)]);
            title.Append(' ');
            title.Append(addingInfo[rnd.Next(addingInfo.Length - 1)]);
            generetatedTitles.Add(title.ToString());
        }
        

        foreach (var t in generetatedTitles)
        {
            string loremShit = Lorem.Sentence(rnd.Next(10, 1_000_000));        
            await GenerateTrashForInkProductAsync(t, loremShit);
        }
    }

    private async static Task GenerateTrashForInkProductAsync(string title, string lorem)
    {
        await using NpgsqlConnection conn = new("Host=localhost;Port=5432;Database=ink_realm;Username=postgres;Password=B&k34RPvvB12F");
        await conn.OpenAsync();
        decimal price;
        bool res = decimal.TryParse((rnd.NextDouble() * rnd.NextInt64(100, 10_000)).ToString(), out price);
        double chance = rnd.NextDouble();

        await using var query = new NpgsqlCommand("INSERT INTO ink_products(title, description, each_price) VALUES ($1, $2, $3)", conn)
        {
            Parameters =
                {
                    new(){ Value = title },
                    new(){ Value = lorem },
                    new(){ Value = res == true ? price : 100.0d },
                }
        };
        await query.ExecuteNonQueryAsync();
        await conn.CloseAsync();
    }
}