using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    /*
     * Let's privately declare an OBJECT field
     * where we can place our LOGIN UI BUTTON later.
     * Also, let's set it in a SERIALIZED FIELD manner
     * so that, we can be able to access it in the INSPECTOR PANEL.
     */
    [SerializeField]
    private Button loginUIButton;

    /*
     * Let's privately declare an OBJECT field
     * where we can store our DOCUMENT REFERENCE later.
     */
    private DocumentReference documentRef;

    /*
     * Let's privately declare an OBJECT field
     * where we can store our Firebase Firestore INSTANCE later.
     */
    private FirebaseFirestore firebaseFirestore;

    /*
     * Let's privately declare an OBJECT field
     * where we can store our Firebase Firestore INSTANCE later.
     */
    private FirebaseUser firebaseUser;

    /*
     * Let's privately declare a BOOLEAN field.
     * If it's value is TRUE.
     * Then, the system is on LOADING state.
     * Else, on IDLE state.
     */
    private bool isLoading;

    /*
     * Let's privately declare a BOOLEAN field.
     * If it's value is TRUE.
     * Then, the system is connected to the internet.
     * Else, not.
     */
    private bool isConnected;

    /*
     * A predefined (built-in) method in UNITY
     * where is called just before any of the Update methods is called the first time.
     * Also, let's set it in an ASYNCHRONOUS manner
     * since it contains an AWAIT statement.
     */
    async void Start()
    {

        /*
         * As the SUMMARY of all the things that we have done here.
         * We basically put our system on LOADING state.
         * Then, create an INSTANCE of an OBJECT in order to be used later.
         * Finally, the system will check if there's a user currently logged in.
         * Therefore, already logged in user must be redirect depends upon
         * the previous process it took.
         * Else, the user must be redirect in the login scene instead.
         */

        /*
         * First, let's now initialize the value of a BOOLEAN to true.
         * Therefore, the system must run in a LOADING state manner.
         */
        isLoading = true;

        /* 
         * Then, let's delay a couple of 1000 milliseconds (1s).
         */
        await Task.Delay(1000);

        /*
         * Then, let's now initialize the value of an OBJECT by creating an INSTANCE of that OBJECT.
         */
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().FirebaseFirestore;

        /*
         * Finally, let's check if there's a user currently logged in.
         */
        CheckCurrentAuthState();

    }

    /*
     * A predefined (built-in) method in UNITY
     * where is called every frame, if the MonoBehaviour is enabled.
     */
    void Update()
    {

        /*
         * A field that continuously holds a boolean value.
         * If it's value is TRUE, then the system is connected to the internet. Else, FALSE.
         */
        isConnected = Application.internetReachability != NetworkReachability.NotReachable;

        FindObjectOfType<GameManager>()
            .Animator
            .SetBool("isLoading", isLoading);

        if (!isLoading)
        {

            loginUIButton.interactable = isConnected;

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

    /*
     * Upon calling this method the system must check if there's a user currently logged in.
     * If there's.
     * Then, already logged in user must be redirect depends upon the previous process it took.
     * Else, the system must go on IDLE state.
     */
    private void CheckCurrentAuthState()
    {

        firebaseUser = FindObjectOfType<FirebaseAuthManager>().FirebaseUser;

        int isStudent = PlayerPrefs.GetInt("player_is_student", -1);

        if (firebaseUser == null)

            isLoading = false;

        else if (isConnected)
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
        float reputation = PlayerPrefs.GetFloat("player_reputation", 0);

        if (!roomId.Equals(""))
        {

            if (reputation > 0)

                return 4;

            else

                return 6;

        }
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
        double playerReputation = player.player_reputation;

        FindObjectOfType<Player>().OnGlobalLoad(player);

        PlayerPrefs.SetInt("player_is_student", !playerIsStudent
            ? 0
            : 1);

        PlayerPrefs.SetFloat("player_reputation", (float) playerReputation);

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
            SceneManager.LoadScene(
                playerReputation > 0 
                ? 4 
                : 6);

        }
        else

            SceneManager.LoadScene(2);

    }


    public void OnSignInSuccess() => SignInSuccess();

}
