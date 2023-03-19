using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    void Awake()
    {

        PlayerAdvertisement = 0;
        PlayerCapital = 1000.00;
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
        PlayerSupplies = new int[]
        {

            0,
            0,
            0,
            0,
            0,

        };
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

}
