using DotNetEnv;
using OrderProcessingApp.ConsoleUI;
using OrderProcessingApp.Domain.Services;
using OrderProcessingApp.Repositories;


string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
string dataFolder = Path.Combine(projectRoot, "Data");
Directory.CreateDirectory(dataFolder);
JsonFileOrderRepository js = new JsonFileOrderRepository();
InMemoryOrderRepository repository = new InMemoryOrderRepository();
bool isChoose = false;
while (!isChoose)
{
    Console.WriteLine("Wybierz dane które chcesz Wczytać:");
    Console.WriteLine("1. Wczytaj dane z testowymi 20 Danymi");
    Console.WriteLine("2. Wczytaj dane z testowymi 10 Danymi");
    Console.WriteLine("3. Wczytaj inne dane");
    Console.WriteLine("4. Pomiń");
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            js.LoadFromFile(Path.Combine(dataFolder, "20Orders.json"), repository);
            isChoose = true;
            break;
        case "2":
            js.LoadFromFile(Path.Combine(dataFolder, "10Orders.json"), repository);
            isChoose = true;
            break;
        case "3":
            Console.WriteLine("Podaj ścieżkę do pliku z danymi:");
            string path = Console.ReadLine();
            js.LoadFromFile(Path.Combine(dataFolder, path), repository);
            isChoose = true;
            break;
        case "4":
            isChoose = true;
            break;
        default:
            Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
            break;

    }
}
OrderService orderService = new OrderService(repository);


bool isRunning = true;

while (isRunning)
{
    Console.WriteLine();
    Console.WriteLine("========== MENU ==========");
    Console.WriteLine("1. Dodaj zamówienie");
    Console.WriteLine("2. Przekazanie zamówienia do Magazynu");
    Console.WriteLine("3. Przekazanie zamówienia do Wysyłki");
    Console.WriteLine("4. Wyświetl wszystkie zamówienia");
    Console.WriteLine("5. Usuń zamówienie");
    Console.WriteLine("6. Wyświetl informacje o zamówieniu");
    Console.WriteLine("7. Wyjdź");
    Console.WriteLine("==========================");
    Console.Write("Wybierz opcję: ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ConsoleMenuActions.DodajZamówienie(orderService);
            break;
        case "2":
            ConsoleMenuActions.PrzekazanieDoMagazynu(orderService);
            break;
        case "3":
            ConsoleMenuActions.PrzekazanieDoWysyłki(orderService);
            break;
        case "4":
            ConsoleMenuActions.WyswietlZamowienia(orderService);
            break;
        case "5":
            ConsoleMenuActions.UsunZamowienie(orderService);
            break;
        case "6":
            ConsoleMenuActions.WyswietlSzczegolyZamowienia(orderService);
            break;
        case "7":
            isRunning = false;
            break;
        default:
            Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
            break;
    }
}
Console.WriteLine("\nCzy chcesz zapisać zmiany do pliku JSON? (T/N)");
string answer = Console.ReadLine();
if (answer == "t" || answer == "tak" || answer == "y" || answer == "yes")
{
    Console.WriteLine("Podaj ścieżkę do pliku JSON:");
    string jsonFilePath = Console.ReadLine();
    jsonFilePath = jsonFilePath.EndsWith(".json") ? jsonFilePath : jsonFilePath + ".json";
    js.SaveToFile(Path.Combine(dataFolder, jsonFilePath), repository);
    Console.WriteLine($"Zmiany zapisane w: {jsonFilePath}");
}
else
{
    Console.WriteLine("Zmiany niezapisane. Kończę działanie programu.");
}
Console.WriteLine("Koniec programu. Naciśnij Enter, aby zakończyć.");
Console.ReadLine();
        