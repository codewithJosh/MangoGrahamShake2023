using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Firestore;
using System.Threading.Tasks;
using Firebase.Extensions;
using System.Collections;
using System.Threading;

public class SignupManager : MonoBehaviour
{

    [SerializeField] private Button signupUIButton;
    [SerializeField] private GameObject countdownUIButton;
    [SerializeField] private List<Sprite> resources;
    [SerializeField] private List<TMP_InputField> valueUIText;
    [SerializeField] private TextMeshProUGUI countdownUIText;

    private DocumentReference documentRef;
    private FirebaseFirestore firebaseFirestore;
    private bool isLoading;
    private int attempts;

    void Start()
    {

        isLoading = false;
        attempts = 5;
        countdownUIButton.SetActive(false);
        Init();

    }

    async void Init()
    {

        await Task.Delay(1000);
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().Firestore;

    }

    void Update()
    {

        FindObjectOfType<GameManager>().GetAnimator.SetBool("isLoading", isLoading);

        if (!isLoading)
        {

            bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;
            bool isEmpty = PlayerLastname.Equals("")
            || PlayerFirstname.Equals("")
            || PlayerValue.Equals("");

            signupUIButton.interactable = isConnected && !isEmpty;

            if (SimpleInput.GetButtonDown("OnSubmit"))
            {

                if (!isConnected)
                    FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first"
                        );

                else if (isEmpty)
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please fill out all the fields first"
                        );

                else
                {

                    bool isStudent = FindObjectOfType<ToggleManager>().IsStudent;

                    if (isStudent)
                        Signup(CheckStudentId());

                    else
                        CheckVerificationCode();

                }

            }

            if (SimpleInput.GetButtonDown("OnBlockSubmit"))
                FindObjectOfType<DialogManager>().OnDialog(
                        "FAILED",
                        "Too many attempts. Please Try again later"
                        );

        }

    }

    private bool CheckStudentId()
    {

        if (PlayerValue.Length != 13)
        {

            FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Student Id must be at least (13) thirteen characters"
                    );
            return false;

        }

        else if (IsInvalidStudentId())
        {

            FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Please provide a valid student Id"
                    );
            return false;

        }

        return true;

    }

    private bool IsInvalidStudentId()
    {

        string validCharacters = "0123456789-";

        for (int i = 0; i < PlayerValue.Length; i++)
        {

            if (i < 2 || i > 2 && i < 7 || i > 7)
            {

                if (!validCharacters.Contains(PlayerValue[i]))
                    return true;

            }
            else
                if (!PlayerValue[i].Equals('-'))
                    return true;

        }

        return false;

    }

    private void CheckVerificationCode()
    {

        documentRef = firebaseFirestore
                .Collection("Verification Codes")
                .Document(PlayerValue);

        documentRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            
            DocumentSnapshot doc = task.Result;

            if (doc != null)
                Signup(doc.Exists 
                    || VerificationFailed());

        });

    }

    private bool VerificationFailed()
    {

        attempts--;

        if (attempts != 0)
            FindObjectOfType<DialogManager>().OnDialog(
                    "FAILED",
                    string.Format("You provide an incorrect verification code ({0} attempts left)", attempts)
                    );

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

    private void Signup(bool _isValid)
    {

        if (_isValid)
        {

            isLoading = true;

        }

    }

    private string PlayerLastname
    {

        get { return valueUIText[0].text.Trim().ToUpper(); }

    }

    private string PlayerFirstname
    {

        get { return valueUIText[1].text.Trim().ToUpper(); }

    }

    private string PlayerValue
    {

        get { return valueUIText[2].text; }

    }

    public bool IsLoading
    {
        
        get { return isLoading; }

    }

}