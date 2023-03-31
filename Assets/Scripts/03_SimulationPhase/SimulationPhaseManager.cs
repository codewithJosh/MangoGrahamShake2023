using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationPhaseManager : MonoBehaviour
{

    #region DECLARATION

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
    private double partialCustomer;

    #endregion

    #region START_METHOD

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
        int quoteState = UnityEngine.Random.Range(0, ENV.QUOTES.Length + 1);
        GameManager.OnNowInforming(ENV.QUOTES[quoteState]);

        LoadInitialPhase();
        LoadSimulationPhase();
        LoadFinalPhase();

    }

    #endregion

    #region UPDATE_METHOD

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnSkip"))

            OnSkip();

    }

    #endregion

    #region INIT_METHOD

    private void Init()
    {

        int countdown = 7;
        canSkip = false;
        pitcher = 0;
        cupsSold = 0;
        criteria = new double[] { 0, 0, 0, 0 };
        StartCoroutine(SimulationToStart(countdown));

    }

    #endregion

    #region LOAD_INITIAL_PHASE_METHOD

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

    #endregion

    #region GET_OVER_PRICED_CUSTOMERS

    private int GetOverPricedCustomers(List<double> _customerBudgets)
    {

        int overPricedCustomers = 0;
        foreach (double customerBudget in _customerBudgets)

            if (playerPrice > customerBudget)

                overPricedCustomers++;

        return overPricedCustomers;

    }

    #endregion

    #region GET_TEMPERATURE_METHOD

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

    #endregion

    #region GET_OVERALL_CUSTOMER_METHOD

    private int GetOverallCustomer()
    {

        double x = population * popularity;
        partialCustomer = x * playerSatisfaction[playerLocation];
        List<int> locationClasses = GetLocationClasses(partialCustomer);
        List<double> customerBudgets = GetCustomerBudgets(locationClasses);
        overPricedCustomers = GetOverPricedCustomers(customerBudgets);
        double z = partialCustomer - overPricedCustomers;
        double a = z + playerConstant;
        double b = population * temperature;
        int c = (int)a + (int)b;
        int overAllCustomer =
            c <= playerSupplies[4]
            ? c
            : playerSupplies[4];

        return overAllCustomer;

    }

    #endregion

    #region LOAD_SIMULATION_PHASE_METHOD

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

    #endregion

    #region GET_SATISFACTION_METHOD

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

        double overAllCriteria = 0;
        for (int recipe = 0; recipe < 4; recipe++)
        {

            criteria[recipe] = GetCriteria(recipe);
            overAllCriteria += criteria[recipe];

        }

        FindObjectOfType<PLAYER>().PlayerCustomerSatisfaction = overAllCriteria;

        double satisfaction = (playerSatisfaction[playerLocation] + overAllCriteria) / 2;

        return satisfaction;

    }

    #endregion

    #region GET_RECIPE_SATISFACTION_METHOD

    private double GetRecipeSatisfaction(int _recipe) =>
        playerRecipe[_recipe] == playerTargetCriteria[_recipe]
            ? 0.25
            : 0.25 - (Math.Abs(playerRecipe[_recipe] - playerTargetCriteria[_recipe]) / 100.0);

    #endregion

    #region LOAD_FINAL_PHASE_METHOD

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
        FindObjectOfType<PLAYER>().PlayerReputation = (FindObjectOfType<PLAYER>().PlayerReputation + reputation) / 2;
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

    #endregion

    #region GET_DATE_METHOD

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

    #endregion

    #region LOAD_RESULTS_METHOD

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

    #endregion

    #region GET_RENT_METHOD

    private double GetRent()
    {

        double rent = ENV.LOCATION[playerLocation, 1];

        foreach (int staff in playerStaffs)

            rent += ENV.STAFF[staff, 0];

        return rent;

    }

    #endregion

    #region GET_CRITERIA_METHOD

    private double GetCriteria(int _recipe) =>
        criteria[_recipe] >= 0
        && criteria[_recipe] <= 0.25
        ? criteria[_recipe]
        : 0;

    #endregion

    #region GET_REPUTATION_METHOD

    private double GetReputation()
    {

        int overAllUnsatisfiedCustomers = impatientCustomers + unsatisfiedCustomers + overPricedCustomers;
        double reputation = 1 - (overAllUnsatisfiedCustomers / partialCustomer);

        return reputation;

    }

    #endregion

    #region GET_FEEDBACK_METHOD

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

    #endregion

    #region GET_REDUCED_SERVING_TIME_METHOD

    private double GetReducedServingTime()
    {

        double reducedServingTime = 0;

        foreach (int staff in playerStaffs)

            reducedServingTime += ENV.STAFF[staff, 1];

        return reducedServingTime;

    }

    #endregion

    #region GET_ADDITIONAL_PITCHER_METHOD

    private int GetAdditionalPitcher(int _pitcher)
    {

        int pitcher = _pitcher * playerStaffs.Count;
        return pitcher;

    }

    #endregion

    #region GET_RESULTS_METHOD

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

    #endregion

    #region SIMULATION_TO_START_METHOD

    private IEnumerator SimulationToStart(int _countdown)
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

    #endregion

    #region ON_SKIP_METHOD

    public void OnSkip()
    {

        if (canSkip == true)

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    #endregion

    #region GET_LOCATION_CLASSES_METHOD

    private List<int> GetLocationClasses(double _y)
    {

        List<int> locationClasses = new();
        for (int i = 0; i < ENV.LOCATION_CLASSES.GetUpperBound(1) + 1; i++)
        {

            double x = ENV.LOCATION_CLASSES[playerLocation, i];
            int y = Convert.ToInt32(_y * x);
            locationClasses.Add(y);

        }

        return locationClasses;

    }

    #endregion

    #region GET_CUSTOMER_BUDGETS

    private List<double> GetCustomerBudgets(List<int> z)
    {

        List<double> customerBudgets = new();
        int classState = 0;
        foreach (int x in z)
        {

            for (int i = 0; i < x; i++)
            {

                double y = UnityEngine.Random.Range((float)ENV.CUSTOMER_BUDGET[classState, 0], (float)ENV.CUSTOMER_BUDGET[classState, 1]);
                customerBudgets.Add(y);

            }
            classState++;

        }

        return customerBudgets;

    }

    #endregion

}
