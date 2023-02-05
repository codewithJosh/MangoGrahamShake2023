[System.Serializable]
public class PlayerModel
{

    public bool player_is_student;
    public double player_advertisement;
    public double player_capital;
    public double player_popularity;
    public double player_price;
    public double player_satisfaction;
    public int player_temperature;
    public int[] player_left;
    public int[] player_per_serve;
    public string player_first_name;
    public string player_id;
    public string player_image;
    public string player_last_name;
    public string player_student_id;
    public string room_id;


    public PlayerModel(PlayerStruct _playerStruct)
    {

        player_is_student = _playerStruct.player_is_student;
        player_advertisement = _playerStruct.player_advertisement;
        player_capital = _playerStruct.player_capital;
        player_popularity = _playerStruct.player_popularity;
        player_price = _playerStruct.player_price;
        player_satisfaction = _playerStruct.player_satisfaction;
        player_temperature = _playerStruct.player_temperature;
        player_left = _playerStruct.player_left;
        player_per_serve = _playerStruct.player_per_serve;
        player_first_name = _playerStruct.player_first_name;
        player_id = _playerStruct.player_id;
        player_image = _playerStruct.player_image;
        player_last_name = _playerStruct.player_last_name;
        player_student_id = _playerStruct.player_student_id;
        room_id = _playerStruct.room_id;

    }

    public PlayerModel(Player _player)
    {

        player_is_student = _player.PlayerIsStudent;
        player_advertisement = _player.PlayerAdvertisement;
        player_capital = _player.PlayerCapital;
        player_popularity = _player.PlayerPopularity;
        player_price = _player.PlayerPrice;
        player_satisfaction = _player.PlayerSatisfaction;
        player_temperature = _player.PlayerTemperature;
        player_left = _player.PlayerLeft;
        player_per_serve = _player.PlayerPerServe;
        player_first_name = _player.PlayerFirstName;
        player_id = _player.PlayerId;
        player_image = _player.PlayerImage;
        player_last_name = _player.PlayerLastName;
        player_student_id = _player.PlayerStudentId;
        room_id = _player.RoomId;

    }

}
