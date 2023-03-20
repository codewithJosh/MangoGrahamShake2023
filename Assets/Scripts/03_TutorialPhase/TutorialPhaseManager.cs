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
    private Image[] bottomNavigationBackgroundUIButtons;

    [SerializeField]
    private Sprite[] bottomNavigationNormalUIButtons;

    [SerializeField]
    private Sprite[] bottomNavigationSelectedUIButtons;

    [SerializeField]
    private TextMeshProUGUI[] bottomNavigationUITexts;

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
    private Image[] suppliesBackgroundUIImages;

    [SerializeField]
    private Sprite[] temperatureSprites;

    [SerializeField]
    private TextMeshProUGUI[] dailyUITexts;

    [SerializeField]
    private TextMeshProUGUI[] suppliesUITexts;

    [SerializeField]
    private TextMeshProUGUI[] suppliesBackgroundUITexts;

    [Header("MARKETING SECTION @PRICE")]
    [SerializeField]
    private Button priceDecrementUIButton;

    [SerializeField]
    private Button priceIncrementUIButton;

    [SerializeField]
    private TextMeshProUGUI[] priceUIText;

    [SerializeField]
    private TextMeshProUGUI[] profitPerCupUIText;

    [Header("RECIPE SECTION")]

    [SerializeField]
    private Button[] recipeResetUIButtons;

    [SerializeField]
    private CanvasGroup[] recipeUIPanel;

    [SerializeField]
    private TextMeshProUGUI[] cupsPerPitcherUIText;

    [SerializeField]
    private TextMeshProUGUI[] recipeQuantityUITexts;

    [SerializeField]
    private TextMeshProUGUI[] recipeQuantity2UITexts;

    [SerializeField]
    private TextMeshProUGUI[] recipeQuantityBackgroundUITexts;

    [Header("SUPPLIES SECTION")]
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

    private double playerCapital;
    private double playerPrice;
    private double playerTemperature;
    private int[] playerRecipe;
    private int[] playerStorage;
    private int[] playerSupplies;

    private bool isConnected;
    private double spend;
    private double[] suppliesCostPerRecipe;
    private int cupsPerPitcher;
    private int suppliesState;
    private int[,] supplies;
    private int stepState;
    private int recipeState;
    private bool isDone;
    private bool isStartingOver;

    void Start()
    {

        int isTutorialStart = PlayerPrefs.GetInt("is_tutorial_start", 0);

        if (isTutorialStart != 0)

            OnTutorialStart();

        Init();

        isDone = false;
        stepState = 0;
        recipeState = 0;
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
        dailyUITexts[2].text = string.Format("{0}", playerCapital.ToString("0.00"));
        GetStorage();

        string bottomNavigationStateText = GetBottomNavigationStateText(GameManager.GetToggleName(bottomNavigationUIPanel));

        if (!bottomNavigationStateText.Equals(""))
        {

            bottomNavigationUITexts[0].text = bottomNavigationStateText;
            bottomNavigationUITexts[1].text = bottomNavigationStateText;

        }

        for (int navigationState = 0; navigationState < 3; navigationState++)
        {

            Sprite navigationSprite = (int)lastBottomNavigationState - 1 == navigationState
                ? bottomNavigationSelectedUIButtons[navigationState]
                : bottomNavigationNormalUIButtons[navigationState];

            bottomNavigationUIButtons[navigationState].sprite = navigationSprite;
            bottomNavigationBackgroundUIButtons[navigationState].sprite = navigationSprite;

        }

        settingsUIButton.alpha =
            lastBottomNavigationState == BottomNavigationStates.idle
            ? 1
            : 0;

        settingsUIButton.blocksRaycasts = lastBottomNavigationState == BottomNavigationStates.idle;

        cupsPerPitcher =
                playerRecipe[3] > ENV.MINIMUM_CUPS
                ? playerRecipe[3]
                : ENV.MINIMUM_CUPS;

        if (SimpleInput.GetButtonUp("OnClose"))

            OnBottomNavigation();

        if (SimpleInput.GetButtonUp("OnBottomNavigation1")
            && (stepState == 2
            || stepState == 7))

            OnBottomNavigation();

        if (SimpleInput.GetButtonUp("OnBottomNavigation2")
            && stepState == 10)

            OnBottomNavigation();

        if (SimpleInput.GetButtonUp("OnBottomNavigation3")
            && (stepState == 15
            || stepState == 17))

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

            priceUIText[0].text = string.Format("₱ {0}", playerPrice.ToString("0.00"));
            priceUIText[1].text = string.Format("₱ {0}", playerPrice.ToString("0.00"));
            profitPerCupUIText[0].text = string.Format("Profit Per Cup:\n₱ {0}", profitPerCup.ToString("0.00"));
            profitPerCupUIText[1].text = string.Format("Profit Per Cup:\n₱ {0}", profitPerCup.ToString("0.00"));
            priceDecrementUIButton.interactable = playerPrice > 0;

            if (SimpleInput.GetButtonDown("OnDecrementPrice"))

                OnPriceDecrement();

            if (SimpleInput.GetButtonDown("OnIncrementPrice")
                && playerPrice != ENV.DEFAULT_PRICE)

                OnPriceIncrement();

        }

        if (bottomNavigationState == BottomNavigationStates.recipe)
        {

            FindObjectOfType<Player>().PlayerRecipe = playerRecipe;

            for (int recipe = 0; recipe < 4; recipe++)
            {

                recipeQuantityUITexts[recipe].text = playerRecipe[recipe].ToString();
                recipeQuantity2UITexts[recipe].text = playerRecipe[recipe].ToString();
                recipeQuantityBackgroundUITexts[recipe].text = playerRecipe[recipe].ToString();
                recipeResetUIButtons[recipe].interactable = playerRecipe[recipe] != ENV.DEFAULT_RECIPE[recipe];

            }

            cupsPerPitcherUIText[0].text = string.Format("Cups Per Pitcher:\n{0}", cupsPerPitcher);
            cupsPerPitcherUIText[1].text = string.Format("Cups Per Pitcher:\n{0}", cupsPerPitcher);
            cupsPerPitcherUIText[2].text = string.Format("Cups Per Pitcher:\n{0}", cupsPerPitcher);

            if (stepState == 12)
            {

                if (recipeState < 4)
                {

                    for (int i = 0; i < 4; i++)
                    {

                        if (i == recipeState)

                            BuildRecipe(i, true);

                        else

                            BuildRecipe(i, false);

                    }

                    if (playerRecipe[recipeState] == ENV.DEFAULT_RECIPE[recipeState])

                        recipeState++;

                }
                else

                    OnNext();

            }

            if (SimpleInput.GetButtonDown("OnDecrementMango"))

                OnRecipeDecrement(0);

            if (SimpleInput.GetButtonDown("OnDecrementGraham"))

                OnRecipeDecrement(1);

            if (SimpleInput.GetButtonDown("OnDecrementMilk"))

                OnRecipeDecrement(2);

            if (SimpleInput.GetButtonDown("OnDecrementIceCubes")
                && playerRecipe[3] != ENV.DEFAULT_RECIPE[3])

                OnRecipeDecrement(3);

            if (SimpleInput.GetButtonDown("OnIncrementMango"))

                OnRecipeIncrement(0);

            if (SimpleInput.GetButtonDown("OnIncrementGraham"))

                OnRecipeIncrement(1);

            if (SimpleInput.GetButtonDown("OnIncrementMilk"))

                OnRecipeIncrement(2);

            if (SimpleInput.GetButtonDown("OnIncrementIceCubes"))

                OnRecipeIncrement(3);

        }

        if (bottomNavigationState == BottomNavigationStates.supplies)
        {

            string conjunctions = GetConjuctions(suppliesState);

            for (int scale = 0; scale < 3; scale++)
            {

                supplyUIImages[scale].sprite = supplySprites[suppliesState];
                supplyPriceUITexts[scale].text = string.Format(
                    "{0} {1} {2}",
                    ENV.SUPPLIES[suppliesState, 0, scale].ToString(),
                    conjunctions,
                    ENV.SUPPLIES[suppliesState, 1, scale].ToString("0.00")
                    );
                supplyQuantityUITexts[scale].text = supplies[suppliesState, scale].ToString();
                supplyDecrementUIButtons[scale].interactable = supplies[suppliesState, scale] > 0;
                supplyIncrementUIButtons[scale].interactable = playerCapital - ENV.SUPPLIES[suppliesState, 1, scale] >= 0
                    && HasAvailableSpace(scale);

            }

            buyUIButton.interactable = FindObjectOfType<Player>().PlayerCapital != playerCapital;

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

            if (SimpleInput.GetButtonDown("OnDecrementSmall"))

                OnSuppliesDecrement(0);

        }

        if (SimpleInput.GetButtonDown("OnYes")
            && IsEnabled
            && isStartingOver)

            OnStartOver(true);

        if (SimpleInput.GetButtonDown("OnNo")
            && IsEnabled
            && isStartingOver)

            OnStartOver(false);


        if (stepState < 19)
        {

            GameManager.OnNowInforming(ENV.TUTORIAL_TEXT[stepState]);
            GameManager
                .Animator
                .SetInteger("stepState", stepState);

        }

        if (SimpleInput.GetButtonDown("OnNext"))
        {

            OnNext();

            if (stepState == 1)
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogTutorialManager>().OnDialog("REQUIRED", "Not enough supplies to start the day. Change your recipe or buy more supplies.");

            }
            else if (stepState == 2)

                OnOK();

            else if (stepState == 4)

                FindObjectOfType<SoundsManager>().OnClicked();

            else if (stepState == 5)
            {

                spend = FindObjectOfType<Player>().PlayerCapital - playerCapital;
                string description = string.Format("You'll going to spend\n₱ {0} on goods.\nThis will be alright.", spend.ToString("0.00"));
                FindObjectOfType<SoundsManager>().OnClicked();
                FindObjectOfType<DialogTutorialManager>().OnDialog("BUYING", description);

            }
            else if (stepState == 6)
            {

                OnBuySuccess();
                OnOK();

            }
            else if (stepState == 19)
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                DialogManager.OnDialog(
                    "TUTORIAL",
                    "Do you want to start the tutorial over again?",
                    "optionPane1"
                    );
                isStartingOver = true;

            }

        }

        if (supplies[0, 0] > 0
            && supplies[1, 0] > 0
            && supplies[2, 0] > 0
            && supplies[3, 0] > 0
            && supplies[4, 0] > 0
            && stepState == 3)

            OnNext();

        if (playerPrice == ENV.DEFAULT_PRICE
            && stepState == 16)

            OnNext();

        if ((stepState == 6
            || stepState == 8
            || stepState == 9
            || stepState == 11
            || stepState == 13
            || stepState == 14)
            && !isDone)

            After();

    }

    private void Init()
    {

        isStartingOver = false;

    }

    private void OnNext()
    {

        stepState++;
        FindObjectOfType<GameManager>().OnNext();

    }

    private void OnOK()
    {

        FindObjectOfType<SoundsManager>().OnGrahamCrack();
        GameManager.OnTrigger("okTutorial");

    }

    private async void After()
    {

        isDone = !isDone;
        await Task.Delay(7000);
        OnNext();
        isDone = !isDone;

    }

    private void BuildRecipe(int _recipeState, bool _isRecipeState)
    {

        recipeUIPanel[_recipeState].alpha =
            _isRecipeState
            ? 1
            : 0;
        recipeUIPanel[_recipeState].blocksRaycasts = _isRecipeState;

    }

    private void OnStartOver(bool _isStartingOver)
    {

        FindObjectOfType<SoundsManager>().OnGrahamCrack();
        GameManager.OnTrigger("ok");

        if (_isStartingOver)
        {

            OnTutorialStart();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        else

            OnStartDay();

        Init();

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

        string navigation = GameManager.GetToggleName(bottomNavigationUIPanel);
        bottomNavigationState = GetBottomNavigationState(navigation);

        if (lastBottomNavigationState == bottomNavigationState)
        {

            GameManager.OnTrigger("back");
            lastBottomNavigationState = BottomNavigationStates.idle;

        }
        else
        {

            if (lastBottomNavigationState == BottomNavigationStates.idle)

                GameManager.OnTrigger("initialNavigation");

            else

                GameManager.OnTrigger("navigation");

            GameManager.Animator.SetInteger("bottomNavigationState", (int)bottomNavigationState);
            lastBottomNavigationState = bottomNavigationState;

        }

        InitState();
        OnCancel();
        OnNext();

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

        int quantityPerPrice = (int)ENV.SUPPLIES[suppliesState, 0, _scale];
        double price = ENV.SUPPLIES[suppliesState, 1, _scale];
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

        int quantityPerPrice = (int)ENV.SUPPLIES[suppliesState, 0, _scale];
        double price = ENV.SUPPLIES[suppliesState, 1, _scale];
        bool isIncrementable = playerCapital - price >= 0;

        if (isIncrementable)
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            supplies[suppliesState, _scale] += quantityPerPrice;
            playerCapital -= price;
            return;

        }
        else if (!HasAvailableSpace(_scale))

            DialogManager.OnDialog(
                "SORRY",
                "You've insufficient storage to store this item",
                "dialog");

        else

            DialogManager.OnDialog(
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

        PlayerPrefs.SetInt("is_tutorial_start", 1);
        FindObjectOfType<Player>().PlayerCapital -= spend;

        for (int supply = 0; supply < 5; supply++)

            for (int scale = 0; scale < 3; scale++)

                playerSupplies[supply] += supplies[supply, scale];

        await Task.Delay(1000);

        Init();
        OnCancel();
        FindObjectOfType<Player>().OnAutoSave();

    }

    private void OnPriceIncrement()
    {

        if (playerPrice < ENV.MAXIMUM_PRICE)
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

    private void OnStartDay()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }

    private void GetStorage()
    {

        for (int supply = 0; supply < playerStorage.Length; supply++)
        {

            suppliesUIImages[supply].fillAmount = (float)playerSupplies[supply] / playerStorage[supply];
            suppliesBackgroundUIImages[supply].fillAmount = (float)playerSupplies[supply] / playerStorage[supply];
            suppliesUITexts[supply].text = playerSupplies[supply].ToString();
            suppliesBackgroundUITexts[supply].text = playerSupplies[supply].ToString();

        }

    }

    private bool HasAvailableSpace(int _scale)
    {

        int overAllSupplies = supplies[suppliesState, 0] + supplies[suppliesState, 1] + supplies[suppliesState, 2];
        bool hasAvailableSpace = playerSupplies[suppliesState] + overAllSupplies + ENV.SUPPLIES[suppliesState, 0, _scale] <= playerStorage[suppliesState];

        return hasAvailableSpace;

    }

    private void OnTutorialStart()
    {

        FindObjectOfType<Player>().PlayerCapital = ENV.CAPITAL;
        FindObjectOfType<Player>().PlayerPrice = ENV.STARTING_PRICE;
        FindObjectOfType<Player>().PlayerRecipe = ENV.STARTING_RECIPE;
        FindObjectOfType<Player>().PlayerSupplies = ENV.STARTING_SUPPLIES;
        FindObjectOfType<Player>().OnAutoSave();

    }

    public bool IsEnabled { private get; set; }

}