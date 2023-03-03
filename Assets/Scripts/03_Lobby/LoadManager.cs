using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] itemAdapter;

    [SerializeField]
    private Transform[] content;

    private FirebaseFirestore firebaseFirestore;

    void Start()
    {

        Init();

    }

    async void Init()
    {

        await Task.Delay(1);
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().FirebaseFirestore;

    }

    private void LoadRooms(List<RoomStruct> _rooms, bool _isStudent)
    {

        foreach (RoomStruct room in _rooms)
        {

            GameObject newItem = Instantiate(RoomAdapter, Rooms);
            if (newItem.TryGetComponent(out RoomAdapter item))
            {

                int roomSlots = room.room_slots;
                string roomId = room.room_id;
                string roomName = room.room_name;
                string roomPassword = room.room_password;
                string roomPlayerId = room.room_player_id;
                string roomPlayerName = room.room_player_name;

                item.RoomId = roomId;
                item.RoomName = roomName;
                item.RoomPassword = roomPassword;
                item.IsRemoveUIButtonVisible = !_isStudent;

                firebaseFirestore
                    .Collection("Players")
                    .WhereEqualTo("room_id", roomId)
                    .GetSnapshotAsync()
                    .ContinueWithOnMainThread(task =>
                    {

                        QuerySnapshot documentSnapshots = task.Result;

                        if (documentSnapshots != null)
                        {


                            bool isFull = roomSlots - documentSnapshots.Count == 0;
                            item.IsRoomFull = isFull;
                            item.OnSetRoomHUDInteractable(!isFull);

                            item.RoomSubtitleUIText = string.Format("{0} / {1} · {2}", documentSnapshots.Count, roomSlots, roomPlayerName);

                        }

                    });

            }

        }

    }

    private void LoadPlayers(List<PlayerStruct> _players)
    {

        int counter = 0;

        foreach (PlayerStruct player in _players)
        {

            GameObject newItem = Instantiate(PlayerAdapter, Players);
            if (newItem.TryGetComponent(out PlayerAdapter item))
            {

                ++counter;
                string playerLastName = player.player_last_name;
                string playerFirstName = player.player_first_name;
                string playerImage = player.player_image;
                string playerName = string.Format("{0} . {1}, {2}", counter.ToString("00"), playerLastName, playerFirstName);
                string playerStudentId = player.player_student_id;
                double reputation = player.player_reputation * 100;
                string playerReputation = string.Format("{0}%", reputation.ToString("00.00"));

                item.PlayerName = playerName;
                item.PlayerStudentID = playerStudentId;
                item.PlayerReputation = playerReputation;
                StartCoroutine(GetImage(item.PlayerImage, playerImage));

            }

        }

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

    private GameObject RoomAdapter => itemAdapter[0];

    private GameObject PlayerAdapter => itemAdapter[1];

    public Transform Rooms
    {

        get => content[0];
        set => content[0] = value;

    }

    public Transform Players
    {

        get => content[1];
        set => content[1] = value;

    }

    public void OnLoadRooms(List<RoomStruct> _rooms, bool _isStudent) { LoadRooms(_rooms, _isStudent); }

    public void OnLoadPlayers(List<PlayerStruct> _players) { LoadPlayers(_players); }

}
