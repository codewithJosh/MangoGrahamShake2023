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
        PlayerStaffs = new List<int> { };
        PlayerReputation = 0;
        PlayerCupsSold = 0;
        PlayerUnsatisfiedCustomers = 0;
        PlayerSatisfiedCustomers = 0;
        PlayerImpatientCustomers = 0;
        PlayerOverPricedCustomers = 0;
        PlayerStorage = new int[]
        {

            50,
            938,
            50,
            750,
            100,

        };
        PlayerCustomerSatisfaction = 0;
        PlayerIceCubesMelted = 0;
        PlayerTopEarnings = 0;
        PlayerDaysWithoutAdvertisement = 0;
        PlayerFeedback = 0;
        PlayerEquipments = 0;
        PlayerProfitAndLoss = 0;
        PlayerUpgrade = new int[]
        {

            0,
            0,
            0,

        };

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
            player_constant = PlayerConstant,
            player_cups_sold = PlayerCupsSold,
            player_date = PlayerDate,
            player_earnings = PlayerEarnings,
            player_expenses = PlayerExpenses,
            player_first_name = PlayerFirstName,
            player_gross_margin = PlayerGrossMargin,
            player_gross_profit = PlayerGrossProfit,
            player_id = PlayerId,
            player_image = PlayerImage,
            player_impatient_customers = PlayerImpatientCustomers,
            player_last_name = PlayerLastName,
            player_location = PlayerLocation,
            player_marketing = PlayerMarketing,
            player_over_priced_customers = PlayerOverPricedCustomers,
            player_popularity = PlayerPopularity,
            player_price = PlayerPrice,
            player_recipe = PlayerRecipe,
            player_rent = PlayerRent,
            player_reputation = PlayerReputation,
            player_revenue = PlayerRevenue,
            player_satisfaction = PlayerSatisfaction,
            player_satisfied_customers = PlayerSatisfiedCustomers,
            player_staffs = PlayerStaffs,
            player_stock_lost = PlayerStockLost,
            player_stock_used = PlayerStockUsed,
            player_student_id = PlayerStudentId,
            player_supplies = PlayerSupplies,
            player_target_criteria = PlayerTargetCriteria,
            player_temperature = PlayerTemperature,
            player_unsatisfied_customers = PlayerUnsatisfiedCustomers,
            room_id = RoomId,
            player_storage = PlayerStorage,
            player_customer_satisfaction = PlayerCustomerSatisfaction,
            player_ice_cubes_melted = PlayerIceCubesMelted,
            player_top_earnings = PlayerTopEarnings,
            player_days_without_advertisement = PlayerDaysWithoutAdvertisement,
            player_feedback = PlayerFeedback,
            player_equipments = PlayerEquipments,
            player_profit_and_loss = PlayerProfitAndLoss,
            player_upgrade = PlayerUpgrade,

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
        PlayerConstant = player.player_constant;
        PlayerCupsSold = player.player_cups_sold;
        PlayerDate = player.player_date;
        PlayerEarnings = player.player_earnings;
        PlayerExpenses = player.player_expenses;
        PlayerFirstName = player.player_first_name;
        PlayerGrossMargin = player.player_gross_margin;
        PlayerGrossProfit = player.player_gross_profit;
        PlayerId = player.player_id;
        PlayerImage = player.player_image;
        PlayerImpatientCustomers = player.player_impatient_customers;
        PlayerLastName = player.player_last_name;
        PlayerLocation = player.player_location;
        PlayerMarketing = player.player_marketing;
        PlayerOverPricedCustomers = player.player_over_priced_customers;
        PlayerPopularity = player.player_popularity;
        PlayerPrice = player.player_price;
        PlayerRecipe = player.player_recipe;
        PlayerRent = player.player_rent;
        PlayerReputation = player.player_reputation;
        PlayerRevenue = player.player_revenue;
        PlayerSatisfaction = player.player_satisfaction;
        PlayerSatisfiedCustomers = player.player_satisfied_customers;
        PlayerStaffs = player.player_staffs;
        PlayerStockLost = player.player_stock_lost;
        PlayerStockUsed = player.player_stock_used;
        PlayerStudentId = player.player_student_id;
        PlayerSupplies = player.player_supplies;
        PlayerTargetCriteria = player.player_target_criteria;
        PlayerTemperature = player.player_temperature;
        PlayerUnsatisfiedCustomers = player.player_unsatisfied_customers;
        RoomId = player.room_id;
        PlayerStorage = player.player_storage;
        PlayerCustomerSatisfaction = player.player_customer_satisfaction;
        PlayerIceCubesMelted = player.player_ice_cubes_melted;
        PlayerTopEarnings = player.player_top_earnings;
        PlayerDaysWithoutAdvertisement = player.player_days_without_advertisement;
        PlayerFeedback = player.player_feedback;
        PlayerEquipments = player.player_equipments;
        PlayerProfitAndLoss = player.player_profit_and_loss;
        PlayerUpgrade = player.player_upgrade;

    }

    private void LocalSave()
    {

        Database.LocalSave(this);

    }

    public bool PlayerIsStudent { get; set; }

    public double PlayerCapital { get; set; }

    public double PlayerCostPerCup { get; set; }

    public double PlayerPrice { get; set; }

    public double PlayerReputation { get; set; }

    public double PlayerTemperature { get; set; }

    public double[] PlayerEarnings { get; set; }

    public double[] PlayerExpenses { get; set; }

    public double[] PlayerGrossMargin { get; set; }

    public double[] PlayerGrossProfit { get; set; }

    public double[] PlayerMarketing { get; set; }

    public double[] PlayerPopularity { get; set; }

    public double[] PlayerRent { get; set; }

    public double[] PlayerRevenue { get; set; }

    public double[] PlayerSatisfaction { get; set; }

    public double[] PlayerStockLost { get; set; }

    public double[] PlayerStockUsed { get; set; }

    public int PlayerAdvertisement { get; set; }

    public int PlayerConstant { get; set; }

    public int PlayerCupsPerPitcher { get; set; }

    public int PlayerCupsSold { get; set; }

    public int PlayerImpatientCustomers { get; set; }

    public int PlayerLocation { get; set; }

    public int PlayerOverPricedCustomers { get; set; }

    public int PlayerSatisfiedCustomers { get; set; }

    public int PlayerUnsatisfiedCustomers { get; set; }

    public int[] PlayerDate { get; set; }

    public int[] PlayerRecipe { get; set; }

    public int[] PlayerSupplies { get; set; }

    public int[] PlayerTargetCriteria { get; set; }

    public List<int> PlayerStaffs { get; set; }

    public string PlayerFirstName { get; set; }

    public string PlayerId { get; set; }

    public string PlayerImage { get; set; }

    public string PlayerLastName { get; set; }

    public string PlayerStudentId { get; set; }

    public string RoomId { get; set; }

    public int[] PlayerStorage { get; set; }

    public double PlayerCustomerSatisfaction { get; set; }

    public int PlayerIceCubesMelted { get; set; }

    public double PlayerTopEarnings { get; set; }

    public int PlayerDaysWithoutAdvertisement { get; set; }

    public int PlayerFeedback { get; set; }

    public double PlayerEquipments { get; set; }

    public double PlayerProfitAndLoss { get; set; }

    public int[] PlayerUpgrade { get; set; }

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
