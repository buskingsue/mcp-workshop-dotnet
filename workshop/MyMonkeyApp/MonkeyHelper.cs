using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMonkeyApp;

/// <summary>
/// 원숭이 데이터 관리를 위한 정적 헬퍼 클래스입니다.
/// </summary>
public static class MonkeyHelper
{
    private static List<Monkey>? monkeys;
    private static readonly Dictionary<string, int> randomAccessCounts = new();

    /// <summary>
    /// MCP 서버에서 모든 원숭이 데이터를 비동기로 가져옵니다.
    /// </summary>
    public static async Task<List<Monkey>> GetMonkeysAsync()
    {
        if (monkeys != null)
            return monkeys;

        // MCP 서버에서 데이터 가져오기 (여기서는 예시로 하드코딩)
        monkeys = new List<Monkey>
        {
            new Monkey { Name = "Baboon", Location = "Africa & Asia", Population = 10000, Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.", Image = "", Latitude = -8.783195, Longitude = 34.508523 },
            new Monkey { Name = "Capuchin Monkey", Location = "Central & South America", Population = 23000, Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.", Image = "", Latitude = 12.769013, Longitude = -85.602364 },
            new Monkey { Name = "Blue Monkey", Location = "Central and East Africa", Population = 12000, Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia", Image = "", Latitude = 1.957709, Longitude = 37.297204 },
            // ... (필요시 추가)
        };
        return monkeys;
    }

    /// <summary>
    /// 이름으로 원숭이 정보를 가져옵니다.
    /// </summary>
    public static async Task<Monkey?> GetMonkeyByNameAsync(string name)
    {
        var list = await GetMonkeysAsync();
        return list.FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 무작위 원숭이를 가져오고, 해당 원숭이의 액세스 횟수를 1 증가시킵니다.
    /// </summary>
    public static async Task<Monkey?> GetRandomMonkeyAsync()
    {
        var list = await GetMonkeysAsync();
        if (list.Count == 0) return null;
        var random = new Random();
        var monkey = list[random.Next(list.Count)];
        if (!randomAccessCounts.ContainsKey(monkey.Name))
            randomAccessCounts[monkey.Name] = 0;
        randomAccessCounts[monkey.Name]++;
        return monkey;
    }

    /// <summary>
    /// 무작위로 선택된 각 원숭이의 액세스 횟수를 반환합니다.
    /// </summary>
    public static IReadOnlyDictionary<string, int> GetRandomAccessCounts() => randomAccessCounts;
}
