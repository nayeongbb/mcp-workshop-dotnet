
using MyMonkeyApp;

class Program
{
	static readonly string[] asciiArts = new[]
	{
		@"  (\__/)
  (•ㅅ•)
  / 　 づ   ",
		@"  ／￣￣￣＼
 /　●　●　\
(　　●　　)
 \＿＿＿／",
		@"  (o o)
 /|\_/|\
((@v@))",
		@"  (¬‿¬)
 ( ͡° ͜ʖ ͡°)",
		@"  (☞ﾟヮﾟ)☞"
	};

	static async Task Main()
	{
		var rnd = new Random();
		Console.WriteLine("Welcome to the Monkey App!");
		Console.WriteLine(asciiArts[rnd.Next(asciiArts.Length)]);

		while (true)
		{
			Console.WriteLine("\n===== Monkey Menu =====");
			Console.WriteLine("1. List all monkeys");
			Console.WriteLine("2. Get details for a specific monkey by name");
			Console.WriteLine("3. Get a random monkey");
			Console.WriteLine("4. Exit app");
			Console.Write("Select an option: ");
			var input = Console.ReadLine();

			if (rnd.Next(0, 3) == 0)
			{
				Console.WriteLine("\n" + asciiArts[rnd.Next(asciiArts.Length)] + "\n");
			}

			switch (input)
			{
				case "1":
					var monkeys = await MonkeyHelper.GetAllMonkeysAsync();
					Console.WriteLine("\n| Name                | Location                | Population |");
					Console.WriteLine("-------------------------------------------------------------");
					foreach (var m in monkeys)
					{
						Console.WriteLine($"| {m.Name,-20} | {m.Location,-22} | {m.Population,9} |");
					}
					break;
				case "2":
					Console.Write("Enter monkey name: ");
					var name = Console.ReadLine();
					var monkey = await MonkeyHelper.GetMonkeyByNameAsync(name ?? "");
					if (monkey != null)
					{
						Console.WriteLine($"\nName: {monkey.Name}\nLocation: {monkey.Location}\nPopulation: {monkey.Population}\nDetails: {monkey.Details}\nImage: {monkey.Image}\nCoords: ({monkey.Latitude}, {monkey.Longitude})");
					}
					else
					{
						Console.WriteLine("Monkey not found.");
					}
					break;
				case "3":
					var randomMonkey = await MonkeyHelper.GetRandomMonkeyAsync();
					if (randomMonkey != null)
					{
						Console.WriteLine($"\nRandom Monkey: {randomMonkey.Name}\nLocation: {randomMonkey.Location}\nPopulation: {randomMonkey.Population}\nDetails: {randomMonkey.Details}\nImage: {randomMonkey.Image}\nCoords: ({randomMonkey.Latitude}, {randomMonkey.Longitude})");
						Console.WriteLine($"Random monkey accessed {MonkeyHelper.GetRandomMonkeyAccessCount()} times.");
					}
					else
					{
						Console.WriteLine("No monkeys available.");
					}
					break;
				case "4":
					Console.WriteLine("Goodbye!");
					return;
				default:
					Console.WriteLine("Invalid option. Try again.");
					break;
			}
		}
	}
}
