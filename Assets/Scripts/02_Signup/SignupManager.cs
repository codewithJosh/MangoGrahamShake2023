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

    private int COUNTDOWN;

    private int attempts;

    void Start()
    {

        IsLoading = false;
        COUNTDOWN = 1800;
        attempts = 5;
        countdownUIButton.SetActive(false);
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().FirebaseFirestore;

        int countdown = PlayerPrefs.GetInt("countdown", 0);

        if (countdown > 0)

            StartCoroutine(CountdownToStart(countdown));

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
            .SetBool("isLoading", IsLoading);

        if (!IsLoading)
        {

            bool isEmpty = LastName.Equals("")
            || FirstName.Equals("")
            || Value.Equals("");

            signupUIButton.interactable = IsConnected && !isEmpty;

            if (SimpleInput.GetButtonDown("OnSubmit"))
            {

                if (!IsConnected)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

                }
                else if (isEmpty)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please fill out all the fields first",
                        "dialog");

                }
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
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                    "FAILED",
                    "Too many attempts.\nPlease try again later.",
                    "dialog");

            }

        }

        if (SimpleInput.GetButtonDown("OnNotYet"))
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            IsLoading = false;
            FindObjectOfType<GameManager>().OnTrigger("ok");
        }

        if (SimpleInput.GetButtonDown("OnDone"))
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>().OnTrigger("ok");
            Signup();

        }


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
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "Student ID is alreay taken",
                        "dialog");

                }

            });

    }

    private bool IsValidStudentId()
    {

        FindObjectOfType<SoundsManager>().OnClicked();

        if (Value.Length != 13)
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "REQUIRED",
                "Student ID must be at least\n(13) thirteen characters",
                "dialog");

            return false;

        }
        else if (IsInvalidStudentId())
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "REQUIRED",
                "Please provide a valid Student ID",
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

        FindObjectOfType<SoundsManager>().OnError();

        attempts--;

        if (attempts != 0)

            FindObjectOfType<DialogManager>().OnDialog(
                "FAILED",
                string.Format("You provide an incorrect Verification Code\n({0} attempts left)", attempts),
                "dialog");

        else

            StartCoroutine(CountdownToStart(COUNTDOWN));

        return false;

    }

    IEnumerator CountdownToStart(int _countdown)
    {

        int countdown = _countdown;
        signupUIButton.image.sprite = resources[0];
        countdownUIButton.SetActive(true);

        while (countdown > 0)
        {

            countdownUIText.text = string.Format("{0}", GetTime(countdown));

            yield return new WaitForSeconds(1f);

            countdown--;

            PlayerPrefs.SetInt("countdown", countdown);

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

            FindObjectOfType<SoundsManager>().OnClicked();
            IsLoading = true;

            FindObjectOfType<DialogManager>().OnDialog(
                "WARNING",
                "Please make sure that all the information you provided are correct. You can?t return once you?re in the lobby.",
                "optionPane2");

        }

    }

    private void Signup()
    {

        bool isStudent = FindObjectOfType<ToggleManager>().IsStudent;
        string playerId = PlayerPrefs.GetString("player_id", "");
        string playerImage = PlayerPrefs.GetString("player_image", "");

        if (!playerId.Equals("")
            && !playerImage.Equals(""))
        {

            PlayerStruct player = FindObjectOfType<Player>().OnGlobalSavePlayer(
            isStudent,
            FirstName,
            playerId,
            playerImage,
            LastName,
            isStudent ? Value : "");

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
                                string.Format("Congratulations!\nYou?re Successfully {0}!", isStudent
                                ? "Added"
                                : "Verified"),
                                "dialog"
                                );

                            PlayerPrefs.SetInt("player_is_student", !isStudent
                                ? 0
                                : 1);

                            PlayerPrefs.SetFloat("player_reputation", 0);

                            await Task.Delay(3000);
                            SceneManager.LoadScene(2);

                        });

                });

        }

    }


    /*
     * Let's privately declare a IsConnected property that has an boolean value.
     * Also, let's add both privately get and set method init.
     */
    private bool IsConnected { get; set; }

    private string LastName => valueUIInputFields[0].text.Trim().ToUpper();

    private string FirstName => valueUIInputFields[1].text.Trim().ToUpper();

    private string Value => valueUIInputFields[2].text;

    public bool IsLoading { get; private set; }

}
