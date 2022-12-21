using Npgsql;
using System.Text;
using LoremNET;
using Npgsql.PostgresTypes;
using InkRealmMVC.Models;
using System.Data;
using InkProductsGenerator;

internal class Program
{
    static readonly Random rnd = new();

    private static void Main(string[] args)
    {
        string txt = @"D:\Studying\Projects of language\InkRealm\InkRealmMVC\wwwroot/img/masters_img/info\dd84ccd5-94eb-4a41-85d0-94ad289625e7.jp";
        Console.WriteLine();


        //Теперь это надо перенести на MasterFetch

        //List<TestModel> testsInfo = new();
        //
        //for (int i = 1; i < 6; i++)
        //{
        //    testsInfo.Add(new()
        //    {
        //        UserId = i,
        //        InkTitle = $"{i}",
        //        InkInfo = $"Some random info {i}"
        //    });
        //
        //    if(i == 3)
        //    {
        //        testsInfo.Add(new()
        //        {
        //            UserId = i,
        //            InkTitle = $"{i}",
        //            InkInfo = $"Some random info {i}"
        //        });
        //    }
        //}
        //
        //Dictionary<int, Dictionary<string, List<string>>> filteredInfo = new();
        //for (int i = 1; i < testsInfo.Count - 1; i++)
        //{
        //    if (testsInfo[i].UserId != testsInfo[i - 1].UserId)
        //    {
        //        filteredInfo[testsInfo[i].UserId] = new Dictionary<string, List<string>>
        //        {
        //            { testsInfo[i].InkTitle, new() { testsInfo[i].InkInfo } }
        //        };
        //    }
        //    else
        //    {
        //        filteredInfo[testsInfo[i].UserId][testsInfo[i].InkTitle].Add(testsInfo[i].InkInfo);
        //    }
        //}
        //
        //foreach (var fInfo in filteredInfo)
        //{
        //    Console.Write($"Key: {fInfo.Key} ");
        //    foreach (var fInfoDict in fInfo.Value)
        //    {
        //        Console.Write($"TitleKey: {fInfoDict.Key} ");
        //        foreach (var fInfoListInDict in fInfoDict.Value)
        //        {
        //            Console.Write($"ListValue: {fInfoListInDict}");
        //        }
        //    }
        //    Console.WriteLine();
        //}

        //await CheckForView();
        // string[] namesFor = { "Набор", "Инструмент", "Средство", "Комплекс" };
        // string[] pretext = { "для", "от", "чтобы", "затем", /"временного", /"переводного", "постоянного" };
        // string[] tatooContext = { "тату", "эскиз", "пирсинга", /"набросок", /"краски" };
        // string[] addingInfo = { "змея", "кролик", "самолёт", "пират", /"птица", /"цитата", "стих", "капибара", "зеркало", "снег" };
        // HashSet<string> generetatedTitles = new();
        //
        // while (generetatedTitles.Count < 500)
        // {
        //     StringBuilder title = new();
        //     title.Append(namesFor[rnd.Next(namesFor.Length - 1)]);
        //     title.Append(' ');
        //     title.Append(pretext[rnd.Next(pretext.Length - 1)]);
        //     title.Append(' ');
        //     title.Append(tatooContext[rnd.Next(tatooContext.Length - 1)]);
        //     title.Append(' ');
        //     title.Append(addingInfo[rnd.Next(addingInfo.Length - 1)]);
        //     generetatedTitles.Add(title.ToString());
        // }
        // 
        //
        // foreach (var t in generetatedTitles)
        // {
        //     string loremShit = Lorem.Sentence(rnd.Next(10, 1_000_000));        
        //     await GenerateTrashForInkProductAsync(t, loremShit);
        // }
    }

    //private async static Task GenerateTrashForInkProductAsync(string title, string lorem)
    //{
    //    await using NpgsqlConnection conn = new("Host=localhost;Port=5432;Database=ink_realm;Username=postgres;Password=B&k34RPvvB12F");
    //    await conn.OpenAsync();
    //    decimal price;
    //    bool res = decimal.TryParse((rnd.NextDouble() * rnd.NextInt64(100, 10_000)).ToString(), out price);
    //    double chance = rnd.NextDouble();
    //
    //    await using var query = new NpgsqlCommand("INSERT INTO ink_products(title, description, each_price) VALUES ($1, $2, $3)", conn)
    //    {
    //        Parameters =
    //            {
    //                new(){ Value = title },
    //                new(){ Value = lorem },
    //                new(){ Value = res == true ? price : 100.0d },
    //            }
    //    };
    //    await query.ExecuteNonQueryAsync();
    //    await conn.CloseAsync();
    //}

    
}