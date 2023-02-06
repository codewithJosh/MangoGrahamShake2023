using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SignupManager : MonoBehaviour
{

    [SerializeField]
    private Button signupUIButton;

    [SerializeField]
    private GameObject countdownUIButton;

    [SerializeField]
    private Sprite[] resources;

    [SerializeField]
    private TMP_InputField[] valueUIInputFields;

    [SerializeField]
    private TextMeshProUGUI countdownUIText;

    private DocumentReference documentRef;
    private FirebaseFirestore firebaseFirestore;
    private Query query;
    private bool isConnected;
    private bool isLoading;
    private int attempts;

    void Start()
    {

        isConnected = FindObjectOfType<GameManager>().IsConnected;
        isLoading = false;
        attempts = 5;
        countdownUIButton.SetActive(false);
        FindObjectOfType<GameManager>().OnCheckCurrentNetworkState();
        Init();

    }

    async void Init()
    {

        await Task.Delay(1);
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().Firestore;

    }

    void Update()
    {

        FindObjectOfType<GameManager>()
            .GetAnimator
            .SetBool("isLoading", isLoading);

        if (!isLoading)
        {

            bool isEmpty = LastName.Equals("")
            || FirstName.Equals("")
            || Value.Equals("");

            signupUIButton.interactable = isConnected && !isEmpty;

            if (SimpleInput.GetButtonDown("OnSubmit"))
            {

                if (!isConnected)

                    FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

                else if (isEmpty)

                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please fill out all the fields first",
                        "dialog");

                else
                {

                    bool isStudent = FindObjectOfType<ToggleManager>().IsStudent;

                    if (isStudent)

                        CheckStudentId(IsValidStudentId());

                    else

                        CheckVerificationCode();

                }

            }

            if (SimpleInput.GetButtonDown("OnBlockSubmit"))

                FindObjectOfType<DialogManager>().OnDialog(
                    "FAILED",
                    "Too many attempts. Please Try again later",
                    "dialog");

        }

        if (SimpleInput.GetButtonDown("OnNotYet"))
        {

            isLoading = false;
            FindObjectOfType<GameManager>()
                .GetAnimator
                .SetTrigger("ok");

        }

        if (SimpleInput.GetButtonDown("OnDone"))

            Signup();

    }

    private void CheckStudentId(bool _isValid)
    {

        query = firebaseFirestore
                .Collection("Players")
                .WhereEqualTo("player_student_id", Value);

        if (_isValid)

            query
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                QuerySnapshot documentSnapshots = task.Result;

                if (documentSnapshots != null && documentSnapshots.Count == 0)

                    Confirmation(true);

                else

                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "Student Id is alreay taken",
                        "dialog");

            });

    }

    private bool IsValidStudentId()
    {

        if (Value.Length != 13)
        {

            FindObjectOfType<DialogManager>().OnDialog(
                "REQUIRED",
                "Student Id must be at least (13) thirteen characters",
                "dialog");
            return false;

        }
        else if (IsInvalidStudentId())
        {

            FindObjectOfType<DialogManager>().OnDialog(
                "REQUIRED",
                "Please provide a valid student Id",
                "dialog");
            return false;

        }

        return true;

    }

    private bool IsInvalidStudentId()
    {

        string validCharacters = "0123456789-";

        for (int i = 0; i < Value.Length; i++)
        {

            if (i < 2 || i > 2 && i < 7 || i > 7)
            {

                if (!validCharacters.Contains(Value[i]))

                    return true;

            }
            else

                if (!Value[i].Equals('-'))

                return true;

        }

        return false;

    }

    private void CheckVerificationCode()
    {

        documentRef = firebaseFirestore
                .Collection("Verification Codes")
                .Document(Value);

        documentRef
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {

                DocumentSnapshot doc = task.Result;

                if (doc != null)

                    Confirmation(doc.Exists
                        || VerificationFailed());

            });

    }

    private bool VerificationFailed()
    {

        attempts--;

        if (attempts != 0)

            FindObjectOfType<DialogManager>().OnDialog(
                "FAILED",
                string.Format("You provide an incorrect verification code ({0} attempts left)", attempts),
                "dialog");

        else

            StartCoroutine(CountdownToStart());

        return false;

    }

    IEnumerator CountdownToStart()
    {

        int countdown = 900;
        signupUIButton.image.sprite = resources[0];
        countdownUIButton.SetActive(true);

        while (countdown > 0)
        {

            countdownUIText.text = string.Format("{0}", GetTime(countdown));

            yield return new WaitForSeconds(1f);

            countdown--;

        }

        attempts = 5;
        signupUIButton.image.sprite = resources[1];
        countdownUIButton.SetActive(false);

    }

    private string GetTime(int _time)
    {

        float minutes = Mathf.FloorToInt(_time / 60);
        float seconds = Mathf.FloorToInt(_time % 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    private void Confirmation(bool _isValid)
    {

        if (_isValid)
        {

            isLoading = true;

            FindObjectOfType<DialogManager>().OnDialog(
                "WARNING",
                "Please make sure that all the information you provided are correct. You can’t return once you’re in the lobby.",
                "optionPane2");

        }

    }

    private void Signup()
    {

        FindObjectOfType<GameManager>()
                .GetAnimator
                .SetTrigger("ok");

        bool isStudent = FindObjectOfType<ToggleManager>().IsStudent;
        string playerId = PlayerPrefs.GetString("player_id", null);
        string playerImage = PlayerPrefs.GetString("player_image", null);

        PlayerStruct player = FindObjectOfType<Player>().OnGlobalSavePlayer(
            isStudent,
            FirstName,
            playerId,
            playerImage,
            LastName,
            isStudent ? Value : null);

        documentRef = firebaseFirestore
            .Collection("Players")
            .Document(playerId);

        documentRef
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {

                DocumentSnapshot doc = task.Result;

                if (doc != null && !doc.Exists)

                    documentRef
                    .SetAsync(player)
                    .ContinueWithOnMainThread(async task =>
                    {

                        FindObjectOfType<DialogManager>().OnDialog(
                            "SUCCESS",
                            string.Format("Congratulations! You’re Successfully {0}!", isStudent
                            ? "Added"
                            : "Verified"),
                            "dialog"
                            );

                        PlayerPrefs.SetInt("player_is_student", !isStudent
                            ? 0
                            : 1);

                        await Task.Delay(3000);
                        SceneManager.LoadScene(2);

                    });

            });

    }

    private string LastName
    {

        get { return valueUIInputFields[0].text.Trim().ToUpper(); }

    }

    private string FirstName
    {

        get { return valueUIInputFields[1].text.Trim().ToUpper(); }

    }

    private string Value
    {

        get { return valueUIInputFields[2].text; }

    }

    public bool IsLoading
    {

        get { return isLoading; }

    }

}
