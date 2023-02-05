using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase.Extensions;

public class CreateGameManager : MonoBehaviour
{

    [SerializeField]
    private Button createUIButton;

    [SerializeField]
    private TextMeshProUGUI maxPlayersUIText;

    [SerializeField]
    private TMP_InputField[] valueUITexts;

    private FirebaseFirestore firebaseFirestore;
    private Query query;
    private bool isLoading;
    private int maxPlayers;

    void Start()
    {

        isLoading = false;
        maxPlayers = PlayerPrefs.GetInt("max_players", 25);
        MaxPlayers = maxPlayers;
        Init();

    }

    async void Init()
    {

        await Task.Delay(1000);
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().Firestore;

    }

    void Update()
    {

        FindObjectOfType<GameManager>()
            .GetAnimator
            .SetBool("isLoading", isLoading);

        if (!isLoading)
        {

            bool hasSomeEmpty = RoomName.Equals("")
            && Password.Equals("")
            && ConfirmPassword.Equals("");
            bool isEmpty = RoomName.Equals("")
                || Password.Equals("")
                || ConfirmPassword.Equals("");
            bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;

            createUIButton.interactable = isConnected && !isEmpty;

            if (SimpleInput.GetButtonDown("OnCancel"))

                if (!hasSomeEmpty)

                    FindObjectOfType<DialogManager>().OnDialog(
                        "WARNING",
                        "Are you sure you're no longer want to create a game?",
                        "optionPane1"
                        );

                else

                    SceneManager.LoadScene(2);

            if (SimpleInput.GetButtonDown("OnYes"))

                SceneManager.LoadScene(2);

            if (SimpleInput.GetButtonDown("OnIncrementMaxPlayers")
                && MaxPlayers < 50)

                MaxPlayers += 1;

            if (SimpleInput.GetButtonDown("OnDecrementMaxPlayers")
                && MaxPlayers > 25)

                MaxPlayers -= 1;

            if (SimpleInput.GetButtonDown("OnCreate"))
            {

                if (!isConnected)

                    FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog"
                        );

                else if (isEmpty)

                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please fill out all the fields first",
                        "dialog"
                        );

                else if (Password.Length < 6)

                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Password must be at least (6) six characters",
                        "dialog"
                        );

                else if (!Password.Equals(ConfirmPassword))

                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Password doesn't match",
                        "dialog"
                        );

                else

                    CheckRoomName();

            }

        }

    }

    private void CheckRoomName()
    {

        query = firebaseFirestore
                .Collection("Rooms")
                .WhereEqualTo("room_name", RoomName);

        query
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {
                QuerySnapshot documentSnapshots = task.Result;

                if (documentSnapshots != null && documentSnapshots.Count == 0)

                    Debug.Log("test");

                else

                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "The Room Name is Unavailable",
                        "dialog");

            });

    }

    private string RoomName
    {

        get { return valueUITexts[0].text.Trim().ToUpper(); }

    }

    private int MaxPlayers
    {

        get { return int.Parse(maxPlayersUIText.text); }
        set { maxPlayersUIText.text = value.ToString(); }

    }

    private string Password
    {

        get { return valueUITexts[1].text; }

    }

    private string ConfirmPassword
    {

        get { return valueUITexts[2].text; }

    }

}
