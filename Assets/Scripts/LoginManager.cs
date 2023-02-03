using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private Button loginUIButton;

    private DocumentReference documentRef;
    private FirebaseAuth firebaseAuth;
    private FirebaseFirestore firebaseFirestore;
    private FirebaseUser firebaseUser;
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
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().Firestore;
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
                        "Please check your internet connection first",
                        "dialog"
                        );

            }

        }

    }

    private void CheckCurrentAuthState()
    {

        if (firebaseAuth.CurrentUser != null)
        {

            PlayerPrefs.SetString("player_id", firebaseAuth.CurrentUser.UserId);
            SignInSuccess();

        }   
        else
            isLoading = false;

    }

    private void SignInSuccess()
    {

        isLoading = true;

        firebaseUser = firebaseAuth.CurrentUser;

        if (firebaseUser != null)
        {

            string playerId = firebaseUser.UserId;
            PlayerPrefs.SetString("player_id", playerId);

            documentRef = firebaseFirestore
                .Collection("Players")
                .Document(playerId);

            documentRef
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(task =>
            {

                DocumentSnapshot doc = task.Result;

                if (doc != null)
                {

                    if (doc.Exists)
                    {
                        
                        FindObjectOfType<DialogManager>().OnDialog(
                            "SUCCESS",
                            "Welcome, you've successfully login!",
                            "dialog"
                            );

                    }
                    else
                    {

                        SceneManager.LoadScene(1);

                    }

                }

            });

        }

    }

    public void OnLoginSuccess() { SignInSuccess(); }
}
