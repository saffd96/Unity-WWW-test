using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public class UserInfosGetter
{
    public event Action<UserData[]> OnUsersInitialized;

    public async UniTask GetUsersByUrl(string url)
    {
        using var request = UnityWebRequest.Get(url);

        request.SetRequestHeader("Content-Type", "application/json");

        await GetUsersAsync(request);

        if (request.result != UnityWebRequest.Result.Success) return;
        
        var usersData = JsonHelper.GetJsonArray<UserData>(request.downloadHandler.text);
        request.Dispose();
        usersData = usersData.OrderBy(x => x.Points).Reverse().ToArray();
        OnUsersInitialized?.Invoke(usersData);
    }


    private async UniTask<string> GetUsersAsync(UnityWebRequest request)
    {
        var op = await request.SendWebRequest();
        return op.downloadHandler.text;
    }
}