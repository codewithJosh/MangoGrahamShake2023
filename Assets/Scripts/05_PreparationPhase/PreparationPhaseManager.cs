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

    void Start()
    {

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

        if (navigationState == NavigationStates.supplies)
        {

            smallQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 0].ToString();
            mediumQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 1].ToString();
            largeQuantityUIText.text = SUPPLIES_INT[suppliesState, 0, 2].ToString();

        }

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

}