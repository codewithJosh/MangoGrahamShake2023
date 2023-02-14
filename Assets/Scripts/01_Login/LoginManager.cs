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
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().FirebaseFirestore;
        CheckCurrentAuthState();

    }

    void Update()
    {

        /*
         * A field that continuously holds a boolean value.
         * If it's value is TRUE, then the system is connected to the internet. Else, FALSE.
         */
        IsConnected = Application.internetReachability != NetworkReachability.NotReachable;

        FindObjectOfType<GameManager>()
            .Animator
            .SetBool("isLoading", isLoading);

        if (!isLoading)
        {

            loginUIButton.interactable = IsConnected;

            if (SimpleInput.GetButtonDown("OnLogin"))

                if (loginUIButton.IsInteractable())
                {

                    FindObjectOfType<SoundsManager>().OnClicked();
                    FindObjectOfType<GoogleAuthManager>().OnLogin();

                }
                else
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

                }

        }

    }

    private void CheckCurrentAuthState()
    {

        firebaseUser = FindObjectOfType<FirebaseAuthManager>().FirebaseUser;

        int isStudent = PlayerPrefs.GetInt("player_is_student", -1);

        if (firebaseUser == null)

            isLoading = false;

        else if (IsConnected)
        {

            if (isStudent != -1)

                FindObjectOfType<Player>().OnAutoSave(true);

            SignInSuccess();

        }
        else
        {

            if (isStudent != -1)

                FindObjectOfType<Player>().OnLocalLoad();

            SceneManager.LoadScene(GetSceneIndex());

        }

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

        firebaseUser = FindObjectOfType<FirebaseAuthManager>().FirebaseUser;
        isLoading = true;

        if (firebaseUser != null)
        {

            string playerId = firebaseUser.UserId;
            string playerImage = firebaseUser.PhotoUrl.ToString();

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
        string hasRoomId = player.room_id;
        bool playerIsStudent = player.player_is_student;

        FindObjectOfType<Player>().OnGlobalLoad(player);

        PlayerPrefs.SetInt("player_is_student", !playerIsStudent
            ? 0
            : 1);

        if (!hasRoomId.Equals(""))
        {

            string roomId = PlayerPrefs.GetString("room_id", "");

            if (roomId.Equals(""))
            {

                FindObjectOfType<DialogManager>().OnDialog(
                "SUCCESS",
                "Welcome, you've successfully login!",
                "dialog");

                PlayerPrefs.SetString("room_id", hasRoomId);

            }

            await Task.Delay(3000);
            SceneManager.LoadScene(4);

        }
        else

            SceneManager.LoadScene(2);

    }


    /*
     * Let's privately declare a IsConnected property that has an boolean value.
     * Also, let's add both privately get and set method init.
     */
    private bool IsConnected { get; set; }

    public void OnLoginSuccess() { SignInSuccess(); }

}
