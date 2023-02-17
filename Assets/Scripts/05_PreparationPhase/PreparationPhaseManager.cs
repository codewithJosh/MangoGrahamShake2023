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
    private TextMeshProUGUI priceUIText;

    [SerializeField]
    private TextMeshProUGUI advertisementUIText;

    [SerializeField]
    private TextMeshProUGUI profitPerCupUIText;

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
    private Button priceDecrementUIButton;

    [SerializeField]
    private Button priceIncrementUIButton;

    [SerializeField]
    private Button advertisementDecrementUIButton;

    [SerializeField]
    private Button advertisementIncrementUIButton;

    [SerializeField]
    private Button advertisementResetUIButton;

    [SerializeField]
    private Button priceResetUIButton;

    [SerializeField]
    private Button buyUIButton;

    [SerializeField]
    private Button cancelUIButton;

    [SerializeField]
    private Button[] recipeDecrementUIButtons;

    [SerializeField]
    private Button[] recipeResetUIButtons;

    [SerializeField]
    private CanvasGroup settingsUIButton;

    private enum NavigationStates { idle, results, upgrades, staff, marketing, recipe, supplies };

    private NavigationStates navigationState;
    private NavigationStates lastNavigationState;

    private double[,] SUPPLIES_DOUBLE;
    private int[,] RECIPE_INT;
    private int[,,] SUPPLIES_INT;
    private int[] LOCATION_INT;
    private double[] LOCATION_DOUBLE;
    private double[,] ADVERTISEMENT_DOUBLE;

    private int suppliesState;
    private double capital;
    private double price;
    private int advertisement;
    private int[] left;
    private int[] perServe;

    private double spend;
    private bool isBuying;
    private bool isConnected;
    private bool isCanceling;
    private bool isSetting;

    private int locationState;

    void Start()
    {

        Init();

        SUPPLIES_DOUBLE = FindObjectOfType<Game>().SUPPLIES_DOUBLE;
        RECIPE_INT = FindObjectOfType<Game>().RECIPE_INT;
        SUPPLIES_INT = FindObjectOfType<Game>().SUPPLIES_INT;
        LOCATION_INT = FindObjectOfType<Game>().LOCATION_INT;
        LOCATION_DOUBLE = FindObjectOfType<Game>().LOCATION_DOUBLE;
        ADVERTISEMENT_DOUBLE = FindObjectOfType<Game>().ADVERTISEMENT_DOUBLE;

        suppliesState = 0;
        locationState = 0;

        capital = FindObjectOfType<Player>().PlayerCapital;
        left = FindObjectOfType<Player>().PlayerLeft;
        perServe = FindObjectOfType<Player>().PlayerPerServe;
        price = FindObjectOfType<Player>().PlayerPrice;
        advertisement = FindObjectOfType<Player>().PlayerAdvertisement;

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

        settingsUIButton.alpha = lastNavigationState == NavigationStates.idle ? 1 : 0;
        settingsUIButton.blocksRaycasts = lastNavigationState == NavigationStates.idle;

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

        if (navigationState == NavigationStates.recipe)
        {

            recipeUITexts[0].text = perServe[0].ToString();
            recipeUITexts[1].text = perServe[1].ToString();
            recipeUITexts[2].text = perServe[2].ToString();
            recipeUITexts[3].text = perServe[3].ToString();

            recipeDecrementUIButtons[0].interactable = perServe[0] != 0;
            recipeDecrementUIButtons[1].interactable = perServe[1] != 0;
            recipeDecrementUIButtons[2].interactable = perServe[2] != 0;
            recipeDecrementUIButtons[3].interactable = perServe[3] != 0;

            recipeResetUIButtons[0].interactable = perServe[0] != 12;
            recipeResetUIButtons[1].interactable = perServe[1] != 37;
            recipeResetUIButtons[2].interactable = perServe[2] != 12;
            recipeResetUIButtons[3].interactable = perServe[3] != 10;

            FindObjectOfType<Player>().PlayerPerServe = perServe;

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

        if (navigationState == NavigationStates.marketing)
        {

            double advertisementPrice = LOCATION_INT[locationState] * ADVERTISEMENT_DOUBLE[advertisement, 0];
            double averagePrice = 20;
            double profitPerCup = price - averagePrice;

            priceUIText.text = string.Format("₱ {0}", price.ToString("0.00"));
            profitPerCupUIText.text = string.Format("Profit Per Cup:\n₱ {0}", profitPerCup.ToString("0.00"));
            advertisementUIText.text = string.Format("₱ {0}", advertisementPrice.ToString("0.00"));

            FindObjectOfType<Player>().PlayerPrice = price;
            FindObjectOfType<Player>().PlayerAdvertisement = advertisement;

            advertisementDecrementUIButton.interactable = advertisement != 0;
            priceDecrementUIButton.interactable = price != 0;

            advertisementIncrementUIButton.interactable = IsAdvertisementIncrementable();
            priceIncrementUIButton.interactable = price < 69;

            advertisementResetUIButton.interactable = advertisement != 0;
            priceResetUIButton.interactable = price != 30;

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

        OnCancel();

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
        advertisement = FindObjectOfType<Player>().PlayerAdvertisement;

    }

    private void Init()
    {

        spend = 0;
        isBuying = false;
        isCanceling = false;
        isSetting = false;
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

    private void OnPriceIncrement()
    {

        if (price < 69)

            price++;

    }

    private void OnPriceDecrement()
    {

        if (price > 0)

            price--;

    }

    private void OnAdvertisementIncrement()
    {

        if (IsAdvertisementIncrementable())
        {
            
            spend = LOCATION_INT[locationState] * ADVERTISEMENT_DOUBLE[++advertisement, 0];
            capital = FindObjectOfType<Player>().PlayerCapital;
            capital -= spend;
            
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

        if (advertisement != 0)
        {

            spend = LOCATION_INT[locationState] * ADVERTISEMENT_DOUBLE[--advertisement, 0];
            capital = FindObjectOfType<Player>().PlayerCapital;
            capital -= spend;

        }
            
    }

    private bool IsAdvertisementIncrementable()
    {

        if (advertisement < 9)
        {
 
            int newAdvertisementState = advertisement;
            spend = LOCATION_INT[locationState] * ADVERTISEMENT_DOUBLE[++newAdvertisementState, 0];

            return capital - spend >= 0;

        }

        return false;

    }

    private void OnPriceReset()
    {

        price = 30;

    }

    private void OnAdvertisementReset()
    {

        capital = FindObjectOfType<Player>().PlayerCapital;
        advertisement = 0;

    }

    private void OnRecipeDecrement(int _recipe)
    {

        if (perServe[_recipe] != 0)
            
            perServe[_recipe]--;

    }

    private void OnRecipeIncrement(int _recipe)
    {

        perServe[_recipe]++;

    }

    private void OnRecipeReset(int _recipe)
    {

        //perServe[_recipe];

    }

}