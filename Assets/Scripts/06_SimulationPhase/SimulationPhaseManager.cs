using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulationPhaseManager : MonoBehaviour
{

    private int playerConstant;
    private double playerTemperature;
    private int playerLocation;
    private double playerPopularity;
    private double[] playerSatisfaction;
    private double playerPrice;
    private int[] playerRecipe;
    private int[] playerSupplies;
    private double playerCostPerCup;
    private int playerCupsPerPitcher;
    private int playerAdvertisement;
    private int[] playerDate;
    private int[] playerTargetCriteria;
    private List<int> playerStaffs;

    private double[,] LOCATION;
    private double[,] TEMPERATURE;
    private int MINIMUM_CUPS;
    private int[] TARGET_CRITERIA;
    private double[,] ADVERTISEMENT;
    private double OVERPRICED;
    private int INCREMENT_POPULARITY_PER_DAY;
    private double[,] STAFF;

    private int population;
    private double popularity;
    private double satisfaction;
    private double price;
    private double temperature;

    private int overAllCustomer;
    private int currentCustomer;

    private int pitcher;
    private int cupsSold;
    private double waitingTime;
    private double[] criteria;
    private int satisfiedCustomers;
    private int unsatisfiedCustomers;
    private int overPricedCustomers;
    private int impatientCustomers;

    void Start()
    {

        Init();

        LOCATION = FindObjectOfType<ENV>().LOCATION;
        TEMPERATURE = FindObjectOfType<ENV>().TEMPERATURE;
        MINIMUM_CUPS = FindObjectOfType<ENV>().MINIMUM_CUPS;
        TARGET_CRITERIA = FindObjectOfType<ENV>().TARGET_CRITERIA;
        ADVERTISEMENT = FindObjectOfType<ENV>().ADVERTISEMENT;
        OVERPRICED = FindObjectOfType<ENV>().OVERPRICED;
        INCREMENT_POPULARITY_PER_DAY = FindObjectOfType<ENV>().INCREMENT_POPULARITY_PER_DAY;
        STAFF = FindObjectOfType<ENV>().STAFF;

        playerConstant = FindObjectOfType<Player>().PlayerConstant;
        playerTemperature = FindObjectOfType<Player>().PlayerTemperature;
        playerLocation = FindObjectOfType<Player>().PlayerLocation;
        playerPopularity = FindObjectOfType<Player>().PlayerPopularity[playerLocation];
        playerSatisfaction = FindObjectOfType<Player>().PlayerSatisfaction;
        playerPrice = FindObjectOfType<Player>().PlayerPrice;
        playerRecipe = FindObjectOfType<Player>().PlayerRecipe;
        playerSupplies = FindObjectOfType<Player>().PlayerSupplies;
        playerCostPerCup = FindObjectOfType<Player>().PlayerCostPerCup;
        playerCupsPerPitcher = FindObjectOfType<Player>().PlayerCupsPerPitcher;
        playerAdvertisement = FindObjectOfType<Player>().PlayerAdvertisement;
        playerDate = FindObjectOfType<Player>().PlayerDate;
        playerTargetCriteria = FindObjectOfType<Player>().PlayerTargetCriteria;
        playerStaffs = FindObjectOfType<Player>().PlayerStaffs;

        population = (int)LOCATION[playerLocation, 0];
        playerPopularity +=
            playerAdvertisement > 0
            ? ADVERTISEMENT[playerAdvertisement, 1]
            : LOCATION[playerLocation, 2];
        popularity =
            playerPopularity >= 1
            ? 1
            : playerPopularity;

        price = GetPricing(playerPrice);
        playerConstant += INCREMENT_POPULARITY_PER_DAY;
        temperature = GetTemperature(playerTemperature);

        overAllCustomer = GetOverallCustomer();
        currentCustomer = overAllCustomer;

        GetPhase();
        GetPerformance();
        Done();

    }

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnSkip"))
        {



        }

    }

    private void Init()
    {

        pitcher = 0;
        cupsSold = 0;
        waitingTime = 1 - 0.1;
        criteria = new double[] { 0, 0, 0, 0, 0 };

    }

    private double GetPricing(double _playerPrice)
    {

        if (_playerPrice > OVERPRICED)
        {

            double range = _playerPrice - OVERPRICED;
            double percentage = range * 0.1;
            double pricing = 1 - percentage;
            return pricing;

        }

        return 1;

    }

    private double GetTemperature(double _temperature)
    {

        if (_temperature >= TEMPERATURE[0, 0] && _temperature <= TEMPERATURE[0, 1])

            return -0.1;

        else if (_temperature >= TEMPERATURE[1, 0] && _temperature <= TEMPERATURE[1, 1])

            return -0.05;

        else if (_temperature >= TEMPERATURE[3, 0] && _temperature <= TEMPERATURE[3, 1])

            return 0.05;

        else if (_temperature >= TEMPERATURE[4, 0] && _temperature <= TEMPERATURE[4, 1])

            return 0.1;

        return 0;

    }

    private int GetOverallCustomer()
    {

        double x = population * popularity;
        double y = x * playerSatisfaction[playerLocation];
        double z = y * price;

        overPricedCustomers = Convert.ToInt32(y - z);

        double a = z + playerConstant;
        double b = population * temperature;
        int c = Convert.ToInt32(a + b);
        int d = c <= playerSupplies[4]
            ? c
            : playerSupplies[4];

        return d;

    }

    private void GetPhase()
    {

        for (int phase = 0; phase < 64; phase++)
        {

            if (playerSupplies[0] >= playerRecipe[0]
                && playerSupplies[1] >= playerRecipe[1]
                && playerSupplies[2] >= playerRecipe[2]
                && playerSupplies[3] >= playerRecipe[3]
                && pitcher == 0)
            {

                if (phase % 2 == 0)
                {

                    playerSupplies[0] -= playerRecipe[0];
                    playerSupplies[1] -= playerRecipe[1];
                    playerSupplies[2] -= playerRecipe[2];
                    playerSupplies[3] -= playerRecipe[3];

                    pitcher = playerCupsPerPitcher;

                    if (currentCustomer >= pitcher)
                    {

                        currentCustomer -= pitcher;
                        cupsSold += pitcher;
                        pitcher = 0;

                    }
                    else
                    {

                        pitcher -= currentCustomer;
                        cupsSold += currentCustomer;

                    }

                }

                else

                    currentCustomer = (int)(currentCustomer * waitingTime);

            }

        }

    }

    private void GetPerformance()
    {

        satisfaction = GetSatisfaction();

        satisfiedCustomers = Convert.ToInt32(cupsSold * satisfaction);
        unsatisfiedCustomers = cupsSold - satisfiedCustomers;
        impatientCustomers = overAllCustomer - cupsSold;

    }

    private double GetSatisfaction()
    {

        if (playerRecipe[3] != playerTargetCriteria[3]
            && playerRecipe[3] % MINIMUM_CUPS == 0)
        {

            if (playerRecipe[3] == 0)
            {

                playerTargetCriteria[0] = TARGET_CRITERIA[0];
                playerTargetCriteria[1] = TARGET_CRITERIA[1];
                playerTargetCriteria[2] = TARGET_CRITERIA[2];
                playerTargetCriteria[3] = TARGET_CRITERIA[3];

            }
            else
            {

                playerTargetCriteria[0] = TARGET_CRITERIA[0] * (playerRecipe[3] / MINIMUM_CUPS);
                playerTargetCriteria[1] = TARGET_CRITERIA[1] * (playerRecipe[3] / MINIMUM_CUPS);
                playerTargetCriteria[2] = TARGET_CRITERIA[2] * (playerRecipe[3] / MINIMUM_CUPS);
                playerTargetCriteria[3] = TARGET_CRITERIA[3] * (playerRecipe[3] / MINIMUM_CUPS);

            }

        }

        criteria[0] = GetRecipeSatisfaction(0);
        criteria[1] = GetRecipeSatisfaction(1);
        criteria[2] = GetRecipeSatisfaction(2);
        criteria[3] = GetRecipeSatisfaction(3);

        criteria[4] = playerPrice <= TARGET_CRITERIA[4]
            ? 0.2
            : 0.2 - (playerPrice - TARGET_CRITERIA[4]) / 100;

        criteria[0] = GetCriteria(0);
        criteria[1] = GetCriteria(1);
        criteria[2] = GetCriteria(2);
        criteria[3] = GetCriteria(3);
        criteria[4] = GetCriteria(4);

        double overAllCriteria = criteria[0] + criteria[1] + criteria[2] + criteria[3] + criteria[4];

        FindObjectOfType<Player>().PlayerCustomerSatisfaction = overAllCriteria;

        double satisfaction = (playerSatisfaction[playerLocation] + overAllCriteria) / 2;

        return satisfaction;

    }

    private double GetRecipeSatisfaction(int _recipe) =>
        playerRecipe[_recipe] == playerTargetCriteria[_recipe]
            ? 0.2
            : 0.2 - (Math.Abs(playerRecipe[_recipe] - playerTargetCriteria[_recipe]) / 100.0);

    private async void Done()
    {

        bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;
        playerSupplies[4] -= cupsSold;
        
        FindObjectOfType<Player>().PlayerIceCubesMelted = playerSupplies[3];
        FindObjectOfType<Player>().PlayerConstant = playerConstant;
        FindObjectOfType<Player>().PlayerTemperature = UnityEngine.Random.Range(20.0f, 45.0f);
        FindObjectOfType<Player>().PlayerPopularity[playerLocation] = popularity;
        FindObjectOfType<Player>().PlayerSatisfaction[playerLocation] = satisfaction;
        FindObjectOfType<Player>().PlayerSupplies = playerSupplies;
        FindObjectOfType<Player>().PlayerDate = GetDate(playerDate);
        FindObjectOfType<Player>().PlayerTargetCriteria = playerTargetCriteria;
        GetResults();
        double earnings = FindObjectOfType<Player>().PlayerEarnings[0];
        FindObjectOfType<Player>().PlayerCapital +=
            earnings > 0
            ? earnings
            : 0;
        FindObjectOfType<Player>().PlayerReputation = GetReputation();
        FindObjectOfType<Player>().PlayerCupsSold = cupsSold;
        FindObjectOfType<Player>().PlayerSatisfiedCustomers = satisfiedCustomers;
        FindObjectOfType<Player>().PlayerUnsatisfiedCustomers = unsatisfiedCustomers;
        FindObjectOfType<Player>().PlayerOverPricedCustomers = overPricedCustomers;
        FindObjectOfType<Player>().PlayerImpatientCustomers = impatientCustomers;

        playerSupplies[3] = 0;

        FindObjectOfType<Player>().OnAutoSave(isConnected);

        await Task.Delay(5000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    private int[] GetDate(int[] _playerDate)
    {

        _playerDate[2]++;

        if (_playerDate[2] > 31)
        {

            _playerDate[2] = 1;
            _playerDate[1]++;

        }
        if (_playerDate[1] > 12)
        {

            _playerDate[1] = 1;
            _playerDate[0]++;

        }

        return _playerDate;

    }

    private void GetResults()
    {

        // CURRENT DAY
        FindObjectOfType<Player>().PlayerRevenue[0] = playerPrice * cupsSold;
        FindObjectOfType<Player>().PlayerStockUsed[0] = playerCostPerCup * cupsSold;
        FindObjectOfType<Player>().PlayerStockLost[0] = playerCostPerCup * pitcher;
        FindObjectOfType<Player>().PlayerGrossProfit[0] = FindObjectOfType<Player>().PlayerRevenue[0] - (FindObjectOfType<Player>().PlayerStockUsed[0] + FindObjectOfType<Player>().PlayerStockLost[0]);
        FindObjectOfType<Player>().PlayerGrossMargin[0] = FindObjectOfType<Player>().PlayerRevenue[0] / FindObjectOfType<Player>().PlayerGrossProfit[0];
        FindObjectOfType<Player>().PlayerRent[0] = GetRent();
        FindObjectOfType<Player>().PlayerMarketing[0] = population * ADVERTISEMENT[playerAdvertisement, 0];
        FindObjectOfType<Player>().PlayerExpenses[0] = FindObjectOfType<Player>().PlayerRent[0] + FindObjectOfType<Player>().PlayerMarketing[0];
        FindObjectOfType<Player>().PlayerEarnings[0] = FindObjectOfType<Player>().PlayerGrossProfit[0] - FindObjectOfType<Player>().PlayerExpenses[0];

        if (FindObjectOfType<Player>().PlayerDate[2] != 1)
        {

            // CURRENT DATE
            FindObjectOfType<Player>().PlayerRevenue[1] += FindObjectOfType<Player>().PlayerRevenue[0];
            FindObjectOfType<Player>().PlayerStockUsed[1] += FindObjectOfType<Player>().PlayerStockUsed[0];
            FindObjectOfType<Player>().PlayerStockLost[1] += FindObjectOfType<Player>().PlayerStockLost[0];
            FindObjectOfType<Player>().PlayerGrossProfit[1] += FindObjectOfType<Player>().PlayerGrossProfit[0];
            FindObjectOfType<Player>().PlayerGrossMargin[1] += FindObjectOfType<Player>().PlayerGrossMargin[0];
            FindObjectOfType<Player>().PlayerRent[1] += FindObjectOfType<Player>().PlayerRent[0];
            FindObjectOfType<Player>().PlayerMarketing[1] += FindObjectOfType<Player>().PlayerMarketing[0];
            FindObjectOfType<Player>().PlayerExpenses[1] += FindObjectOfType<Player>().PlayerExpenses[0];
            FindObjectOfType<Player>().PlayerEarnings[1] += FindObjectOfType<Player>().PlayerEarnings[0];

        }
        else
        {

            // CURRENT DATE
            FindObjectOfType<Player>().PlayerRevenue[1] = FindObjectOfType<Player>().PlayerRevenue[0];
            FindObjectOfType<Player>().PlayerStockUsed[1] = FindObjectOfType<Player>().PlayerStockUsed[0];
            FindObjectOfType<Player>().PlayerStockLost[1] = FindObjectOfType<Player>().PlayerStockLost[0];
            FindObjectOfType<Player>().PlayerGrossProfit[1] = FindObjectOfType<Player>().PlayerGrossProfit[0];
            FindObjectOfType<Player>().PlayerGrossMargin[1] = FindObjectOfType<Player>().PlayerGrossMargin[0];
            FindObjectOfType<Player>().PlayerRent[1] = FindObjectOfType<Player>().PlayerRent[0];
            FindObjectOfType<Player>().PlayerMarketing[1] = FindObjectOfType<Player>().PlayerMarketing[0];
            FindObjectOfType<Player>().PlayerExpenses[1] = FindObjectOfType<Player>().PlayerExpenses[0];
            FindObjectOfType<Player>().PlayerEarnings[1] = FindObjectOfType<Player>().PlayerEarnings[0];

        }

        if (FindObjectOfType<Player>().PlayerDate[2] == 1)
        {

            // LAST DATE
            FindObjectOfType<Player>().PlayerRevenue[2] = FindObjectOfType<Player>().PlayerRevenue[1];
            FindObjectOfType<Player>().PlayerStockUsed[2] = FindObjectOfType<Player>().PlayerStockUsed[1];
            FindObjectOfType<Player>().PlayerStockLost[2] = FindObjectOfType<Player>().PlayerStockLost[1];
            FindObjectOfType<Player>().PlayerGrossProfit[2] = FindObjectOfType<Player>().PlayerGrossProfit[1];
            FindObjectOfType<Player>().PlayerGrossMargin[2] = FindObjectOfType<Player>().PlayerGrossMargin[1];
            FindObjectOfType<Player>().PlayerRent[2] = FindObjectOfType<Player>().PlayerRent[1];
            FindObjectOfType<Player>().PlayerMarketing[2] = FindObjectOfType<Player>().PlayerMarketing[1];
            FindObjectOfType<Player>().PlayerExpenses[2] = FindObjectOfType<Player>().PlayerExpenses[1];
            FindObjectOfType<Player>().PlayerEarnings[2] = FindObjectOfType<Player>().PlayerEarnings[1];

            // BEST DATE
            FindObjectOfType<Player>().PlayerRevenue[3] =
                FindObjectOfType<Player>().PlayerRevenue[2] > FindObjectOfType<Player>().PlayerRevenue[3]
                ? FindObjectOfType<Player>().PlayerRevenue[2]
                : FindObjectOfType<Player>().PlayerRevenue[3];

            FindObjectOfType<Player>().PlayerStockUsed[3] =
                FindObjectOfType<Player>().PlayerStockUsed[2] > FindObjectOfType<Player>().PlayerStockUsed[3]
                ? FindObjectOfType<Player>().PlayerStockUsed[2]
                : FindObjectOfType<Player>().PlayerStockUsed[3];

            FindObjectOfType<Player>().PlayerStockLost[3] =
                FindObjectOfType<Player>().PlayerStockLost[2] > FindObjectOfType<Player>().PlayerStockLost[3]
                ? FindObjectOfType<Player>().PlayerStockLost[2]
                : FindObjectOfType<Player>().PlayerStockLost[3];

            FindObjectOfType<Player>().PlayerGrossProfit[3] =
                FindObjectOfType<Player>().PlayerGrossProfit[2] > FindObjectOfType<Player>().PlayerGrossProfit[3]
                ? FindObjectOfType<Player>().PlayerGrossProfit[2]
                : FindObjectOfType<Player>().PlayerGrossProfit[3];

            FindObjectOfType<Player>().PlayerGrossMargin[3] =
                FindObjectOfType<Player>().PlayerGrossMargin[2] > FindObjectOfType<Player>().PlayerGrossMargin[3]
                ? FindObjectOfType<Player>().PlayerGrossMargin[2]
                : FindObjectOfType<Player>().PlayerGrossMargin[3];

            FindObjectOfType<Player>().PlayerRent[3] =
                FindObjectOfType<Player>().PlayerRent[2] > FindObjectOfType<Player>().PlayerRent[3]
                ? FindObjectOfType<Player>().PlayerRent[2]
                : FindObjectOfType<Player>().PlayerRent[3];

            FindObjectOfType<Player>().PlayerMarketing[3] =
                FindObjectOfType<Player>().PlayerMarketing[2] > FindObjectOfType<Player>().PlayerMarketing[3]
                ? FindObjectOfType<Player>().PlayerMarketing[2]
                : FindObjectOfType<Player>().PlayerMarketing[3];

            FindObjectOfType<Player>().PlayerExpenses[3] =
                FindObjectOfType<Player>().PlayerExpenses[2] > FindObjectOfType<Player>().PlayerExpenses[3]
                ? FindObjectOfType<Player>().PlayerExpenses[2]
                : FindObjectOfType<Player>().PlayerExpenses[3];

            FindObjectOfType<Player>().PlayerEarnings[3] =
                FindObjectOfType<Player>().PlayerEarnings[2] > FindObjectOfType<Player>().PlayerEarnings[3]
                ? FindObjectOfType<Player>().PlayerEarnings[2]
                : FindObjectOfType<Player>().PlayerEarnings[3];

        }

    }

    private double GetRent()
    {
        double x = LOCATION[playerLocation, 1];
        double y = 0;

        foreach (int i in playerStaffs)

            y += STAFF[i, 0];

        double z = x + y;

        return z;

    }

    private double GetCriteria(int _recipe) =>
        criteria[_recipe] >= 0 && criteria[_recipe] <= 0.2 ? criteria[_recipe] : 0;

    private double GetReputation()
    {

        double overAllSatisfaction = 0;

        foreach (double i in playerSatisfaction)

            overAllSatisfaction += i;

        double reputation = overAllSatisfaction / playerSatisfaction.Length;

        return reputation;

    }

}
