using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PreparationPhaseManager : MonoBehaviour
{

    [SerializeField] private Image actionUIButton;
    [SerializeField] private Sprite[] actionNormalUIButtons;
    [SerializeField] private Sprite[] actionPressedUIButtons;
    [SerializeField] private ToggleGroup navigationPanel;

    private enum NavigationStates 
    { 
        idle, 
        results, 
        upgrades, 
        staff, 
        marketing, 
        recipe, 
        supplies 
    };

    private NavigationStates navigationState;
    private NavigationStates lastNavigationState;

    void Start()
    {


    }

    void Update()
    {

        actionUIButton.sprite = 
            SimpleInput.GetButton("OnAction") 
            ? actionPressedUIButtons[1]
            : actionNormalUIButtons[1];

        if (SimpleInput.GetButtonUp("OnNavigation") 
            || SimpleInput.GetButtonUp("OnAction"))
            OnNavigation();

    }

    private void OnNavigation()
    {

        string navigation = GetNavigation(navigationPanel);
        navigationState = GetNavigationState(navigation);

        if (lastNavigationState != navigationState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationState", (int) navigationState);
            lastNavigationState = navigationState;
            /*mangoUINavButton.isOn = true;
            OnSuppliesQuantityClear();
            OnSuppliesNavigation(0);
            capital = FindObjectOfType<Player>().playerCapital;*/

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

}
