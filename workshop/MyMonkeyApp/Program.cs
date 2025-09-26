
using MyMonkeyApp;

class Program
{
	private static readonly string[] AsciiArts = new[]
	{
		@"  (o.o)   ",
		@" ( (..) ) ",
		@"  ( : )   ",
		@"  ("""")   ",
		@"  (='.'=)  ",
		@"  (o^.^o)  ",
		@"  ('.')    "
	};

	static async Task Main()
	{
		while (true)
		{
			Console.Clear();
			PrintRandomAsciiArt();
			Console.WriteLine("============================");
			Console.WriteLine("🐒 Monkey Console App 🐒");
			Console.WriteLine("============================");
			Console.WriteLine("1. 모든 원숭이 나열");
			Console.WriteLine("2. 이름으로 특정 원숭이의 세부 정보 가져오기");
			Console.WriteLine("3. 무작위 원숭이 가져오기");
			Console.WriteLine("4. 앱 종료");
			Console.Write("메뉴를 선택하세요: ");
			var input = Console.ReadLine();
			Console.WriteLine();
			switch (input)
			{
				case "1":
					await ListAllMonkeys();
					break;
				case "2":
					await ShowMonkeyByName();
					break;
				case "3":
					await ShowRandomMonkey();
					break;
				case "4":
					Console.WriteLine("앱을 종료합니다.");
					return;
				default:
					Console.WriteLine("잘못된 입력입니다. 엔터를 눌러 계속하세요.");
					Console.ReadLine();
					break;
			}
		}
	}

	private static void PrintRandomAsciiArt()
	{
		var random = new Random();
		var art = AsciiArts[random.Next(AsciiArts.Length)];
		Console.WriteLine(art);
		Console.WriteLine();
	}

	private static async Task ListAllMonkeys()
	{
		var monkeys = await MonkeyHelper.GetMonkeysAsync();
		Console.WriteLine("이름 | 서식지 | 개체수");
		Console.WriteLine("-----------------------------");
		foreach (var m in monkeys)
		{
			Console.WriteLine($"{m.Name} | {m.Location} | {m.Population}");
		}
		Console.WriteLine("\n엔터를 눌러 계속하세요.");
		Console.ReadLine();
	}

	private static async Task ShowMonkeyByName()
	{
		Console.Write("원숭이 이름을 입력하세요: ");
		var name = Console.ReadLine();
		if (string.IsNullOrWhiteSpace(name))
		{
			Console.WriteLine("이름이 비어 있습니다. 엔터를 눌러 계속하세요.");
			Console.ReadLine();
			return;
		}
		var monkey = await MonkeyHelper.GetMonkeyByNameAsync(name);
		if (monkey == null)
		{
			Console.WriteLine("해당 이름의 원숭이를 찾을 수 없습니다. 엔터를 눌러 계속하세요.");
			Console.ReadLine();
			return;
		}
		PrintMonkeyDetails(monkey);
		Console.WriteLine("\n엔터를 눌러 계속하세요.");
		Console.ReadLine();
	}

	private static async Task ShowRandomMonkey()
	{
		var monkey = await MonkeyHelper.GetRandomMonkeyAsync();
		if (monkey == null)
		{
			Console.WriteLine("원숭이 데이터가 없습니다. 엔터를 눌러 계속하세요.");
			Console.ReadLine();
			return;
		}
		PrintMonkeyDetails(monkey);
		var counts = MonkeyHelper.GetRandomAccessCounts();
		Console.WriteLine($"(무작위로 이 원숭이가 선택된 횟수: {counts[monkey.Name]})");
		Console.WriteLine("\n엔터를 눌러 계속하세요.");
		Console.ReadLine();
	}

	private static void PrintMonkeyDetails(Monkey m)
	{
		Console.WriteLine($"이름: {m.Name}");
		Console.WriteLine($"서식지: {m.Location}");
		Console.WriteLine($"개체수: {m.Population}");
		Console.WriteLine($"설명: {m.Details}");
		if (!string.IsNullOrWhiteSpace(m.Image))
		{
			Console.WriteLine($"이미지: {m.Image}");
		}
		Console.WriteLine($"위도: {m.Latitude}, 경도: {m.Longitude}");
	}
}
