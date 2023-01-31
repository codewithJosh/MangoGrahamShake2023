using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreparationPhaseManager : MonoBehaviour
{

    [SerializeField] private Sprite[] selectedUIButtons;
    [SerializeField] private Sprite[] normalUIButtons;
    [SerializeField] private Image[] UIButtons;
    [SerializeField] private Image smallSupplyHUD;
    [SerializeField] private Image mediumSupplyHUD;
    [SerializeField] private Image largeSupplyHUD;
    [SerializeField] private Sprite[] resources;
    [SerializeField] private TextMeshProUGUI bottomNavigationStateUIText;
    [SerializeField] private TextMeshProUGUI smallPriceUIText;
    [SerializeField] private TextMeshProUGUI smallQuantityUIText;
    [SerializeField] private TextMeshProUGUI mediumPriceUIText;
    [SerializeField] private TextMeshProUGUI mediumQuantityUIText;
    [SerializeField] private TextMeshProUGUI largePriceUIText;
    [SerializeField] private TextMeshProUGUI largeQuantityUIText;
    [SerializeField] private Toggle mangoUINavButton;
    [SerializeField] private ToggleGroup navigationPanel;

    private enum NavigationStates { idle, results, upgrades, staff, marketing, recipe, supplies };

    private NavigationStates navigationState;
    private NavigationStates lastNavigationState;

    private float[,] SUPPLIES_FLOAT;
    private int[,] RECIPE_INT;
    private int[,,] SUPPLIES_INT;

    private int suppliesState;

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

        SUPPLIES_FLOAT = new float[5, 3]
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
        SUPPLIES_INT[1, 1, 0] = 12;
        SUPPLIES_INT[1, 1, 1] = 20;
        SUPPLIES_INT[1, 1, 2] = 50;
        SUPPLIES_INT[2, 1, 0] = 12;
        SUPPLIES_INT[2, 1, 1] = 20;
        SUPPLIES_INT[2, 1, 2] = 50;
        SUPPLIES_INT[3, 1, 0] = 50;
        SUPPLIES_INT[3, 1, 1] = 200;
        SUPPLIES_INT[3, 1, 2] = 500;
        SUPPLIES_INT[4, 1, 0] = 75;
        SUPPLIES_INT[4, 1, 1] = 225;
        SUPPLIES_INT[4, 1, 2] = 400;

        SUPPLIES_FLOAT[0, 0] = 216.00f;
        SUPPLIES_FLOAT[0, 1] = 324.00f;
        SUPPLIES_FLOAT[0, 2] = 432.00f;
        SUPPLIES_FLOAT[1, 0] = 216.00f;
        SUPPLIES_FLOAT[1, 1] = 315.00f;
        SUPPLIES_FLOAT[1, 2] = 675.00f;
        SUPPLIES_FLOAT[2, 0] = 216.00f;
        SUPPLIES_FLOAT[2, 1] = 315.00f;
        SUPPLIES_FLOAT[2, 2] = 675.00f;
        SUPPLIES_FLOAT[3, 0] = 45.00f;
        SUPPLIES_FLOAT[3, 1] = 135.00f;
        SUPPLIES_FLOAT[3, 2] = 225.00f;
        SUPPLIES_FLOAT[4, 0] = 45.00f;
        SUPPLIES_FLOAT[4, 1] = 105.75f;
        SUPPLIES_FLOAT[4, 2] = 168.75f;

        RECIPE_INT[0, 1] = 20;
        RECIPE_INT[1, 1] = 10;
        RECIPE_INT[2, 1] = 10;
        RECIPE_INT[3, 1] = 7;

        suppliesState = 0;

    }

    void Update()
    {

        string bottomNavigationState = GetBottomNavigationState(GetNavigation(navigationPanel));

        if (bottomNavigationState != "")
            bottomNavigationStateUIText.text = bottomNavigationState;

        if (SimpleInput.GetButtonUp("OnNavigation"))
            OnNavigation();

        if (lastNavigationState == NavigationStates.results)
            UIButtons[0].sprite = selectedUIButtons[0];
        else
            UIButtons[0].sprite = normalUIButtons[0];

        if (lastNavigationState == NavigationStates.upgrades)
            UIButtons[1].sprite = selectedUIButtons[1];
        else
            UIButtons[1].sprite = normalUIButtons[1];

        if (lastNavigationState == NavigationStates.staff)
            UIButtons[2].sprite = selectedUIButtons[2];
        else
            UIButtons[2].sprite = normalUIButtons[2];

        if (lastNavigationState == NavigationStates.marketing)
            UIButtons[3].sprite = selectedUIButtons[3];
        else
            UIButtons[3].sprite = normalUIButtons[3];

        if (lastNavigationState == NavigationStates.recipe)
            UIButtons[4].sprite = selectedUIButtons[4];
        else
            UIButtons[4].sprite = normalUIButtons[4];

        if (lastNavigationState == NavigationStates.supplies)
            UIButtons[5].sprite = selectedUIButtons[5];
        else
            UIButtons[5].sprite = normalUIButtons[5];

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

        string navigation = GetNavigation(navigationPanel);
        navigationState = GetNavigationState(navigation);

        if (lastNavigationState == navigationState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetTrigger("back");
            lastNavigationState = NavigationStates.idle;

        }
        else
        {

            if (lastNavigationState == NavigationStates.idle)
                FindObjectOfType<GameManager>().GetAnimator.SetTrigger("initialNavigation");
            else
                FindObjectOfType<GameManager>().GetAnimator.SetTrigger("navigation");

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationState", (int)navigationState);
            lastNavigationState = navigationState;

        }

        if (navigationState == NavigationStates.supplies)
        {

            mangoUINavButton.isOn = true;
            OnSuppliesQuantityClear();
            OnSuppliesNavigation(0);

        }

    }

    private string GetNavigation(ToggleGroup _toggleGroup)
    {

        Toggle navigation = _toggleGroup.ActiveToggles().FirstOrDefault();
        return navigation.name.ToString();

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

        smallSupplyHUD.sprite = resources[_suppliesNavigationState];
        mediumSupplyHUD.sprite = resources[_suppliesNavigationState];
        largeSupplyHUD.sprite = resources[_suppliesNavigationState];

        smallPriceUIText.text = string.Format(
            "{0} {1} {2}",
            SUPPLIES_INT[_suppliesNavigationState, 1, 0].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES_FLOAT[_suppliesNavigationState, 0].ToString("0.00")
            );

        mediumPriceUIText.text = string.Format(
            "{0} {1} {2}",
            SUPPLIES_INT[_suppliesNavigationState, 1, 1].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES_FLOAT[_suppliesNavigationState, 1].ToString("0.00")
            );

        largePriceUIText.text = string.Format(
            "{0} {1} {2}",
            SUPPLIES_INT[_suppliesNavigationState, 1, 2].ToString(),
            GetConjuctions(_suppliesNavigationState),
            SUPPLIES_FLOAT[_suppliesNavigationState, 2].ToString("0.00")
            );

    }

    private string GetConjuctions(int _supply)
    {

        return _supply switch
        {

            0 => "mangoes = ₱",

            1 => "cups = ₱",

            2 => "cups = ₱",

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
