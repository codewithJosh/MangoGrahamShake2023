using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private Button loginUIButton;

    void Update()
    {

        loginUIButton.interactable = Application.internetReachability != NetworkReachability.NotReachable;



        if (SimpleInput.GetButtonDown("OnLogin") && loginUIButton.IsInteractable())
            FindObjectOfType<AuthManager>().OnLogin();

    }

}
