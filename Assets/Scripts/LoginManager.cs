using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private Button loginUIButton;
    [SerializeField] private TextMeshProUGUI statusUIText;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {

        loginUIButton.interactable = Application.internetReachability != NetworkReachability.NotReachable;

        statusUIText.text = 
            loginUIButton.IsInteractable()
            ? "Let's Get Started!"
            : "No Internet Connection!";

    }
}
