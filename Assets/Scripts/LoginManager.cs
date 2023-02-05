using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    [SerializeField]
    private Button loginUIButton;

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

        await Task.Delay(1);
        firebaseAuth = FindObjectOfType<FirebaseAuthManager>().Auth;
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().Firestore;
        CheckCurrentAuthState();

    }

    void Update()
    {

        FindObjectOfType<GameManager>()
            .GetAnimator
            .SetBool("isLoading", isLoading);

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

            if (Application.internetReachability != NetworkReachability.NotReachable)

                SignInSuccess();

            else

                SceneManager.LoadScene(GetSceneIndex());

        }
        else

            isLoading = false;

    }

    private int GetSceneIndex()
    {

        string roomId = PlayerPrefs.GetString("room_id", "");
        int isStudent = PlayerPrefs.GetInt("player_is_student", -1);

        if (!roomId.Equals(""))

            return 4;

        else if (isStudent != -1)

            return 2;

        return 1;

    }

    private void SignInSuccess()
    {

        isLoading = true;
        firebaseUser = firebaseAuth.CurrentUser;

        if (firebaseUser != null)
        {

            string playerId = firebaseUser.UserId;
            string playerImage = firebaseAuth.CurrentUser.PhotoUrl.ToString();

            PlayerPrefs.SetString("player_id", playerId);
            PlayerPrefs.SetString("player_image", playerImage);

            documentRef = firebaseFirestore
                .Collection("Players")
                .Document(playerId);

            documentRef
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(task =>
                {

                    DocumentSnapshot doc = task.Result;

                    if (doc != null && doc.Exists)

                        CheckPlayerIsStudent(doc);

                    else

                        SceneManager.LoadScene(1);

                });

        }

    }

    private async void CheckPlayerIsStudent(DocumentSnapshot _doc)
    {

        PlayerStruct player = _doc.ConvertTo<PlayerStruct>();
        string roomId = player.room_id;
        bool playerIsStudent = player.player_is_student;

        FindObjectOfType<Player>().OnGlobalLoad(player);

        PlayerPrefs.SetInt("player_is_student", !playerIsStudent
            ? 0
            : 1);

        if (roomId != null)
        {

            FindObjectOfType<DialogManager>().OnDialog(
                "SUCCESS",
                "Welcome, you've successfully login!",
                "dialog"
                );

            PlayerPrefs.SetString("room_id", roomId);
            await Task.Delay(3000);
            SceneManager.LoadScene(4);

        }
        else

            SceneManager.LoadScene(2);

    }

    public void OnLoginSuccess() { SignInSuccess(); }

}
