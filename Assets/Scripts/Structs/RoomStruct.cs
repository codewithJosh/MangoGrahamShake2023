using Firebase.Firestore;

[FirestoreData]
public struct RoomStruct
{

    [FirestoreProperty]
    public int room_slots { get; set; }

    [FirestoreProperty]
    public string room_id { get; set; }

    [FirestoreProperty]
    public string room_name { get; set; }

    [FirestoreProperty]
    public string room_password { get; set; }

    [FirestoreProperty]
    public string room_player_id { get; set; }

}
