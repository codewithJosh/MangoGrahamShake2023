using System.Collections.Generic;
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

            1,
            1,
            1,

        };
        PlayerTemperature = Random.Range(20.0f, 45.0f);
        PlayerConstant = 8;
        PlayerTargetCriteria = new int[]
        {

            4,
            30,
            2,
            10,

        };
        PlayerRevenue = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerStockUsed = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerStockLost = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerGrossProfit = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerGrossMargin = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerRent = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerMarketing = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerExpenses = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerEarnings = new double[]
        {

            0,
            0,
            0,
            0,

        };
        PlayerStaffs = new List<int>{};

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
            player_constant = PlayerConstant,
            player_target_criteria = PlayerTargetCriteria,
            player_revenue = PlayerRevenue,
            player_stock_used = PlayerStockUsed,
            player_stock_lost = PlayerStockLost,
            player_gross_profit = PlayerGrossProfit,
            player_gross_margin = PlayerGrossMargin,
            player_rent = PlayerRent,
            player_marketing = PlayerMarketing,
            player_expenses = PlayerExpenses,
            player_earnings = PlayerEarnings,
            player_staffs = PlayerStaffs,

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
        PlayerConstant = player.player_constant;
        PlayerTargetCriteria = player.player_target_criteria;
        PlayerRevenue = player.player_revenue;
        PlayerStockUsed = player.player_stock_used;
        PlayerStockLost = player.player_stock_lost;
        PlayerGrossProfit = player.player_gross_profit;
        PlayerGrossMargin = player.player_gross_margin;
        PlayerRent = player.player_rent;
        PlayerMarketing = player.player_marketing;
        PlayerExpenses = player.player_expenses;
        PlayerEarnings = player.player_earnings;
        PlayerStaffs = player.player_staffs;

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

    public int PlayerConstant { get; set; }

    public double PlayerCostPerCup { get; set; }

    public double PlayerProfitPerCup { get; set; }

    public int PlayerCupsPerPitcher { get; set; }

    public int[] PlayerTargetCriteria { get; set; }

    public double[] PlayerRevenue { get; set; }

    public double[] PlayerStockUsed { get; set; }

    public double[] PlayerStockLost { get; set; }

    public double[] PlayerGrossProfit { get; set; }

    public double[] PlayerGrossMargin { get; set; }

    public double[] PlayerRent { get; set; }

    public double[] PlayerMarketing { get; set; }

    public double[] PlayerExpenses { get; set; }

    public double[] PlayerEarnings { get; set; }

    public List<int> PlayerStaffs { get; set; }

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
