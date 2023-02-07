using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // At the beginning, let's privately declare a SERIALIZED ANIMATOR field for later use.
    [SerializeField]
    private Animator animator;

    void Awake()
    {

        /*
         * A field that holds a boolean value.
         * If it's value is TRUE, then the system is connected to the internet. Else, FALSE.
         */
        IsConnected = Application.internetReachability != NetworkReachability.NotReachable;

    }

    /*
     * Upon calling this method it must return a string value of current active toggle belong to the toggle group.
     */
    private string GetToggleName(ToggleGroup _toggleGroup)
    {

        /*
         * Let's locally declare a TOGGLE field.
         * Also, let's initialize it's value to a PARAMETERIZED TOGGLE GROUP
         * that returns a value of current active TOGGLE belong to that TOGGLE GROUP.
         */
        Toggle toggle = _toggleGroup
            .ActiveToggles()
            .FirstOrDefault();

        // Lastly, let's returns a string value of current active toggle.
        return toggle.name.ToString();

    }

    /*
     * Upon calling this method the system must terminate within 3000 milliseconds (3s)
     * due to the absence of internet connection.
     */
    private async void CheckCurrentNetworkState()
    {

        // First, let's checks if the system is NOT currently connected to the internet.
        if (!IsConnected)
        {

            /*
             * Then, if it's true let's display a dialog that inform the player about
             * checking his/ her internet connection first.
             */
            FindObjectOfType<DialogManager>().OnDialog(
                "NOTICE",
                "Please check your internet connection first",
                "dialog");

            // Next, let's delay a couple of 3000 milliseconds (3s).
            await Task.Delay(3000);

            // Finally, let's terminate the system.
            Application.Quit();

        }

    }

    /*
     * Let's publicly declare an Animator property that has an Animator value.
     * Also, let's add a publicly get method init.
     * Upon calling this property, the caller is allowed to communicate with the ANIMATOR INSTANCE -or- OBJECT.
     */
    public Animator Animator => animator;

    /*
     * Let's privately declare a IsConnected property that has a boolean value.
     * Also, let's add both privately get and set method init.
     * Upon calling this property, the caller is allowed to change the value of this property.
     */
    private bool IsConnected { get; set; }

    /*
     * Let's publicly declare a GetToggleName method that has a string value.
     * Also, let's add a publicly get method init.
     */
    public string OnGetToggleName(ToggleGroup _toggleGroup) => GetToggleName(_toggleGroup);

    //Let's publicly declare a CheckCurrentNetworkState method.
    public void OnCheckCurrentNetworkState() { CheckCurrentNetworkState(); }

}