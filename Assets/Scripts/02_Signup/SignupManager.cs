using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignupManager : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private Button signupUIButton;

    [SerializeField]
    private TMP_InputField lastNameUIInputField;

    [SerializeField]
    private TMP_InputField firstNameUIInputField;

    private static DocumentReference documentRef;

    #endregion

    #region START_METHOD

    void Start()
    {

        STATUS.IS_LOADING = false;

    }

    #endregion

    #region UPDATE_METHOD

    void Update()
    {

        GameManager.OnBool(ENV.IS_LOADING, STATUS.IS_LOADING);

        BuildSignup();

        if (SimpleInput.GetButtonDown("OnYes")
            && STATUS.STATE == STATUS.STATES.CONFIRMATION)
        {

            Signup();
            STATUS.STATE = STATUS.STATES.IDLE;

        }

        if (SimpleInput.GetButtonDown("OnNo")
            && STATUS.STATE == STATUS.STATES.CONFIRMATION)

            STATUS.IS_LOADING = false;

    }

    #endregion

    #region CONFIRMATION_METHOD

    private void Confirmation()
    {

        FindObjectOfType<SoundsManager>().OnClicked();
        STATUS.IS_LOADING = true;
        DialogManager.OnDialog(
            "WARNING",
            "Please make sure that all the information you provided are correct. You can’t return once you’re in the game.",
            "optionPane2");
        STATUS.STATE = STATUS.STATES.CONFIRMATION;

    }

    #endregion

    #region SIGNUP_METHOD

    private void Signup()
    {

        string playerId = PlayerPrefs.GetString("player_id", "");
        string playerImage = PlayerPrefs.GetString("player_image", "");

        if (playerId.Equals("")
            && playerImage.Equals(""))

            return;

        string playerLastName = lastNameUIInputField.text.Trim().ToUpper();
        string playerFirstName = firstNameUIInputField.text.Trim().ToUpper();

        PlayerStruct player = FindObjectOfType<PLAYER>().OnGlobalSavePlayer(
            playerFirstName,
            playerId,
            playerImage,
            playerLastName);

        documentRef = STATUS.FIREBASE_FIRESTORE
            .Collection("Players")
            .Document(playerId);

        documentRef
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {

                DocumentSnapshot doc = task.Result;

                if (doc != null
                && !doc.Exists)

                    documentRef
                    .SetAsync(player)
                    .ContinueWithOnMainThread(async task =>
                    {

                        PlayerPrefs.SetFloat("player_reputation", 0);

                        string description = "Congratulations!\nYou’re Successfully Added!";
                        DialogManager.OnDialog(
                            "SUCCESS",
                            description,
                            "dialog");

                        await Task.Delay(3000);
                        SceneManager.LoadScene(2);

                    });

            });

    }

    #endregion

    #region BUILD_SIGNUP_METHOD

    private void BuildSignup()
    {

        if (STATUS.IS_LOADING)

            return;

        bool isEmpty = lastNameUIInputField.text.Equals("")
            || firstNameUIInputField.text.Equals("");

        signupUIButton.interactable = STATUS.IS_CONNECTED && !isEmpty;

        if (SimpleInput.GetButtonDown("OnSubmit"))
        {

            if (!STATUS.IS_CONNECTED)
            {

                FindObjectOfType<SoundsManager>().OnError();
                DialogManager.OnDialog(
                    "NOTICE",
                    "Please check your internet connection first",
                    "dialog");

            }
            else if (isEmpty)
            {

                FindObjectOfType<SoundsManager>().OnError();
                DialogManager.OnDialog(
                    "REQUIRED",
                    "Please fill out all the fields first",
                    "dialog");

            }
            else

                Confirmation();

        }

    }

    #endregion

}
