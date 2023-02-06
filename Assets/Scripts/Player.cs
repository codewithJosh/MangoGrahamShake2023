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

        PlayerIsStudent = _isStudent;
        PlayerAdvertisement = 0;
        PlayerCapital = 2000.00;
        PlayerPopularity = 0.01;
        PlayerPrice = 65f;
        PlayerSatisfaction = 1;
        PlayerTemperature = Random.Range(20, 45);
        PlayerLeft = new int[]
        {
            0,
            0,
            0,
            0,
            0
        };
        PlayerPerServe = new int[]
        {
            4,
            2,
            2,
            2
        };
        PlayerFirstName = _firstName;
        PlayerId = _playerId;
        PlayerImage = _playerImage;
        PlayerLastName = _lastName;
        PlayerStudentId = _studentId;
        RoomId = null;

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
            player_popularity = PlayerPopularity,
            player_price = PlayerPrice,
            player_satisfaction = PlayerSatisfaction,
            player_temperature = PlayerTemperature,
            player_left = PlayerLeft,
            player_per_serve = PlayerPerServe,
            player_first_name = PlayerFirstName,
            player_id = PlayerId,
            player_image = PlayerImage,
            player_last_name = PlayerLastName,
            player_student_id = PlayerStudentId,
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
        PlayerPopularity = player.player_popularity;
        PlayerPrice = player.player_price;
        PlayerSatisfaction = player.player_satisfaction;
        PlayerTemperature = player.player_temperature;
        PlayerLeft = player.player_left;
        PlayerPerServe = player.player_per_serve;
        PlayerFirstName = player.player_first_name;
        PlayerId = player.player_id;
        PlayerImage = player.player_image;
        PlayerLastName = player.player_last_name;
        PlayerStudentId = player.player_student_id;
        RoomId = player.room_id;

    }

    private void LocalSave()
    {

        Database.LocalSave(this);

    }

    public bool PlayerIsStudent { get; set; }
    public double PlayerAdvertisement { get; set; }
    public double PlayerCapital { get; set; }
    public double PlayerPopularity { get; set; }
    public double PlayerPrice { get; set; }
    public double PlayerSatisfaction { get; set; }
    public int PlayerTemperature { get; set; }
    public int[] PlayerLeft { get; set; }
    public int[] PlayerPerServe { get; set; }
    public string PlayerFirstName { get; set; }
    public string PlayerId { get; set; }
    public string PlayerImage { get; set; }
    public string PlayerLastName { get; set; }
    public string PlayerStudentId { get; set; }
    public string RoomId { get; set; }

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
