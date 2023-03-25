using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    #region DECLARATION

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
    private static DocumentReference documentRef;

    #endregion

    #region START_METHOD

    /*
     * A predefined (built-in) method in UNITY
     * where is called just before any of the Update methods is called the first time.
     * Also, let's set it in an ASYNCHRONOUS manner
     * since it contains an AWAIT statement.
     */
    void Start()
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
        STATUS.IS_LOADING = true;

        /*
         * Finally, let's check if there's a user currently logged in.
         */
        CheckCurrentAuthState();

    }

    #endregion

    #region UPDATE_METHOD

    /*
     * A predefined (built-in) method in UNITY
     * where is called every frame, if the MonoBehaviour is enabled.
     */
    void Update()
    {

        GameManager.OnBool(ENV.IS_LOADING, STATUS.IS_LOADING);

        BuildLogin();

    }

    #endregion

    #region CHECK_CURRENT_AUTH_STATE_METHOD

    /*
     * Upon calling this method the system must check if there's a user currently logged in.
     * If there's.
     * Then, already logged in user must be redirect depends upon the previous process it took.
     * Else, the system must go on IDLE state.
     */
    private async static void CheckCurrentAuthState()
    {

        /* 
         * Then, let's delay a couple of 1000 milliseconds (1s).
         */
        await Task.Delay(1000);

        if (STATUS.FIREBASE_USER == null)

            STATUS.IS_LOADING = false;

        else if (STATUS.IS_CONNECTED)
        {

            FindObjectOfType<Player>().OnAutoSave();
            SignInSuccess();

        }
        else
        {

            FindObjectOfType<Player>().OnLocalLoad();
            SceneManager.LoadScene(GetSceneIndex());

        }

    }

    #endregion

    #region GET_SCENE_INDEX_METHOD

    private static int GetSceneIndex()
    {

        string hasPlayerId = PlayerPrefs.GetString("has_player_id", "");
        float reputation = PlayerPrefs.GetFloat("player_reputation", 0);

        if (hasPlayerId.Equals(""))

            return 1;

        else if (reputation <= 0)

            return 2;

        else

            return 4;

    }

    #endregion

    #region SIGN_IN_SUCCESS_METHOD

    private static void SignInSuccess()
    {

        STATUS.IS_LOADING = true;

        if (STATUS.FIREBASE_USER == null)

            return;

        string playerId = STATUS.FIREBASE_USER.UserId;
        string playerImage = STATUS.FIREBASE_USER.PhotoUrl.ToString();

        PlayerPrefs.SetString("player_id", playerId);
        PlayerPrefs.SetString("player_image", playerImage);

        documentRef = STATUS.FIREBASE_FIRESTORE
            .Collection("Players")
            .Document(playerId);

        documentRef
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {

                DocumentSnapshot doc = task.Result;

                if (doc != null
                && doc.Exists)

                    LoadPlayer(doc);

                else

                    SceneManager.LoadScene(1);

            });

    }

    #endregion

    #region LOAD_PLAYER_METHOD

    private static async void LoadPlayer(DocumentSnapshot _doc)
    {

        PlayerStruct player = _doc.ConvertTo<PlayerStruct>();
        string hasPlayerId = player.player_id;
        double playerReputation = player.player_reputation;

        PlayerPrefs.SetString("has_player_id", hasPlayerId);
        PlayerPrefs.SetFloat("player_reputation", (float)playerReputation);

        FindObjectOfType<Player>().OnGlobalLoad(player);

        DialogManager.OnDialog(
            "SUCCESS",
            "Welcome, you've successfully login!",
            "dialog");

        await Task.Delay(3000);
        SceneManager.LoadScene(
            playerReputation <= 0
            ? 2
            : 4);

    }

    #endregion

    #region BUILD_LOGIN_METHOD

    private void BuildLogin()
    {

        if (STATUS.IS_LOADING)

            return;

        loginUIButton.interactable = STATUS.IS_CONNECTED;

        if (SimpleInput.GetButtonDown("OnLogin"))

            if (!loginUIButton.IsInteractable())
            {

                FindObjectOfType<SoundsManager>().OnError();
                DialogManager.OnDialog(
                    "NOTICE",
                    "Please check your internet connection first",
                    "dialog");

            }
            else
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                GoogleAuthManager.OnLogin();

            }

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public static void OnSignInSuccess() => SignInSuccess();

    #endregion

}
