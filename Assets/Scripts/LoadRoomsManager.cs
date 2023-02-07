using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LoadRoomsManager : MonoBehaviour
{

    [SerializeField]
    private GameObject itemAdapter;

    [SerializeField]
    private Transform content;

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

    private void LoadRooms(bool _isStudent, List<RoomStruct> _rooms)
    {

        string roomId = PlayerPrefs.GetString("room_id", "");

        if (content != null) content.ClearChildren();

        foreach (RoomStruct room in _rooms)
        {

            GameObject newItem = Instantiate(itemAdapter, content);
            if (newItem.TryGetComponent(out RoomAdapter item))
            {

                string _roomId = room.room_id;
                int roomSlots = room.room_slots;
                string roomPlayerId = room.room_player_id;

                firebaseFirestore
                    .Collection("Players")
                    .WhereEqualTo("room_id", _roomId)
                    .GetSnapshotAsync()
                    .ContinueWithOnMainThread(task =>
                    {

                        QuerySnapshot documentSnapshots = task.Result;

                        if (documentSnapshots != null)
                        {

                            
                            bool isFull = roomSlots - documentSnapshots.Count == 0;
                            item.IsRoomFull = isFull;
                            item.OnSetRoomHUDInteractable(!isFull);

                            firebaseFirestore.Collection("Players").Document(roomPlayerId).GetSnapshotAsync().ContinueWithOnMainThread(task =>
                            {

                            DocumentSnapshot doc = task.Result;

                                if (doc != null && doc.Exists)
                                {

                                    PlayerStruct player = doc.ConvertTo<PlayerStruct>();

                                    string playerLastName = player.player_last_name;
                                    string playerFirstName = player.player_first_name;

                                    item.RoomSubtitleUIText = string.Format("{0} / {1} · {2}, {3}", documentSnapshots.Count, roomSlots, playerLastName, playerFirstName);

                                }

                            });

                        }

                    });

                item.RoomId = _roomId;
                item.RoomName = room.room_name;
                item.RoomPassword = room.room_password;
                item.IsRemoveUIButtonVisible = !_isStudent;
                item.IsLeaveUIButtonVisible = _isStudent && roomId.Equals(room.room_id);

            }

        }

    }

    public void OnLoadRooms(bool _isStudent, List<RoomStruct> _rooms) { LoadRooms(_isStudent, _rooms); }

}
