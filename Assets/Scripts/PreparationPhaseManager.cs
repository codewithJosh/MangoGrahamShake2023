using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreparationPhaseManager : MonoBehaviour
{

    [SerializeField] private Image actionUIButton;
    [SerializeField] private Sprite[] actionNormalUIButtons;
    [SerializeField] private Sprite[] actionPressedUIButtons;
    [SerializeField] private TextMeshProUGUI bottomNavigationStateUIText;
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

    private 

    void Start()
    {


    }

    void Update()
    {

        string bottomNavigationState = GetBottomNavigationState(GetNavigation(navigationPanel));

        if (bottomNavigationState != "")
            bottomNavigationStateUIText.text = bottomNavigationState;

        int checkNavigationState = lastNavigationState == NavigationStates.idle 
            ? 0 
            : 1;

        actionUIButton.sprite = 
            SimpleInput.GetButton("OnAction") 
            ? actionPressedUIButtons[checkNavigationState]
            : actionNormalUIButtons[checkNavigationState];

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

            
            if (lastNavigationState == NavigationStates.idle)
            {

                FindObjectOfType<GameManager>().GetAnimator.SetTrigger("initialNavigation");

            }
            else if (navigationState == NavigationStates.idle)
            {

                FindObjectOfType<GameManager>().GetAnimator.SetTrigger("back");

            }
            else
            {

                FindObjectOfType<GameManager>().GetAnimator.SetTrigger("navigation");

            }

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationState", (int)navigationState);
            lastNavigationState = navigationState;

        }
        /*else
        {

            FindObjectOfType<GameManager>().GetAnimator.SetTrigger("back");

        }*/


        /*if (lastNavigationState != navigationState)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetInteger("navigationState", (int)navigationState);
            lastNavigationState = navigationState;
            *//*mangoUINavButton.isOn = true;
            OnSuppliesQuantityClear();
            OnSuppliesNavigation(0);
            capital = FindObjectOfType<Player>().playerCapital;*//*

        }
        else if (lastNavigationState != 0)
        {

            FindObjectOfType<GameManager>().GetAnimator.SetTrigger("navigation");
            lastNavigationState = navigationState;

        }*/

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

            "ResultsUINavButton" => "Results",

            _ => ""

        };

    }

}
