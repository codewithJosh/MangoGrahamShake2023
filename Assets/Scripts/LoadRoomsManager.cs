using System.Collections.Generic;
using UnityEngine;

public class LoadRoomsManager : MonoBehaviour
{

    [SerializeField]
    private GameObject itemAdapter;

    [SerializeField]
    private Transform content;

    private void LoadRooms(bool _isStudent, List<FirebaseRoomModel> _rooms)
    {

        string roomId = PlayerPrefs.GetString("room_id", "");

        foreach (FirebaseRoomModel room in _rooms)
        {

            GameObject newItem = Instantiate(itemAdapter, content);
            if (newItem.TryGetComponent(out RoomAdapter item))
            {

                item.RoomName = room.room_name;
                item.RoomSlots = room.room_slots;
                item.RemoveUIButton = !_isStudent;
                item.LeaveUIButton = _isStudent && roomId.Equals(room.room_id);

            }

        }

    }

    public void OnLoadRooms(bool _isStudent, List<FirebaseRoomModel> _rooms) { LoadRooms(_isStudent, _rooms); }

}
