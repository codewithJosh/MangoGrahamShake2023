using System;
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

    [Header("RESULTS SECTION @YESTERDAYS PERFORMANCE AND SETTING")]
    [SerializeField]
    private TextMeshProUGUI performanceCupsSoldUIText;

    [SerializeField]
    private TextMeshProUGUI performanceProfitUIText;

    [SerializeField]
    private TextMeshProUGUI unsatisfiedCustomersUIText;

    [SerializeField]
    private TextMeshProUGUI SatisfiedCustomersUIText;

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

    [SerializeField]
    private ToggleGroup resultsNavigationUIPanel;

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
    private Sprite[] temperatureSprites;

    [SerializeField]
    private TextMeshProUGUI[] dailyUITexts;

    [SerializeField]
    private TextMeshProUGUI[] suppliesUITexts;

    [SerializeField]
    private TextMeshProUGUI[] locationUITexts;

    private enum BottomNavigationStates { idle, results, location, upgrades, staff, marketing, recipe, supplies };
    private enum ResultsNavigationStates { yesterdaysPerformanceAndSettings, yesterdaysResults, charts, profitAndLoss, balanceSheet };

    private BottomNavigationStates navigationState;
    private BottomNavigationStates lastNavigationState;
    private ResultsNavigationStates resultsNavigationState;

    private double[,] ADVERTISEMENT;
    private double[,] LOCATION;
    private double[,,] SUPPLIES;
    private double DEFAULT_PRICE;
    private double MAXIMUM_PRICE;
    private int[] DEFAULT_RECIPE;
    private int MINIMUM_CUPS;
    private double[,] TEMPERATURE;
    private string[,] LOCATION_TEXT;
    private double[] AVERAGE_SUPPLIES_COST;

    private double capital;
    private double price;
    private double[] popularity;
    private double[] satisfaction;
    private int[] supplies;
    private int[] recipe;
    private int advertisement;

    private bool isBuying;
    private bool isCanceling;
    private bool isConnected;
    private double spend;
    private int cupsPerPitcher;
    private int location;
    private int suppliesState;

    private int[] date;
    private double temperature;
    private double[] suppliesCostPerRecipe;
    private double costPerCup;
    private double profitPerCup;

    void Start()
    {

        Init();

        suppliesCostPerRecipe = new double[] 
        { 

            0, 
            0, 
            0, 
            0, 
            0 

        };
        cupsPerPitcher = 0;
        costPerCup = 0;
        profitPerCup = 0;

        ADVERTISEMENT = FindObjectOfType<ENV>().ADVERTISEMENT;
        DEFAULT_PRICE = FindObjectOfType<ENV>().DEFAULT_PRICE;
        DEFAULT_RECIPE = FindObjectOfType<ENV>().DEFAULT_RECIPE;
        LOCATION = FindObjectOfType<ENV>().LOCATION;
        LOCATION_TEXT = FindObjectOfType<ENV>().LOCATION_TEXT;
        MAXIMUM_PRICE = FindObjectOfType<ENV>().MAXIMUM_PRICE;
        MINIMUM_CUPS = FindObjectOfType<ENV>().MINIMUM_CUPS;
        SUPPLIES = FindObjectOfType<ENV>().SUPPLIES;
        TEMPERATURE = FindObjectOfType<ENV>().TEMPERATURE;
        AVERAGE_SUPPLIES_COST = FindObjectOfType<ENV>().AVERAGE_SUPPLIES_COST;

        advertisement = FindObjectOfType<Player>().PlayerAdvertisement;
        capital = FindObjectOfType<Player>().PlayerCapital;
        location = FindObjectOfType<Player>().PlayerLocation;
        price = FindObjectOfType<Player>().PlayerPrice;
        recipe = FindObjectOfType<Player>().PlayerRecipe;
        supplies = FindObjectOfType<Player>().PlayerSupplies;
        date = FindObjectOfType<Player>().PlayerDate;
        temperature = FindObjectOfType<Player>().PlayerTemperature;
        popularity = FindObjectOfType<Player>().PlayerPopularity;
        satisfaction = FindObjectOfType<Player>().PlayerSatisfaction;

        temperatureUIImage.sprite = GetTemperatureSprite(temperature);
        popularityUIImage.fillAmount = (float) popularity[location];
        satisfactionUIImage.fillAmount = (float) satisfaction[location];
        locationUITexts[0].text = LOCATION_TEXT[location, 0];
        locationUITexts[1].text = LOCATION_TEXT[location, 1];
        dailyUITexts[0].text = string.Format("{0} - {1} - {2}", date[0].ToString("00"), date[1].ToString("00"), date[2].ToString("00"));
        dailyUITexts[1].text = string.Format("{0}°", temperature.ToString("0.0"));

    }

    void Update()
    {

        FindObjectOfType<Player>().PlayerCupsPerPitcher = cupsPerPitcher;

        isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        dailyUITexts[2].text = string.Format("{0}", capital.ToString("0.00"));

        suppliesUITexts[0].text = supplies[0].ToString();
        suppliesUITexts[1].text = supplies[1].ToString();
        suppliesUITexts[2].text = supplies[2].ToString();
        suppliesUITexts[3].text = supplies[3].ToString();
        suppliesUITexts[4].text = supplies[4].ToString();

        string bottomNavigationState = GetBottomNavigationStateText(FindObjectOfType<GameManager>().GetToggleName(bottomNavigationUIPanel), FindObjectOfType<GameManager>().GetToggleName(resultsNavigationUIPanel));

        if (!bottomNavigationState.Equals(""))

            bottomNavigationUIText.text = bottomNavigationState;

        bottomNavigationUIButtons[0].sprite = 
            lastNavigationState == BottomNavigationStates.results
            ? bottomNavigationSelectedUIButtons[0] 
            : bottomNavigationNormalUIButtons[0];

        bottomNavigationUIButtons[1].sprite = 
            lastNavigationState == BottomNavigationStates.location
            ? bottomNavigationSelectedUIButtons[1] 
            : bottomNavigationNormalUIButtons[1];

        bottomNavigationUIButtons[2].sprite =
            lastNavigationState == BottomNavigationStates.upgrades
            ? bottomNavigationSelectedUIButtons[2]
            : bottomNavigationNormalUIButtons[2];

        bottomNavigationUIButtons[3].sprite =
            lastNavigationState == BottomNavigationStates.staff
            ? bottomNavigationSelectedUIButtons[3]
            : bottomNavigationNormalUIButtons[3];

        bottomNavigationUIButtons[4].sprite =
            lastNavigationState == BottomNavigationStates.marketing
            ? bottomNavigationSelectedUIButtons[4]
            : bottomNavigationNormalUIButtons[4];

        bottomNavigationUIButtons[5].sprite =
            lastNavigationState == BottomNavigationStates.recipe
            ? bottomNavigationSelectedUIButtons[5]
            : bottomNavigationNormalUIButtons[5];

        bottomNavigationUIButtons[6].sprite =
            lastNavigationState == BottomNavigationStates.supplies
            ? bottomNavigationSelectedUIButtons[6]
            : bottomNavigationNormalUIButtons[6];

        settingsUIButton.alpha = 
            lastNavigationState == BottomNavigationStates.idle 
            ? 1 
            : 0;

        settingsUIButton.blocksRaycasts = lastNavigationState == BottomNavigationStates.idle;

        cupsPerPitcher =
                recipe[3] > MINIMUM_CUPS
                ? recipe[3]
                : MINIMUM_CUPS;

        if (SimpleInput.GetButtonUp("OnNavigation"))

            OnNavigation();

        if (navigationState == BottomNavigationStates.supplies)
        {

            supplyQuantityUITexts[0].text = SUPPLIES[suppliesState, 0, 0].ToString();
            supplyQuantityUITexts[1].text = SUPPLIES[suppliesState, 0, 1].ToString();
            supplyQuantityUITexts[2].text = SUPPLIES[suppliesState, 0, 2].ToString();

            supplyDecrementUIButtons[0].interactable = SUPPLIES[suppliesState, 0, 0] > 0;
            supplyDecrementUIButtons[1].interactable = SUPPLIES[suppliesState, 0, 1] > 0;
            supplyDecrementUIButtons[2].interactable = SUPPLIES[suppliesState, 0, 2] > 0;

            supplyIncrementUIButtons[0].interactable = capital - SUPPLIES[suppliesState, 2, 0] >= 0;
            supplyIncrementUIButtons[1].interactable = capital - SUPPLIES[suppliesState, 2, 1] >= 0;
            supplyIncrementUIButtons[2].interactable = capital - SUPPLIES[suppliesState, 2, 2] >= 0;

            buyUIButton.interactable = FindObjectOfType<Player>().PlayerCapital != capital;
            cancelUIButton.interactable = FindObjectOfType<Player>().PlayerCapital != capital;

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

                    spend = FindObjectOfType<Player>().PlayerCapital - capital;
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

        if (navigationState == BottomNavigationStates.recipe)
        {

            FindObjectOfType<Player>().PlayerRecipe = recipe;

            recipeQuantityUITexts[0].text = recipe[0].ToString();
            recipeQuantityUITexts[1].text = recipe[1].ToString();
            recipeQuantityUITexts[2].text = recipe[2].ToString();
            recipeQuantityUITexts[3].text = recipe[3].ToString();

            recipeDecrementUIButtons[0].interactable = recipe[0] > 0;
            recipeDecrementUIButtons[1].interactable = recipe[1] > 0;
            recipeDecrementUIButtons[2].interactable = recipe[2] > 0;
            recipeDecrementUIButtons[3].interactable = recipe[3] > 0;

            recipeResetUIButtons[0].interactable = recipe[0] != DEFAULT_RECIPE[0];
            recipeResetUIButtons[1].interactable = recipe[1] != DEFAULT_RECIPE[1];
            recipeResetUIButtons[2].interactable = recipe[2] != DEFAULT_RECIPE[2];
            recipeResetUIButtons[3].interactable = recipe[3] != DEFAULT_RECIPE[3];

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

        if (navigationState == BottomNavigationStates.marketing)
        {

            FindObjectOfType<Player>().PlayerPrice = price;
            FindObjectOfType<Player>().PlayerAdvertisement = advertisement;
            FindObjectOfType<Player>().PlayerCostPerCup = costPerCup;
            FindObjectOfType<Player>().PlayerProfitPerCup = profitPerCup;

            double advertisementPrice = LOCATION[location, 0] * ADVERTISEMENT[advertisement, 0];
            costPerCup = GetCostPerCup();
            profitPerCup = price - costPerCup;
            profitPerCup =
                profitPerCup > 0 
                ? profitPerCup
                : 0;

            priceUIText.text = string.Format("₱ {0}", price.ToString("0.00"));
            profitPerCupUIText.text = string.Format("Profit Per Cup:\n₱ {0}", profitPerCup.ToString("0.00"));
            advertisementUIText.text = string.Format("₱ {0}", advertisementPrice.ToString("0.00"));

            priceDecrementUIButton.interactable = price > 0;
            priceIncrementUIButton.interactable = price < MAXIMUM_PRICE;
            priceResetUIButton.interactable = price != DEFAULT_PRICE;

            advertisementDecrementUIButton.interactable = advertisement > 0;
            advertisementIncrementUIButton.interactable = IsAdvertisementIncrementable();
            advertisementResetUIButton.interactable = advertisement > 0;

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

            if (supplies[0] < recipe[0]
                || supplies[1] < recipe[1]
                || supplies[2] < recipe[2]
                || supplies[3] < recipe[3]
                || supplies[4] < 1)
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Not enough supplies to start the day. Change your recipe or buy more supplies.",
                    "dialog");

            }
            else if (advertisement > 0)
            {

                spend = LOCATION[location, 0] * ADVERTISEMENT[advertisement, 0];

                if (capital - spend < 0)
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

    }

    private void Init()
    {

        isBuying = false;
        isCanceling = false;
        spend = 0;
        suppliesState = 0;

    }

    private void OnNavigation()
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        string navigation = FindObjectOfType<GameManager>().GetToggleName(bottomNavigationUIPanel);
        navigationState = GetBottomNavigationState(navigation);

        if (lastNavigationState == navigationState)
        {

            FindObjectOfType<GameManager>().Animator.SetTrigger("back");
            lastNavigationState = BottomNavigationStates.idle;

        }
        else
        {

            if (lastNavigationState == BottomNavigationStates.idle)

                FindObjectOfType<GameManager>().Animator.SetTrigger("initialNavigation");

            else

                FindObjectOfType<GameManager>().Animator.SetTrigger("navigation");

            FindObjectOfType<GameManager>().Animator.SetInteger("navigationState", (int)navigationState);
            lastNavigationState = navigationState;

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

    private string GetBottomNavigationStateText(string _bottomNavigation, string _resultsNavigation)
    {

        return _bottomNavigation switch
        {

            "LocationUINavButton" => "Location",

            "UpgradesUINavButton" => "Upgrades",

            "StaffUINavButton" => "Staff",

            "MarketingUINavButton" => "Marketing",

            "RecipeUINavButton" => "Recipe",

            "SuppliesUINavButton" => "Supplies",

            _ => GetResultsNavigationStateText(_resultsNavigation),

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
            capital += price;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnSuppliesIncrement(int _scale)
    {

        double quantityPerPrice = SUPPLIES[suppliesState, 1, _scale];
        double price = SUPPLIES[suppliesState, 2, _scale];

        if (capital - price >= 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            SUPPLIES[suppliesState, 0, _scale] += quantityPerPrice;
            capital -= price;

        }
        else
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You've insufficient money to increment this item",
                "dialog");

        }

    }

    private void OnCancel()
    {

        OnSuppliesQuantityClear();
        capital = FindObjectOfType<Player>().PlayerCapital;
        advertisement = FindObjectOfType<Player>().PlayerAdvertisement;

    }

    private async void OnBuySuccess()
    {

        FindObjectOfType<Player>().PlayerCapital -= spend;

        supplies[0] += Convert.ToInt32(SUPPLIES[0, 0, 0] + SUPPLIES[0, 0, 1] + SUPPLIES[0, 0, 2]);
        supplies[1] += Convert.ToInt32(SUPPLIES[1, 0, 0] + SUPPLIES[1, 0, 1] + SUPPLIES[1, 0, 2]);
        supplies[2] += Convert.ToInt32(SUPPLIES[2, 0, 0] + SUPPLIES[2, 0, 1] + SUPPLIES[2, 0, 2]);
        supplies[3] += Convert.ToInt32(SUPPLIES[3, 0, 0] + SUPPLIES[3, 0, 1] + SUPPLIES[3, 0, 2]);
        supplies[4] += Convert.ToInt32(SUPPLIES[4, 0, 0] + SUPPLIES[4, 0, 1] + SUPPLIES[4, 0, 2]);

        await Task.Delay(1000);

        Init();
        OnCancel();

        FindObjectOfType<Player>().OnAutoSave(isConnected);

    }

    private void OnPriceIncrement()
    {

        if (price < MAXIMUM_PRICE)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            price++;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnPriceDecrement()
    {

        if (price > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            price--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnAdvertisementIncrement()
    {

        if (IsAdvertisementIncrementable())
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            spend = LOCATION[location, 0] * ADVERTISEMENT[++advertisement, 0];
            capital = FindObjectOfType<Player>().PlayerCapital;
            capital -= spend;
            
        }
        else if (advertisement == 10)
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

        if (advertisement > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            spend = LOCATION[location, 0] * ADVERTISEMENT[--advertisement, 0];
            capital = FindObjectOfType<Player>().PlayerCapital;
            capital -= spend;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private bool IsAdvertisementIncrementable()
    {

        if (advertisement < 10)
        {
 
            int newAdvertisement = advertisement;
            spend = LOCATION[location, 0] * ADVERTISEMENT[++newAdvertisement, 0];

            return capital - spend >= 0;

        }

        return false;

    }

    private void OnPriceReset()
    {

        if (price != DEFAULT_PRICE)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            price = DEFAULT_PRICE;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnAdvertisementReset()
    {

        if (advertisement != 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            capital = FindObjectOfType<Player>().PlayerCapital;
            advertisement = 0;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnRecipeDecrement(int _recipe)
    {

        if (recipe[_recipe] > 0)
        {

            FindObjectOfType<SoundsManager>().OnClicked();

            recipe[_recipe]--;

        }
        else

            FindObjectOfType<SoundsManager>().OnError();

    }

    private void OnRecipeIncrement(int _recipe)
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        recipe[_recipe]++;

    }

    private void OnRecipeReset(int _recipe)
    {

        if (_recipe == 0 && recipe[_recipe] != DEFAULT_RECIPE[0])

            recipe[_recipe] = DEFAULT_RECIPE[0];

        else if (_recipe == 1 && recipe[_recipe] != DEFAULT_RECIPE[1])

            recipe[_recipe] = DEFAULT_RECIPE[1];

        else if (_recipe == 2 && recipe[_recipe] != DEFAULT_RECIPE[2])

            recipe[_recipe] = DEFAULT_RECIPE[2];

        else if (_recipe == 3 && recipe[_recipe] != DEFAULT_RECIPE[3])

            recipe[_recipe] = DEFAULT_RECIPE[3];

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

        suppliesCostPerRecipe[0] = AVERAGE_SUPPLIES_COST[0] * recipe[0];
        suppliesCostPerRecipe[1] = AVERAGE_SUPPLIES_COST[1] * recipe[1];
        suppliesCostPerRecipe[2] = AVERAGE_SUPPLIES_COST[2] * recipe[2];
        suppliesCostPerRecipe[3] = AVERAGE_SUPPLIES_COST[3] * recipe[3];
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

        if (lastNavigationState == navigationState)
        {

            FindObjectOfType<GameManager>().Animator.SetTrigger("back");
            lastNavigationState = BottomNavigationStates.idle;

        }
        else
        {

            if (lastNavigationState == BottomNavigationStates.idle)

                FindObjectOfType<GameManager>().Animator.SetTrigger("initialNavigation");

            else

                FindObjectOfType<GameManager>().Animator.SetTrigger("navigation");

            FindObjectOfType<GameManager>().Animator.SetInteger("navigationState", (int)navigationState);
            lastNavigationState = navigationState;

        }

        mangoUINavButton.isOn = true;
        OnSuppliesQuantityClear();
        OnSuppliesNavigation(0);

        OnCancel();

    }

    private ResultsNavigationStates GetResultsNavigationState(string _navigation)
    {

        return _navigation switch
        {

            "YesterdaysPerformanceAndSettingUINavButton" => ResultsNavigationStates.yesterdaysPerformanceAndSettings,

            "YesterdaysResultUINavButton" => ResultsNavigationStates.yesterdaysResults,

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

            "YesterdaysResultUINavButton" => "Yesterday's Results",

            "ChartsUINavButton" => "Charts",

            "ProfitAndLossUINavButton" => "Profit & Loss",

            _ => "Balance Sheet"

        };

    }

}