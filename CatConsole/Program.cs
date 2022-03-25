// See https://aka.ms/new-console-template for more information
using CatConsole;
using CatConsole.Utils;
using OfficeOpenXml;
using System.Text;

Config config = new();
Console.WriteLine("欢迎使用CatConsole");
Console.WriteLine();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
UTF8Encoding utf8 = new UTF8Encoding(false);
var excels_data = new ExcelUtils("./猫咪档案.xlsx");
List<Cat>? labels = excels_data.getCatList();
/*foreach (Cat? label in labels)
{
    Console.WriteLine("Hello, World!");
    Console.WriteLine($"{label.Name}");
}*/
// Console.WriteLine(labels);
// Console.ReadLine();
List<string> names = new List<string>();
foreach (var cat in labels)
{
    names.Add(cat.Name);
}


var dictory = new DirectoryInfo(@"./cats/");
if (!dictory.Exists)
{
    dictory.Create();
}

await File.WriteAllTextAsync("../app.json", "{\n  \"pages\": [ \n    \"pages/index/index\",\n", utf8);

foreach (Cat cat in labels)
{
    // Console.WriteLine("Hello, World!");
    if (cat.isInAtlas >= 0)
    {
        var catdirectory = new DirectoryInfo($"./cats/{cat.Name}");
        if (!catdirectory.Exists)
        {
            catdirectory.Create();
        }

        await File.WriteAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", $"var app = getApp()\n Page({{\n data: {{\n catname:\"{cat.Name}\",\n catitems:[\n", utf8);

        var properties = cat.GetType().GetProperties();
        foreach (var property in properties)
        {
            if (property.Name!="isInAtlas" && property.Name!="Audio" && property.Name !="Video" && (property.GetValue(cat) != null))
            {
                if (property.Name=="ColorIndex")
                {
                    // Console.WriteLine(config.Color[property.GetValue(cat).ToString()]);
                    await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                    $"{{category: \"{config.ClassMap[property.Name]}\",\n content: \"{config.Color[property.GetValue(cat).ToString()]}\",}},\n", utf8);
                }
                else if (property.Name== "Sex")
                {
                    // Console.WriteLine(config.Sex[property.GetValue(cat)]);
                    await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                    $"{{category: \"{config.ClassMap[property.Name]}\",\n content: \"{config.Sex[property.GetValue(cat)]}\",}},\n", utf8);

                }
                else if (property.Name== "Character")
                {
                    // Console.WriteLine(config.Character[property.GetValue(cat).ToString()]);
                    await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                    $"{{category: \"{config.ClassMap[property.Name]}\",\n content: \"{config.Character[property.GetValue(cat).ToString()]} \",}},\n", utf8);

                }
                else if (property.Name == "isSterilize")
                {
                    await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                    $"{{category: \"{config.ClassMap[property.Name]}\",\n content: \"{config.Sterilize[property.GetValue(cat)]} \",}},\n", utf8);

                }
                else if (property.PropertyType == typeof(DateTime?))
                {
                    var dt = (DateTime?)property.GetValue(cat);
                    // Console.WriteLine(dt);
                    await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                    $"{{category: \"{config.ClassMap[property.Name]}\",\n content: \"{dt.Value.ToString("yyyy/MM/dd")} \",}},\n", utf8);
                }
                else
                {
                    // Console.WriteLine(property.PropertyType.Name);
                    await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                    $"{{category: \"{config.ClassMap[property.Name]}\",\n content: \"{property.GetValue(cat)} \",}},\n", utf8);
                }
            }
        }
        await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
        "\n], \nurl: app.globalData.url,\n", utf8);
        await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "relationship:[");
        foreach (var name in names)
        {
            if (cat.Relationship != null)
            {
                if (cat.Relationship.Contains(name) && cat.Name != name)
                {
                    await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", $"{{ rela: \"{name}\"}},\n");
                }
            }

        }
        await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "],\n");
        await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "nums:[\n");
        for (int i = 0; i < cat.isInAtlas; i++)
        {
            await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                $"{{ num: {i+1} }},\n");
        }
        await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "],\n");
        if (cat.Video != null && cat.Video > 0)
        {
            await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "MovieNums:[\n");

            for (int i = 0; i < cat.Video; i++)
            {
                await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js",
                    $"{{ num: {i+1} }},\n");
            }
            await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "],\n");
        }
        if (cat.Audio != null && cat.Audio > 0)
        {
            Uri url = new Uri($"https:{config.Link}{cat.Name}/");
            await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "audioArr:[\n");
            for (int i = 0; i < cat.Audio; i++)
            {
                await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", $"{{\n src: \'https:{url.OriginalString}{i+1}.m4a\',\nbl: false\n}},\n");
            }
            await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "],\n  audKey: \'\', \n},\n");
        }
        else
        {
            await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", "},");
        }
        await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", File.ReadAllText("./js.txt"));
        await File.WriteAllTextAsync($"cats/{cat.Name}/{cat.Name}.wxml", File.ReadAllText("./wxml.txt"));
        await File.WriteAllTextAsync($"cats/{cat.Name}/{cat.Name}.json", File.ReadAllText("./json.txt"));
        await File.AppendAllTextAsync("../app.json", $"    \"pages/cats/{cat.Name}/{cat.Name}\",\n");
        // await File.AppendAllTextAsync($"cats/{cat.Name}/{cat.Name}.js", $"{{ rela: \"{name}\"}},\n");
        Console.WriteLine($"成功添加了{cat.Name}");
    }

}
await File.AppendAllTextAsync("../app.json", File.ReadAllText("./appjson.txt"));


