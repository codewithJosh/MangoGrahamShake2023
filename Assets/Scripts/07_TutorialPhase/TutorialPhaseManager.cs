using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialPhaseManager : MonoBehaviour
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
    private CanvasGroup settingsUIButton;

    [SerializeField]
    private Image temperatureUIImage;

    [SerializeField]
    private Image[] suppliesUIImages;

    [SerializeField]
    private Sprite[] temperatureSprites;

    [SerializeField]
    private TextMeshProUGUI[] dailyUITexts;

    [SerializeField]
    private TextMeshProUGUI[] suppliesUITexts;

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

    private enum BottomNavigationStates { idle, marketing, recipe, supplies };

    private BottomNavigationStates bottomNavigationState;
    private BottomNavigationStates lastBottomNavigationState;

    private double DEFAULT_PRICE;
    private double MAXIMUM_PRICE;
    private double[,,] SUPPLIES;
    private double[,] TEMPERATURE;
    private double[] AVERAGE_SUPPLIES_COST;
    private int MINIMUM_CUPS;
    private int[] DEFAULT_RECIPE;

    private double playerCapital;
    private double playerPrice;
    private double playerTemperature;
    private int[] playerRecipe;
    private int[] playerStorage;
    private int[] playerSupplies;

    private bool isBuying;
    private bool isCanceling;
    private bool isConnected;
    private double spend;
    private double[] suppliesCostPerRecipe;
    private int cupsPerPitcher;
    private int suppliesState;
    private int[,] supplies;

    void Start()
    {

        Init();

        cupsPerPitcher = 0;
        suppliesCostPerRecipe = new double[] { 0, 0, 0, 0, 0, };
        supplies = new int[5, 3]
        {

            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },

        };

        AVERAGE_SUPPLIES_COST = FindObjectOfType<ENV>().AVERAGE_SUPPLIES_COST;
        DEFAULT_PRICE = FindObjectOfType<ENV>().DEFAULT_PRICE;
        DEFAULT_RECIPE = FindObjectOfType<ENV>().DEFAULT_RECIPE;
        MAXIMUM_PRICE = FindObjectOfType<ENV>().MAXIMUM_PRICE;
        MINIMUM_CUPS = FindObjectOfType<ENV>().MINIMUM_CUPS;
        SUPPLIES = FindObjectOfType<ENV>().SUPPLIES;
        TEMPERATURE = FindObjectOfType<ENV>().TEMPERATURE;
        
        playerCapital = FindObjectOfType<Player>().PlayerCapital;
        playerPrice = FindObjectOfType<Player>().PlayerPrice;
        playerRecipe = FindObjectOfType<Player>().PlayerRecipe;
        playerStorage = FindObjectOfType<Player>().PlayerStorage;
        playerSupplies = FindObjectOfType<Player>().PlayerSupplies;
        playerTemperature = FindObjectOfType<Player>().PlayerTemperature;

        temperatureUIImage.sprite = GetTemperatureSprite(playerTemperature);
        dailyUITexts[0].text = string.Format("{0}°", playerTemperature.ToString("0.0"));

        InitState();

    }

    void Update()
    {

        FindObjectOfType<Player>().PlayerCupsPerPitcher = cupsPerPitcher;

        isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        dailyUITexts[1].text = string.Format("{0}", playerCapital.ToString("0.00"));
        GetStorage();

        string bottomNavigationStateText = GetBottomNavigationStateText(FindObjectOfType<GameManager>().GetToggleName(bottomNavigationUIPanel));

        if (!bottomNavigationStateText.Equals(""))

            bottomNavigationUIText.text = bottomNavigationStateText;

        for (int navigationState = 0; navigationState < 3; navigationState++)

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
            else

                OnStartDay();

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
        FindObjectOfType<SettingsMenu>().IsEnabled = true;

    }

    private void InitState()
    {

        suppliesState = 0;
        spend = 0;
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

        "MarketingUINavButton" => BottomNavigationStates.marketing,

        "RecipeUINavButton" => BottomNavigationStates.recipe,

        "SuppliesUINavButton" => BottomNavigationStates.supplies,

        _ => BottomNavigationStates.idle,

    };

    private string GetBottomNavigationStateText(string _bottomNavigation) => _bottomNavigation switch
    {

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

        int quantityPerPrice = (int)SUPPLIES[suppliesState, 0, _scale];
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

        int quantityPerPrice = (int)SUPPLIES[suppliesState, 0, _scale];
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

    private void OnStartDay()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void GetStorage()
    {

        for (int supply = 0; supply < playerStorage.Length; supply++)
        {

            suppliesUIImages[supply].fillAmount = playerSupplies[supply] / playerStorage[supply];
            suppliesUITexts[supply].text = playerSupplies[supply].ToString();

        }

    }

    private bool HasAvailableSpace(int _scale)
    {

        int overAllSupplies = supplies[suppliesState, 0] + supplies[suppliesState, 1] + supplies[suppliesState, 2];
        bool hasAvailableSpace = playerSupplies[suppliesState] + overAllSupplies + SUPPLIES[suppliesState, 0, _scale] <= playerStorage[suppliesState];

        return hasAvailableSpace;

    }

    public bool IsEnabled { private get; set; }

}