using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreparationPhaseManager : MonoBehaviour
{

    [SerializeField] private Sprite[] bottomNavigationSelectedUIButtons;
    [SerializeField] private Sprite[] bottomNavigationNormalUIButtons;
    [SerializeField] private Image[] bottomNavigationUIButtons;
    [SerializeField] private Image smallSupplyHUD;
    [SerializeField] private Image mediumSupplyHUD;
    [SerializeField] private Image largeSupplyHUD;
    [SerializeField] private Sprite[] suppliesHUD;
    [SerializeField] private TextMeshProUGUI bottomNavigationStateUIText;
    [SerializeField] private TextMeshProUGUI smallPriceUIText;
    [SerializeField] private TextMeshProUGUI smallQuantityUIText;
    [SerializeField] private TextMeshProUGUI mediumPriceUIText;
    [SerializeField] private TextMeshProUGUI mediumQuantityUIText;
    [SerializeField] private TextMeshProUGUI largePriceUIText;
    [SerializeField] private TextMeshProUGUI largeQuantityUIText;
    [SerializeField] private Toggle mangoUINavButton;
    [SerializeField] private ToggleGroup navigationPanel;

    [SerializeField]
    private TextMeshProUGUI capitalUIText;

    [SerializeField]
    private TextMeshProUGUI[] suppliesUITexts;

    [SerializeField]
    private TextMeshProUGUI[] recipeUITexts;

    [SerializeField]
    private Button smallDecrementUIButton;

    [SerializeField]
    private Button mediumDecrementUIButton;

    [SerializeField]
    private Button largeDecrementUIButton;

    [SerializeField]
    private Button smallIncrementUIButton;

    [SerializeField]
    private Button mediumIncrementUIButton;

    [SerializeField]
    private Button largeIncrementUIButton;

    [SerializeField]
    private Button buyUIButton;

    [SerializeField]
    private Button cancelUIButton;


    private enum NavigationStates { idle, results, upgrades, staff, marketing, recipe, supplies };

    private NavigationStates navigationState;
    private NavigationStates lastNavigationState;

    private double[,] SUPPLIES_DOUBLE;
    private int[,] RECIPE_INT;
    private int[,,] SUPPLIES_INT;

    private int suppliesState;
    private double capital;
    private int[] left;
    private int[] perServe;

    private double spend;
    private bool isBuying;
    private bool isConnected;
    private bool isCanceling;

    void Start()
    {

        Init();

        SUPPLIES_INT = new int[5, 2, 3]
        {

            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } },
            { { 0, 0, 0 }, { 0, 0, 0 } }

        };

        SUPPLIES_DOUBLE = new double[5, 3]
        {

            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }

        };

        RECIPE_INT = new int[4, 2]
        {

            { 0, 0 },
            { 0, 0 },
            { 0, 0 },
            { 0, 0 }

        };

        SUPPLIES_INT[0, 1, 0] = 12;
        SUPPLIES_INT[0, 1, 1] = 24;
        SUPPLIES_INT[0, 1, 2] = 48;

        SUPPLIES_INT[1, 1, 0] = 50;
        SUPPLIES_INT[1, 1, 1] = 200;
        SUPPLIES_INT[1, 1, 2] = 500;

        SUPPLIES_INT[2, 1, 0] = 12;
        SUPPLIES_INT[2, 1, 1] = 20;
        SUPPLIES_INT[2, 1, 2] = 50;

        SUPPLIES_INT[3, 1, 0] = 20;
        SUPPLIES_INT[3, 1, 1] = 50;
        SUPPLIES_INT[3, 1, 2] = 120;

        SUPPLIES_INT[4, 1, 0] = 75;
        SUPPLIES_INT[4, 1, 1] = 225;
        SUPPLIES_INT[4, 1, 2] = 500;

        SUPPLIES_DOUBLE[0, 0] = 260;
        SUPPLIES_DOUBLE[0, 1] = 500;
        SUPPLIES_DOUBLE[0, 2] = 970;

        SUPPLIES_DOUBLE[1, 0] = 86;
        SUPPLIES_DOUBLE[1, 1] = 330;
        SUPPLIES_DOUBLE[1, 2] = 800;

        SUPPLIES_DOUBLE[2, 0] = 300;
        SUPPLIES_DOUBLE[2, 1] = 480;
        SUPPLIES_DOUBLE[2, 2] = 1150;

        SUPPLIES_DOUBLE[3, 0] = 30;
        SUPPLIES_DOUBLE[3, 1] = 60;
        SUPPLIES_DOUBLE[3, 2] = 120;

        SUPPLIES_DOUBLE[4, 0] = 70;
        SUPPLIES_DOUBLE[4, 1] = 200;
        SUPPLIES_DOUBLE[4, 2] = 420;

        RECIPE_INT[0, 1] = 20;
        RECIPE_INT[1, 1] = 10;
        RECIPE_INT[2, 1] = 10;
        RECIPE_INT[3, 1] = 7;

        suppliesState = 0;

        capital = FindObjectOfType<Player>().PlayerCapital;
        left = FindObjectOfType<Player>().PlayerLeft;
        perServe = FindObjectOfType<Player>().PlayerPerServe;

    }

    void Update()
    {

        isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        capitalUIText.text = string.Format("₱ {0}", capital.ToString("0.00"));

        suppliesUITexts[0].text = left[0].ToString();
        suppliesUITexts[1].text = left[1].ToString();
        suppliesUITexts[2].text = left[2].ToString();
        suppliesUITexts[3].text = left[3].ToString();
        suppliesUITexts[4].text = left[4].ToString();

        recipeUITexts[0].text = perServe[0].ToString();
        recipeUITexts[1].text = perServe[1].ToString();
        recipeUITexts[2].text = perServe[2].ToString();
        recipeUITexts[3].text = perServe[3].ToString();

        string bottomNavigationState = GetBottomNavigationState(FindObjectOfType<GameManager>().GetToggleName(navigationPanel));

        if (bottomNavigationState != "")

            bottomNavigationStateUIText.text = bottomNavigationState;

        if (SimpleInput.GetButtonUp("OnNavigation"))

            OnNavigation();

        bottomNavigationUIButtons[0].sprite = 
            lastNavigationState == NavigationStates.results
            ? bottomNavigationSelectedUIButtons[0] 
            : bottomNavigationNormalUIButtons[0];

        bottomNavigationUIButtons[1].sprite = 
            lastNavigationState == NavigationStates.upgrades 
            ? bottomNavigationSelectedUIButtons[1] 
            : bottomNavigationNormalUIButtons[1];

        bottomNavigationUIButtons[2].sprite =
            lastNavigationState == NavigationStates.staff
            ? bottomNavigationSelectedUIButtons[2]
            : bottomNavigationNormalUIButtons[2];

        bottomNavigationUIButtons[3].sprite =
            lastNavigationState == NavigationStates.marketing
            ? bottomNavigationSelectedUIButtons[3]
            : bottomNavigationNormalUIButtons[3];

        bottomNavigationUIButtons[4].sprite =
            lastNavigationState == NavigationStates.recipe
            ? bottomNavigationSelectedUIButtons[4]
            : bottomNavigationNormalUIButtons[4];

        bottomNavigationUIButtons[5].sprite =
            lastNavigationState == NavigationStates.supplies
            ? bottomNavigationSelectedUIButtons[5]
            : bottomNavigationNormalUIButtons[5];

        

        if (navigationState == NavigationStates.supplies)
        {

            smallQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 0].ToString();
            mediumQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 1].ToString();
            largeQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 2].ToString();

            smallDecrementUIButton.interactable = SUPPLIES_INT[suppliesState, 0, 0] != 0;
            mediumDecrementUIButton.interactable = SUPPLIES_INT[suppliesState, 0, 1] != 0;
            largeDecrementUIButton.interactable = SUPPLIES_INT[suppliesState, 0, 2] != 0;

            smallIncrementUIButton.interactable = capital - SUPPLIES_DOUBLE[suppliesState, 0] >= 0;
            mediumIncrementUIButton.interactable = capital - SUPPLIES_DOUBLE[suppliesState, 1] >= 0;
            largeIncrementUIButton.interactable = capital - SUPPLIES_DOUBLE[suppliesState, 2] >= 0;

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

            if (SimpleInput.GetButtonDown("OnYes") && isBuying)
            {

                FindObjectOfType<SoundsManager>().OnGrahamCrack();
                FindObjectOfType<GameManager>()
                    .Animator
                    .SetTrigger("ok");
                OnBuySuccess();
                isBuying = !isBuying;

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

        }

        if (SimpleInput.GetButtonDown("OnOK"))

            Init();

    }

    private void OnNavigation()
    {

        string navigation = FindObjectOfType<GameManager>().GetToggleName(navigationPanel);
        navigationState = GetNavigationState(navigation);

        if (lastNavigationState == navigationState)
        {

            FindObjectOfType<GameManager>().Animator.SetTrigger("back");
            lastNavigationState = NavigationStates.idle;

        }
        else
        {

            if (lastNavigationState == NavigationStates.idle)

                FindObjectOfType<GameManager>().Animator.SetTrigger("initialNavigation");

            else

                FindObjectOfType<GameManager>().Animator.SetTrigger("navigation");

            FindObjectOfType<GameManager>().Animator.SetInteger("navigationState", (int)navigationState);
            lastNavigationState = navigationState;

        }

        if (navigationState == NavigationStates.supplies)
        {

            mangoUINavButton.isOn = true;
            OnSuppliesQuantityClear();
            OnSuppliesNavigation(0);

        }

    }

    private NavigationStates GetNavigationState(string _navigation)
    {

        return _navigation switch
        {

            "ResultsUINavButton" => NavigationStates.results,

            "UpgradesUINavButton" => NavigationStates.upgrades,

            "StaffUINavButton" => NavigationStates.staff,

            "MarketingUINavButton" => NavigationStates.marketing,

            "RecipeUINavButton" => NavigationStates.recipe,

            "SuppliesUINavButton" => NavigationStates.supplies,

            _ => NavigationStates.idle,

        };

    }

    private string GetBottomNavigationState(string _navigation)
    {

        return _navigation switch
        {

            "UpgradesUINavButton" => "Upgrades",

            "StaffUINavButton" => "Staff",

            "MarketingUINavButton" => "Marketing",

            "RecipeUINavButton" => "Recipe",

            "SuppliesUINavButton" => "Supplies",

            _ => "Results"

        };

    }

    private void OnSuppliesNavigation(int _suppliesNavigationState)
    {

        suppliesState = _suppliesNavigationState;

        smallSupplyHUD.sprite = suppliesHUD[_suppliesNavigationState];
        mediumSupplyHUD.sprite = suppliesHUD[_suppliesNavigationState];
        largeSupplyHUD.sprite = suppliesHUD[_suppliesNavigationState];

        smallPriceUIText.text = string.Format(
            "{0} {1} {2}",
            SUPPLIES_INT[_suppliesNavigationState, 1, 0].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES_DOUBLE[_suppliesNavigationState, 0].ToString("0.00")
            );

        mediumPriceUIText.text = string.Format(
            "{0} {1} {2}",
            SUPPLIES_INT[_suppliesNavigationState, 1, 1].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES_DOUBLE[_suppliesNavigationState, 1].ToString("0.00")
            );

        largePriceUIText.text = string.Format(
            "{0} {1} {2}",
            SUPPLIES_INT[_suppliesNavigationState, 1, 2].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES_DOUBLE[_suppliesNavigationState, 2].ToString("0.00")
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

        SUPPLIES_INT[0, 0, 0] = 0;
        SUPPLIES_INT[0, 0, 1] = 0;
        SUPPLIES_INT[0, 0, 2] = 0;
        SUPPLIES_INT[1, 0, 0] = 0;
        SUPPLIES_INT[1, 0, 1] = 0;
        SUPPLIES_INT[1, 0, 2] = 0;
        SUPPLIES_INT[2, 0, 0] = 0;
        SUPPLIES_INT[2, 0, 1] = 0;
        SUPPLIES_INT[2, 0, 2] = 0;
        SUPPLIES_INT[3, 0, 0] = 0;
        SUPPLIES_INT[3, 0, 1] = 0;
        SUPPLIES_INT[3, 0, 2] = 0;
        SUPPLIES_INT[4, 0, 0] = 0;
        SUPPLIES_INT[4, 0, 1] = 0;
        SUPPLIES_INT[4, 0, 2] = 0;

    }

    private void OnSuppliesDecrement(int _scale)
    {

        int quantityPerPrice = SUPPLIES_INT[suppliesState, 1, _scale];
        double price = SUPPLIES_DOUBLE[suppliesState, _scale];

        if (SUPPLIES_INT[suppliesState, 0, _scale] - quantityPerPrice >= 0)
        {

            SUPPLIES_INT[suppliesState, 0, _scale] -= quantityPerPrice;
            capital += price;

        }

    }

    private void OnSuppliesIncrement(int _scale)
    {

        int quantityPerPrice = SUPPLIES_INT[suppliesState, 1, _scale];
        double price = SUPPLIES_DOUBLE[suppliesState, _scale];

        if (capital - price >= 0)
        {

            SUPPLIES_INT[suppliesState, 0, _scale] += quantityPerPrice;
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

    }

    private void Init()
    {

        spend = 0;
        isBuying = false;
        isCanceling = false;

    }

    private async void OnBuySuccess()
    {

        FindObjectOfType<Player>().PlayerCapital -= spend;

        left[0] += SUPPLIES_INT[0, 0, 0] + SUPPLIES_INT[0, 0, 1] + SUPPLIES_INT[0, 0, 2];
        left[1] += SUPPLIES_INT[1, 0, 0] + SUPPLIES_INT[1, 0, 1] + SUPPLIES_INT[1, 0, 2];
        left[2] += SUPPLIES_INT[2, 0, 0] + SUPPLIES_INT[2, 0, 1] + SUPPLIES_INT[2, 0, 2];
        left[3] += SUPPLIES_INT[3, 0, 0] + SUPPLIES_INT[3, 0, 1] + SUPPLIES_INT[3, 0, 2];
        left[4] += SUPPLIES_INT[4, 0, 0] + SUPPLIES_INT[4, 0, 1] + SUPPLIES_INT[4, 0, 2];

        await Task.Delay(1000);

        Init();
        OnCancel();

        FindObjectOfType<Player>().OnAutoSave(isConnected);

    }

}