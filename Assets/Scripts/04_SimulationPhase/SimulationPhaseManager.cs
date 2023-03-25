using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationPhaseManager : MonoBehaviour
{

    [SerializeField]
    private Button skipUIButton;

    [SerializeField]
    private Image locationHUD;

    [SerializeField]
    private Sprite[] locationSprites;

    private double playerCostPerCup;
    private double playerPopularity;
    private double playerPrice;
    private double playerTemperature;
    private double[] playerSatisfaction;
    private int playerAdvertisement;
    private int playerConstant;
    private int playerCupsPerPitcher;
    private int playerDaysWithoutAdvertisement;
    private int playerLocation;
    private int[] playerDate;
    private int[] playerRecipe;
    private int[] playerSupplies;
    private int[] playerTargetCriteria;
    private int[] playerUpgrade;
    private List<int> playerStaffs;

    private bool canSkip;
    private double popularity;
    private double priceSatisfaction;
    private double satisfaction;
    private double servingTime;
    private double temperature;
    private double[] criteria;
    private int cupsSold;
    private int currentCustomer;
    private int impatientCustomers;
    private int overAllCustomer;
    private int overPricedCustomers;
    private int pitcher;
    private int satisfiedCustomers;
    private int unsatisfiedCustomers;
    private int population;

    void Start()
    {

        Init();

        playerAdvertisement = FindObjectOfType<PLAYER>().PlayerAdvertisement;
        playerConstant = FindObjectOfType<PLAYER>().PlayerConstant;
        playerCostPerCup = FindObjectOfType<PLAYER>().PlayerCostPerCup;
        playerCupsPerPitcher = FindObjectOfType<PLAYER>().PlayerCupsPerPitcher;
        playerDate = FindObjectOfType<PLAYER>().PlayerDate;
        playerDaysWithoutAdvertisement = FindObjectOfType<PLAYER>().PlayerDaysWithoutAdvertisement;
        playerLocation = FindObjectOfType<PLAYER>().PlayerLocation;
        playerPopularity = FindObjectOfType<PLAYER>().PlayerPopularity[playerLocation];
        playerPrice = FindObjectOfType<PLAYER>().PlayerPrice;
        playerRecipe = FindObjectOfType<PLAYER>().PlayerRecipe;
        playerSatisfaction = FindObjectOfType<PLAYER>().PlayerSatisfaction;
        playerStaffs = FindObjectOfType<PLAYER>().PlayerStaffs;
        playerSupplies = FindObjectOfType<PLAYER>().PlayerSupplies;
        playerTargetCriteria = FindObjectOfType<PLAYER>().PlayerTargetCriteria;
        playerTemperature = FindObjectOfType<PLAYER>().PlayerTemperature;
        playerUpgrade = FindObjectOfType<PLAYER>().PlayerUpgrade;

        locationHUD.sprite = locationSprites[playerLocation];

        LoadInitialPhase();
        LoadSimulationPhase();
        LoadFinalPhase();

    }

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnSkip"))

            OnSkip();

    }

    private void Init()
    {

        int countdown = 7;
        canSkip = false;
        pitcher = 0;
        cupsSold = 0;
        criteria = new double[] { 0, 0, 0, 0, 0 };
        StartCoroutine(SimulationToStart(countdown));

    }

    private void LoadInitialPhase()
    {

        population = (int)ENV.LOCATION[playerLocation, 0];

        playerPopularity +=
            playerAdvertisement > 0
            ? ENV.ADVERTISEMENT[playerAdvertisement, 1]
            : ENV.LOCATION[playerLocation, 2];

        popularity =
            playerPopularity >= 1
            ? 1
            : playerPopularity;

        priceSatisfaction = GetPriceSatisfaction(playerPrice);
        playerConstant += ENV.INCREMENT_POPULARITY_PER_DAY;
        temperature = GetTemperature(playerTemperature);
        servingTime = ENV.UPGRADE[0, playerUpgrade[0], 1] + GetReducedServingTime();
        overAllCustomer = GetOverallCustomer();
        currentCustomer = overAllCustomer;

        FindObjectOfType<PLAYER>().PlayerDaysWithoutAdvertisement = playerDaysWithoutAdvertisement;

        if (playerAdvertisement > 0)

            playerDaysWithoutAdvertisement = 0;

        else

            playerDaysWithoutAdvertisement++;

    }

    private double GetPriceSatisfaction(double _playerPrice)
    {

        if (_playerPrice > ENV.OVERPRICED)
        {

            double range = _playerPrice - ENV.OVERPRICED;
            double percentage = range * 0.1;
            double price = 1 - percentage;
            return price;

        }

        return 1;

    }

    private double GetTemperature(double _temperature)
    {

        if (_temperature >= ENV.TEMPERATURE[0, 0]
            && _temperature <= ENV.TEMPERATURE[0, 1])

            return -0.1;

        else if (_temperature >= ENV.TEMPERATURE[1, 0]
            && _temperature <= ENV.TEMPERATURE[1, 1])

            return -0.05;

        else if (_temperature >= ENV.TEMPERATURE[3, 0]
            && _temperature <= ENV.TEMPERATURE[3, 1])

            return 0.05;

        else if (_temperature >= ENV.TEMPERATURE[4, 0]
            && _temperature <= ENV.TEMPERATURE[4, 1])

            return 0.1;

        return 0;

    }

    private int GetOverallCustomer()
    {

        double x = population * popularity;
        double y = x * playerSatisfaction[playerLocation];
        double z = y * priceSatisfaction;

        overPricedCustomers = (int)y - (int)z;

        double a = z + playerConstant;
        double b = population * temperature;
        int c = (int)a + (int)b;
        int overAllCustomer =
            c <= playerSupplies[4]
            ? c
            : playerSupplies[4];

        return overAllCustomer;

    }

    private void LoadSimulationPhase()
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

                    pitcher = playerCupsPerPitcher + GetAdditionalPitcher(playerCupsPerPitcher);

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

                    currentCustomer *= (int)servingTime;

            }

        }

    }

    private double GetSatisfaction()
    {

        if (playerRecipe[3] != playerTargetCriteria[3]
            && playerRecipe[3] % ENV.MINIMUM_CUPS == 0)

            for (int recipe = 0; recipe < 4; recipe++)

                playerTargetCriteria[recipe] =
                    playerRecipe[3] == 0
                    ? ENV.TARGET_CRITERIA[recipe]
                    : ENV.TARGET_CRITERIA[recipe] * (playerRecipe[3] / ENV.MINIMUM_CUPS);

        for (int recipe = 0; recipe < 4; recipe++)

            criteria[recipe] = GetRecipeSatisfaction(recipe);

        criteria[4] = playerPrice <= ENV.TARGET_CRITERIA[4]
            ? 0.2
            : 0.2 - (playerPrice - ENV.TARGET_CRITERIA[4]) / 100;

        double overAllCriteria = 0;
        for (int recipeAndPrice = 0; recipeAndPrice < 5; recipeAndPrice++)
        {

            criteria[recipeAndPrice] = GetCriteria(recipeAndPrice);
            overAllCriteria += criteria[recipeAndPrice];

        }

        FindObjectOfType<PLAYER>().PlayerCustomerSatisfaction = overAllCriteria;

        double satisfaction = (playerSatisfaction[playerLocation] + overAllCriteria) / 2;

        return satisfaction;

    }

    private double GetRecipeSatisfaction(int _recipe) =>
        playerRecipe[_recipe] == playerTargetCriteria[_recipe]
            ? 0.2
            : 0.2 - (Math.Abs(playerRecipe[_recipe] - playerTargetCriteria[_recipe]) / 100.0);

    private void LoadFinalPhase()
    {

        satisfaction = GetSatisfaction();

        satisfiedCustomers = cupsSold * (int)satisfaction;
        unsatisfiedCustomers = cupsSold - satisfiedCustomers;
        impatientCustomers = overAllCustomer - cupsSold;
        playerSupplies[4] -= cupsSold;
        double reputation = GetReputation();
        PlayerPrefs.SetFloat("player_reputation", (float)reputation);

        FindObjectOfType<PLAYER>().PlayerConstant = playerConstant;
        FindObjectOfType<PLAYER>().PlayerCupsSold = cupsSold;
        FindObjectOfType<PLAYER>().PlayerDate = GetDate(playerDate);
        FindObjectOfType<PLAYER>().PlayerFeedback = GetFeedback();
        FindObjectOfType<PLAYER>().PlayerImpatientCustomers = impatientCustomers;
        FindObjectOfType<PLAYER>().PlayerOverPricedCustomers = overPricedCustomers;
        FindObjectOfType<PLAYER>().PlayerPopularity[playerLocation] = popularity;
        FindObjectOfType<PLAYER>().PlayerReputation = reputation;
        FindObjectOfType<PLAYER>().PlayerSatisfaction[playerLocation] = satisfaction;
        FindObjectOfType<PLAYER>().PlayerSatisfiedCustomers = satisfiedCustomers;
        FindObjectOfType<PLAYER>().PlayerSupplies = playerSupplies;
        FindObjectOfType<PLAYER>().PlayerTargetCriteria = playerTargetCriteria;
        FindObjectOfType<PLAYER>().PlayerTemperature = UnityEngine.Random.Range(20.0f, 45.0f);
        FindObjectOfType<PLAYER>().PlayerUnsatisfiedCustomers = unsatisfiedCustomers;
        FindObjectOfType<PLAYER>().PlayerIceCubesMelted = playerSupplies[3];

        LoadResults();
        FindObjectOfType<PLAYER>().PlayerProfitAndLoss += FindObjectOfType<PLAYER>().PlayerEarnings[0];

        double earnings = FindObjectOfType<PLAYER>().PlayerEarnings[0];

        FindObjectOfType<PLAYER>().PlayerCapital +=
            earnings > 0
            ? earnings
            : 0;

        playerSupplies[3] = 0;
        FindObjectOfType<PLAYER>().OnAutoSave();

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

    private void LoadResults()
    {

        double revenue = playerPrice * cupsSold;
        double stockUsed = playerCostPerCup * cupsSold;
        double stockLost = (playerCostPerCup * pitcher) + (playerSupplies[3] * ENV.AVERAGE_SUPPLIES_COST[3]);
        double grossProfit = revenue - (stockUsed + stockLost);
        double grossMargin = grossProfit / revenue;
        double rent = GetRent();
        double marketing = population * ENV.ADVERTISEMENT[playerAdvertisement, 0];
        double expenses = rent + marketing;
        double earnings = grossProfit - expenses;

        // CURRENT DAY
        FindObjectOfType<PLAYER>().PlayerEarnings[0] = earnings;
        FindObjectOfType<PLAYER>().PlayerExpenses[0] = expenses;
        FindObjectOfType<PLAYER>().PlayerGrossMargin[0] = grossMargin;
        FindObjectOfType<PLAYER>().PlayerGrossProfit[0] = grossProfit;
        FindObjectOfType<PLAYER>().PlayerMarketing[0] = marketing;
        FindObjectOfType<PLAYER>().PlayerRent[0] = rent;
        FindObjectOfType<PLAYER>().PlayerStockLost[0] = stockLost;
        FindObjectOfType<PLAYER>().PlayerStockUsed[0] = stockUsed;
        FindObjectOfType<PLAYER>().PlayerRevenue[0] = revenue;

        if (FindObjectOfType<PLAYER>().PlayerDate[2] == 1)
        {

            // LAST DATE
            GetResults(2, 1);

            // CURRENT DATE
            GetResults(1, 0);

            // BEST DATE
            FindObjectOfType<PLAYER>().PlayerEarnings[3] =
                FindObjectOfType<PLAYER>().PlayerEarnings[2] > FindObjectOfType<PLAYER>().PlayerEarnings[3]
                ? FindObjectOfType<PLAYER>().PlayerEarnings[2]
                : FindObjectOfType<PLAYER>().PlayerEarnings[3];

            FindObjectOfType<PLAYER>().PlayerExpenses[3] =
                FindObjectOfType<PLAYER>().PlayerExpenses[2] > FindObjectOfType<PLAYER>().PlayerExpenses[3]
                ? FindObjectOfType<PLAYER>().PlayerExpenses[2]
                : FindObjectOfType<PLAYER>().PlayerExpenses[3];

            FindObjectOfType<PLAYER>().PlayerGrossMargin[3] =
                FindObjectOfType<PLAYER>().PlayerGrossMargin[2] > FindObjectOfType<PLAYER>().PlayerGrossMargin[3]
                ? FindObjectOfType<PLAYER>().PlayerGrossMargin[2]
                : FindObjectOfType<PLAYER>().PlayerGrossMargin[3];

            FindObjectOfType<PLAYER>().PlayerGrossProfit[3] =
                FindObjectOfType<PLAYER>().PlayerGrossProfit[2] > FindObjectOfType<PLAYER>().PlayerGrossProfit[3]
                ? FindObjectOfType<PLAYER>().PlayerGrossProfit[2]
                : FindObjectOfType<PLAYER>().PlayerGrossProfit[3];

            FindObjectOfType<PLAYER>().PlayerMarketing[3] =
                FindObjectOfType<PLAYER>().PlayerMarketing[2] > FindObjectOfType<PLAYER>().PlayerMarketing[3]
                ? FindObjectOfType<PLAYER>().PlayerMarketing[2]
                : FindObjectOfType<PLAYER>().PlayerMarketing[3];

            FindObjectOfType<PLAYER>().PlayerRent[3] =
                FindObjectOfType<PLAYER>().PlayerRent[2] > FindObjectOfType<PLAYER>().PlayerRent[3]
                ? FindObjectOfType<PLAYER>().PlayerRent[2]
                : FindObjectOfType<PLAYER>().PlayerRent[3];

            FindObjectOfType<PLAYER>().PlayerStockLost[3] =
                FindObjectOfType<PLAYER>().PlayerStockLost[2] > FindObjectOfType<PLAYER>().PlayerStockLost[3]
                ? FindObjectOfType<PLAYER>().PlayerStockLost[2]
                : FindObjectOfType<PLAYER>().PlayerStockLost[3];

            FindObjectOfType<PLAYER>().PlayerStockUsed[3] =
                FindObjectOfType<PLAYER>().PlayerStockUsed[2] > FindObjectOfType<PLAYER>().PlayerStockUsed[3]
                ? FindObjectOfType<PLAYER>().PlayerStockUsed[2]
                : FindObjectOfType<PLAYER>().PlayerStockUsed[3];

            FindObjectOfType<PLAYER>().PlayerRevenue[3] =
                FindObjectOfType<PLAYER>().PlayerRevenue[2] > FindObjectOfType<PLAYER>().PlayerRevenue[3]
                ? FindObjectOfType<PLAYER>().PlayerRevenue[2]
                : FindObjectOfType<PLAYER>().PlayerRevenue[3];

        }
        else
        {

            // CURRENT DATE
            FindObjectOfType<PLAYER>().PlayerEarnings[1] += FindObjectOfType<PLAYER>().PlayerEarnings[0];
            FindObjectOfType<PLAYER>().PlayerExpenses[1] += FindObjectOfType<PLAYER>().PlayerExpenses[0];
            FindObjectOfType<PLAYER>().PlayerGrossMargin[1] = (FindObjectOfType<PLAYER>().PlayerGrossMargin[1] + FindObjectOfType<PLAYER>().PlayerGrossMargin[0]) / 2;
            FindObjectOfType<PLAYER>().PlayerGrossProfit[1] += FindObjectOfType<PLAYER>().PlayerGrossProfit[0];
            FindObjectOfType<PLAYER>().PlayerMarketing[1] += FindObjectOfType<PLAYER>().PlayerMarketing[0];
            FindObjectOfType<PLAYER>().PlayerRent[1] += FindObjectOfType<PLAYER>().PlayerRent[0];
            FindObjectOfType<PLAYER>().PlayerStockLost[1] += FindObjectOfType<PLAYER>().PlayerStockLost[0];
            FindObjectOfType<PLAYER>().PlayerStockUsed[1] += FindObjectOfType<PLAYER>().PlayerStockUsed[0];
            FindObjectOfType<PLAYER>().PlayerRevenue[1] += FindObjectOfType<PLAYER>().PlayerRevenue[0];

        }

    }

    private double GetRent()
    {

        double rent = ENV.LOCATION[playerLocation, 1];

        foreach (int staff in playerStaffs)

            rent += ENV.STAFF[staff, 0];

        return rent;

    }

    private double GetCriteria(int _recipe) =>
        criteria[_recipe] >= 0
        && criteria[_recipe] <= 0.2
        ? criteria[_recipe]
        : 0;

    private double GetReputation()
    {

        double reputation = 0;

        foreach (double satisfaction in playerSatisfaction)

            reputation += satisfaction;

        reputation /= playerSatisfaction.Length;

        return reputation;

    }

    private int GetFeedback()
    {

        if (playerSupplies[0] < playerRecipe[0]
                || playerSupplies[1] < playerRecipe[1]
                || playerSupplies[2] < playerRecipe[2]
                || playerSupplies[3] < playerRecipe[3]
                || playerSupplies[4] < overAllCustomer)

            return 1;

        else if (unsatisfiedCustomers > impatientCustomers
            && unsatisfiedCustomers > overPricedCustomers)

            return 2;

        else if (overPricedCustomers > unsatisfiedCustomers
            && overPricedCustomers > impatientCustomers)

            return 3;

        else if (impatientCustomers > unsatisfiedCustomers
            && impatientCustomers > overPricedCustomers)

            return 4;

        else if (playerDaysWithoutAdvertisement >= 7)

            return 5;

        return 0;

    }

    private double GetReducedServingTime()
    {

        double reducedServingTime = 0;

        foreach (int staff in playerStaffs)

            reducedServingTime += ENV.STAFF[staff, 1];

        return reducedServingTime;

    }

    private int GetAdditionalPitcher(int _pitcher)
    {

        int pitcher = _pitcher * playerStaffs.Count;
        return pitcher;

    }

    private void GetResults(int _field, int _value)
    {

        FindObjectOfType<PLAYER>().PlayerEarnings[_field] = FindObjectOfType<PLAYER>().PlayerEarnings[_value];
        FindObjectOfType<PLAYER>().PlayerExpenses[_field] = FindObjectOfType<PLAYER>().PlayerExpenses[_value];
        FindObjectOfType<PLAYER>().PlayerGrossMargin[_field] = FindObjectOfType<PLAYER>().PlayerGrossMargin[_value];
        FindObjectOfType<PLAYER>().PlayerGrossProfit[_field] = FindObjectOfType<PLAYER>().PlayerGrossProfit[_value];
        FindObjectOfType<PLAYER>().PlayerMarketing[_field] = FindObjectOfType<PLAYER>().PlayerMarketing[_value];
        FindObjectOfType<PLAYER>().PlayerRent[_field] = FindObjectOfType<PLAYER>().PlayerRent[_value];
        FindObjectOfType<PLAYER>().PlayerStockLost[_field] = FindObjectOfType<PLAYER>().PlayerStockLost[_value];
        FindObjectOfType<PLAYER>().PlayerStockUsed[_field] = FindObjectOfType<PLAYER>().PlayerStockUsed[_value];
        FindObjectOfType<PLAYER>().PlayerRevenue[_field] = FindObjectOfType<PLAYER>().PlayerRevenue[_value];

    }

    IEnumerator SimulationToStart(int _countdown)
    {

        int count = 0;

        while (_countdown > 0)
        {

            skipUIButton.interactable = canSkip;

            if (count == 3
                && canSkip == false)

                canSkip = true;

            else if (count < 4)

                count++;

            yield return new WaitForSeconds(1f);

            _countdown--;

        }

        OnSkip();

    }

    public void OnSkip()
    {

        if (canSkip == true)

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

}
