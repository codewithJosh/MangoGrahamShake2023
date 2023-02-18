using Firebase.Firestore;

[FirestoreData]
public struct PlayerStruct
{

    [FirestoreProperty]
    public bool player_is_student { get; set; }

    [FirestoreProperty]
    public double player_capital { get; set; }

    [FirestoreProperty]
    public double player_price { get; set; }

    [FirestoreProperty]
    public double[] player_popularity { get; set; }

    [FirestoreProperty]
    public double[] player_satisfaction { get; set; }

    [FirestoreProperty]
    public int player_advertisement { get; set; }

    [FirestoreProperty]
    public int player_location { get; set; }

    [FirestoreProperty]
    public int player_temperature { get; set; }

    [FirestoreProperty]
    public int[] player_recipe { get; set; }

    [FirestoreProperty]
    public int[] player_supplies { get; set; }

    [FirestoreProperty]
    public string player_first_name { get; set; }

    [FirestoreProperty]
    public string player_id { get; set; }

    [FirestoreProperty]
    public string player_image { get; set; }

    [FirestoreProperty]
    public string player_last_name { get; set; }

    [FirestoreProperty]
    public string player_student_id { get; set; }

    [FirestoreProperty]
    public string room_id { get; set; }

}
