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
    private bool isRoomLoading;
    private bool isEnabled;

    void Start()
    {

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);
        isStudent = playerIsStudent == 1;
        isRoomLoading = true;
        isEnabled = false;
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

        FindObjectOfType<GameManager>()
            .Animator
            .SetBool("isRoomLoading", isRoomLoading);

        ActionUIButton = resources[IsConnected ? isStudent ? 0 : 1 : isStudent ? 4 : 5];

        refreshUIButton.interactable = IsConnected;

        if (SimpleInput.GetButton("OnAction") && IsConnected)

            ActionUIButton = resources[isStudent ? 2 : 3];

        if (SimpleInput.GetButtonDown("OnAction"))
        {

            if (IsConnected)
            {

                if (isStudent)

                    JoinGame();

                else

                    CreateGame();

            }
            else

                FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

        }

        if (SimpleInput.GetButtonDown("OnRefresh"))

            LoadRooms();

        if (SimpleInput.GetButtonDown("OnYes"))
        {

            FindObjectOfType<GameManager>().Animator.SetTrigger("ok");
            if (isStudent)

                Debug.Log("I AM STUDENT");

            else

                RemoveGame();

        }

        if (SimpleInput.GetButtonDown("OnJoin"))

            CheckPassword();

        if (SimpleInput.GetButtonDown("OnOK") && isEnabled)
        {

            RecheckPassword();
            
        }  

        if (SimpleInput.GetButtonDown("OnCancel"))
        {

            FindObjectOfType<DialogManager>().Password = "";
            FindObjectOfType<GameManager>().Animator.SetTrigger("ok");
            isEnabled = false;
            FindObjectOfType<DialogManager>().IsEnabled = !isEnabled;

        }

    }

    private void LoadRooms()
    {

        isRoomLoading = true;
        RoomName = "";
        string playerId = PlayerPrefs.GetString("player_id", "");
        PlayerPrefs.SetString("selected_room_id", "");

        if (!IsConnected)

            FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first",
                        "dialog");

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

            FindObjectOfType<LoadRoomsManager>().OnLoadRooms(isStudent, rooms);
            isRoomLoading = false;

        }
        else

            FindObjectOfType<DialogManager>().OnDialog(
                "NOTICE",
                isStudent 
                ? "There are still no room available. Please contact your teacher" 
                : "To get started, let's create a game and invite your students",
                "dialog");

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

        if (roomId.Equals(""))

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

            FindObjectOfType<DialogManager>().OnInputDialog(
                "JOIN GAME",
                string.Format("Are you sure you want to join\n{0}?", RoomName),
                "inputDialog");
            isEnabled = true;
            FindObjectOfType<DialogManager>().IsEnabled = !isEnabled;

        }

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
                "Password must be at least (4) four characters",
                "inputDialogToDialog");

        else if (!roomPassword.Equals("") && !password.Equals(roomPassword))

            FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Password doesn't match",
                    "inputDialogToDialog");

        else

            Join();

    }

    private async void Join()
    {

        FindObjectOfType<GameManager>().Animator.SetTrigger("ok");
        string roomId = PlayerPrefs.GetString("selected_room_id", "");

        if (!roomId.Equals(""))
        {

            FindObjectOfType<Player>().RoomId = roomId;
            FindObjectOfType<Player>().OnAutoSave(IsConnected);
            FindObjectOfType<DialogManager>().OnDialog(
                     "SUCCESS",
                     "Welcome, you've successfully login!",
                     "dialog");

            PlayerPrefs.SetString("room_id", roomId);
            await Task.Delay(3000);
            SceneManager.LoadScene(4);

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

}
