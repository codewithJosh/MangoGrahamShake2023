using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{

    [SerializeField]
    private Image actionUIButton;

    [SerializeField]
    private Button refreshUIButton;

    [SerializeField]
    private Sprite[] resources;

    [SerializeField]
    private TextMeshProUGUI roomNameUIText;

    private FirebaseFirestore firebaseFirestore;
    private bool isStudent;
    private bool isPlayerLoading;
    private bool isRoomLoading;
    private bool isRechecking;
    private bool hasRoomId;

    void Start()
    {

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);
        isStudent = playerIsStudent == 1;
        isRoomLoading = true;
        isRechecking = false;
        IsRemoving = false;
        Init();

    }

    async void Init()
    {

        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().FirebaseFirestore;
        await Task.Delay(1);
        LoadRooms();

    }

    void Update()
    {

        /*
         * A field that continuously holds a boolean value.
         * If it's value is TRUE, then the system is connected to the internet. Else, FALSE.
         */
        IsConnected = Application.internetReachability != NetworkReachability.NotReachable;
        string roomId = PlayerPrefs.GetString("room_id", "");
        hasRoomId = !roomId.Equals("");

        FindObjectOfType<GameManager>()
            .Animator
            .SetBool("isRoomLoading", isRoomLoading);

        FindObjectOfType<GameManager>()
            .Animator
            .SetBool("isPlayerLoading", isPlayerLoading);

        ActionUIButton = resources[IsConnected
            ? isStudent
                ? !hasRoomId
                    ? 0
                    : 4
                : 1
            : isStudent
                ? 4
                : 5];

        refreshUIButton.interactable = IsConnected;

        if (SimpleInput.GetButton("OnAction") && IsConnected)

            ActionUIButton = resources[
                isStudent 
                ? 2 
                : 3];

        if (SimpleInput.GetButtonDown("OnAction"))
        {

            if (!IsConnected)
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

            }
            else
            {

                FindObjectOfType<SoundsManager>().OnClicked();

                if (isStudent)

                    JoinGame();

                else

                    CreateGame();

            }

        }

        if (SimpleInput.GetButtonDown("OnRefresh"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            LoadRooms();

        }

        if (SimpleInput.GetButtonDown("OnYes") && IsRemoving)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            IsRemoving = false;
            RemoveGame();
            FindObjectOfType<SettingsMenu>().IsEnabled = true;

        }

        if (SimpleInput.GetButtonDown("OnJoin"))
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            CheckPassword();

        }

        if (SimpleInput.GetButtonDown("OnOK") && isRechecking)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            RecheckPassword();

        }

        if (SimpleInput.GetButtonDown("OnCancel"))
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<DialogManager>().Password = "";
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            isRechecking = false;
            FindObjectOfType<DialogManager>().IsEnabled = true;

        }

        if (SimpleInput.GetButtonDown("OnNo"))
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            IsRemoving = false;
            FindObjectOfType<SettingsMenu>().IsEnabled = true;

        }

    }

    private void LoadRooms()
    {

        ClearList();

        isRoomLoading = true;
        RoomName = "";
        string playerId = PlayerPrefs.GetString("player_id", "");
        PlayerPrefs.SetString("selected_room_id", "");

        if (!IsConnected)
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

        }
        else if (!playerId.Equals(""))

            if (isStudent)

                firebaseFirestore
                    .Collection("Rooms")
                    .GetSnapshotAsync()
                    .ContinueWithOnMainThread(task => HandleLoadRooms(task));

            else

                firebaseFirestore
                    .Collection("Rooms")
                    .WhereEqualTo("room_player_id", playerId)
                    .GetSnapshotAsync()
                    .ContinueWithOnMainThread(task => HandleLoadRooms(task));

    }

    private async void CreateGame()
    {

        await Task.Delay(500);
        SceneManager.LoadScene(3);

    }

    private void HandleLoadRooms(Task<QuerySnapshot> _task)
    {

        QuerySnapshot documentSnapshots = _task.Result;

        if (documentSnapshots != null && documentSnapshots.Count != 0)
        {

            List<RoomStruct> rooms = new();

            foreach (DocumentSnapshot doc in documentSnapshots)
            {

                RoomStruct room = doc.ConvertTo<RoomStruct>();
                rooms.Add(room);

            }

            FindObjectOfType<LoadManager>().OnLoadRooms(rooms, isStudent);
            isRoomLoading = false;

        }
        else
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                "NOTICE",
                isStudent
                ? "There are still no room available. Please contact your teacher"
                : "To get started, let's create a game and invite your students.",
                "dialog");

        }

    }

    private void RemoveGame()
    {

        string roomId = PlayerPrefs.GetString("selected_room_id", "");

        if (!roomId.Equals(""))

            firebaseFirestore
                .Collection("Rooms")
                .Document(roomId)
                .DeleteAsync()
                .ContinueWithOnMainThread(task =>
                {

                    FindObjectOfType<DialogManager>().OnDialog(
                        "SUCCESS",
                        "The room is successfully removed!",
                        "dialog");

                    PlayerPrefs.SetString("selected_room_id", "");
                    LoadRooms();

                });

    }

    private void JoinGame()
    {

        string roomId = PlayerPrefs.GetString("selected_room_id", "");
        int currentIsFull = PlayerPrefs.GetInt("selected_is_room_full", -1);
        bool isFull = currentIsFull != 0;

        if (hasRoomId)

            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "You can only join\n(1) one room at a time",
                "dialog");

        else if (roomId.Equals(""))

            FindObjectOfType<DialogManager>().OnDialog(
                "REQUIRED",
                "Please choose a room to join first",
                "dialog");

        else if (isFull)

            FindObjectOfType<DialogManager>().OnDialog(
                "SORRY",
                "The room is already full",
                "dialog");

        else
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            FindObjectOfType<DialogManager>().OnInputDialog(
                "JOIN GAME",
                string.Format("Are you sure you want to join\n{0}?", RoomName),
                "inputDialog");
            isRechecking = true;
            FindObjectOfType<DialogManager>().IsEnabled = false;
            return;

        }

        FindObjectOfType<SoundsManager>().OnError();

    }

    private async void CheckPassword()
    {

        await Task.Delay(300);
        string roomPassword = PlayerPrefs.GetString("selected_room_password", "");
        string password = FindObjectOfType<DialogManager>().Password;

        if (!IsConnected)

            FindObjectOfType<DialogManager>().OnDialog(
                "NOTICE",
                "Please check your internet connection first",
                "inputDialogToDialog");

        else if (password.Equals(""))

            FindObjectOfType<DialogManager>().OnDialog(
                "REQUIRED",
                "Password cannot be empty",
                "inputDialogToDialog");

        else if (password.Length < 4)

            FindObjectOfType<DialogManager>().OnDialog(
                "REQUIRED",
                "Password must be at least\n(4) four characters",
                "inputDialogToDialog");

        else if (!roomPassword.Equals("") && !password.Equals(roomPassword))

            FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Password doesn't match",
                    "inputDialogToDialog");

        else
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            Join();
            return;

        }

        FindObjectOfType<SoundsManager>().OnError();

    }

    private async void Join()
    {

        string roomId = PlayerPrefs.GetString("selected_room_id", "");


        if (!roomId.Equals(""))
        {

            float playerReputation = PlayerPrefs.GetFloat("player_reputation", 0);

            FindObjectOfType<Player>().RoomId = roomId;
            FindObjectOfType<Player>().OnAutoSave(IsConnected);
            FindObjectOfType<DialogManager>().OnDialog(
                     "SUCCESS",
                     "Welcome!\nYou've successfully login!",
                     "dialog");

            PlayerPrefs.SetString("room_id", roomId);
            await Task.Delay(3000);
            SceneManager.LoadScene(
                playerReputation > 0
                ? 4
                : 6);

        }

    }

    private async void RecheckPassword()
    {

        await Task.Delay(300);
        FindObjectOfType<DialogManager>().Password = "";
        FindObjectOfType<DialogManager>().OnInputDialog(
            "JOIN GAME",
            string.Format("Are you sure you want to join\n{0}?", RoomName),
            "dialogToInputDialog");

    }

    private void LoadPlayers()
    {

        Transform Players = FindObjectOfType<LoadManager>().Players;

        if (Players != null)

            Players.ClearChildren();


        isPlayerLoading = true;
        string roomId = PlayerPrefs.GetString("selected_room_id", "");

        if (!IsConnected)
        {

            FindObjectOfType<SoundsManager>().OnError();
            FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

        }
        else if (!roomId.Equals(""))

            firebaseFirestore
                    .Collection("Players")
                    .WhereEqualTo("room_id", roomId)
                    .GetSnapshotAsync()
                    .ContinueWithOnMainThread(task =>
                    {

                        QuerySnapshot documentSnapshots = task.Result;

                        if (documentSnapshots != null && documentSnapshots.Count != 0)
                        {

                            List<PlayerStruct> players = new();

                            foreach (DocumentSnapshot doc in documentSnapshots)
                            {

                                PlayerStruct player = doc.ConvertTo<PlayerStruct>();
                                players.Add(player);

                            }

                            players.Sort((player1, player2) => player2.player_reputation.CompareTo(player1.player_reputation));

                            FindObjectOfType<LoadManager>().OnLoadPlayers(players);
                            isPlayerLoading = false;

                        }

                    });

    }

    private void ClearList()
    {

        Transform Rooms = FindObjectOfType<LoadManager>().Rooms;
        Transform Players = FindObjectOfType<LoadManager>().Players;

        if (Rooms != null)

            Rooms.ClearChildren();

        if (Players != null)

            Players.ClearChildren();

    }

    private Sprite ActionUIButton
    {

        set => actionUIButton.sprite = value;

    }

    /*
     * Let's privately declare a IsConnected property that has an boolean value.
     * Also, let's add both privately get and set method init.
     */
    private bool IsConnected { get; set; }

    public string RoomName
    {

        get => roomNameUIText.text;
        set => roomNameUIText.text = value;

    }

    public void OnJoinGame() => JoinGame();

    public void OnLoadPlayers() => LoadPlayers();

    public void OnLoadRooms() => LoadRooms();

    public bool IsRemoving { private get; set; }

}
