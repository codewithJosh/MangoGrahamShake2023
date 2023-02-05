using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGameManager : MonoBehaviour
{

    [SerializeField]
    private Button createUIButton;

    [SerializeField]
    private TextMeshProUGUI maxPlayersUIText;

    [SerializeField]
    private TMP_InputField[] valueUITexts;

    private DocumentReference documentRef;
    private FirebaseFirestore firebaseFirestore;
    private Query query;
    private bool isLoading;

    void Start()
    {

        isLoading = false;
        int maxPlayers = PlayerPrefs.GetInt("max_players", 25);
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

                    CreateGame();

                else

                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "The Room Name is Unavailable",
                        "dialog");

            });

    }

    private void CreateGame()
    {

        isLoading = true;

        string roomId = firebaseFirestore
            .Collection("Rooms")
            .Document()
            .Id;
        string playerId = PlayerPrefs.GetString("player_id", null);

        FirebaseRoomModel firebaseRoomModel = new()
        {

            room_slots = MaxPlayers,
            room_id = roomId,
            room_name = RoomName,
            room_password = Password,
            room_player_id = playerId

        };

        documentRef = firebaseFirestore
            .Collection("Rooms")
            .Document(roomId);

        documentRef
            .GetSnapshotAsync()
            .ContinueWithOnMainThread(task =>
            {

                DocumentSnapshot doc = task.Result;

                if (doc != null && !doc.Exists)

                    documentRef
                    .SetAsync(firebaseRoomModel)
                    .ContinueWithOnMainThread(async task =>
                    {

                        FindObjectOfType<DialogManager>().OnDialog(
                            "SUCCESS",
                            "Congratulations! The room is successfully added!",
                            "dialog"
                            );

                        PlayerPrefs.SetInt("max_players", MaxPlayers);

                        await Task.Delay(3000);
                        SceneManager.LoadScene(2);

                    });

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
