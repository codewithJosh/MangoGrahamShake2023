using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreparationPhaseManager : MonoBehaviour
{

    #region DECLARATION @BOTTOM NAVIGATION

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

    #endregion

    #region DECLARATION @IDLE SECTION

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

    #endregion

    #region DECLARATION @RESULTS SECTION

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

    #endregion

    #region DECLARATION @RESULTS SECTION @YESTERDAYS RESULTS

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

    #endregion

    #region DECLARATION @RESULTS SECTION @PROFIT AND LOSS

    [Header("RESULTS SECTION @PROFIT AND LOSS")]
    [SerializeField]
    private TextMeshProUGUI[] currentProfitAndLossUITexts;

    [SerializeField]
    private TextMeshProUGUI[] lastProfitAndLossUITexts;

    [SerializeField]
    private TextMeshProUGUI[] bestProfitAndLossUITexts;

    [SerializeField]
    private TextMeshProUGUI profitAndLossCurrentDateUIText;

    #endregion

    #region DECLARATION @RESULTS SECTION @BALANCE SHEET

    [Header("RESULTS SECTION @BALANCE SHEET")]
    [SerializeField]
    private TextMeshProUGUI[] balanceSheetUITexts;

    #endregion

    #region DECLARATION @LOCATION SECTION

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

    #endregion

    #region DECLARATION @UPGRADES SECTION

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

    #endregion

    #region DECLARATION @STAFF SECTION

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

    #endregion

    #region DECLARATION @MARKETING SECTION @PRICE

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

    #endregion

    #region DECLARATION @MARKETING SECTION @ADVERTISEMENT

    [Header("MARKETING SECTION @ADVERTISEMENT")]
    [SerializeField]
    private Button advertisementDecrementUIButton;

    [SerializeField]
    private Button advertisementIncrementUIButton;

    [SerializeField]
    private Button advertisementResetUIButton;

    [SerializeField]
    private TextMeshProUGUI advertisementUIText;

    #endregion

    #region DECLARATION @RECIPE SECTION

    [Header("RECIPE SECTION")]
    [SerializeField]
    private Button[] recipeDecrementUIButtons;

    [SerializeField]
    private Button[] recipeResetUIButtons;

    [SerializeField]
    private TextMeshProUGUI cupsPerPitcherUIText;

    [SerializeField]
    private TextMeshProUGUI[] recipeQuantityUITexts;

    #endregion

    #region DECLARATION @SUPPLIES SECTION

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

    #endregion

    #region DECLARATION

    private enum BOTTOM_NAVIGATION_STATES { IDLE, RESULTS, LOCATION, UPGRADES, STAFF, MARKETING, RECIPE, SUPPLIES };
    private enum RESTULS_NAVIGATION_STATES { YESTERDAYS_PERFORMANCE_AND_SETTINGS, YESTERDAYS_RESULTS, CHARTS, PROFIT_AND_LOSS, BALANCE_SHEET };

    private BOTTOM_NAVIGATION_STATES BOTTOM_NAVIGATION_STATE;
    private BOTTOM_NAVIGATION_STATES LAST_BOTTOM_NAVIGATION_STATE;
    private RESTULS_NAVIGATION_STATES RESTULS_NAVIGATION_STATE;

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
    private int playerTip;
    private int[] playerDate;
    private int[] playerRecipe;
    private int[] playerStorage;
    private int[] playerSupplies;
    private int[] playerUpgrade;
    private List<int> playerStaffs;

    private bool isTipping;
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

    #endregion

    #region START_METHOD

    void Start()
    {

        STATUS.STATE = STATUS.STATES.IDLE;

        isTipping = true;
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

        playerAdvertisement = FindObjectOfType<PLAYER>().PlayerAdvertisement;
        playerCapital = FindObjectOfType<PLAYER>().PlayerCapital;
        playerCupsSold = FindObjectOfType<PLAYER>().PlayerCupsSold;
        playerCustomerSatisfaction = FindObjectOfType<PLAYER>().PlayerCustomerSatisfaction;
        playerDate = FindObjectOfType<PLAYER>().PlayerDate;
        playerEarnings = FindObjectOfType<PLAYER>().PlayerEarnings;
        playerEquipments = FindObjectOfType<PLAYER>().PlayerEquipments;
        playerExpenses = FindObjectOfType<PLAYER>().PlayerExpenses;
        playerFeedback = FindObjectOfType<PLAYER>().PlayerFeedback;
        playerGrossMargin = FindObjectOfType<PLAYER>().PlayerGrossMargin;
        playerGrossProfit = FindObjectOfType<PLAYER>().PlayerGrossProfit;
        playerIceCubesMelted = FindObjectOfType<PLAYER>().PlayerIceCubesMelted;
        playerImpatientCustomers = FindObjectOfType<PLAYER>().PlayerImpatientCustomers;
        playerLocation = FindObjectOfType<PLAYER>().PlayerLocation;
        playerMarketing = FindObjectOfType<PLAYER>().PlayerMarketing;
        playerOverPricedCustomers = FindObjectOfType<PLAYER>().PlayerOverPricedCustomers;
        playerPopularity = FindObjectOfType<PLAYER>().PlayerPopularity;
        playerPrice = FindObjectOfType<PLAYER>().PlayerPrice;
        playerProfitAndLoss = FindObjectOfType<PLAYER>().PlayerProfitAndLoss;
        playerRecipe = FindObjectOfType<PLAYER>().PlayerRecipe;
        playerRent = FindObjectOfType<PLAYER>().PlayerRent;
        playerRevenue = FindObjectOfType<PLAYER>().PlayerRevenue;
        playerSatisfaction = FindObjectOfType<PLAYER>().PlayerSatisfaction;
        playerSatisfiedCustomers = FindObjectOfType<PLAYER>().PlayerSatisfiedCustomers;
        playerStaffs = FindObjectOfType<PLAYER>().PlayerStaffs;
        playerStockLost = FindObjectOfType<PLAYER>().PlayerStockLost;
        playerStockUsed = FindObjectOfType<PLAYER>().PlayerStockUsed;
        playerStorage = FindObjectOfType<PLAYER>().PlayerStorage;
        playerSupplies = FindObjectOfType<PLAYER>().PlayerSupplies;
        playerTemperature = FindObjectOfType<PLAYER>().PlayerTemperature;
        playerTopEarnings = FindObjectOfType<PLAYER>().PlayerTopEarnings;
        playerUnsatisfiedCustomers = FindObjectOfType<PLAYER>().PlayerUnsatisfiedCustomers;
        playerUpgrade = FindObjectOfType<PLAYER>().PlayerUpgrade;
        playerTip = FindObjectOfType<PLAYER>().PlayerTip;

        temperatureUIImage.sprite = GetTemperatureSprite(playerTemperature);
        dailyUITexts[0].text = $"{playerDate[0]:00} - {playerDate[1]:00} - {playerDate[2]:00}";
        dailyUITexts[1].text = $"{playerTemperature:0.0}°";

        lastRent = ENV.LOCATION_TEXT[playerLocation, 0];
        lastAdvertisement = ENV.LOCATION[playerLocation, 0] * ENV.ADVERTISEMENT[playerAdvertisement, 0];
        lastPrice = playerPrice;
        lastRecipe = (int[])playerRecipe.Clone();
        lastDate = (int[])playerDate.Clone();
        lastDate = GetYesterdaysDate(lastDate);
        missedSales = playerImpatientCustomers + playerOverPricedCustomers;

        InitState();

    }

    #endregion

    #region UPDATE_METHOD

    void Update()
    {

        #region IDLE_SECTION

        FindObjectOfType<PLAYER>().PlayerCupsPerPitcher = cupsPerPitcher;

        locationHUD.sprite = locationSprites[playerLocation];
        popularityUIImage.fillAmount = (float)playerPopularity[playerLocation];
        satisfactionUIImage.fillAmount = (float)playerSatisfaction[playerLocation];
        currentLocationUITexts[0].text = ENV.LOCATION_TEXT[playerLocation, 0];
        currentLocationUITexts[1].text = ENV.LOCATION_TEXT[playerLocation, 2];
        dailyUITexts[2].text = $"{playerCapital:0.00}";
        iceCubes = playerSupplies[3] + (int)ENV.UPGRADE[1, playerUpgrade[1], 1];
        GetStorage();
        OnTip();

        string bottomNavigationStateText =
            BOTTOM_NAVIGATION_STATE != BOTTOM_NAVIGATION_STATES.RESULTS
            ? GetBottomNavigationStateText(GameManager.GetToggleName(bottomNavigationUIPanel))
            : GetResultsNavigationStateText(GameManager.GetToggleName(resultsNavigationUIPanel));

        if (!bottomNavigationStateText.Equals(""))

            bottomNavigationUIText.text = bottomNavigationStateText;

        for (int navigationState = 0; navigationState < 7; navigationState++)

            bottomNavigationUIButtons[navigationState].sprite =
                (int)LAST_BOTTOM_NAVIGATION_STATE - 1 == navigationState
                ? bottomNavigationSelectedUIButtons[navigationState]
                : bottomNavigationNormalUIButtons[navigationState];

        settingsUIButton.alpha =
            LAST_BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.IDLE
            ? 1
            : 0;

        settingsUIButton.blocksRaycasts = LAST_BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.IDLE;

        cupsPerPitcher =
                playerRecipe[3] > ENV.MINIMUM_CUPS
                ? playerRecipe[3]
                : ENV.MINIMUM_CUPS;

        if (SimpleInput.GetButtonUp("OnBottomNavigation"))

            OnBottomNavigation();

        if (SimpleInput.GetButtonDown("OnStartDay"))
        {

            double advertisementExpense = ENV.LOCATION[playerLocation, 0] * ENV.ADVERTISEMENT[playerAdvertisement, 0];
            double rentExpense = ENV.LOCATION[playerLocation, 1] + GetStaffExpense();
            bool isRentUnaffordable = playerCapital - rentExpense < 0;
            bool isAdvertisementUnaffordable = playerCapital - advertisementExpense < 0;

            if (playerSupplies[0] < playerRecipe[0]
                || playerSupplies[1] < playerRecipe[1]
                || playerSupplies[2] < playerRecipe[2]
                || iceCubes < playerRecipe[3]
                || playerSupplies[4] < 1)

                DialogManager.OnDialog(
                    "REQUIRED",
                    "Not enough supplies to start the day. Change your recipe or buy more supplies.",
                    ENV.DIALOG);

            else if (isRentUnaffordable)

                DialogManager.OnDialog(
                    "REQUIRED",
                    "You don't have enough money to pay for your rent. Move to a less expensive place or go back to The Home, or fire your staff.",
                    ENV.DIALOG);

            else if (isAdvertisementUnaffordable)

                DialogManager.OnDialog(
                    "REQUIRED",
                    "You don't have enough money to pay for your advertisement. Lower your advertising budget.",
                    ENV.DIALOG);

            else
            {

                OnStartDay(advertisementExpense, rentExpense);
                return;

            }

            FindObjectOfType<SoundsManager>().OnError();

        }

        if (SimpleInput.GetButtonDown("OnYes")
            && STATUS.STATE != STATUS.STATES.LOG_OUT)
        {

            if (STATUS.STATE == STATUS.STATES.CANCELING)

                OnCancel();

            else if (STATUS.STATE == STATUS.STATES.BUYING)

                OnBuySuccess();

            else if (STATUS.STATE == STATUS.STATES.RENTING)

                OnRentSuccess();

            else if (STATUS.STATE == STATUS.STATES.UPGRADING)

                OnUpgradeSuccess();

            STATUS.STATE = STATUS.STATES.IDLE;

        }

        #endregion

        #region RESULTS_SECTION

        if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.RESULTS)
        {

            if (SimpleInput.GetButtonUp("OnResultsNavigation"))

                OnResultsNavigation();

            for (int navigationState = 0; navigationState < 5; navigationState++)

                resultsUIPanels[navigationState].SetActive((int)RESTULS_NAVIGATION_STATE == navigationState);

            #region RESULTS_SECTION @YESTERDAYS_PERFORMANCE_AND_SETTINGS

            if (RESTULS_NAVIGATION_STATE == RESTULS_NAVIGATION_STATES.YESTERDAYS_PERFORMANCE_AND_SETTINGS)
            {

                performanceCupsSoldUIText.text = playerCupsSold.ToString();
                performanceProfitUIText.text = $"₱ {playerGrossProfit[0]:0.00}";
                unsatisfiedCustomersUIText.text = playerUnsatisfiedCustomers.ToString();
                satisfiedCustomersUIText.text = playerSatisfiedCustomers.ToString();
                impatientCustomersUIText.text = playerImpatientCustomers.ToString();
                overPricedCustomersUIText.text = playerOverPricedCustomers.ToString();
                settingRentUIText.text = lastRent;
                settingAdvertisingUIText.text = $"₱ {lastAdvertisement:0.00}";
                settingPriceUIText.text = $"₱ {lastPrice:0.00}";

                for (int recipe = 0; recipe < 4; recipe++)

                    settingRecipeUIText[recipe].text = lastRecipe[recipe].ToString();

            }

            #endregion

            #region RESULTS_SECTION @YESTERDAYS_RESULTS

            if (RESTULS_NAVIGATION_STATE == RESTULS_NAVIGATION_STATES.YESTERDAYS_RESULTS)
            {

                grossMargin[0] = playerGrossMargin[0] * 100;
                double customerSatisfaction = playerCustomerSatisfaction * 100;

                yesterdaysResultsUITexts[0].text = $"Year {lastDate[0]:00} - Month {lastDate[1]:00} - Day {lastDate[2]:00}";
                yesterdaysResultsUITexts[1].text = $"{playerCupsSold} cups";
                yesterdaysResultsUITexts[2].text = $"₱ {playerRevenue[0]:0.00}";
                yesterdaysResultsUITexts[3].text = $"₱ {playerStockUsed[0]:0.00}";
                yesterdaysResultsUITexts[4].text = $"₱ {playerStockLost[0]:0.00}";
                yesterdaysResultsUITexts[5].text = $"₱ {playerGrossProfit[0]:0.00}";
                yesterdaysResultsUITexts[6].text = $"{grossMargin[0]:0.00}%";
                yesterdaysResultsUITexts[7].text = $"₱ {playerRent[0]:0.00}";
                yesterdaysResultsUITexts[8].text = $"₱ {playerMarketing[0]:0.00}";
                yesterdaysResultsUITexts[9].text = $"₱ {playerExpenses[0]:0.00}";
                yesterdaysResultsUITexts[10].text = $"₱ {playerEarnings[0]:0.00}";

                standingUIImage.sprite = GetStandingImage();
                standingUIText.text = GetStandingText();
                customerSatisfactionAndMissedSalesUIText.text = $"Customer satisfaction: {customerSatisfaction:0.00}%\nYou missed {missedSales} sale(s).";
                customersFeedbackUIText.text = GetCustomersFeedback(playerFeedback);
                iceCubesMeltedUIText.text =
                    playerIceCubesMelted > 0
                    ? $"{playerIceCubesMelted} ice cubes melted."
                    : "";

            }

            #endregion

            #region RESULTS_SECTION @PROFIT_AND_LOSS

            if (RESTULS_NAVIGATION_STATE == RESTULS_NAVIGATION_STATES.PROFIT_AND_LOSS)
            {

                profitAndLossCurrentDateUIText.text = $"Current (Year {playerDate[0]:00} / Month {playerDate[1]:00})";

                for (int record = 1; record < 4; record++)

                    grossMargin[record] = playerGrossMargin[record] * 100;

                currentProfitAndLossUITexts[0].text = $"₱ {playerRevenue[1]:0.00}";
                currentProfitAndLossUITexts[1].text = $"₱ {playerStockUsed[1]:0.00}";
                currentProfitAndLossUITexts[2].text = $"₱ {playerStockLost[1]:0.00}";
                currentProfitAndLossUITexts[3].text = $"₱ {playerGrossProfit[1]:0.00}";
                currentProfitAndLossUITexts[4].text = $"{grossMargin[1]:0.00}%";
                currentProfitAndLossUITexts[5].text = $"₱ {playerRent[1]:0.00}";
                currentProfitAndLossUITexts[6].text = $"₱ {playerMarketing[1]:0.00}";
                currentProfitAndLossUITexts[7].text = $"₱ {playerExpenses[1]:0.00}";
                currentProfitAndLossUITexts[8].text = $"₱ {playerEarnings[1]:0.00}";

                lastProfitAndLossUITexts[0].text = $"₱ {playerRevenue[2]:0.00}";
                lastProfitAndLossUITexts[1].text = $"₱ {playerStockUsed[2]:0.00}";
                lastProfitAndLossUITexts[2].text = $"₱ {playerStockLost[2]:0.00}";
                lastProfitAndLossUITexts[3].text = $"₱ {playerGrossProfit[2]:0.00}";
                lastProfitAndLossUITexts[4].text = $"{grossMargin[2]:0.00}%";
                lastProfitAndLossUITexts[5].text = $"₱ {playerRent[2]:0.00}";
                lastProfitAndLossUITexts[6].text = $"₱ {playerMarketing[2]:0.00}";
                lastProfitAndLossUITexts[7].text = $"₱ {playerExpenses[2]:0.00}";
                lastProfitAndLossUITexts[8].text = $"₱ {playerEarnings[2]:0.00}";

                bestProfitAndLossUITexts[0].text = $"₱ {playerRevenue[3]:0.00}";
                bestProfitAndLossUITexts[1].text = $"₱ {playerStockUsed[3]:0.00}";
                bestProfitAndLossUITexts[2].text = $"₱ {playerStockLost[3]:0.00}";
                bestProfitAndLossUITexts[3].text = $"₱ {playerGrossProfit[3]:0.00}";
                bestProfitAndLossUITexts[4].text = $"{grossMargin[3]:0.00}%";
                bestProfitAndLossUITexts[5].text = $"₱ {playerRent[3]:0.00}";
                bestProfitAndLossUITexts[6].text = $"₱ {playerMarketing[3]:0.00}";
                bestProfitAndLossUITexts[7].text = $"₱ {playerExpenses[3]:0.00}";
                bestProfitAndLossUITexts[8].text = $"₱ {playerEarnings[3]:0.00}";

            }

            #endregion

            #region RESULTS_SECTION @BALANCE_SHEET

            if (RESTULS_NAVIGATION_STATE == RESTULS_NAVIGATION_STATES.BALANCE_SHEET)
            {

                double stock = GetStock();
                double assets = playerCapital + stock + playerEquipments;
                double shareCapital = ENV.STARTING_CAPITAL;
                double equity = shareCapital + playerProfitAndLoss;

                balanceSheetUITexts[0].text = $"₱ {playerCapital:0.00}";
                balanceSheetUITexts[1].text = $"₱ {stock:0.00}";
                balanceSheetUITexts[2].text = $"₱ {playerEquipments:0.00}";
                balanceSheetUITexts[3].text = $"₱ {assets:0.00}";
                balanceSheetUITexts[4].text = $"₱ {shareCapital:0.00}";
                balanceSheetUITexts[5].text = $"₱ {playerProfitAndLoss:0.00}";
                balanceSheetUITexts[6].text = $"₱ {equity:0.00}";

            }

            #endregion

        }

        #endregion

        #region LOCATION_SECTION

        if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.LOCATION)
        {

            playerCapital = FindObjectOfType<PLAYER>().PlayerCapital;
            double rentExpense = ENV.LOCATION[locationState, 1];
            bool isAffordable = playerCapital - rentExpense >= 0;

            if (isAffordable
                && playerLocation != locationState)

                playerCapital -= rentExpense;

            locationUIImage.sprite = locationSprites[locationState];
            locationUITexts[0].text = ENV.LOCATION_TEXT[locationState, 0];
            locationUITexts[1].text = ENV.LOCATION_TEXT[locationState, 1];
            locationUITexts[2].text =
                locationState != 0
                ? $"₱ {ENV.LOCATION[locationState, 1]:0.00}"
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
                    DialogManager.OnDialog(
                        "SORRY",
                        "You've insufficient money to rent this place",
                        ENV.DIALOG);

                }
                else
                {

                    string description = string.Format(
                        locationState != 0
                        ? "Are you sure you want to rent this place for\n₱ {0}?"
                        : "Are you sure you want to go back to your own neighborhood? The rent is free.", ENV.LOCATION[locationState, 1].ToString("0.00"));

                    FindObjectOfType<SoundsManager>().OnClicked();
                    DialogManager.OnDialog(
                        "RENTING",
                        description,
                        ENV.OPTION_PANE);
                    STATUS.STATE = STATUS.STATES.RENTING;

                }

            }

        }

        #endregion

        #region UPGRADES_SECTION

        if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.UPGRADES)
        {

            bool isAffordable;
            bool isMaxLevel = playerUpgrade[upgradeState] == 5
                && upgradeState != 2;
            string level =
                isMaxLevel
                ? "Lv. {0}"
                : "Lv. {0} -> Lv. {1}";

            upgradeUITexts[0].text = ENV.UPGRADE_TEXT[upgradeState, 0];
            upgradeUITexts[1].text = ENV.UPGRADE_TEXT[upgradeState, 1];
            upgradeUITexts[3].text = string.Format(level, playerUpgrade[upgradeState], playerUpgrade[upgradeState] + 1);
            upgradeUIImage.sprite = upgradeSprites[upgradeState];
            previousUIButtons[1].interactable = upgradeState > 0;
            nextUIButtons[1].interactable = upgradeState < 2;

            if (upgradeState == 2)
            {

                spend = ENV.UPGRADE[2, 0, 0] + ((playerUpgrade[2] + 1) * 5000);
                upgradeLevelFillUIImage.fillAmount =
                    playerUpgrade[2] > 0
                    ? 1
                    : 0;

            }
            else
            {

                spend =
                    !isMaxLevel
                    ? ENV.UPGRADE[upgradeState, playerUpgrade[upgradeState] + 1, 0]
                    : 0;

                upgradeLevelFillUIImage.fillAmount = (float)playerUpgrade[upgradeState] / 5;

            }

            upgradeUITexts[2].text = string.Format(
                !isMaxLevel
                ? "₱ {0}"
                : "₱ {1}", spend.ToString("0.00"), ENV.UPGRADE[upgradeState, playerUpgrade[upgradeState], 0].ToString("0.00"));

            playerCapital = FindObjectOfType<PLAYER>().PlayerCapital;
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
                    DialogManager.OnDialog(
                        "SORRY",
                        "You've already reached the maximum level of this item",
                        ENV.DIALOG);

                }
                else if (!isAffordable)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    DialogManager.OnDialog(
                        "SORRY",
                        "You've insufficient money to upgrade this item",
                        ENV.DIALOG);

                }
                else
                {

                    spend = FindObjectOfType<PLAYER>().PlayerCapital - playerCapital;
                    string description = $"Are you sure you want to spend\n₱ {spend:0.00} on upgrades?";
                    FindObjectOfType<SoundsManager>().OnClicked();
                    DialogManager.OnDialog(
                        "UPGRADING",
                        description,
                        ENV.OPTION_PANE);
                    STATUS.STATE = STATUS.STATES.UPGRADING;

                }

            }

        }

        #endregion

        #region STAFF_SECTION

        if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.STAFF)
        {

            bool isAffordable;
            bool isHired = playerStaffs.Contains(staffState);
            hireAndFireUIButton.isOn = isHired;
            spend = ENV.STAFF[staffState, 0];
            playerCapital = FindObjectOfType<PLAYER>().PlayerCapital;
            isAffordable = playerCapital - spend >= 0;

            staffUITexts[0].text = ENV.STAFF_TEXT[staffState, 0];
            staffUITexts[1].text = ENV.STAFF_TEXT[staffState, 1];
            staffUIImage.sprite = staffSprites[staffState];
            previousUIButtons[2].interactable = staffState > 0;
            nextUIButtons[2].interactable = staffState < 2;
            staffUITexts[2].text = $"₱ {spend:0.00}";
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
                    DialogManager.OnDialog(
                        "SORRY",
                        "You've insufficient money to hire the staff",
                        ENV.DIALOG);
                    return;

                }

                FindObjectOfType<SoundsManager>().OnClicked();

            }

        }

        #endregion

        #region MARKETING_SECTION

        if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.MARKETING)
        {

            #region MARKETING_SECTION @PRICE

            double costPerCup = GetCostPerCup();
            double profitPerCup = playerPrice - costPerCup;
            profitPerCup =
                profitPerCup > 0
                ? profitPerCup
                : 0;

            FindObjectOfType<PLAYER>().PlayerPrice = playerPrice;
            FindObjectOfType<PLAYER>().PlayerCostPerCup = costPerCup;

            priceUIText.text = $"₱ {playerPrice:0.00}";
            profitPerCupUIText.text = $"Profit Per Cup:\n₱ {profitPerCup:0.00}";
            priceDecrementUIButton.interactable = playerPrice > 0;
            priceResetUIButton.interactable = playerPrice != ENV.DEFAULT_PRICE;

            if (SimpleInput.GetButtonDown("OnDecrementPrice"))

                OnPriceDecrement();

            if (SimpleInput.GetButtonDown("OnIncrementPrice"))

                OnPriceIncrement();

            if (SimpleInput.GetButtonDown("OnResetPrice"))

                OnPriceReset();

            #endregion

            #region MARKETING_SECTION @ADVERTISEMENT

            FindObjectOfType<PLAYER>().PlayerAdvertisement = playerAdvertisement;

            playerCapital = FindObjectOfType<PLAYER>().PlayerCapital;
            double advertisementExpense = ENV.LOCATION[playerLocation, 0] * ENV.ADVERTISEMENT[playerAdvertisement, 0];
            bool isAffordable = playerCapital - advertisementExpense >= 0;

            if (isAffordable)

                playerCapital -= advertisementExpense;

            advertisementUIText.text = $"₱ {advertisementExpense:0.00}";
            advertisementDecrementUIButton.interactable = playerAdvertisement > 0;
            advertisementIncrementUIButton.interactable = IsAdvertisementIncrementable();
            advertisementResetUIButton.interactable = playerAdvertisement > 0;

            if (SimpleInput.GetButtonDown("OnDecrementAdvertisement"))

                OnAdvertisementDecrement();

            if (SimpleInput.GetButtonDown("OnIncrementAdvertisement"))

                OnAdvertisementIncrement();

            if (SimpleInput.GetButtonDown("OnResetAdvertisement"))

                OnAdvertisementReset();

            #endregion

        }

        #endregion

        #region RECIPE_SECTION

        if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.RECIPE)
        {

            FindObjectOfType<PLAYER>().PlayerRecipe = playerRecipe;

            for (int recipe = 0; recipe < 4; recipe++)
            {

                recipeQuantityUITexts[recipe].text = playerRecipe[recipe].ToString();
                recipeDecrementUIButtons[recipe].interactable = playerRecipe[recipe] > 0;
                recipeResetUIButtons[recipe].interactable = playerRecipe[recipe] != ENV.DEFAULT_RECIPE[recipe];

            }

            cupsPerPitcherUIText.text = $"Cups Per Pitcher:\n{cupsPerPitcher}";

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

        #endregion

        #region SUPPLIES_SECTION

        if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.SUPPLIES)
        {

            string conjunctions = GetConjuctions(suppliesState);

            for (int scale = 0; scale < 3; scale++)
            {

                double price = suppliesState == 0
                    ? ENV.SUPPLIES_MANGO_PRICES[playerDate[0] - 1, scale]
                    : ENV.SUPPLIES[suppliesState, 1, scale];

                supplyUIImages[scale].sprite = supplySprites[suppliesState];
                supplyPriceUITexts[scale].text = $"{ENV.SUPPLIES[suppliesState, 0, scale]} {conjunctions} {price:0.00}";
                supplyQuantityUITexts[scale].text = supplies[suppliesState, scale].ToString();
                supplyDecrementUIButtons[scale].interactable = supplies[suppliesState, scale] > 0;
                supplyIncrementUIButtons[scale].interactable = playerCapital - price >= 0
                    && HasAvailableSpace(scale);

            }

            buyUIButton.interactable = FindObjectOfType<PLAYER>().PlayerCapital != playerCapital;
            cancelUIButton.interactable = FindObjectOfType<PLAYER>().PlayerCapital != playerCapital;

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
                    DialogManager.OnDialog(
                        "REQUIRED",
                        "Please increment an item first",
                        ENV.DIALOG);

                }
                else
                {

                    FindObjectOfType<SoundsManager>().OnClicked();
                    DialogManager.OnDialog(
                        "CANCELING",
                        "Are you sure you want clear the counter?",
                        ENV.OPTION_PANE);
                    STATUS.STATE = STATUS.STATES.CANCELING;

                }

            }

            if (SimpleInput.GetButtonDown("OnBuy"))
            {

                if (!buyUIButton.interactable)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    DialogManager.OnDialog(
                        "REQUIRED",
                        "Please increment an item first",
                        ENV.DIALOG);

                }
                else
                {

                    spend = FindObjectOfType<PLAYER>().PlayerCapital - playerCapital;
                    string description = $"Are you sure you want to spend ₱ {spend:0.00} on goods?";
                    FindObjectOfType<SoundsManager>().OnClicked();
                    DialogManager.OnDialog(
                        "BUYING",
                        description,
                        ENV.OPTION_PANE);
                    STATUS.STATE = STATUS.STATES.BUYING;

                }

            }

        }

        #endregion

    }

    #endregion

    #region INIT_STATE_METHOD

    private void InitState()
    {

        locationState = playerLocation;
        upgradeState = 0;
        staffState = 0;
        suppliesState = 0;
        spend = 0;
        RESTULS_NAVIGATION_STATE = RESTULS_NAVIGATION_STATES.YESTERDAYS_RESULTS;
        yesterdaysResultsUINavButton.isOn = true;
        mangoUINavButton.isOn = true;

    }

    #endregion

    #region ON_BOTTOM_NAVIGATION_METHOD

    private void OnBottomNavigation()
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        string navigation = GameManager.GetToggleName(bottomNavigationUIPanel);
        BOTTOM_NAVIGATION_STATE = GetBottomNavigationState(navigation);

        if (LAST_BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATE)
        {

            GameManager.OnTrigger("back");
            LAST_BOTTOM_NAVIGATION_STATE = BOTTOM_NAVIGATION_STATES.IDLE;

        }
        else
        {

            if (LAST_BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.IDLE)

                GameManager.OnTrigger("initialNavigation");

            else

                GameManager.OnTrigger("navigation");

            GameManager.Animator.SetInteger("bottomNavigationState", (int)BOTTOM_NAVIGATION_STATE);
            LAST_BOTTOM_NAVIGATION_STATE = BOTTOM_NAVIGATION_STATE;

        }

        InitState();
        OnSuppliesQuantityClear();
        OnCancel();

    }

    #endregion

    #region GET_BOTTOM_NAVIGATION_STATE_METHOD

    private BOTTOM_NAVIGATION_STATES GetBottomNavigationState(string _navigation) => _navigation switch
    {

        "ResultsUINavButton" => BOTTOM_NAVIGATION_STATES.RESULTS,

        "LocationUINavButton" => BOTTOM_NAVIGATION_STATES.LOCATION,

        "UpgradesUINavButton" => BOTTOM_NAVIGATION_STATES.UPGRADES,

        "StaffUINavButton" => BOTTOM_NAVIGATION_STATES.STAFF,

        "MarketingUINavButton" => BOTTOM_NAVIGATION_STATES.MARKETING,

        "RecipeUINavButton" => BOTTOM_NAVIGATION_STATES.RECIPE,

        "SuppliesUINavButton" => BOTTOM_NAVIGATION_STATES.SUPPLIES,

        _ => BOTTOM_NAVIGATION_STATES.IDLE,

    };

    #endregion

    #region GET_BOTTOM_NAVIGATION_STATE_TEXT_METHOD

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

    #endregion

    #region ON_SUPPLIES_NAVIGATION_METHOD

    private void OnSuppliesNavigation(int _suppliesState)
    {

        FindObjectOfType<SoundsManager>().OnClicked();
        suppliesState = _suppliesState;

    }

    #endregion

    #region GET_CONJUNCTIONS_METHOD

    private string GetConjuctions(int _supply) => _supply switch
    {

        0 => "mangoes = ₱",

        1 => "pieces = ₱",

        2 => "cans = ₱",

        3 => "cubes = ₱",

        _ => "cups = ₱",

    };

    #endregion

    #region ON_SUPPLIES_QUANTITY_CLEAR_METHOD

    private void OnSuppliesQuantityClear()
    {

        for (int supply = 0; supply < 5; supply++)

            for (int scale = 0; scale < 3; scale++)

                supplies[supply, scale] = 0;

    }

    #endregion

    #region ON_SUPPLIES_DECREMENT_METHOD

    private void OnSuppliesDecrement(int _scale)
    {

        int quantityPerPrice = (int)ENV.SUPPLIES[suppliesState, 0, _scale];
        double price = suppliesState == 0
                    ? ENV.SUPPLIES_MANGO_PRICES[playerDate[0] - 1, _scale]
                    : ENV.SUPPLIES[suppliesState, 1, _scale];
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

    #endregion

    #region ON_SUPPLIES_INCREMENT_METHOD

    private void OnSuppliesIncrement(int _scale)
    {

        int quantityPerPrice = (int)ENV.SUPPLIES[suppliesState, 0, _scale];
        double price = suppliesState == 0
                    ? ENV.SUPPLIES_MANGO_PRICES[playerDate[0] - 1, _scale]
                    : ENV.SUPPLIES[suppliesState, 1, _scale];
        bool isIncrementable = playerCapital - price >= 0;

        if (!HasAvailableSpace(_scale))

            DialogManager.OnDialog(
                "SORRY",
                "You've insufficient storage to store this item",
                ENV.DIALOG);

        else if (!isIncrementable)

            DialogManager.OnDialog(
                "SORRY",
                "You've insufficient money to increment this item",
                ENV.DIALOG);

        else
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            supplies[suppliesState, _scale] += quantityPerPrice;
            playerCapital -= price;
            return;

        }

        FindObjectOfType<SoundsManager>().OnError();

    }

    #endregion

    #region ON_CANCEL_METHOD

    private void OnCancel()
    {

        OnSuppliesQuantityClear();
        playerCapital = FindObjectOfType<PLAYER>().PlayerCapital;
        playerAdvertisement = FindObjectOfType<PLAYER>().PlayerAdvertisement;
        playerLocation = FindObjectOfType<PLAYER>().PlayerLocation;
        playerUpgrade = FindObjectOfType<PLAYER>().PlayerUpgrade;
        playerStaffs = FindObjectOfType<PLAYER>().PlayerStaffs;

    }

    #endregion

    #region ON_BUY_SUCCESS_METHOD

    private async void OnBuySuccess()
    {

        FindObjectOfType<PLAYER>().PlayerCapital -= spend;

        for (int supply = 0; supply < 5; supply++)

            for (int scale = 0; scale < 3; scale++)

                playerSupplies[supply] += supplies[supply, scale];

        await Task.Delay(1000);

        OnCancel();
        FindObjectOfType<PLAYER>().OnAutoSave();

    }

    #endregion

    #region ON_PRICE_INCREMENT_METHOD

    private void OnPriceIncrement()
    {

        FindObjectOfType<SoundsManager>().OnClicked();
        playerPrice++;

    }

    #endregion

    #region ON_PRICE_DECREMENT_METHOD

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

    #endregion

    #region ON_PRICE_RESET_METHOD

    private void OnPriceReset()
    {

        if (playerPrice != ENV.DEFAULT_PRICE)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            playerPrice = ENV.DEFAULT_PRICE;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    #endregion

    #region IS_ADVERTISEMENT_INCREMENTABLE_METHOD

    private bool IsAdvertisementIncrementable()
    {

        if (playerAdvertisement < 10)
        {

            double advertisementExpense = ENV.LOCATION[playerLocation, 0] * ENV.ADVERTISEMENT[playerAdvertisement + 1, 0];
            return playerCapital - advertisementExpense >= 0;

        }

        return false;

    }

    #endregion

    #region ON_ADVERTISEMENT_INCREMENT_METHOD

    private void OnAdvertisementIncrement()
    {

        if (IsAdvertisementIncrementable())
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            playerAdvertisement++;
            return;

        }
        else if (playerAdvertisement == 10)

            DialogManager.OnDialog(
                "SORRY",
                "You've already reached the maximum advertisement",
                ENV.DIALOG);

        else

            DialogManager.OnDialog(
                "SORRY",
                "You've insufficient money to avail this advertisement",
                ENV.DIALOG);

        FindObjectOfType<SoundsManager>().OnError();

    }

    #endregion

    #region ON_ADVERTISEMENT_DECREMENT_METHOD

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

    #endregion

    #region ON_ADVERTISEMENT_RESET_METHOD

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

    #endregion

    #region ON_RECIPE_DECREMENT_METHOD

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

    #endregion

    #region ON_RECIPE_INCREMENT_METHOD

    private void OnRecipeIncrement(int _recipe)
    {

        FindObjectOfType<SoundsManager>().OnClicked();
        playerRecipe[_recipe]++;

    }

    #endregion

    #region ON_RECIPE_RESET_METHOD

    private void OnRecipeReset(int _recipe)
    {

        if (_recipe == 0
            && playerRecipe[_recipe] != ENV.DEFAULT_RECIPE[0])

            playerRecipe[_recipe] = ENV.DEFAULT_RECIPE[0];

        else if (_recipe == 1
            && playerRecipe[_recipe] != ENV.DEFAULT_RECIPE[1])

            playerRecipe[_recipe] = ENV.DEFAULT_RECIPE[1];

        else if (_recipe == 2
            && playerRecipe[_recipe] != ENV.DEFAULT_RECIPE[2])

            playerRecipe[_recipe] = ENV.DEFAULT_RECIPE[2];

        else if (_recipe == 3
            && playerRecipe[_recipe] != ENV.DEFAULT_RECIPE[3])

            playerRecipe[_recipe] = ENV.DEFAULT_RECIPE[3];

        else
        {

            FindObjectOfType<SoundsManager>().OnError();
            return;

        }

        FindObjectOfType<SoundsManager>().OnClicked();

    }

    #endregion

    #region GET_TEMPERATURE_SPRITE_METHOD

    private Sprite GetTemperatureSprite(double _temperature)
    {

        if (_temperature >= ENV.TEMPERATURE[0, 0]
            && _temperature <= ENV.TEMPERATURE[1, 1])

            return temperatureSprites[0];

        else if (_temperature >= ENV.TEMPERATURE[3, 0]
            && _temperature <= ENV.TEMPERATURE[4, 1])

            return temperatureSprites[2];

        return temperatureSprites[1];

    }

    #endregion

    #region GET_COST_PER_CUP_METHOD

    private double GetCostPerCup()
    {

        suppliesCostPerRecipe[0] = ENV.AVERAGE_SUPPLIES_COST[0] * playerRecipe[0];
        suppliesCostPerRecipe[1] = ENV.AVERAGE_SUPPLIES_COST[1] * playerRecipe[1];
        suppliesCostPerRecipe[2] = ENV.AVERAGE_SUPPLIES_COST[2] * playerRecipe[2];
        suppliesCostPerRecipe[3] = ENV.AVERAGE_SUPPLIES_COST[3] * playerRecipe[3];
        suppliesCostPerRecipe[4] = ENV.AVERAGE_SUPPLIES_COST[4] * cupsPerPitcher;

        double cost = suppliesCostPerRecipe[0]
            + suppliesCostPerRecipe[1]
            + suppliesCostPerRecipe[2]
            + suppliesCostPerRecipe[3]
            + suppliesCostPerRecipe[4];

        double costPerCup = cost / cupsPerPitcher;

        return costPerCup;

    }

    #endregion

    #region ON_START_DAY_METHOD

    private void OnStartDay(double _advertisementExpense, double _rentExpense)
    {

        FindObjectOfType<PLAYER>().PlayerTopEarnings =
            playerEarnings[0] > playerTopEarnings
            ? playerEarnings[0]
            : playerTopEarnings;
        FindObjectOfType<PLAYER>().PlayerSupplies[3] = iceCubes;
        FindObjectOfType<PLAYER>().PlayerCapital -= (_advertisementExpense + _rentExpense);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    #endregion

    #region ON_RESULTS_NAVIGATION_METHOD

    private void OnResultsNavigation()
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        string navigation = GameManager.GetToggleName(resultsNavigationUIPanel);
        RESTULS_NAVIGATION_STATE = GetResultsNavigationState(navigation);

    }

    #endregion

    #region GET_RESULTS_NAVIGATION_STATE_METHOD

    private RESTULS_NAVIGATION_STATES GetResultsNavigationState(string _navigation) => _navigation switch
    {

        "YesterdaysPerformanceAndSettingUINavButton" => RESTULS_NAVIGATION_STATES.YESTERDAYS_PERFORMANCE_AND_SETTINGS,

        "YesterdaysResultsUINavButton" => RESTULS_NAVIGATION_STATES.YESTERDAYS_RESULTS,

        "ChartsUINavButton" => RESTULS_NAVIGATION_STATES.CHARTS,

        "ProfitAndLossUINavButton" => RESTULS_NAVIGATION_STATES.PROFIT_AND_LOSS,

        _ => RESTULS_NAVIGATION_STATES.BALANCE_SHEET,

    };

    #endregion

    #region GET_RESULTS_NAVIGATION_STATE_TEXT_METHOD

    private string GetResultsNavigationStateText(string _resultsNavigation) => _resultsNavigation switch
    {

        "YesterdaysPerformanceAndSettingUINavButton" => "Yesterday's Performance & Setting",

        "YesterdaysResultsUINavButton" => "Yesterday's Results",

        "ChartsUINavButton" => "Charts",

        "ProfitAndLossUINavButton" => "Profit & Loss",

        _ => "Balance Sheet"

    };

    #endregion

    #region GET_STORAGE_METHOD

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

    #endregion

    #region HAS_AVAILABLE_SPACE_METHOD

    private bool HasAvailableSpace(int _scale)
    {

        int overAllSupplies = supplies[suppliesState, 0] + supplies[suppliesState, 1] + supplies[suppliesState, 2];
        int iceCubes =
            suppliesState == 3
            ? (int)ENV.UPGRADE[1, playerUpgrade[1], 1]
            : 0;
        bool hasAvailableSpace = playerSupplies[suppliesState] + overAllSupplies + ENV.SUPPLIES[suppliesState, 0, _scale] + iceCubes <= playerStorage[suppliesState];

        return hasAvailableSpace;

    }

    #endregion

    #region GET_YESTERDAYS_DATE_METHOD

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

    #endregion

    #region GET_STANDING_IMAGE_METHOD

    private Sprite GetStandingImage()
    {

        if (playerEarnings[0] > playerTopEarnings
            && playerCustomerSatisfaction >= ENV.STANDING[0, 0]
            && playerCustomerSatisfaction <= ENV.STANDING[0, 1])

            return standingSprites[0];

        else if (playerEarnings[0] > playerTopEarnings
            && playerCustomerSatisfaction >= ENV.STANDING[1, 0]
            && playerCustomerSatisfaction <= ENV.STANDING[1, 1])

            return standingSprites[1];

        else if (playerEarnings[0] > playerTopEarnings
            && playerCustomerSatisfaction >= ENV.STANDING[2, 0]
            && playerCustomerSatisfaction <= ENV.STANDING[2, 1])

            return standingSprites[2];

        else if (playerEarnings[0] > 0)

            return standingSprites[3];

        return standingSprites[4];

    }

    #endregion

    #region GET_STANDING_TEXT_METHOD

    private string GetStandingText()
    {

        if (playerEarnings[0] > playerTopEarnings)

            return "Congratulations!\nA new profit record!";

        else if (playerEarnings[0] > 0)

            return "Keep up the good\nwork!";

        return "Is that the best you\ncan do?";

    }

    #endregion

    #region GET_CUSTOMERS_FEEDBACK_METHOD

    private string GetCustomersFeedback(int _feedback) => _feedback switch
    {

        1 => "You went out of stock!",

        2 => "Customers complained about\nyour recipe.",

        3 => "Customers complained about\nyour pricing.",

        4 => "Customers complained about long\nserving times.",

        5 => "Find a way to attract more\ncustomers to your stand.",

        _ => "",

    };

    #endregion

    #region GET_STOCK_METHOD

    private double GetStock()
    {

        suppliesCostPerStock[0] = ENV.AVERAGE_SUPPLIES_COST[0] * playerSupplies[0];
        suppliesCostPerStock[1] = ENV.AVERAGE_SUPPLIES_COST[1] * playerSupplies[1];
        suppliesCostPerStock[2] = ENV.AVERAGE_SUPPLIES_COST[2] * playerSupplies[2];
        suppliesCostPerStock[3] = ENV.AVERAGE_SUPPLIES_COST[3] * iceCubes;
        suppliesCostPerStock[4] = ENV.AVERAGE_SUPPLIES_COST[4] * playerSupplies[4];

        double stock = suppliesCostPerStock[0]
            + suppliesCostPerStock[1]
            + suppliesCostPerStock[2]
            + suppliesCostPerStock[3]
            + suppliesCostPerStock[4];

        return stock;

    }

    #endregion

    #region ON_LOCATION_PREVIOUS_METHOD

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

    #endregion

    #region ON_LOCATION_NEXT_METHOD

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

    #endregion

    #region ON_RENT_SUCCESS_METHOD

    private void OnRentSuccess()
    {

        FindObjectOfType<PLAYER>().PlayerLocation = locationState;

        OnCancel();
        FindObjectOfType<PLAYER>().OnAutoSave();

    }

    #endregion

    #region ON_PREVIOUS_METHOD

    private void OnPrevious(int _state)
    {

        if (_state > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.UPGRADES)

                upgradeState--;

            else if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.STAFF)

                staffState--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    #endregion

    #region ON_NEXT_METHOD

    private void OnNext(int _state)
    {

        if (_state < 2)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.UPGRADES)

                upgradeState++;

            else if (BOTTOM_NAVIGATION_STATE == BOTTOM_NAVIGATION_STATES.STAFF)

                staffState++;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    #endregion

    #region ON_UPGRADE_SUCCESS_METHOD

    private void OnUpgradeSuccess()
    {

        if (upgradeState == 2)

            OnUpgradeStorage();

        FindObjectOfType<PLAYER>().PlayerCapital -= spend;
        FindObjectOfType<PLAYER>().PlayerEquipments += spend;
        playerUpgrade[upgradeState]++;
        FindObjectOfType<PLAYER>().PlayerUpgrade = playerUpgrade;
        FindObjectOfType<PLAYER>().PlayerReputation = (FindObjectOfType<PLAYER>().PlayerReputation + ENV.UPGRADE_BOOST) / 2;

        OnCancel();
        FindObjectOfType<PLAYER>().OnAutoSave();

    }

    #endregion

    #region ON_UPGRADE_STORAGE_METHOD

    private void OnUpgradeStorage()
    {

        for (int i = 0; i < playerStorage.Length; i++)

            playerStorage[i] += ENV.STORAGE[i];

        FindObjectOfType<PLAYER>().PlayerStorage = playerStorage;

    }

    #endregion

    #region GET_STAFF_EXPENSE_METHOD

    private double GetStaffExpense()
    {

        double staffExpense = 0;

        foreach (int staff in playerStaffs)

            staffExpense += ENV.STAFF[staff, 0];

        return staffExpense;

    }

    #endregion

    #region ON_TIP_METHOD

    private void OnTip()
    {

        if (isTipping)
        {

            isTipping = !isTipping;
            GameManager.OnNowInforming(ENV.TIPS[playerTip]);

        }

    }

    #endregion

}