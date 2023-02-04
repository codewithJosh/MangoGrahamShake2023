using Firebase.Firestore;

[FirestoreData]
public struct FirebasePlayerModel
{

    [FirestoreProperty]
    public string player_first_name { get; set; }

    [FirestoreProperty]
    public string player_id { get; set; }

    [FirestoreProperty]
    public string player_image { get; set; }

    [FirestoreProperty]
    public bool player_is_student { get; set; }

    [FirestoreProperty]
    public string player_last_name { get; set; }

    [FirestoreProperty]
    public string player_student_id { get; set; }

    [FirestoreProperty]
    public string room_id { get; set; }

}
