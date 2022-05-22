using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task3.Console.Registrations;
using Task3.Console.Services;
using Task3.Model.DTOs.Requests;
using Task3.Model.DTOs.Requests.Item;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

var configuration = builder.Build();

var services = new ServiceCollection();
ConfigureServices(services, configuration);


var dbServise = services
    .AddSingleton<DbService, DbService>()
    .BuildServiceProvider()
    .GetService<DbService>();


ExecuteUserCommunications(dbServise);


static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services
        .RegisterOptions(configuration)
        .RegisterRepositories()
        .RegisterDomains();
}

static void ExecuteUserCommunications(DbService dbServise)
{
    string welcomeText = string.Format("Hi! You could type:{0}", string.Join("", new string[] {
        "\n\t'q' to quit,",
        "\n\t'1' to create a new item,",
        "\n\t'2' to get ab item by ID",
        "\n\t'3' to get all items,",
        "\n\t'4' to update an item,",
        "\n\t'5' to delete an item" }));

    var input = string.Empty;
    while(!input.Equals("q"))
    {
        Console.WriteLine(welcomeText);
        input = Console.ReadLine();

        switch(input)
        {
            case "1":
                CreateItem(dbServise);
                break;
            case "2":
                GetItemById(dbServise);
                break;
            case "3":
                GetAllItems(dbServise);
                break;
            case "4":
                UpdateItem(dbServise);
                break;
            case "5":
                DeleteItem(dbServise);
                break;
        }

    }
}


static async void CreateItem(DbService dbServise)
{
    Console.Write("Enter 'Name', please: ");
    var input = Console.ReadLine();
    var result = await dbServise.CreateItemAsync(new CreateItemRequest() { Name = input });
    Console.WriteLine(result);
    Console.ReadKey();
}

static async void GetItemById(DbService dbServise)
{
    Console.Write("Enter 'ID', please: ");
    var input = Console.ReadLine();
    if(int.TryParse(input, out var id))
    {
        var result = await dbServise.GetItemByIdAsync(id);
        Console.WriteLine(result);
    }
    else
    {
        Console.WriteLine("Wrong input. Id should be a number");
    }
    Console.ReadKey();
}

static async void GetAllItems(DbService dbServise)
{
    var pageSize = 3;
    Console.Write("Enter page index, please: ");
    var input = Console.ReadLine();
    var page = 1;

    if(!string.IsNullOrEmpty(input) && !int.TryParse(input, out page))
    {
        Console.WriteLine("Wrong input. Page index should be a number");
        return;
    }

    var paginatedRequest = string.IsNullOrEmpty(input) ? null : new PaginatedRequest() { PageIndex = page, PageSize = pageSize };
    var result = await dbServise.GetAllAsync(new PaginatedRequest() { PageIndex = page, PageSize = pageSize });
    Console.WriteLine(result);
    Console.ReadKey();
}

static async void UpdateItem(DbService dbServise)
{
    Console.Write("Enter 'Id', please: ");
    var inputID = Console.ReadLine();

    if(!int.TryParse(inputID, out var id))
    {
        Console.WriteLine("Wrong input. ID should be a number");
        return;
    }

    Console.Write("Enter 'Name', please: ");
    var inputName = Console.ReadLine();

    var result = await dbServise.UpdateItemAsync(new UpdateItemRequest() { Id = id, Name = inputName });
    Console.WriteLine(result);
    Console.ReadKey();
}

static async void DeleteItem(DbService dbServise)
{
    Console.Write("Enter 'Id', please: ");
    var input = Console.ReadLine();
    if(!int.TryParse(input, out var id))
    {
        Console.WriteLine("Wrong input. ID should be a number");
        return;
    }

    var result = await dbServise.DeleteItemAsync(id);
    Console.WriteLine(result);
    Console.ReadKey();
}
