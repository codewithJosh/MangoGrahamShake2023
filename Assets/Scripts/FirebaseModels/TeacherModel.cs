using Firebase.Firestore;

[FirestoreData]
public struct TeacherModel
{

    [FirestoreProperty]
    public string player_first_name { get; set; }

    [FirestoreProperty]
    public string player_id { get; set; }

    [FirestoreProperty]
    public bool player_is_student { get; set; }

    [FirestoreProperty]
    public string player_last_name { get; set; }

}
