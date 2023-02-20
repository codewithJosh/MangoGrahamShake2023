using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private void AutoSave(bool _isConnected)
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)

            LocalLoad();

        LocalSave();

        if (_isConnected)

            FindObjectOfType<FirebaseFirestoreManager>().OnGlobalSave(GlobalSavePlayer());

    }

    private PlayerStruct GlobalSavePlayer(
        bool _isStudent,
        string _firstName,
        string _playerId,
        string _playerImage,
        string _lastName,
        string _studentId)
    {

        PlayerAdvertisement = 0;
        PlayerCapital = 1000.00;
        PlayerId = _playerId;
        PlayerImage = _playerImage;
        PlayerLastName = _lastName;
        PlayerLocation = 0;
        PlayerPopularity = new double[]
        {

            0.1,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,
            0,

        };
        PlayerSatisfaction = new double[]
        {

            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,
            1,

        };
        PlayerStudentId = _studentId;
        PlayerSupplies = new int[]
        {

            0,
            0,
            0,
            0,
            0,

        };
        RoomId = "";
        PlayerFirstName = _firstName;
        PlayerIsStudent = _isStudent;
        PlayerPrice = 30;
        PlayerRecipe = new int[]
        {

            12,
            37,
            12,
            10,

        };
        PlayerDate = new int[]
        {

            01,
            01,
            2023,

        };
        PlayerTemperature = Random.Range(20.0f, 45.0f);

        LocalSave();
        LocalLoad();

        return GlobalSavePlayer();

    }

    private PlayerStruct GlobalSavePlayer()
    {

        PlayerStruct player = new()
        {

            player_is_student = PlayerIsStudent,
            player_advertisement = PlayerAdvertisement,
            player_capital = PlayerCapital,
            player_first_name = PlayerFirstName,
            player_id = PlayerId,
            player_image = PlayerImage,
            player_last_name = PlayerLastName,
            player_location = PlayerLocation,
            player_popularity = PlayerPopularity,
            player_price = PlayerPrice,
            player_recipe = PlayerRecipe,
            player_satisfaction = PlayerSatisfaction,
            player_student_id = PlayerStudentId,
            player_supplies = PlayerSupplies,
            player_temperature = PlayerTemperature,
            player_date = PlayerDate,
            room_id = RoomId,

        };

        return player;

    }

    private void GlobalLoad(PlayerStruct _playerStruct)
    {

        Database.LocalSave(_playerStruct);
        LocalLoad();

    }

    private void LocalLoad()
    {

        PlayerModel player = Database.LocalLoadPlayer();

        PlayerIsStudent = player.player_is_student;
        PlayerAdvertisement = player.player_advertisement;
        PlayerCapital = player.player_capital;
        PlayerFirstName = player.player_first_name;
        PlayerId = player.player_id;
        PlayerImage = player.player_image;
        PlayerLastName = player.player_last_name;
        PlayerLocation = player.player_location;
        PlayerPopularity = player.player_popularity;
        PlayerPrice = player.player_price;
        PlayerRecipe = player.player_recipe;
        PlayerSatisfaction = player.player_satisfaction;
        PlayerStudentId = player.player_student_id;
        PlayerSupplies = player.player_supplies;
        PlayerTemperature = player.player_temperature;
        PlayerDate = player.player_date;
        RoomId = player.room_id;

    }

    private void LocalSave()
    {

        Database.LocalSave(this);

    }

    public bool PlayerIsStudent { get; set; }

    public double PlayerCapital { get; set; }

    public double PlayerPrice { get; set; }

    public double[] PlayerPopularity { get; set; }

    public double[] PlayerSatisfaction { get; set; }

    public int PlayerAdvertisement { get; set; }

    public int PlayerLocation { get; set; }

    public double PlayerTemperature { get; set; }

    public int[] PlayerRecipe { get; set; }

    public int[] PlayerSupplies { get; set; }

    public string PlayerFirstName { get; set; }

    public string PlayerId { get; set; }

    public string PlayerImage { get; set; }

    public string PlayerLastName { get; set; }

    public string PlayerStudentId { get; set; }

    public string RoomId { get; set; }

    public int[] PlayerDate { get; set; }

    public void OnAutoSave(bool _isConnected) => AutoSave(_isConnected);

    public PlayerStruct OnGlobalSavePlayer(
        bool _isStudent,
        string _firstName,
        string _playerId,
        string _playerImage,
        string _lastName,
        string _studentId) => GlobalSavePlayer(
            _isStudent,
            _firstName,
            _playerId,
            _playerImage,
            _lastName,
            _studentId);

    public PlayerStruct OnLocalLoadPlayer => GlobalSavePlayer();

    public void OnGlobalLoad(PlayerStruct _playerStruct) { GlobalLoad(_playerStruct); }

    public void OnLocalLoad() { LocalLoad(); }

    public void OnLocalSave() { LocalSave(); }

}
