using Firebase.Auth;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private Button loginUIButton;

    private FirebaseAuth firebaseAuth;
    private bool isLoading;

    void Start()
    {

        isLoading = true;
        Init();

    }

    async void Init()
    {

        await Task.Delay(1000);
        firebaseAuth = FindObjectOfType<FirebaseAuthManager>().Auth;
        CheckCurrentAuthState();

    }

    void Update()
    {

        FindObjectOfType<GameManager>().GetAnimator.SetBool("isLoading", isLoading);

        if (!isLoading)
        {

            loginUIButton.interactable = Application.internetReachability != NetworkReachability.NotReachable;

            if (SimpleInput.GetButtonDown("OnLogin"))
            {

                if (loginUIButton.IsInteractable())
                    FindObjectOfType<GoogleAuthManager>().OnLogin();
                else
                    FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first"
                        );

            }

        }

    }

    private void CheckCurrentAuthState()
    {

        if (firebaseAuth.CurrentUser != null)
            PlayerPrefs.SetString("player_id", firebaseAuth.CurrentUser.UserId);
        else
            isLoading = false;

    }

    private void SignInSuccess()
    {

        FindObjectOfType<DialogManager>().OnDialog(
            "SUCCESS",
            "Welcome, you've successfully login!"
            );

    }

    public void OnLoginSuccess() { SignInSuccess(); }
}
