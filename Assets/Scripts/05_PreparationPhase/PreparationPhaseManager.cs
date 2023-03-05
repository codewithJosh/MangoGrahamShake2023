﻿using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreparationPhaseManager : MonoBehaviour
{

    [Header("BOTTOM NAVIGATION")]
    [SerializeField]
    private Image[] bottomNavigationUIButtons;

    [SerializeField]
    private Sprite[] bottomNavigationNormalUIButtons;

    [SerializeField]
    private Sprite[] bottomNavigationSelectedUIButtons;

    [SerializeField]
    private TextMeshProUGUI bottomNavigationUIText;

    [SerializeField]
    private ToggleGroup bottomNavigationUIPanel;

    [Header("SUPPLIES SECTION")]
    [SerializeField]
    private Button cancelUIButton;

    [SerializeField]
    private Button buyUIButton;

    [SerializeField]
    private Button[] supplyDecrementUIButtons;

    [SerializeField]
    private Button[] supplyIncrementUIButtons;

    [SerializeField]
    private Image[] supplyUIImages;

    [SerializeField]
    private Sprite[] supplySprites;

    [SerializeField]
    private TextMeshProUGUI[] supplyPriceUITexts;

    [SerializeField]
    private TextMeshProUGUI[] supplyQuantityUITexts;

    [SerializeField]
    private Toggle mangoUINavButton;

    [Header("RECIPE SECTION")]
    [SerializeField]
    private Button[] recipeDecrementUIButtons;

    [SerializeField]
    private Button[] recipeResetUIButtons;

    [SerializeField]
    private TextMeshProUGUI cupsPerPitcherUIText;

    [SerializeField]
    private TextMeshProUGUI[] recipeQuantityUITexts;

    [Header("MARKETING SECTION @PRICE")]
    [SerializeField]
    private Button priceDecrementUIButton;

    [SerializeField]
    private Button priceIncrementUIButton;

    [SerializeField]
    private Button priceResetUIButton;

    [SerializeField]
    private TextMeshProUGUI priceUIText;

    [SerializeField]
    private TextMeshProUGUI profitPerCupUIText;

    [Header("MARKETING SECTION @ADVERTISEMENT")]
    [SerializeField]
    private Button advertisementDecrementUIButton;

    [SerializeField]
    private Button advertisementIncrementUIButton;

    [SerializeField]
    private Button advertisementResetUIButton;

    [SerializeField]
    private TextMeshProUGUI advertisementUIText;

    [Header("RESULTS SECTION")]
    [SerializeField]
    private Toggle yesterdaysResultsUINavButton;

    [SerializeField]
    private ToggleGroup resultsNavigationUIPanel;

    [Header("RESULTS SECTION @YESTERDAYS PERFORMANCE AND SETTING")]
    [SerializeField]
    private TextMeshProUGUI performanceCupsSoldUIText;

    [SerializeField]
    private TextMeshProUGUI performanceProfitUIText;

    [SerializeField]
    private TextMeshProUGUI unsatisfiedCustomersUIText;

    [SerializeField]
    private TextMeshProUGUI satisfiedCustomersUIText;

    [SerializeField]
    private TextMeshProUGUI impatientCustomersUIText;

    [SerializeField]
    private TextMeshProUGUI overPricedCustomersUIText;

    [SerializeField]
    private TextMeshProUGUI settingRentUIText;

    [SerializeField]
    private TextMeshProUGUI settingAdvertisingUIText;

    [SerializeField]
    private TextMeshProUGUI settingPriceUIText;

    [SerializeField]
    private TextMeshProUGUI[] settingRecipeUIText;

    [Header("MAIN SECTION")]
    [SerializeField]
    private CanvasGroup settingsUIButton;

    [SerializeField]
    private Image temperatureUIImage;

    [SerializeField]
    private Image popularityUIImage;

    [SerializeField]
    private Image satisfactionUIImage;

    [SerializeField]
    private Image[] suppliesUIImages;

    [SerializeField]
    private Sprite[] temperatureSprites;

    [SerializeField]
    private TextMeshProUGUI[] dailyUITexts;

    [SerializeField]
    private TextMeshProUGUI[] suppliesUITexts;

    [SerializeField]
    private TextMeshProUGUI[] locationUITexts;

    private enum BottomNavigationStates { idle, results, location, upgrades, staff, marketing, recipe, supplies };
    private enum ResultsNavigationStates { yesterdaysPerformanceAndSettings, yesterdaysResults, charts, profitAndLoss, balanceSheet };

    private BottomNavigationStates bottomNavigationState;
    private BottomNavigationStates lastBottomNavigationState;
    private ResultsNavigationStates resultsNavigationState;

    private double[,] ADVERTISEMENT;
    private double DEFAULT_PRICE;
    private double MAXIMUM_PRICE;
    private double[,,] SUPPLIES;
    private double[,] LOCATION;
    private double[,] TEMPERATURE;
    private double[] AVERAGE_SUPPLIES_COST;
    private int MINIMUM_CUPS;
    private int[] DEFAULT_RECIPE;
    private string[,] LOCATION_TEXT;

    private double playerCapital;
    private double playerPrice;
    private double playerTemperature;
    private double[] playerGrossProfit;
    private double[] playerPopularity;
    private double[] playerSatisfaction;
    private int playerAdvertisement;
    private int playerCupsSold;
    private int playerImpatientCustomers;
    private int playerLocation;
    private int playerOverPricedCustomers;
    private int playerSatisfiedCustomers;
    private int playerUnsatisfiedCustomers;
    private int[] playerDate;
    private int[] playerRecipe;
    private int[] playerSupplies;
    private int[] playerStorage;

    private bool isBuying;
    private bool isCanceling;
    private bool isConnected;
    private double costPerCup;
    private double lastAdvertisement;
    private double lastPrice;
    private double profitPerCup;
    private double spend;
    private double[] suppliesCostPerRecipe;
    private int cupsPerPitcher;
    private int suppliesState;
    private int[] lastRecipe;
    private string lastRent;

    void Start()
    {

        Init();

        suppliesCostPerRecipe = new double[] { 0, 0, 0, 0, 0 };
        cupsPerPitcher = 0;
        costPerCup = 0;
        profitPerCup = 0;
        playerCupsSold = 0;
        lastRecipe = new int[] { 0, 0, 0, 0, 0 };

        ADVERTISEMENT = FindObjectOfType<ENV>().ADVERTISEMENT;
        AVERAGE_SUPPLIES_COST = FindObjectOfType<ENV>().AVERAGE_SUPPLIES_COST;
        DEFAULT_PRICE = FindObjectOfType<ENV>().DEFAULT_PRICE;
        DEFAULT_RECIPE = FindObjectOfType<ENV>().DEFAULT_RECIPE;
        LOCATION = FindObjectOfType<ENV>().LOCATION;
        LOCATION_TEXT = FindObjectOfType<ENV>().LOCATION_TEXT;
        MAXIMUM_PRICE = FindObjectOfType<ENV>().MAXIMUM_PRICE;
        MINIMUM_CUPS = FindObjectOfType<ENV>().MINIMUM_CUPS;
        SUPPLIES = FindObjectOfType<ENV>().SUPPLIES;
        TEMPERATURE = FindObjectOfType<ENV>().TEMPERATURE;

        playerAdvertisement = FindObjectOfType<Player>().PlayerAdvertisement;
        playerCapital = FindObjectOfType<Player>().PlayerCapital;
        playerCupsSold = FindObjectOfType<Player>().PlayerCupsSold;
        playerDate = FindObjectOfType<Player>().PlayerDate;
        playerGrossProfit = FindObjectOfType<Player>().PlayerGrossProfit;
        playerImpatientCustomers = FindObjectOfType<Player>().PlayerImpatientCustomers;
        playerLocation = FindObjectOfType<Player>().PlayerLocation;
        playerOverPricedCustomers = FindObjectOfType<Player>().PlayerOverPricedCustomers;
        playerPopularity = FindObjectOfType<Player>().PlayerPopularity;
        playerPrice = FindObjectOfType<Player>().PlayerPrice;
        playerRecipe = FindObjectOfType<Player>().PlayerRecipe;
        playerSatisfaction = FindObjectOfType<Player>().PlayerSatisfaction;
        playerSatisfiedCustomers = FindObjectOfType<Player>().PlayerSatisfiedCustomers;
        playerSupplies = FindObjectOfType<Player>().PlayerSupplies;
        playerTemperature = FindObjectOfType<Player>().PlayerTemperature;
        playerUnsatisfiedCustomers = FindObjectOfType<Player>().PlayerUnsatisfiedCustomers;
        playerStorage = FindObjectOfType<Player>().PlayerStorage;

        temperatureUIImage.sprite = GetTemperatureSprite(playerTemperature);
        popularityUIImage.fillAmount = (float)playerPopularity[playerLocation];
        satisfactionUIImage.fillAmount = (float)playerSatisfaction[playerLocation];
        locationUITexts[0].text = LOCATION_TEXT[playerLocation, 0];
        locationUITexts[1].text = LOCATION_TEXT[playerLocation, 1];
        dailyUITexts[0].text = string.Format("{0} - {1} - {2}", playerDate[0].ToString("00"), playerDate[1].ToString("00"), playerDate[2].ToString("00"));
        dailyUITexts[1].text = string.Format("{0}°", playerTemperature.ToString("0.0"));

        lastRent = LOCATION_TEXT[playerLocation, 0];
        lastAdvertisement = LOCATION[playerLocation, 0] * ADVERTISEMENT[playerAdvertisement, 0];
        lastPrice = playerPrice;
        lastRecipe = (int[]) playerRecipe.Clone();
        GetStorage();

    }

    void Update()
    {

        FindObjectOfType<Player>().PlayerCupsPerPitcher = cupsPerPitcher;

        isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        dailyUITexts[2].text = string.Format("{0}", playerCapital.ToString("0.00"));

        string bottomNavigationStateText =
            bottomNavigationState != BottomNavigationStates.results
            ? GetBottomNavigationStateText(FindObjectOfType<GameManager>().GetToggleName(bottomNavigationUIPanel))
            : GetResultsNavigationStateText(FindObjectOfType<GameManager>().GetToggleName(resultsNavigationUIPanel));

        if (!bottomNavigationStateText.Equals(""))

            bottomNavigationUIText.text = bottomNavigationStateText;

        bottomNavigationUIButtons[0].sprite =
            lastBottomNavigationState == BottomNavigationStates.results
            ? bottomNavigationSelectedUIButtons[0]
            : bottomNavigationNormalUIButtons[0];

        bottomNavigationUIButtons[1].sprite =
            lastBottomNavigationState == BottomNavigationStates.location
            ? bottomNavigationSelectedUIButtons[1]
            : bottomNavigationNormalUIButtons[1];

        bottomNavigationUIButtons[2].sprite =
            lastBottomNavigationState == BottomNavigationStates.upgrades
            ? bottomNavigationSelectedUIButtons[2]
            : bottomNavigationNormalUIButtons[2];

        bottomNavigationUIButtons[3].sprite =
            lastBottomNavigationState == BottomNavigationStates.staff
            ? bottomNavigationSelectedUIButtons[3]
            : bottomNavigationNormalUIButtons[3];

        bottomNavigationUIButtons[4].sprite =
            lastBottomNavigationState == BottomNavigationStates.marketing
            ? bottomNavigationSelectedUIButtons[4]
            : bottomNavigationNormalUIButtons[4];

        bottomNavigationUIButtons[5].sprite =
            lastBottomNavigationState == BottomNavigationStates.recipe
            ? bottomNavigationSelectedUIButtons[5]
            : bottomNavigationNormalUIButtons[5];

        bottomNavigationUIButtons[6].sprite =
            lastBottomNavigationState == BottomNavigationStates.supplies
            ? bottomNavigationSelectedUIButtons[6]
            : bottomNavigationNormalUIButtons[6];

        settingsUIButton.alpha =
            lastBottomNavigationState == BottomNavigationStates.idle
            ? 1
            : 0;

        settingsUIButton.blocksRaycasts = lastBottomNavigationState == BottomNavigationStates.idle;

        cupsPerPitcher =
                playerRecipe[3] > MINIMUM_CUPS
                ? playerRecipe[3]
                : MINIMUM_CUPS;

        if (SimpleInput.GetButtonUp("OnBottomNavigation"))

            OnBottomNavigation();

        if (bottomNavigationState == BottomNavigationStates.supplies)
        {

            supplyQuantityUITexts[0].text = SUPPLIES[suppliesState, 0, 0].ToString();
            supplyQuantityUITexts[1].text = SUPPLIES[suppliesState, 0, 1].ToString();
            supplyQuantityUITexts[2].text = SUPPLIES[suppliesState, 0, 2].ToString();

            supplyDecrementUIButtons[0].interactable = SUPPLIES[suppliesState, 0, 0] > 0;
            supplyDecrementUIButtons[1].interactable = SUPPLIES[suppliesState, 0, 1] > 0;
            supplyDecrementUIButtons[2].interactable = SUPPLIES[suppliesState, 0, 2] > 0;

            supplyIncrementUIButtons[0].interactable = playerCapital - SUPPLIES[suppliesState, 2, 0] >= 0 
                && HasAvailableSpace(0);

            supplyIncrementUIButtons[1].interactable = playerCapital - SUPPLIES[suppliesState, 2, 1] >= 0
                && HasAvailableSpace(1);

            supplyIncrementUIButtons[2].interactable = playerCapital - SUPPLIES[suppliesState, 2, 2] >= 0
                && HasAvailableSpace(2);

            buyUIButton.interactable = FindObjectOfType<Player>().PlayerCapital != playerCapital;
            cancelUIButton.interactable = FindObjectOfType<Player>().PlayerCapital != playerCapital;

            if (SimpleInput.GetButtonUp("OnSuppliesNavigationMango"))

                OnSuppliesNavigation(0);

            if (SimpleInput.GetButtonUp("OnSuppliesNavigationGraham"))

                OnSuppliesNavigation(1);

            if (SimpleInput.GetButtonUp("OnSuppliesNavigationMilk"))

                OnSuppliesNavigation(2);

            if (SimpleInput.GetButtonUp("OnSuppliesNavigationIceCubes"))

                OnSuppliesNavigation(3);

            if (SimpleInput.GetButtonUp("OnSuppliesNavigationCups"))

                OnSuppliesNavigation(4);

            if (SimpleInput.GetButtonDown("OnIncrementSmall"))

                OnSuppliesIncrement(0);

            if (SimpleInput.GetButtonDown("OnIncrementMedium"))

                OnSuppliesIncrement(1);

            if (SimpleInput.GetButtonDown("OnIncrementLarge"))

                OnSuppliesIncrement(2);

            if (SimpleInput.GetButtonDown("OnDecrementSmall"))

                OnSuppliesDecrement(0);

            if (SimpleInput.GetButtonDown("OnDecrementMedium"))

                OnSuppliesDecrement(1);

            if (SimpleInput.GetButtonDown("OnDecrementLarge"))

                OnSuppliesDecrement(2);

            if (SimpleInput.GetButtonDown("OnCancel"))
            {

                if (!cancelUIButton.interactable)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please increment an item first",
                        "dialog");

                }
                else
                {

                    FindObjectOfType<SoundsManager>().OnClicked();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "CANCELING",
                        "Are you sure you want clear the counter?",
                        "optionPane1");
                    isCanceling = !isCanceling;

                }


            }

            if (SimpleInput.GetButtonDown("OnBuy"))
            {

                if (!buyUIButton.interactable)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please increment an item first",
                        "dialog");

                }
                else
                {

                    spend = FindObjectOfType<Player>().PlayerCapital - playerCapital;
                    string description = string.Format("Are you sure you want to spend ₱ {0} on goods?", spend.ToString("0.00"));
                    FindObjectOfType<SoundsManager>().OnClicked();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "BUYING",
                        description,
                        "optionPane1");
                    isBuying = !isBuying;

                }

            }

            if (SimpleInput.GetButtonDown("OnYes") && isCanceling)
            {

                FindObjectOfType<SoundsManager>().OnGrahamCrack();
                FindObjectOfType<GameManager>()
                    .Animator
                    .SetTrigger("ok");
                OnCancel();
                isCanceling = !isCanceling;

            }

            if (SimpleInput.GetButtonDown("OnYes") && isBuying)
            {

                FindObjectOfType<SoundsManager>().OnGrahamCrack();
                FindObjectOfType<GameManager>()
                    .Animator
                    .SetTrigger("ok");
                OnBuySuccess();
                isBuying = !isBuying;

            }

        }

        if (bottomNavigationState == BottomNavigationStates.recipe)
        {

            FindObjectOfType<Player>().PlayerRecipe = playerRecipe;

            recipeQuantityUITexts[0].text = playerRecipe[0].ToString();
            recipeQuantityUITexts[1].text = playerRecipe[1].ToString();
            recipeQuantityUITexts[2].text = playerRecipe[2].ToString();
            recipeQuantityUITexts[3].text = playerRecipe[3].ToString();

            recipeDecrementUIButtons[0].interactable = playerRecipe[0] > 0;
            recipeDecrementUIButtons[1].interactable = playerRecipe[1] > 0;
            recipeDecrementUIButtons[2].interactable = playerRecipe[2] > 0;
            recipeDecrementUIButtons[3].interactable = playerRecipe[3] > 0;

            recipeResetUIButtons[0].interactable = playerRecipe[0] != DEFAULT_RECIPE[0];
            recipeResetUIButtons[1].interactable = playerRecipe[1] != DEFAULT_RECIPE[1];
            recipeResetUIButtons[2].interactable = playerRecipe[2] != DEFAULT_RECIPE[2];
            recipeResetUIButtons[3].interactable = playerRecipe[3] != DEFAULT_RECIPE[3];

            cupsPerPitcherUIText.text = string.Format("Cups Per Pitcher:\n{0}", cupsPerPitcher);

            if (SimpleInput.GetButtonDown("OnDecrementMango"))

                OnRecipeDecrement(0);

            if (SimpleInput.GetButtonDown("OnDecrementGraham"))

                OnRecipeDecrement(1);

            if (SimpleInput.GetButtonDown("OnDecrementMilk"))

                OnRecipeDecrement(2);

            if (SimpleInput.GetButtonDown("OnDecrementIceCubes"))

                OnRecipeDecrement(3);

            if (SimpleInput.GetButtonDown("OnIncrementMango"))

                OnRecipeIncrement(0);

            if (SimpleInput.GetButtonDown("OnIncrementGraham"))

                OnRecipeIncrement(1);

            if (SimpleInput.GetButtonDown("OnIncrementMilk"))

                OnRecipeIncrement(2);

            if (SimpleInput.GetButtonDown("OnIncrementIceCubes"))

                OnRecipeIncrement(3);

            if (SimpleInput.GetButtonDown("OnResetMango"))

                OnRecipeReset(0);

            if (SimpleInput.GetButtonDown("OnResetGraham"))

                OnRecipeReset(1);

            if (SimpleInput.GetButtonDown("OnResetMilk"))

                OnRecipeReset(2);

            if (SimpleInput.GetButtonDown("OnResetIceCubes"))

                OnRecipeReset(3);

        }

        if (bottomNavigationState == BottomNavigationStates.marketing)
        {

            FindObjectOfType<Player>().PlayerPrice = playerPrice;
            FindObjectOfType<Player>().PlayerAdvertisement = playerAdvertisement;
            FindObjectOfType<Player>().PlayerCostPerCup = costPerCup;

            double advertisementPrice = LOCATION[playerLocation, 0] * ADVERTISEMENT[playerAdvertisement, 0];
            costPerCup = GetCostPerCup();
            profitPerCup = playerPrice - costPerCup;
            profitPerCup =
                profitPerCup > 0
                ? profitPerCup
                : 0;

            priceUIText.text = string.Format("₱ {0}", playerPrice.ToString("0.00"));
            profitPerCupUIText.text = string.Format("Profit Per Cup:\n₱ {0}", profitPerCup.ToString("0.00"));
            advertisementUIText.text = string.Format("₱ {0}", advertisementPrice.ToString("0.00"));

            priceDecrementUIButton.interactable = playerPrice > 0;
            priceIncrementUIButton.interactable = playerPrice < MAXIMUM_PRICE;
            priceResetUIButton.interactable = playerPrice != DEFAULT_PRICE;

            advertisementDecrementUIButton.interactable = playerAdvertisement > 0;
            advertisementIncrementUIButton.interactable = IsAdvertisementIncrementable();
            advertisementResetUIButton.interactable = playerAdvertisement > 0;

            if (SimpleInput.GetButtonDown("OnDecrementPrice"))

                OnPriceDecrement();

            if (SimpleInput.GetButtonDown("OnIncrementPrice"))

                OnPriceIncrement();

            if (SimpleInput.GetButtonDown("OnResetPrice") && priceResetUIButton.interactable)

                OnPriceReset();

            if (SimpleInput.GetButtonDown("OnDecrementAdvertisement"))

                OnAdvertisementDecrement();

            if (SimpleInput.GetButtonDown("OnIncrementAdvertisement"))

                OnAdvertisementIncrement();

            if (SimpleInput.GetButtonDown("OnResetAdvertisement") && advertisementResetUIButton.interactable)

                OnAdvertisementReset();

        }

        if (SimpleInput.GetButtonDown("OnNo"))
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            Init();

        }

        if (SimpleInput.GetButtonDown("OnStartDay"))
        {

            if (playerSupplies[0] < playerRecipe[0]
                || playerSupplies[1] < playerRecipe[1]
                || playerSupplies[2] < playerRecipe[2]
                || playerSupplies[3] < playerRecipe[3]
                || playerSupplies[4] < 1)
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Not enough supplies to start the day. Change your recipe or buy more supplies.",
                    "dialog");

            }
            else if (playerAdvertisement > 0)
            {

                spend = LOCATION[playerLocation, 0] * ADVERTISEMENT[playerAdvertisement, 0];

                if (playerCapital - spend < 0)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "You don't have enough money to pay for your advertisement. Lower your advertising budget.",
                        "dialog");

                }

            }
            else if (false)
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "You don't have enough money to pay for your rent. Move to a less expensive place or go back to the HOME, or fire your staff.",
                    "dialog");

            }
            else

                StartDay();

        }

        if (SimpleInput.GetButtonUp("OnResultsNavigation"))

            OnResultsNavigation();

        if (bottomNavigationState == BottomNavigationStates.results)
        {

            if (resultsNavigationState == ResultsNavigationStates.yesterdaysPerformanceAndSettings)
            {

                performanceCupsSoldUIText.text = playerCupsSold.ToString();
                performanceProfitUIText.text = string.Format("₱ {0}", playerGrossProfit[0].ToString("0.00"));
                unsatisfiedCustomersUIText.text = playerUnsatisfiedCustomers.ToString();
                satisfiedCustomersUIText.text = playerSatisfiedCustomers.ToString();
                impatientCustomersUIText.text = playerImpatientCustomers.ToString();
                overPricedCustomersUIText.text = playerOverPricedCustomers.ToString();
                settingRentUIText.text = lastRent;
                settingAdvertisingUIText.text = string.Format("₱ {0}", lastAdvertisement.ToString("0.00"));
                settingPriceUIText.text = string.Format("₱ {0}", lastPrice.ToString("0.00"));
                settingRecipeUIText[0].text = lastRecipe[0].ToString();
                settingRecipeUIText[1].text = lastRecipe[1].ToString();
                settingRecipeUIText[2].text = lastRecipe[2].ToString();
                settingRecipeUIText[3].text = lastRecipe[3].ToString();

            }

        }

    }

    private void Init()
    {

        isBuying = false;
        isCanceling = false;
        spend = 0;
        suppliesState = 0;

    }

    private void OnBottomNavigation()
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        string navigation = FindObjectOfType<GameManager>().GetToggleName(bottomNavigationUIPanel);
        bottomNavigationState = GetBottomNavigationState(navigation);

        if (lastBottomNavigationState == bottomNavigationState)
        {

            FindObjectOfType<GameManager>().Animator.SetTrigger("back");
            lastBottomNavigationState = BottomNavigationStates.idle;

        }
        else
        {

            if (lastBottomNavigationState == BottomNavigationStates.idle)

                FindObjectOfType<GameManager>().Animator.SetTrigger("initialNavigation");

            else

                FindObjectOfType<GameManager>().Animator.SetTrigger("navigation");

            FindObjectOfType<GameManager>().Animator.SetInteger("bottomNavigationState", (int)bottomNavigationState);
            lastBottomNavigationState = bottomNavigationState;

        }

        if (bottomNavigationState == BottomNavigationStates.results)
        {

            yesterdaysResultsUINavButton.isOn = true;
            FindObjectOfType<GameManager>().Animator.SetInteger("resultsNavigationState", (int)ResultsNavigationStates.yesterdaysResults);

        }

        mangoUINavButton.isOn = true;
        OnSuppliesQuantityClear();
        OnSuppliesNavigation(0);

        OnCancel();

    }

    private BottomNavigationStates GetBottomNavigationState(string _navigation)
    {

        return _navigation switch
        {

            "ResultsUINavButton" => BottomNavigationStates.results,

            "LocationUINavButton" => BottomNavigationStates.location,

            "UpgradesUINavButton" => BottomNavigationStates.upgrades,

            "StaffUINavButton" => BottomNavigationStates.staff,

            "MarketingUINavButton" => BottomNavigationStates.marketing,

            "RecipeUINavButton" => BottomNavigationStates.recipe,

            "SuppliesUINavButton" => BottomNavigationStates.supplies,

            _ => BottomNavigationStates.idle,

        };

    }

    private string GetBottomNavigationStateText(string _bottomNavigation)
    {

        return _bottomNavigation switch
        {

            "LocationUINavButton" => "Location",

            "UpgradesUINavButton" => "Upgrades",

            "StaffUINavButton" => "Staff",

            "MarketingUINavButton" => "Marketing",

            "RecipeUINavButton" => "Recipe",

            "SuppliesUINavButton" => "Supplies",

            _ => "Results",

        };

    }

    private void OnSuppliesNavigation(int _suppliesNavigationState)
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        suppliesState = _suppliesNavigationState;

        supplyUIImages[0].sprite = supplySprites[_suppliesNavigationState];
        supplyUIImages[1].sprite = supplySprites[_suppliesNavigationState];
        supplyUIImages[2].sprite = supplySprites[_suppliesNavigationState];

        supplyPriceUITexts[0].text = string.Format(
            "{0} {1} {2}",
            SUPPLIES[_suppliesNavigationState, 1, 0].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES[_suppliesNavigationState, 2, 0].ToString("0.00")
            );

        supplyPriceUITexts[1].text = string.Format(
            "{0} {1} {2}",
            SUPPLIES[_suppliesNavigationState, 1, 1].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES[_suppliesNavigationState, 2, 1].ToString("0.00")
            );

        supplyPriceUITexts[2].text = string.Format(
            "{0} {1} {2}",
            SUPPLIES[_suppliesNavigationState, 1, 2].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES[_suppliesNavigationState, 2, 2].ToString("0.00")
            );

    }

    private string GetConjuctions(int _supply)
    {

        return _supply switch
        {

            0 => "mangoes = ₱",

            1 => "pieces = ₱",

            2 => "cans = ₱",

            3 => "cubes = ₱",

            _ => "cups = ₱",

        };

    }

    private void OnSuppliesQuantityClear()
    {

        SUPPLIES[0, 0, 0] = 0;
        SUPPLIES[0, 0, 1] = 0;
        SUPPLIES[0, 0, 2] = 0;
        SUPPLIES[1, 0, 0] = 0;
        SUPPLIES[1, 0, 1] = 0;
        SUPPLIES[1, 0, 2] = 0;
        SUPPLIES[2, 0, 0] = 0;
        SUPPLIES[2, 0, 1] = 0;
        SUPPLIES[2, 0, 2] = 0;
        SUPPLIES[3, 0, 0] = 0;
        SUPPLIES[3, 0, 1] = 0;
        SUPPLIES[3, 0, 2] = 0;
        SUPPLIES[4, 0, 0] = 0;
        SUPPLIES[4, 0, 1] = 0;
        SUPPLIES[4, 0, 2] = 0;

    }

    private void OnSuppliesDecrement(int _scale)
    {

        double quantityPerPrice = SUPPLIES[suppliesState, 1, _scale];
        double price = SUPPLIES[suppliesState, 2, _scale];

        if (SUPPLIES[suppliesState, 0, _scale] - quantityPerPrice >= 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            SUPPLIES[suppliesState, 0, _scale] -= quantityPerPrice;
            playerCapital += price;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnSuppliesIncrement(int _scale)
    {

        double quantityPerPrice = SUPPLIES[suppliesState, 1, _scale];
        double price = SUPPLIES[suppliesState, 2, _scale];

        if (playerCapital - price < 0)
        {
            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've insufficient money to increment this item",
                "dialog");
            

        }
        else if (!HasAvailableSpace(_scale))
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've reach the maximum limit",
                "dialog");

        }
        else
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            SUPPLIES[suppliesState, 0, _scale] += quantityPerPrice;
            playerCapital -= price;

        }

    }

    private void OnCancel()
    {

        OnSuppliesQuantityClear();
        playerCapital = FindObjectOfType<Player>().PlayerCapital;
        playerAdvertisement = FindObjectOfType<Player>().PlayerAdvertisement;

    }

    private async void OnBuySuccess()
    {

        FindObjectOfType<Player>().PlayerCapital -= spend;

        playerSupplies[0] += Convert.ToInt32(SUPPLIES[0, 0, 0] + SUPPLIES[0, 0, 1] + SUPPLIES[0, 0, 2]);
        playerSupplies[1] += Convert.ToInt32(SUPPLIES[1, 0, 0] + SUPPLIES[1, 0, 1] + SUPPLIES[1, 0, 2]);
        playerSupplies[2] += Convert.ToInt32(SUPPLIES[2, 0, 0] + SUPPLIES[2, 0, 1] + SUPPLIES[2, 0, 2]);
        playerSupplies[3] += Convert.ToInt32(SUPPLIES[3, 0, 0] + SUPPLIES[3, 0, 1] + SUPPLIES[3, 0, 2]);
        playerSupplies[4] += Convert.ToInt32(SUPPLIES[4, 0, 0] + SUPPLIES[4, 0, 1] + SUPPLIES[4, 0, 2]);

        await Task.Delay(1000);

        Init();
        GetStorage();
        OnCancel();

        FindObjectOfType<Player>().OnAutoSave(isConnected);

    }

    private void OnPriceIncrement()
    {

        if (playerPrice < MAXIMUM_PRICE)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            playerPrice++;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnPriceDecrement()
    {

        if (playerPrice > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            playerPrice--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnAdvertisementIncrement()
    {

        if (IsAdvertisementIncrementable())
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            spend = LOCATION[playerLocation, 0] * ADVERTISEMENT[++playerAdvertisement, 0];
            playerCapital = FindObjectOfType<Player>().PlayerCapital;
            playerCapital -= spend;

        }
        else if (playerAdvertisement == 10)
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've already reached the maximum advertisement",
                "dialog");

        }
        else
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've insufficient money to avail this advertisement",
                "dialog");

        }

    }

    private void OnAdvertisementDecrement()
    {

        if (playerAdvertisement > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            spend = LOCATION[playerLocation, 0] * ADVERTISEMENT[--playerAdvertisement, 0];
            playerCapital = FindObjectOfType<Player>().PlayerCapital;
            playerCapital -= spend;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private bool IsAdvertisementIncrementable()
    {

        if (playerAdvertisement < 10)
        {

            int newAdvertisement = playerAdvertisement;
            spend = LOCATION[playerLocation, 0] * ADVERTISEMENT[++newAdvertisement, 0];

            return playerCapital - spend >= 0;

        }

        return false;

    }

    private void OnPriceReset()
    {

        if (playerPrice != DEFAULT_PRICE)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            playerPrice = DEFAULT_PRICE;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnAdvertisementReset()
    {

        if (playerAdvertisement != 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            playerCapital = FindObjectOfType<Player>().PlayerCapital;
            playerAdvertisement = 0;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnRecipeDecrement(int _recipe)
    {

        if (playerRecipe[_recipe] > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            playerRecipe[_recipe]--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnRecipeIncrement(int _recipe)
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        playerRecipe[_recipe]++;

    }

    private void OnRecipeReset(int _recipe)
    {

        if (_recipe == 0 && playerRecipe[_recipe] != DEFAULT_RECIPE[0])

            playerRecipe[_recipe] = DEFAULT_RECIPE[0];

        else if (_recipe == 1 && playerRecipe[_recipe] != DEFAULT_RECIPE[1])

            playerRecipe[_recipe] = DEFAULT_RECIPE[1];

        else if (_recipe == 2 && playerRecipe[_recipe] != DEFAULT_RECIPE[2])

            playerRecipe[_recipe] = DEFAULT_RECIPE[2];

        else if (_recipe == 3 && playerRecipe[_recipe] != DEFAULT_RECIPE[3])

            playerRecipe[_recipe] = DEFAULT_RECIPE[3];

        else
        {

            FindObjectOfType<SoundsManager>().OnError();
            return;

        }

        FindObjectOfType<SoundsManager>().OnClicked();

    }

    private Sprite GetTemperatureSprite(double _temperature)
    {

        if (_temperature >= TEMPERATURE[0, 0] && _temperature <= TEMPERATURE[1, 1])

            return temperatureSprites[0];

        else if (_temperature >= TEMPERATURE[3, 0] && _temperature <= TEMPERATURE[4, 1])

            return temperatureSprites[2];

        return temperatureSprites[1];

    }

    private double GetCostPerCup()
    {

        suppliesCostPerRecipe[0] = AVERAGE_SUPPLIES_COST[0] * playerRecipe[0];
        suppliesCostPerRecipe[1] = AVERAGE_SUPPLIES_COST[1] * playerRecipe[1];
        suppliesCostPerRecipe[2] = AVERAGE_SUPPLIES_COST[2] * playerRecipe[2];
        suppliesCostPerRecipe[3] = AVERAGE_SUPPLIES_COST[3] * playerRecipe[3];
        suppliesCostPerRecipe[4] = AVERAGE_SUPPLIES_COST[4] * cupsPerPitcher;

        double cost = suppliesCostPerRecipe[0]
            + suppliesCostPerRecipe[1]
            + suppliesCostPerRecipe[2]
            + suppliesCostPerRecipe[3]
            + suppliesCostPerRecipe[4];

        costPerCup = cost / cupsPerPitcher;

        return costPerCup;

    }

    private void StartDay()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void OnResultsNavigation()
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        string navigation = FindObjectOfType<GameManager>().GetToggleName(resultsNavigationUIPanel);
        resultsNavigationState = GetResultsNavigationState(navigation);

        FindObjectOfType<GameManager>().Animator.SetInteger("resultsNavigationState", (int)resultsNavigationState);

    }

    private ResultsNavigationStates GetResultsNavigationState(string _navigation)
    {

        return _navigation switch
        {

            "YesterdaysPerformanceAndSettingUINavButton" => ResultsNavigationStates.yesterdaysPerformanceAndSettings,

            "YesterdaysResultsUINavButton" => ResultsNavigationStates.yesterdaysResults,

            "ChartsUINavButton" => ResultsNavigationStates.charts,

            "ProfitAndLossUINavButton" => ResultsNavigationStates.profitAndLoss,

            _ => ResultsNavigationStates.balanceSheet,

        };

    }

    private string GetResultsNavigationStateText(string _resultsNavigation)
    {

        return _resultsNavigation switch
        {

            "YesterdaysPerformanceAndSettingUINavButton" => "Yesterday's Performance & Setting",

            "YesterdaysResultsUINavButton" => "Yesterday's Results",

            "ChartsUINavButton" => "Charts",

            "ProfitAndLossUINavButton" => "Profit & Loss",

            _ => "Balance Sheet"

        };

    }

    private void GetStorage()
    {

        for (int supply = 0; supply < playerStorage.Length; supply++)
        {

            suppliesUIImages[supply].fillAmount = (float)playerSupplies[supply] / playerStorage[supply];
            suppliesUITexts[supply].text = playerSupplies[supply].ToString();

        }
            
    }

    private bool HasAvailableSpace(int _scale)
    {

        int overAllSupplies = Convert.ToInt32(SUPPLIES[suppliesState, 0, 0] + SUPPLIES[suppliesState, 0, 1] + SUPPLIES[suppliesState, 0, 2]);
        bool hasAvailableSpace = playerSupplies[suppliesState] + overAllSupplies + SUPPLIES[suppliesState, 1, _scale] <= playerStorage[suppliesState];
        return hasAvailableSpace;

    }

}