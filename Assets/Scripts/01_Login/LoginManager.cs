using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField]
    private TMP_InputField nameUIInputField;

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

        Build();

    }

    #endregion

    #region CHECK_CURRENT_AUTH_STATE_METHOD

    /*
     * Upon calling this method the system must check if there's a user currently logged in.
     * If there's.
     * Then, already logged in user must be redirect depends upon the previous process it took.
     * Else, the system must go on IDLE state.
     */
    private async void CheckCurrentAuthState()
    {

        string hasPlayerId = PlayerPrefs.GetString("has_player_id", "");

        /* 
         * Then, let's delay a couple of 1000 milliseconds (1s).
         */
        await Task.Delay(1000);

        if (STATUS.FIREBASE_USER == null)

            STATUS.IS_LOADING = false;

        else if (hasPlayerId.Equals(""))

            Signup();

        else if (STATUS.IS_CONNECTED)
        {

            FindObjectOfType<PLAYER>().OnAutoSave();
            SignInSuccess();

        }
        else
        {

            FindObjectOfType<PLAYER>().OnLocalLoad();
            SceneManager.LoadScene(GetSceneIndex());

        }

    }

    #endregion

    #region GET_SCENE_INDEX_METHOD

    private static int GetSceneIndex()
    {

        float reputation = PlayerPrefs.GetFloat("player_reputation", 0);
        
        if (reputation <= 0)

            return 1;
        
        return 3;

    }

    #endregion

    #region SIGN_IN_SUCCESS_METHOD

    private void SignInSuccess()
    {

        STATUS.IS_LOADING = true;

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

                    Signup();

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

        FindObjectOfType<PLAYER>().OnGlobalLoad(player);

        await Task.Delay(3000);
        SceneManager.LoadScene(
            playerReputation <= 0
            ? 1
            : 3);

    }

    #endregion

    #region SIGNUP_METHOD

    private void Signup()
    {

        string[] playerName = STATUS.FIREBASE_USER.DisplayName.ToString().ToUpper().Split(" ");

        nameUIInputField.text = playerName[0];

        DialogManager.OnDialog(
            "REQUIRED",
            "How would you like to be called?",
            ENV.INPUT_PANE);

    }

    #endregion

    #region ON_SIGNUP_SUCCESS_METHOD

    private void OnSignUpSuccess()
    {



    }

    #endregion

    #region BUILD_METHOD

    private void Build()
    {

        if (SimpleInput.GetButtonDown("Submit"))
        {

            /*if ()
            {



            }
            else

                OnSignUpSuccess();*/

        }

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
                    ENV.DIALOG);

            }
            else
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                GoogleAuthManager.OnLogin();

            }

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public void OnSignInSuccess() => SignInSuccess();

    #endregion

}
