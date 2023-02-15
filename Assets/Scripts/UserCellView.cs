using Cysharp.Threading.Tasks;
using EnhancedUI.EnhancedScroller;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UserCellView : EnhancedScrollerCellView
{
    [SerializeField] private Sprite defaultSprite;
    
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    public TMP_Text NameText => nameText;
    public TMP_Text ScoreText => scoreText;
    
    public async UniTask DownloadImage(string url)
    {
        image.sprite = defaultSprite;
        using var request = UnityWebRequestTexture.GetTexture(url);

        await UniTask.Create(() => GetTextureAsync(request));

        var texture2D = DownloadHandlerTexture.GetContent(request);
        var sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
            Vector2.one * 0.5f);

        image.sprite = sprite;
    }
    
    private async UniTask<Texture2D> GetTextureAsync(UnityWebRequest request)
    {
        await request.SendWebRequest();
        return request.result != UnityWebRequest.Result.Success ? DownloadHandlerTexture.GetContent(request) : null;
    }
}
