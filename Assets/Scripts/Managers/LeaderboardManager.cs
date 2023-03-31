using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private TextMeshProUGUI playerRankUIText;

    [SerializeField]
    private TextMeshProUGUI playerNameUIText;

    [SerializeField]
    private TextMeshProUGUI playerEmailUIText;

    [SerializeField]
    private TextMeshProUGUI playerReputationUIText;

    [SerializeField]
    private Image playerImageHUD;

    #endregion

    void Start()
    {

        playerNameUIText.text = FindObjectOfType<PLAYER>().PlayerName;
        playerEmailUIText.text = FindObjectOfType<PLAYER>().PlayerEmail;
        StartCoroutine(GetImage(playerImageHUD, FindObjectOfType<PLAYER>().PlayerImage));

    }

    void Update()
    {

        playerReputationUIText.text = $"{FindObjectOfType<PLAYER>().PlayerReputation * 100.0:0.00}%";

    }

    private IEnumerator GetImage(Image PlayerImage, string _playerImage)
    {

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(_playerImage);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)

            Debug.Log(request.error);

        else
        {

            Texture2D texture2D = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
            PlayerImage.sprite = sprite;

        }

    }

}
