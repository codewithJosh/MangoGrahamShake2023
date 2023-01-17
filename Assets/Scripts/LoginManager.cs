using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private Button loginUIButton;
    [SerializeField] private TextMeshProUGUI statusUIText;

    void Update()
    {

        loginUIButton.interactable = Application.internetReachability != NetworkReachability.NotReachable;

        statusUIText.text =
            loginUIButton.IsInteractable()
            ? "Let's Get Started!"
            : "No Internet Connection!";

        if (SimpleInput.GetButtonDown("OnLogin") && loginUIButton.IsInteractable())
            FindObjectOfType<AuthManager>().OnLogin();

    }

}