foreach (var i in config.Color.Keys)
{
    // Console.WriteLine(config.Color[i]);
    var dir = new DirectoryInfo($"./index/{config.Color[i]}");
    if (!dir.Exists)
    {
        dir.Create();
    }
    await File.WriteAllTextAsync($"./index/{config.Color[i]}/{config.Color[i]}.js",
        "var app = getApp()\n Page({\ndata: { \n catlist: [\n", utf8);

    foreach (var cat in labels)
    {
        if (cat.ColorIndex == Convert.ToInt32(i))
        {
            await File.AppendAllTextAsync($"./index/{config.Color[i]}/{config.Color[i]}.js",
            $"{{ name:\"{cat.Name}\"}},", utf8);
        }

    }
    await File.AppendAllTextAsync($"./index/{config.Color[i]}/{config.Color[i]}.js",
   File.ReadAllText("./js2.txt"), utf8);
}


var dire = new DirectoryInfo($"./index/所有");
if (!dire.Exists)
{
    dire.Create();
}
await File.WriteAllTextAsync($"index/所有/所有.js",
    "var app = getApp()\n Page({\ndata: { \n catlist: [\n", utf8);

foreach (var cat in labels)
{
    await File.AppendAllTextAsync($"index/所有/所有.js",
        $"{{ name:\"{cat.Name}\"}},", utf8);


}
await File.AppendAllTextAsync($"index/所有/所有.js",
    File.ReadAllText("./js2.txt"), utf8);


List<Cat> health = new List<Cat>();
List<Cat> fostered = new List<Cat>();
List<Cat> dead = new List<Cat>();
List<Cat> unknown = new List<Cat>();

foreach (var cat in labels)
{
    if (cat.isInAtlas >= 0)
    {
        if (cat.State == "离世")
        {
            dead.Add(cat);
        }
        if (cat.State =="送养")
        {
            fostered.Add(cat);
        }
        if (cat.State == "健康" && cat.State == "口炎")
        {
            health.Add(cat);
        }
        if (cat.State == "不明")
        {
            unknown.Add(cat);
        }
    }
}

dead.OrderBy(l => l.DeathTime);
fostered.OrderBy(l => l.AdoptionTime);

await File.WriteAllTextAsync("index/index.js", "var app = getApp()\n Page({\ndata: { \n");
await File.AppendAllTextAsync("index/index.js", "fostered_catlist: [\n");
foreach (var cat in fostered)
{
    await File.AppendAllTextAsync("index/index.js", $"{{ name: \"{cat.Name}\"}},\n");

}
await File.AppendAllTextAsync("index/index.js", "],\n");
// unknown
await File.AppendAllTextAsync("index/index.js", "  unknown_catlist: [\n");
foreach (var cat in unknown)
{
    await File.AppendAllTextAsync("index/index.js", $"{{ name: \"{cat.Name}\"}},\n");

}
await File.AppendAllTextAsync("index/index.js", "],\n");
// dead
await File.AppendAllTextAsync("index/index.js", " dead_catlist: [\n");
foreach (var cat in dead)
{
    await File.AppendAllTextAsync("index/index.js", $"{{ name: \"{cat.Name}\"}},\n");
}

await File.AppendAllTextAsync("index/index.js", "],\n");
await File.AppendAllTextAsync("index/index.js", File.ReadAllText("js_index.txt"));

var app = File.ReadAllText("../app.json");
File.WriteAllText("../app.json", app, utf8);
Console.WriteLine();
Console.WriteLine("已全部添加完毕, 按任意键以继续...");
Console.ReadLine();

