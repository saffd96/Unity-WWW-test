using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class UserPanel : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    public Image Image => image;
    public TMP_Text NameText => nameText;
    public TMP_Text ScoreText => scoreText;

    public void Initialization(UserData userData)
    {
        StartCoroutine(DownloadImage(userData.AvatarUrl));
        nameText.text = userData.Username;
        scoreText.text = userData.Points.ToString();
    }

    private IEnumerator DownloadImage(string url)
    {
        var request = UnityWebRequestTexture.GetTexture(url);

        using (request)
        {
            yield return request.SendWebRequest();
            var texture2D = DownloadHandlerTexture.GetContent(request);
            request.Dispose();
            image.sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height),
                Vector2.one * 0.5f);
        }
    }
}