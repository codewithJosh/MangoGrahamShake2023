using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private bool player_is_student;
    private double player_advertisement;
    private double player_capital;
    private double player_popularity;
    private double player_price;
    private double player_satisfaction;
    private int player_temperature;
    private int[] player_left;
    private int[] player_per_serve;
    private string player_first_name;
    private string player_id;
    private string player_image;
    private string player_last_name;
    private string player_student_id;
    private string room_id;

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

        player_is_student = _isStudent;
        player_advertisement = 0;
        player_capital = 2000.00;
        player_popularity = 0.01;
        player_price = 65f;
        player_satisfaction = 1;
        player_temperature = Random.Range(20, 45);
        player_left = new int[]
        {
            0,
            0,
            0,
            0,
            0
        };
        player_per_serve = new int[]
        {
            4,
            2,
            2,
            2
        };
        player_first_name = _firstName;
        player_id = _playerId;
        player_image = _playerImage;
        player_last_name = _lastName;
        player_student_id = _studentId;
        room_id = null;

        LocalSave();
        LocalLoad();

        return GlobalSavePlayer();

    }

    private PlayerStruct GlobalSavePlayer()
    {

        PlayerStruct player = new()
        {

            player_is_student = player_is_student,
            player_advertisement = player_advertisement,
            player_capital = player_capital,
            player_popularity = player_popularity,
            player_price = player_price,
            player_satisfaction = player_satisfaction,
            player_temperature = player_temperature,
            player_left = player_left,
            player_per_serve = player_per_serve,
            player_first_name = player_first_name,
            player_id = player_id,
            player_image = player_image,
            player_last_name = player_last_name,
            player_student_id = player_student_id,
            room_id = room_id,

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

        player_is_student = player.player_is_student;
        player_advertisement = player.player_advertisement;
        player_capital = player.player_capital;
        player_popularity = player.player_popularity;
        player_price = player.player_price;
        player_satisfaction = player.player_satisfaction;
        player_temperature = player.player_temperature;
        player_left = player.player_left;
        player_per_serve = player.player_per_serve;
        player_first_name = player.player_first_name;
        player_id = player.player_id;
        player_image = player.player_image;
        player_last_name = player.player_last_name;
        player_student_id = player.player_student_id;
        room_id = player.room_id;

    }

    private void LocalSave()
    {

        Database.LocalSave(this);

    }

    public bool PlayerIsStudent { get => player_is_student; set { player_is_student = value; } }
    public double PlayerAdvertisement { get => player_advertisement; set { player_advertisement = value; } }
    public double PlayerCapital { get => player_capital; set { player_capital = value; } }
    public double PlayerPopularity { get => player_popularity; set { player_popularity = value; } }
    public double PlayerPrice { get => player_price; set { player_price = value; } }
    public double PlayerSatisfaction { get => player_satisfaction; set { player_satisfaction = value; } }
    public int PlayerTemperature { get => player_temperature; set { player_temperature = value; } }
    public int[] PlayerLeft { get => player_left; set { player_left = value; } }
    public int[] PlayerPerServe { get => player_per_serve; set { player_per_serve = value; } }
    public string PlayerFirstName { get => player_first_name; set { player_first_name = value; } }
    public string PlayerId { get => player_id; set { player_id = value; } }
    public string PlayerImage { get => player_image; set { player_image = value; } }
    public string PlayerLastName { get => player_last_name; set { player_last_name = value; } }
    public string PlayerStudentId { get => player_student_id; set { player_student_id = value; } }
    public string RoomId { get => room_id; set { room_id = value; } }

    public void OnAutoSave(bool _isConnected) { AutoSave(_isConnected); }

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
