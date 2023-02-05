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
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().Firestore;

    }

    private void LoadRooms(bool _isStudent, List<FirebaseRoomModel> _rooms)
    {

        string roomId = PlayerPrefs.GetString("room_id", "");

        foreach (FirebaseRoomModel room in _rooms)
        {

            GameObject newItem = Instantiate(itemAdapter, content);
            if (newItem.TryGetComponent(out RoomAdapter item))
            {

                string _roomId = room.room_id;
                int roomSlots = room.room_slots;

                firebaseFirestore
                    .Collection("Players")
                    .WhereEqualTo("room_id", _roomId)
                    .GetSnapshotAsync()
                    .ContinueWithOnMainThread(task =>
                    {

                        QuerySnapshot documentSnapshots = task.Result;

                        if (documentSnapshots != null)
                            item.RoomSlots = string.Format("{0} / {1}", documentSnapshots.Count, roomSlots);

                    });

                item.RoomName = room.room_name;
                
                item.RemoveUIButton = !_isStudent;
                item.LeaveUIButton = _isStudent && roomId.Equals(room.room_id);

            }

        }

    }

    public void OnLoadRooms(bool _isStudent, List<FirebaseRoomModel> _rooms) { LoadRooms(_isStudent, _rooms); }

}
