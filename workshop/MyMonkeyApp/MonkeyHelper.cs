using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMonkeyApp;

/// <summary>
/// 원숭이 데이터 관리를 위한 헬퍼 클래스
/// </summary>
public static class MonkeyHelper
{
    private static List<Monkey>? monkeys;
    private static int randomMonkeyAccessCount = 0;
    private static readonly object lockObj = new();

    /// <summary>
    /// MCP 서버에서 원숭이 데이터를 비동기로 가져옵니다.
    /// </summary>
    public static async Task<List<Monkey>> GetMonkeysAsync()
    {
        if (monkeys != null)
            return monkeys;

        // MCP 서버에서 데이터 가져오기 (예시)
        monkeys = await MonkeyMcpClient.FetchMonkeysAsync();
        return monkeys;
    }

    /// <summary>
    /// 모든 원숭이 목록을 반환합니다.
    /// </summary>
    public static async Task<IEnumerable<Monkey>> GetAllMonkeysAsync()
    {
        var list = await GetMonkeysAsync();
        return list;
    }

    /// <summary>
    /// 랜덤 원숭이 하나를 반환하고, 접근 횟수를 증가시킵니다.
    /// </summary>
    public static async Task<Monkey?> GetRandomMonkeyAsync()
    {
        var list = await GetMonkeysAsync();
        if (list.Count == 0) return null;
        lock (lockObj)
        {
            randomMonkeyAccessCount++;
        }
        var rnd = new Random();
        return list[rnd.Next(list.Count)];
    }

    /// <summary>
    /// 이름으로 원숭이를 찾습니다.
    /// </summary>
    public static async Task<Monkey?> GetMonkeyByNameAsync(string name)
    {
        var list = await GetMonkeysAsync();
        return list.FirstOrDefault(m => m.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 랜덤 원숭이 접근 횟수를 반환합니다.
    /// </summary>
    public static int GetRandomMonkeyAccessCount()
    {
        lock (lockObj)
        {
            return randomMonkeyAccessCount;
        }
    }
}

/// <summary>
/// MCP 서버와 통신하는 클라이언트 예시 (실제 구현 필요)
/// </summary>
public static class MonkeyMcpClient
{
    public static async Task<List<Monkey>> FetchMonkeysAsync()
    {
        // 실제 MCP 서버 API 호출 코드로 대체 필요
        await Task.Delay(100); // 네트워크 지연 시뮬레이션
        return new List<Monkey>
        {
            new Monkey { Name = "Baboon", Location = "Africa & Asia", Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.", Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/baboon.jpg", Population = 10000, Latitude = -8.783195, Longitude = 34.508523 },
            new Monkey { Name = "Capuchin Monkey", Location = "Central & South America", Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.", Image = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/capuchin.jpg", Population = 23000, Latitude = 12.769013, Longitude = -85.602364 },
            // ... 나머지 원숭이 데이터 추가 ...
        };
    }
}
