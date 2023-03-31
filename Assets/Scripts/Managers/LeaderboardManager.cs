using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
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

        LoadPlayers();

        playerNameUIText.text = FindObjectOfType<PLAYER>().PlayerName;
        playerEmailUIText.text = FindObjectOfType<PLAYER>().PlayerEmail;
        StartCoroutine(GetImage(playerImageHUD, FindObjectOfType<PLAYER>().PlayerImage));

    }

    void Update()
    {

        GameManager
            .Animator
            .SetBool(ENV.IS_LEADERBOARD_LOADING, STATUS.IS_LEADERBOARD_LOADING);

        playerReputationUIText.text = $"{FindObjectOfType<PLAYER>().PlayerReputation * 100.0:0.00}%";

        if (SimpleInput.GetButtonDown("OnClose"))

            GameManager.OnTrigger(ENV.BACK);

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

    private void LoadPlayers()
    {

        STATUS.IS_LEADERBOARD_LOADING = true;

        int[] playerDate = new int[] { 1, 1, 1 };

        STATUS.FIREBASE_FIRESTORE
            .Collection("Players")
            .WhereNotEqualTo("player_date", playerDate)
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {

                QuerySnapshot documentSnapshots = task.Result;

                if (documentSnapshots != null && documentSnapshots.Count != 0)
                {

                    List<PlayerStruct> players = new();

                    foreach (DocumentSnapshot doc in documentSnapshots)
                    {

                        PlayerStruct player = doc.ConvertTo<PlayerStruct>();
                        players.Add(player);

                    }

                    players.Sort((player1, player2) => player2.player_reputation.CompareTo(player1.player_reputation));

                    FindObjectOfType<LoadManager>().OnLoadPlayers(players);
                    STATUS.IS_LEADERBOARD_LOADING = false;

                }

            });

    }

    public int PlayerRank
    {

        set => playerRankUIText.text = $"{value:00}";

    }

}
