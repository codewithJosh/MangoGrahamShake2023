[System.Serializable]
public class PlayerModel
{

    public bool player_is_student;
    public double player_capital;
    public double player_price;
    public double[] player_popularity;
    public double[] player_satisfaction;
    public int player_advertisement;
    public int player_location;
    public int player_temperature;
    public int[] player_recipe;
    public int[] player_supplies;
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
        player_first_name = _playerStruct.player_first_name;
        player_id = _playerStruct.player_id;
        player_image = _playerStruct.player_image;
        player_last_name = _playerStruct.player_last_name;
        player_location = _playerStruct.player_location;
        player_popularity = _playerStruct.player_popularity;
        player_price = _playerStruct.player_price;
        player_recipe = _playerStruct.player_recipe;
        player_satisfaction = _playerStruct.player_satisfaction;
        player_student_id = _playerStruct.player_student_id;
        player_supplies = _playerStruct.player_supplies;
        player_temperature = _playerStruct.player_temperature;
        room_id = _playerStruct.room_id;

    }

    public PlayerModel(Player _player)
    {

        player_is_student = _player.PlayerIsStudent;
        player_advertisement = _player.PlayerAdvertisement;
        player_capital = _player.PlayerCapital;
        player_first_name = _player.PlayerFirstName;
        player_id = _player.PlayerId;
        player_image = _player.PlayerImage;
        player_last_name = _player.PlayerLastName;
        player_location = _player.PlayerLocation;
        player_popularity = _player.PlayerPopularity;
        player_price = _player.PlayerPrice;
        player_recipe = _player.PlayerRecipe;
        player_satisfaction = _player.PlayerSatisfaction;
        player_student_id = _player.PlayerStudentId;
        player_supplies = _player.PlayerSupplies;
        player_temperature = _player.PlayerTemperature;
        room_id = _player.RoomId;

    }

}
