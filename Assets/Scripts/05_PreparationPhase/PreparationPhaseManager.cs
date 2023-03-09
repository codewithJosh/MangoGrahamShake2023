using System;
using System.Collections.Generic;
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

    [Header("IDLE SECTION")]
    [SerializeField]
    private Button[] previousUIButtons;

    [SerializeField]
    private Button[] nextUIButtons;

    [SerializeField]
    private CanvasGroup settingsUIButton;

    [SerializeField]
    private Image locationHUD;

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
    private TextMeshProUGUI[] currentLocationUITexts;

    [SerializeField]
    private GameObject[] resultsUIPanels;

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

    [Header("RESULTS SECTION @YESTERDAYS RESULTS")]
    [SerializeField]
    private Image standingUIImage;

    [SerializeField]
    private Sprite[] standingSprites;

    [SerializeField]
    private TextMeshProUGUI[] yesterdaysResultsUITexts;

    [SerializeField]
    private TextMeshProUGUI standingUIText;

    [SerializeField]
    private TextMeshProUGUI customerSatisfactionAndMissedSalesUIText;

    [SerializeField]
    private TextMeshProUGUI customersFeedbackUIText;

    [SerializeField]
    private TextMeshProUGUI iceCubesMeltedUIText;

    [Header("RESULTS SECTION @PROFIT AND LOSS")]
    [SerializeField]
    private TextMeshProUGUI[] currentProfitAndLossUITexts;

    [SerializeField]
    private TextMeshProUGUI[] lastProfitAndLossUITexts;

    [SerializeField]
    private TextMeshProUGUI[] bestProfitAndLossUITexts;

    [SerializeField]
    private TextMeshProUGUI profitAndLossCurrentDateUIText;

    [Header("RESULTS SECTION @BALANCE SHEET")]
    [SerializeField]
    private TextMeshProUGUI[] balanceSheetUITexts;

    [Header("LOCATION SECTION")]
    [SerializeField]
    private Button rentUIButton;

    [SerializeField]
    private GameObject isNotRentableHUD;

    [SerializeField]
    private Image[] locationFillUIImages;

    [SerializeField]
    private Image locationUIImage;

    [SerializeField]
    private Sprite[] locationSprites;

    [SerializeField]
    private TextMeshProUGUI[] locationUITexts;

    [Header("UPGRADES SECTION")]
    [SerializeField]
    private Button upgradeUIButton;

    [SerializeField]
    private Image upgradeLevelFillUIImage;

    [SerializeField]
    private Image upgradeUIImage;

    [SerializeField]
    private Sprite[] upgradeSprites;

    [SerializeField]
    private TextMeshProUGUI[] upgradeUITexts;

    [Header("STAFF SECTION")]
    [SerializeField]
    private Toggle hireAndFireUIButton;

    [SerializeField]
    private Image staffUIImage;

    [SerializeField]
    private Sprite[] hireAndFireSprites;

    [SerializeField]
    private Sprite[] staffSprites;

    [SerializeField]
    private TextMeshProUGUI[] staffUITexts;

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

    [Header("RECIPE SECTION")]
    [SerializeField]
    private Button[] recipeDecrementUIButtons;

    [SerializeField]
    private Button[] recipeResetUIButtons;

    [SerializeField]
    private TextMeshProUGUI cupsPerPitcherUIText;

    [SerializeField]
    private TextMeshProUGUI[] recipeQuantityUITexts;

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

    private enum BottomNavigationStates { idle, results, location, upgrades, staff, marketing, recipe, supplies };
    private enum ResultsNavigationStates { yesterdaysPerformanceAndSettings, yesterdaysResults, charts, profitAndLoss, balanceSheet };

    private BottomNavigationStates bottomNavigationState;
    private BottomNavigationStates lastBottomNavigationState;
    private ResultsNavigationStates resultsNavigationState;

    private double[,] ADVERTISEMENT;
    private double DEFAULT_PRICE;
    private double MAXIMUM_PRICE;
    private double[,,] SUPPLIES;
    private double[,,] UPGRADE;
    private double[,] LOCATION;
    private double[,] STAFF;
    private double[,] STANDING;
    private double[,] TEMPERATURE;
    private double[] AVERAGE_SUPPLIES_COST;
    private int MINIMUM_CUPS;
    private int[] DEFAULT_RECIPE;
    private int[] STORAGE;
    private string[,] LOCATION_TEXT;
    private string[,] STAFF_TEXT;
    private string[,] UPGRADE_TEXT;

    private double playerCapital;
    private double playerCustomerSatisfaction;
    private double playerEquipments;
    private double playerPrice;
    private double playerProfitAndLoss;
    private double playerTemperature;
    private double playerTopEarnings;
    private double[] playerEarnings;
    private double[] playerExpenses;
    private double[] playerGrossMargin;
    private double[] playerGrossProfit;
    private double[] playerMarketing;
    private double[] playerPopularity;
    private double[] playerRent;
    private double[] playerRevenue;
    private double[] playerSatisfaction;
    private double[] playerStockLost;
    private double[] playerStockUsed;
    private int playerAdvertisement;
    private int playerCupsSold;
    private int playerFeedback;
    private int playerIceCubesMelted;
    private int playerImpatientCustomers;
    private int playerLocation;
    private int playerOverPricedCustomers;
    private int playerSatisfiedCustomers;
    private int playerUnsatisfiedCustomers;
    private int[] playerDate;
    private int[] playerRecipe;
    private int[] playerStorage;
    private int[] playerSupplies;
    private int[] playerUpgrade;
    private List<int> playerStaffs;

    private bool isBuying;
    private bool isCanceling;
    private bool isConnected;
    private bool isRenting;
    private bool isUpgrading;
    private double lastAdvertisement;
    private double lastPrice;
    private double spend;
    private double[] grossMargin;
    private double[] suppliesCostPerRecipe;
    private double[] suppliesCostPerStock;
    private int cupsPerPitcher;
    private int iceCubes;
    private int locationState;
    private int missedSales;
    private int staffState;
    private int suppliesState;
    private int upgradeState;
    private int[,] supplies;
    private int[] lastDate;
    private int[] lastRecipe;
    private string lastRent;

    void Start()
    {

        Init();

        cupsPerPitcher = 0;
        grossMargin = new double[] { 0, 0, 0, 0, };
        lastRecipe = new int[] { 0, 0, 0, 0, 0, };
        suppliesCostPerRecipe = new double[] { 0, 0, 0, 0, 0, };
        suppliesCostPerStock = new double[] { 0, 0, 0, 0, 0, };
        supplies = new int[5, 3]
        {

            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },

        };

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
        STANDING = FindObjectOfType<ENV>().STANDING;
        UPGRADE_TEXT = FindObjectOfType<ENV>().UPGRADE_TEXT;
        UPGRADE = FindObjectOfType<ENV>().UPGRADE;
        STORAGE = FindObjectOfType<ENV>().STORAGE;
        STAFF = FindObjectOfType<ENV>().STAFF;
        STAFF_TEXT = FindObjectOfType<ENV>().STAFF_TEXT;

        playerAdvertisement = FindObjectOfType<Player>().PlayerAdvertisement;
        playerCapital = FindObjectOfType<Player>().PlayerCapital;
        playerCupsSold = FindObjectOfType<Player>().PlayerCupsSold;
        playerCustomerSatisfaction = FindObjectOfType<Player>().PlayerCustomerSatisfaction;
        playerDate = FindObjectOfType<Player>().PlayerDate;
        playerEarnings = FindObjectOfType<Player>().PlayerEarnings;
        playerEquipments = FindObjectOfType<Player>().PlayerEquipments;
        playerExpenses = FindObjectOfType<Player>().PlayerExpenses;
        playerFeedback = FindObjectOfType<Player>().PlayerFeedback;
        playerGrossMargin = FindObjectOfType<Player>().PlayerGrossMargin;
        playerGrossProfit = FindObjectOfType<Player>().PlayerGrossProfit;
        playerIceCubesMelted = FindObjectOfType<Player>().PlayerIceCubesMelted;
        playerImpatientCustomers = FindObjectOfType<Player>().PlayerImpatientCustomers;
        playerLocation = FindObjectOfType<Player>().PlayerLocation;
        playerMarketing = FindObjectOfType<Player>().PlayerMarketing;
        playerOverPricedCustomers = FindObjectOfType<Player>().PlayerOverPricedCustomers;
        playerPopularity = FindObjectOfType<Player>().PlayerPopularity;
        playerPrice = FindObjectOfType<Player>().PlayerPrice;
        playerProfitAndLoss = FindObjectOfType<Player>().PlayerProfitAndLoss;
        playerRecipe = FindObjectOfType<Player>().PlayerRecipe;
        playerRent = FindObjectOfType<Player>().PlayerRent;
        playerRevenue = FindObjectOfType<Player>().PlayerRevenue;
        playerSatisfaction = FindObjectOfType<Player>().PlayerSatisfaction;
        playerSatisfiedCustomers = FindObjectOfType<Player>().PlayerSatisfiedCustomers;
        playerStaffs = FindObjectOfType<Player>().PlayerStaffs;
        playerStockLost = FindObjectOfType<Player>().PlayerStockLost;
        playerStockUsed = FindObjectOfType<Player>().PlayerStockUsed;
        playerStorage = FindObjectOfType<Player>().PlayerStorage;
        playerSupplies = FindObjectOfType<Player>().PlayerSupplies;
        playerTemperature = FindObjectOfType<Player>().PlayerTemperature;
        playerTopEarnings = FindObjectOfType<Player>().PlayerTopEarnings;
        playerUnsatisfiedCustomers = FindObjectOfType<Player>().PlayerUnsatisfiedCustomers;
        playerUpgrade = FindObjectOfType<Player>().PlayerUpgrade;

        temperatureUIImage.sprite = GetTemperatureSprite(playerTemperature);
        dailyUITexts[0].text = string.Format("{0} - {1} - {2}", playerDate[0].ToString("00"), playerDate[1].ToString("00"), playerDate[2].ToString("00"));
        dailyUITexts[1].text = string.Format("{0}°", playerTemperature.ToString("0.0"));

        lastRent = LOCATION_TEXT[playerLocation, 0];
        lastAdvertisement = LOCATION[playerLocation, 0] * ADVERTISEMENT[playerAdvertisement, 0];
        lastPrice = playerPrice;
        lastRecipe = (int[])playerRecipe.Clone();
        lastDate = (int[])playerDate.Clone();
        lastDate = GetYesterdaysDate(lastDate);
        missedSales = playerImpatientCustomers + playerOverPricedCustomers;

        InitState();

    }

    void Update()
    {

        FindObjectOfType<Player>().PlayerCupsPerPitcher = cupsPerPitcher;

        isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        locationHUD.sprite = locationSprites[playerLocation];
        popularityUIImage.fillAmount = (float)playerPopularity[playerLocation];
        satisfactionUIImage.fillAmount = (float)playerSatisfaction[playerLocation];
        currentLocationUITexts[0].text = LOCATION_TEXT[playerLocation, 0];
        currentLocationUITexts[1].text = LOCATION_TEXT[playerLocation, 2];
        dailyUITexts[2].text = string.Format("{0}", playerCapital.ToString("0.00"));
        iceCubes = playerSupplies[3] + (int)UPGRADE[1, playerUpgrade[1], 1];
        GetStorage();

        string bottomNavigationStateText =
            bottomNavigationState != BottomNavigationStates.results
            ? GetBottomNavigationStateText(FindObjectOfType<GameManager>().GetToggleName(bottomNavigationUIPanel))
            : GetResultsNavigationStateText(FindObjectOfType<GameManager>().GetToggleName(resultsNavigationUIPanel));

        if (!bottomNavigationStateText.Equals(""))

            bottomNavigationUIText.text = bottomNavigationStateText;

        for (int navigationState = 0; navigationState < 7; navigationState++)

            bottomNavigationUIButtons[navigationState].sprite =
                (int)lastBottomNavigationState - 1 == navigationState
                ? bottomNavigationSelectedUIButtons[navigationState]
                : bottomNavigationNormalUIButtons[navigationState];

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

        if (bottomNavigationState == BottomNavigationStates.results)
        {

            if (SimpleInput.GetButtonUp("OnResultsNavigation"))

                OnResultsNavigation();

            for (int navigationState = 0; navigationState < 5; navigationState++)

                resultsUIPanels[navigationState].SetActive((int)resultsNavigationState == navigationState);

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

                for (int recipe = 0; recipe < 4; recipe++)
                    
                    settingRecipeUIText[recipe].text = lastRecipe[recipe].ToString();

            }

            if (resultsNavigationState == ResultsNavigationStates.yesterdaysResults)
            {

                grossMargin[0] = playerGrossMargin[0] * 100;
                double customerSatisfaction = playerCustomerSatisfaction * 100;

                yesterdaysResultsUITexts[0].text = string.Format("Year {0} - Month {1} - Day {2}", lastDate[0].ToString("00"), lastDate[1].ToString("00"), lastDate[2].ToString("00"));
                yesterdaysResultsUITexts[1].text = string.Format("{0} cups", playerCupsSold);
                yesterdaysResultsUITexts[2].text = string.Format("₱ {0}", playerRevenue[0].ToString("0.00"));
                yesterdaysResultsUITexts[3].text = string.Format("₱ {0}", playerStockUsed[0].ToString("0.00"));
                yesterdaysResultsUITexts[4].text = string.Format("₱ {0}", playerStockLost[0].ToString("0.00"));
                yesterdaysResultsUITexts[5].text = string.Format("₱ {0}", playerGrossProfit[0].ToString("0.00"));
                yesterdaysResultsUITexts[6].text = string.Format("{0}%", grossMargin[0].ToString("0.00"));
                yesterdaysResultsUITexts[7].text = string.Format("₱ {0}", playerRent[0].ToString("0.00"));
                yesterdaysResultsUITexts[8].text = string.Format("₱ {0}", playerMarketing[0].ToString("0.00"));
                yesterdaysResultsUITexts[9].text = string.Format("₱ {0}", playerExpenses[0].ToString("0.00"));
                yesterdaysResultsUITexts[10].text = string.Format("₱ {0}", playerEarnings[0].ToString("0.00"));

                standingUIImage.sprite = GetStandingImage();
                standingUIText.text = GetStandingText();
                customerSatisfactionAndMissedSalesUIText.text = string.Format("Customer satisfaction: {0}%\nYou missed {1} sale(s).", customerSatisfaction.ToString("0.00"), missedSales);
                customersFeedbackUIText.text = GetCustomersFeedback(playerFeedback);
                iceCubesMeltedUIText.text =
                    playerIceCubesMelted > 0
                    ? string.Format("{0} ice cubes melted.", playerIceCubesMelted)
                    : "";

            }

            if (resultsNavigationState == ResultsNavigationStates.profitAndLoss)
            {

                profitAndLossCurrentDateUIText.text = string.Format("Current (Year {0} / Month {1})", playerDate[0].ToString("00"), playerDate[1].ToString("00"));

                for (int record = 1; record < 4; record++)
                    
                    grossMargin[record] = playerGrossMargin[record] * 100;

                currentProfitAndLossUITexts[0].text = string.Format("₱ {0}", playerRevenue[1].ToString("0.00"));
                currentProfitAndLossUITexts[1].text = string.Format("₱ {0}", playerStockUsed[1].ToString("0.00"));
                currentProfitAndLossUITexts[2].text = string.Format("₱ {0}", playerStockLost[1].ToString("0.00"));
                currentProfitAndLossUITexts[3].text = string.Format("₱ {0}", playerGrossProfit[1].ToString("0.00"));
                currentProfitAndLossUITexts[4].text = string.Format("{0}%", grossMargin[1].ToString("0.00"));
                currentProfitAndLossUITexts[5].text = string.Format("₱ {0}", playerRent[1].ToString("0.00"));
                currentProfitAndLossUITexts[6].text = string.Format("₱ {0}", playerMarketing[1].ToString("0.00"));
                currentProfitAndLossUITexts[7].text = string.Format("₱ {0}", playerExpenses[1].ToString("0.00"));
                currentProfitAndLossUITexts[8].text = string.Format("₱ {0}", playerEarnings[1].ToString("0.00"));

                lastProfitAndLossUITexts[0].text = string.Format("₱ {0}", playerRevenue[2].ToString("0.00"));
                lastProfitAndLossUITexts[1].text = string.Format("₱ {0}", playerStockUsed[2].ToString("0.00"));
                lastProfitAndLossUITexts[2].text = string.Format("₱ {0}", playerStockLost[2].ToString("0.00"));
                lastProfitAndLossUITexts[3].text = string.Format("₱ {0}", playerGrossProfit[2].ToString("0.00"));
                lastProfitAndLossUITexts[4].text = string.Format("{0}%", grossMargin[2].ToString("0.00"));
                lastProfitAndLossUITexts[5].text = string.Format("₱ {0}", playerRent[2].ToString("0.00"));
                lastProfitAndLossUITexts[6].text = string.Format("₱ {0}", playerMarketing[2].ToString("0.00"));
                lastProfitAndLossUITexts[7].text = string.Format("₱ {0}", playerExpenses[2].ToString("0.00"));
                lastProfitAndLossUITexts[8].text = string.Format("₱ {0}", playerEarnings[2].ToString("0.00"));

                bestProfitAndLossUITexts[0].text = string.Format("₱ {0}", playerRevenue[3].ToString("0.00"));
                bestProfitAndLossUITexts[1].text = string.Format("₱ {0}", playerStockUsed[3].ToString("0.00"));
                bestProfitAndLossUITexts[2].text = string.Format("₱ {0}", playerStockLost[3].ToString("0.00"));
                bestProfitAndLossUITexts[3].text = string.Format("₱ {0}", playerGrossProfit[3].ToString("0.00"));
                bestProfitAndLossUITexts[4].text = string.Format("{0}%", grossMargin[3].ToString("0.00"));
                bestProfitAndLossUITexts[5].text = string.Format("₱ {0}", playerRent[3].ToString("0.00"));
                bestProfitAndLossUITexts[6].text = string.Format("₱ {0}", playerMarketing[3].ToString("0.00"));
                bestProfitAndLossUITexts[7].text = string.Format("₱ {0}", playerExpenses[3].ToString("0.00"));
                bestProfitAndLossUITexts[8].text = string.Format("₱ {0}", playerEarnings[3].ToString("0.00"));

            }

            if (resultsNavigationState == ResultsNavigationStates.balanceSheet)
            {

                double stock = GetStock();
                double assets = playerCapital + stock + playerEquipments;
                double shareCapital = 1000.00;
                double equity = shareCapital + playerProfitAndLoss;

                balanceSheetUITexts[0].text = string.Format("₱ {0}", playerCapital.ToString("0.00"));
                balanceSheetUITexts[1].text = string.Format("₱ {0}", stock.ToString("0.00"));
                balanceSheetUITexts[2].text = string.Format("₱ {0}", playerEquipments.ToString("0.00"));
                balanceSheetUITexts[3].text = string.Format("₱ {0}", assets.ToString("0.00"));
                balanceSheetUITexts[4].text = string.Format("₱ {0}", shareCapital.ToString("0.00"));
                balanceSheetUITexts[5].text = string.Format("₱ {0}", playerProfitAndLoss.ToString("0.00"));
                balanceSheetUITexts[6].text = string.Format("₱ {0}", equity.ToString("0.00"));

            }

        }

        if (bottomNavigationState == BottomNavigationStates.location)
        {

            playerCapital = FindObjectOfType<Player>().PlayerCapital;
            double rentExpense = LOCATION[locationState, 1];
            bool isAffordable = playerCapital - rentExpense >= 0;

            if (isAffordable 
                && playerLocation != locationState)

                playerCapital -= rentExpense;

            locationUIImage.sprite = locationSprites[locationState];
            locationUITexts[0].text = LOCATION_TEXT[locationState, 0];
            locationUITexts[1].text = LOCATION_TEXT[locationState, 1];
            locationUITexts[2].text =
                locationState != 0
                ? string.Format("₱ {0}", LOCATION[locationState, 1].ToString("0.00"))
                : "FREE";
            locationUITexts[2].color =
                isAffordable
                ? Color.green
                : Color.red;
            locationFillUIImages[0].fillAmount = (float)playerPopularity[locationState];
            locationFillUIImages[1].fillAmount = (float)playerSatisfaction[locationState];
            previousUIButtons[0].interactable = locationState > 0;
            nextUIButtons[0].interactable = locationState < 10;
            rentUIButton.interactable = playerLocation != locationState;
            isNotRentableHUD.SetActive(!isAffordable);

            if (SimpleInput.GetButtonDown("OnPrevious"))

                OnLocationPrevious();

            if (SimpleInput.GetButtonDown("OnNext"))

                OnLocationNext();

            if (SimpleInput.GetButtonDown("OnRent"))
            {

                if (playerLocation == locationState)

                    FindObjectOfType<SoundsManager>().OnError();

                else if (!isAffordable)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "You've insufficient money to rent this place",
                        "dialog");

                }
                else
                {

                    string description = string.Format(
                        locationState != 0
                        ? "Are you sure you want to rent this place for\n₱ {0}?"
                        : "Are you sure you want to go back to your own neighborhood? The rent is free.", LOCATION[locationState, 1].ToString("0.00"));

                    FindObjectOfType<SoundsManager>().OnClicked();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "RENTING",
                        description,
                        "optionPane1");
                    isRenting = true;
                    FindObjectOfType<SettingsMenu>().IsEnabled = false;

                }

            }

        }

        if (bottomNavigationState == BottomNavigationStates.upgrades)
        {

            bool isAffordable;
            bool isMaxLevel = playerUpgrade[upgradeState] == 5 
                && upgradeState != 2;
            string level =
                isMaxLevel
                ? "Lv. {0}"
                : "Lv. {0} -> Lv. {1}";

            upgradeUITexts[0].text = UPGRADE_TEXT[upgradeState, 0];
            upgradeUITexts[1].text = UPGRADE_TEXT[upgradeState, 1];
            upgradeUITexts[3].text = string.Format(level, playerUpgrade[upgradeState], playerUpgrade[upgradeState] + 1);
            upgradeUIImage.sprite = upgradeSprites[upgradeState];
            previousUIButtons[1].interactable = upgradeState > 0;
            nextUIButtons[1].interactable = upgradeState < 2;

            if (upgradeState == 2)
            {

                spend = UPGRADE[2, 0, 0] + ((playerUpgrade[2] + 1) * 5000);
                upgradeLevelFillUIImage.fillAmount =
                    playerUpgrade[2] > 0
                    ? 1
                    : 0;

            }
            else
            {

                spend =
                    !isMaxLevel
                    ? UPGRADE[upgradeState, playerUpgrade[upgradeState] + 1, 0]
                    : 0;

                upgradeLevelFillUIImage.fillAmount = (float)playerUpgrade[upgradeState] / 5;

            }

            upgradeUITexts[2].text = string.Format(
                !isMaxLevel
                ? "₱ {0}"
                : "₱ {1}", spend.ToString("0.00"), UPGRADE[upgradeState, playerUpgrade[upgradeState], 0].ToString("0.00"));

            playerCapital = FindObjectOfType<Player>().PlayerCapital;
            isAffordable = playerCapital - spend >= 0;

            if (isAffordable && !isMaxLevel)

                playerCapital -= spend;

            upgradeUIButton.interactable = !isMaxLevel;
            upgradeUITexts[2].color =
                isAffordable
                ? Color.green
                : Color.red;

            if (SimpleInput.GetButtonDown("OnPrevious"))

                OnPrevious(upgradeState);

            if (SimpleInput.GetButtonDown("OnNext"))

                OnNext(upgradeState);

            if (SimpleInput.GetButtonDown("OnUpgrade"))
            {

                if (playerUpgrade[upgradeState] == 5
                    && upgradeState != 2)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "You've already reached the maximum level of this item",
                        "dialog");

                }
                else if (!isAffordable)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "You've insufficient money to upgrade this item",
                        "dialog");

                }
                else
                {

                    spend = FindObjectOfType<Player>().PlayerCapital - playerCapital;
                    string description = string.Format("Are you sure you want to spend\n₱ {0} on upgrades?", spend.ToString("0.00"));
                    FindObjectOfType<SoundsManager>().OnClicked();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "UPGRADING",
                        description,
                        "optionPane1");
                    isUpgrading = true;
                    FindObjectOfType<SettingsMenu>().IsEnabled = false;

                }

            }

        }

        if (bottomNavigationState == BottomNavigationStates.staff)
        {

            bool isAffordable;
            bool isHired = playerStaffs.Contains(staffState);
            hireAndFireUIButton.isOn = isHired;
            spend = STAFF[staffState, 0];
            playerCapital = FindObjectOfType<Player>().PlayerCapital;
            isAffordable = playerCapital - spend >= 0;

            staffUITexts[0].text = STAFF_TEXT[staffState, 0];
            staffUITexts[1].text = STAFF_TEXT[staffState, 1];
            staffUIImage.sprite = staffSprites[staffState];
            previousUIButtons[2].interactable = staffState > 0;
            nextUIButtons[2].interactable = staffState < 2;
            staffUITexts[2].text = string.Format("₱ {0}", spend.ToString("0.00"));
            staffUITexts[2].color =
                isAffordable
                ? Color.green
                : Color.red;

            if (isAffordable 
                && !isHired)

                playerCapital -= spend;

            hireAndFireUIButton.image.sprite =
                SimpleInput.GetButtonDown("OnHireAndFire")
                ? isHired
                    ? hireAndFireSprites[0]
                    : hireAndFireSprites[1]
                : isHired
                    ? hireAndFireSprites[2]
                    : hireAndFireSprites[3];

            if (SimpleInput.GetButtonDown("OnPrevious"))

                OnPrevious(staffState);

            if (SimpleInput.GetButtonDown("OnNext"))

                OnNext(staffState);

            if (SimpleInput.GetButtonUp("OnHireAndFire"))
            {

                if (isHired) 
                    
                    playerStaffs.Remove(staffState);

                else if (isAffordable)

                    playerStaffs.Add(staffState);

                else
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "You've insufficient money to hire the staff",
                        "dialog");
                    return;

                }

                FindObjectOfType<SoundsManager>().OnClicked();

            }

        }

        if (bottomNavigationState == BottomNavigationStates.marketing)
        {

            double costPerCup = GetCostPerCup();
            double profitPerCup = playerPrice - costPerCup;
            profitPerCup =
                profitPerCup > 0
                ? profitPerCup
                : 0;

            FindObjectOfType<Player>().PlayerPrice = playerPrice;
            FindObjectOfType<Player>().PlayerCostPerCup = costPerCup;

            priceUIText.text = string.Format("₱ {0}", playerPrice.ToString("0.00"));
            profitPerCupUIText.text = string.Format("Profit Per Cup:\n₱ {0}", profitPerCup.ToString("0.00"));
            priceDecrementUIButton.interactable = playerPrice > 0;
            priceIncrementUIButton.interactable = playerPrice < MAXIMUM_PRICE;
            priceResetUIButton.interactable = playerPrice != DEFAULT_PRICE;

            if (SimpleInput.GetButtonDown("OnDecrementPrice"))

                OnPriceDecrement();

            if (SimpleInput.GetButtonDown("OnIncrementPrice"))

                OnPriceIncrement();

            if (SimpleInput.GetButtonDown("OnResetPrice"))

                OnPriceReset();
            
            FindObjectOfType<Player>().PlayerAdvertisement = playerAdvertisement;

            playerCapital = FindObjectOfType<Player>().PlayerCapital;
            double advertisementExpense = LOCATION[playerLocation, 0] * ADVERTISEMENT[playerAdvertisement, 0];
            bool isAffordable = playerCapital - advertisementExpense >= 0;

            if (isAffordable)

                playerCapital -= advertisementExpense;

            advertisementUIText.text = string.Format("₱ {0}", advertisementExpense.ToString("0.00"));
            advertisementDecrementUIButton.interactable = playerAdvertisement > 0;
            advertisementIncrementUIButton.interactable = IsAdvertisementIncrementable();
            advertisementResetUIButton.interactable = playerAdvertisement > 0;

            if (SimpleInput.GetButtonDown("OnDecrementAdvertisement"))

                OnAdvertisementDecrement();

            if (SimpleInput.GetButtonDown("OnIncrementAdvertisement"))

                OnAdvertisementIncrement();

            if (SimpleInput.GetButtonDown("OnResetAdvertisement"))

                OnAdvertisementReset();

        }

        if (bottomNavigationState == BottomNavigationStates.recipe)
        {

            FindObjectOfType<Player>().PlayerRecipe = playerRecipe;

            for (int recipe = 0; recipe < 4; recipe++)
            {

                recipeQuantityUITexts[recipe].text = playerRecipe[recipe].ToString();
                recipeDecrementUIButtons[recipe].interactable = playerRecipe[recipe] > 0;
                recipeResetUIButtons[recipe].interactable = playerRecipe[recipe] != DEFAULT_RECIPE[recipe];

            }

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

        if (bottomNavigationState == BottomNavigationStates.supplies)
        {

            string conjunctions = GetConjuctions(suppliesState);

            for (int scale = 0; scale < 3; scale++)
            {

                supplyUIImages[scale].sprite = supplySprites[suppliesState];
                supplyPriceUITexts[scale].text = string.Format(
                    "{0} {1} {2}",
                    SUPPLIES[suppliesState, 0, scale].ToString(),
                    conjunctions,
                    SUPPLIES[suppliesState, 1, scale].ToString("0.00")
                    );
                supplyQuantityUITexts[scale].text = supplies[suppliesState, scale].ToString();
                supplyDecrementUIButtons[scale].interactable = supplies[suppliesState, scale] > 0;
                supplyIncrementUIButtons[scale].interactable = playerCapital - SUPPLIES[suppliesState, 1, scale] >= 0
                    && HasAvailableSpace(scale);

            }

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
                    isCanceling = true;
                    FindObjectOfType<SettingsMenu>().IsEnabled = false;

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
                    isBuying = true;
                    FindObjectOfType<SettingsMenu>().IsEnabled = false;

                }

            }

        }

        if (SimpleInput.GetButtonDown("OnStartDay"))
        {

            double advertisementExpense = LOCATION[playerLocation, 0] * ADVERTISEMENT[playerAdvertisement, 0];
            double rentExpense = LOCATION[playerLocation, 1] + GetStaffExpense();
            bool isRentUnaffordable = playerCapital - rentExpense < 0;
            bool isAdvertisementUnaffordable = playerCapital - advertisementExpense < 0;

            if (playerSupplies[0] < playerRecipe[0]
                || playerSupplies[1] < playerRecipe[1]
                || playerSupplies[2] < playerRecipe[2]
                || iceCubes < playerRecipe[3]
                || playerSupplies[4] < 1)

                FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Not enough supplies to start the day. Change your recipe or buy more supplies.",
                    "dialog");

            else if (isRentUnaffordable)

                FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "You don't have enough money to pay for your rent. Move to a less expensive place or go back to The Home, or fire your staff.",
                    "dialog");

            else if (isAdvertisementUnaffordable)

                FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "You don't have enough money to pay for your advertisement. Lower your advertising budget.",
                    "dialog");

            else
            {

                OnStartDay(advertisementExpense, rentExpense);
                return;

            }

            FindObjectOfType<SoundsManager>().OnError();

        }

        if (SimpleInput.GetButtonDown("OnYes")
            && IsEnabled)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");

            if (isCanceling)
            {

                OnCancel();
                isCanceling = false;
                FindObjectOfType<SettingsMenu>().IsEnabled = true;

            }
            else if (isBuying)
            {

                OnBuySuccess();
                isBuying = false;
                FindObjectOfType<SettingsMenu>().IsEnabled = true;

            }
            else if (isRenting)
            {

                OnRentSuccess();
                isRenting = false;
                FindObjectOfType<SettingsMenu>().IsEnabled = true;

            }
            else if (isUpgrading)
            {

                OnUpgradeSuccess();
                isUpgrading = false;
                FindObjectOfType<SettingsMenu>().IsEnabled = true;

            }

        }

        if (SimpleInput.GetButtonDown("OnNo") 
            && IsEnabled)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            Init();

        }

    }

    private void Init()
    {

        isBuying = false;
        isCanceling = false;
        isRenting = false;
        isUpgrading = false;
        FindObjectOfType<SettingsMenu>().IsEnabled = true;

    }

    private void InitState()
    {

        locationState = playerLocation;
        upgradeState = 0;
        staffState = 0;
        suppliesState = 0;
        spend = 0;
        resultsNavigationState = ResultsNavigationStates.yesterdaysResults;
        yesterdaysResultsUINavButton.isOn = true;
        mangoUINavButton.isOn = true;

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

        InitState();
        OnSuppliesQuantityClear();
        OnCancel();

    }

    private BottomNavigationStates GetBottomNavigationState(string _navigation) => _navigation switch
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

    private string GetBottomNavigationStateText(string _bottomNavigation) => _bottomNavigation switch
    {

        "LocationUINavButton" => "Location",

        "UpgradesUINavButton" => "Upgrades",

        "StaffUINavButton" => "Staff",

        "MarketingUINavButton" => "Marketing",

        "RecipeUINavButton" => "Recipe",

        "SuppliesUINavButton" => "Supplies",

        _ => "",

    };

    private void OnSuppliesNavigation(int _suppliesState)
    {

        FindObjectOfType<SoundsManager>().OnClicked();
        suppliesState = _suppliesState;

    }

    private string GetConjuctions(int _supply) => _supply switch
    {

        0 => "mangoes = ₱",

        1 => "pieces = ₱",

        2 => "cans = ₱",

        3 => "cubes = ₱",

        _ => "cups = ₱",

    };

    private void OnSuppliesQuantityClear()
    {

        for (int supply = 0; supply < 5; supply++)

            for (int scale = 0; scale < 3; scale++)

                supplies[supply, scale] = 0;

    }

    private void OnSuppliesDecrement(int _scale)
    {

        int quantityPerPrice = (int) SUPPLIES[suppliesState, 0, _scale];
        double price = SUPPLIES[suppliesState, 1, _scale];
        bool isDecrementable = supplies[suppliesState, _scale] - quantityPerPrice >= 0;
        
        if (isDecrementable)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            supplies[suppliesState, _scale] -= quantityPerPrice;
            playerCapital += price;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnSuppliesIncrement(int _scale)
    {

        int quantityPerPrice = (int) SUPPLIES[suppliesState, 0, _scale];
        double price = SUPPLIES[suppliesState, 1, _scale];
        bool isIncrementable = playerCapital - price >= 0;

        if (isIncrementable)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            supplies[suppliesState, _scale] += quantityPerPrice;
            playerCapital -= price;
            return;

        }
        else if (!HasAvailableSpace(_scale))

            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've insufficient storage to store this item",
                "dialog");

        else

            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've insufficient money to increment this item",
                "dialog");

        FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnCancel()
    {

        OnSuppliesQuantityClear();
        playerCapital = FindObjectOfType<Player>().PlayerCapital;
        playerAdvertisement = FindObjectOfType<Player>().PlayerAdvertisement;
        playerLocation = FindObjectOfType<Player>().PlayerLocation;
        playerUpgrade = FindObjectOfType<Player>().PlayerUpgrade;
        playerStaffs = FindObjectOfType<Player>().PlayerStaffs;

    }

    private async void OnBuySuccess()
    {

        FindObjectOfType<Player>().PlayerCapital -= spend;

        for (int supply = 0; supply < 5; supply++)

            for (int scale = 0; scale < 3; scale++)

                playerSupplies[supply] += supplies[supply, scale];

        await Task.Delay(1000);

        Init();
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

    private bool IsAdvertisementIncrementable()
    {

        if (playerAdvertisement < 10)
        {

            double advertisementExpense = LOCATION[playerLocation, 0] * ADVERTISEMENT[playerAdvertisement + 1, 0];
            return playerCapital - advertisementExpense >= 0;

        }

        return false;

    }

    private void OnAdvertisementIncrement()
    {

        if (IsAdvertisementIncrementable())
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            playerAdvertisement++;
            return;

        }
        else if (playerAdvertisement == 10)

            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've already reached the maximum advertisement",
                "dialog");

        else

            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've insufficient money to avail this advertisement",
                "dialog");

        FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnAdvertisementDecrement()
    {

        if (playerAdvertisement > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            playerAdvertisement--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

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

        if (_recipe == 0 
            && playerRecipe[_recipe] != DEFAULT_RECIPE[0])

            playerRecipe[_recipe] = DEFAULT_RECIPE[0];

        else if (_recipe == 1 
            && playerRecipe[_recipe] != DEFAULT_RECIPE[1])

            playerRecipe[_recipe] = DEFAULT_RECIPE[1];

        else if (_recipe == 2 
            && playerRecipe[_recipe] != DEFAULT_RECIPE[2])

            playerRecipe[_recipe] = DEFAULT_RECIPE[2];

        else if (_recipe == 3 
            && playerRecipe[_recipe] != DEFAULT_RECIPE[3])

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

        if (_temperature >= TEMPERATURE[0, 0] 
            && _temperature <= TEMPERATURE[1, 1])

            return temperatureSprites[0];

        else if (_temperature >= TEMPERATURE[3, 0] 
            && _temperature <= TEMPERATURE[4, 1])

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

        double costPerCup = cost / cupsPerPitcher;

        return costPerCup;

    }

    private void OnStartDay(double _advertisementExpense, double _rentExpense)
    {

        FindObjectOfType<Player>().PlayerTopEarnings =
            playerEarnings[0] > playerTopEarnings
            ? playerEarnings[0]
            : playerTopEarnings;
        FindObjectOfType<Player>().PlayerSupplies[3] = iceCubes;
        FindObjectOfType<Player>().PlayerCapital -= (_advertisementExpense + _rentExpense);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void OnResultsNavigation()
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        string navigation = FindObjectOfType<GameManager>().GetToggleName(resultsNavigationUIPanel);
        resultsNavigationState = GetResultsNavigationState(navigation);

    }

    private ResultsNavigationStates GetResultsNavigationState(string _navigation) => _navigation switch
    {

        "YesterdaysPerformanceAndSettingUINavButton" => ResultsNavigationStates.yesterdaysPerformanceAndSettings,

        "YesterdaysResultsUINavButton" => ResultsNavigationStates.yesterdaysResults,

        "ChartsUINavButton" => ResultsNavigationStates.charts,

        "ProfitAndLossUINavButton" => ResultsNavigationStates.profitAndLoss,

        _ => ResultsNavigationStates.balanceSheet,

    };

    private string GetResultsNavigationStateText(string _resultsNavigation) => _resultsNavigation switch
    {

        "YesterdaysPerformanceAndSettingUINavButton" => "Yesterday's Performance & Setting",

        "YesterdaysResultsUINavButton" => "Yesterday's Results",

        "ChartsUINavButton" => "Charts",

        "ProfitAndLossUINavButton" => "Profit & Loss",

        _ => "Balance Sheet"

    };

    private void GetStorage()
    {

        for (int supply = 0; supply < playerStorage.Length; supply++)
        {

            float supplies = 
                supply == 3 
                ? iceCubes 
                : playerSupplies[supply];

            suppliesUIImages[supply].fillAmount = supplies / playerStorage[supply];
            suppliesUITexts[supply].text = supplies.ToString();

        }

    }

    private bool HasAvailableSpace(int _scale)
    {

        int overAllSupplies = supplies[suppliesState, 0] + supplies[suppliesState, 1] + supplies[suppliesState, 2];
        int iceCubes = 
            suppliesState == 3
            ? (int)UPGRADE[1, playerUpgrade[1], 1]
            : 0;
        bool hasAvailableSpace = playerSupplies[suppliesState] + overAllSupplies + SUPPLIES[suppliesState, 0, _scale] + iceCubes <= playerStorage[suppliesState];

        return hasAvailableSpace;

    }

    private int[] GetYesterdaysDate(int[] _playerDate)
    {

        _playerDate[2]--;

        if (_playerDate[2] == 0)
        {

            _playerDate[2] = 31;
            _playerDate[1]--;

        }
        if (_playerDate[1] == 0)
        {

            _playerDate[1] = 1;
            _playerDate[0]--;

        }

        return _playerDate;

    }

    private Sprite GetStandingImage()
    {

        if (playerEarnings[0] > playerTopEarnings
            && playerCustomerSatisfaction >= STANDING[0, 0]
            && playerCustomerSatisfaction <= STANDING[0, 1])

            return standingSprites[0];

        else if (playerEarnings[0] > playerTopEarnings
            && playerCustomerSatisfaction >= STANDING[1, 0]
            && playerCustomerSatisfaction <= STANDING[1, 1])

            return standingSprites[1];

        else if (playerEarnings[0] > playerTopEarnings
            && playerCustomerSatisfaction >= STANDING[2, 0]
            && playerCustomerSatisfaction <= STANDING[2, 1])

            return standingSprites[2];

        else if (playerEarnings[0] > 0)

            return standingSprites[3];

        return standingSprites[4];

    }

    private string GetStandingText()
    {

        if (playerEarnings[0] > playerTopEarnings)

            return "Congratulations!\nA new profit record!";

        else if (playerEarnings[0] > 0)

            return "Keep up the good\nwork!";

        return "Is that the best you\ncan do?";

    }

    private string GetCustomersFeedback(int _feedback) => _feedback switch
    {

        1 => "You went out of stock!",

        2 => "Customers complained about\nyour recipe.",

        3 => "Customers complained about\nyour pricing.",

        4 => "Customers complained about long\nserving times.",

        5 => "Find a way to attract more\ncustomers to your stand.",

        _ => "",

    };

    private double GetStock()
    {

        suppliesCostPerStock[0] = AVERAGE_SUPPLIES_COST[0] * playerSupplies[0];
        suppliesCostPerStock[1] = AVERAGE_SUPPLIES_COST[1] * playerSupplies[1];
        suppliesCostPerStock[2] = AVERAGE_SUPPLIES_COST[2] * playerSupplies[2];
        suppliesCostPerStock[3] = AVERAGE_SUPPLIES_COST[3] * iceCubes;
        suppliesCostPerStock[4] = AVERAGE_SUPPLIES_COST[4] * playerSupplies[4];

        double stock = suppliesCostPerStock[0]
            + suppliesCostPerStock[1]
            + suppliesCostPerStock[2]
            + suppliesCostPerStock[3]
            + suppliesCostPerStock[4];

        return stock;

    }

    private void OnLocationPrevious()
    {

        if (locationState > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            locationState--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnLocationNext()
    {

        if (locationState < 10)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            locationState++;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnRentSuccess()
    {

        FindObjectOfType<Player>().PlayerLocation = locationState;

        Init();
        OnCancel();

        FindObjectOfType<Player>().OnAutoSave(isConnected);

    }

    private void OnPrevious(int _state)
    {

        if (_state > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            if (bottomNavigationState == BottomNavigationStates.upgrades)

                upgradeState--;

            else if (bottomNavigationState == BottomNavigationStates.staff)

                staffState--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnNext(int _state)
    {

        if (_state < 2)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            
            if (bottomNavigationState == BottomNavigationStates.upgrades)

                upgradeState++;
            
            else if (bottomNavigationState == BottomNavigationStates.staff)

                staffState++;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnUpgradeSuccess()
    {

        if (upgradeState == 2)

            OnUpgradeStorage();

        FindObjectOfType<Player>().PlayerCapital -= spend;
        FindObjectOfType<Player>().PlayerEquipments += spend;
        playerUpgrade[upgradeState]++;
        FindObjectOfType<Player>().PlayerUpgrade = playerUpgrade;

        Init();
        OnCancel();

        FindObjectOfType<Player>().OnAutoSave(isConnected);

    }

    private void OnUpgradeStorage()
    {

        for (int i = 0; i < playerStorage.Length; i++)

            playerStorage[i] += STORAGE[i];

        FindObjectOfType<Player>().PlayerStorage = playerStorage;

    }

    private double GetStaffExpense()
    {

        double staffExpense = 0;

        foreach (int staff in playerStaffs)

            staffExpense += STAFF[staff, 0];

        return staffExpense;

    }

    public bool IsEnabled { private get; set; }

}