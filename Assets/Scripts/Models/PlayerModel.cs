using Firebase.Firestore;

[FirestoreData]
public struct PlayerModel
{

    [FirestoreProperty]
    public string Player_id { get; set; }

    [FirestoreProperty]
    public string Player_last_name { get; set; }

    [FirestoreProperty]
    public string Player_first_name { get; set; }

    [FirestoreProperty]
    public string Player_student_id { get; set; }

}
