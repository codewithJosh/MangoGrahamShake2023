using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    private async void CheckCurrentNetworkState()
    {

        if (!IsConnected)
        {

            FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog"
                        );
            await Task.Delay(3000);
            Application.Quit();

        }

    }

    public string GetToggle(ToggleGroup _toggleGroup)
    {

        Toggle toggle = _toggleGroup
            .ActiveToggles()
            .FirstOrDefault();
        return toggle.name.ToString();

    }

    public Animator GetAnimator => animator;

    public bool IsConnected => Application.internetReachability != NetworkReachability.NotReachable;

    public void OnCheckCurrentNetworkState() { CheckCurrentNetworkState(); }

}