using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private GameObject itemAdapter;

    [SerializeField]
    private Transform content;

    #endregion

    #region LOAD_LEADERBOARD_METHOD

    private void LoadLeaderboard(List<PlayerStruct> _players)
    {

        content.ClearChildren();

        int counter = 0;

        foreach (PlayerStruct player in _players)
        {

            GameObject newItem = Instantiate(itemAdapter, content);
            if (newItem.TryGetComponent(out PlayerAdapter item))
            {

                double playerReputation = player.player_reputation;
                string playerImage = player.player_image;
                string playerName = player.player_name;
                string playerEmail = player.player_email;

                item.PlayerRank = $"{++counter:00}";
                item.PlayerName = playerName;
                item.PlayerEmail = playerEmail;
                item.PlayerReputation = playerReputation;
                StartCoroutine(GetImage(item.PlayerImage, playerImage));

                if (STATUS.FIREBASE_USER.UserId == player.player_id)

                    FindObjectOfType<LeaderboardManager>().PlayerRank = counter;

            }

        }

    }

    #endregion

    #region GET_IMAGE_METHOD

    private static IEnumerator GetImage(Image PlayerImage, string _playerImage)
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

    #endregion

    #region AUTOMATED_PROPERTY

    public void OnLoadLeaderboard(List<PlayerStruct> _players) => LoadLeaderboard(_players);

    #endregion

}
