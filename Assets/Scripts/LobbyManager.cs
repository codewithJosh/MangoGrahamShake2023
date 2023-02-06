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
    private Sprite[] resources;

    [SerializeField]
    private TextMeshProUGUI roomNameUIText;

    private FirebaseFirestore firebaseFirestore;
    private bool isConnected;
    private bool isStudent;
    private bool isRoomLoading;
    private bool isEnabled;
    void Start()
    {

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);

        isConnected = FindObjectOfType<GameManager>().IsConnected;
        isStudent = playerIsStudent == 1;
        isRoomLoading = true;
        isEnabled = false;
        FindObjectOfType<GameManager>().OnCheckCurrentNetworkState();
        Init();

    }

    async void Init()
    {

        await Task.Delay(1);
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().Firestore;
        await Task.Delay(1);
        LoadRooms();

    }

    void Update()
    {

        FindObjectOfType<GameManager>()
            .GetAnimator
            .SetBool("isRoomLoading", isRoomLoading);

        ActionUIButton = resources[isConnected ? isStudent ? 0 : 1 : isStudent ? 4 : 5];

        if (SimpleInput.GetButton("OnAction") && isConnected)

            ActionUIButton = resources[isStudent ? 2 : 3];

        if (SimpleInput.GetButtonDown("OnAction"))
        {

            if (isConnected)
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

            FindObjectOfType<GameManager>().GetAnimator.SetTrigger("ok");
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

            FindObjectOfType<GameManager>().GetAnimator.SetTrigger("ok");
            isEnabled = false;
            FindObjectOfType<DialogManager>().IsEnabled = !isEnabled;

        }

    }

    private void LoadRooms()
    {

        isRoomLoading = true;
        string playerId = PlayerPrefs.GetString("player_id", "");

        if (!playerId.Equals(""))

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
                "EMPTY",
                isStudent ? "" : "To get started, let's create a game and invite your students",
                "dialog");

    }

    private void RemoveGame()
    {

        string roomId = PlayerPrefs.GetString("current_room_id", "");

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

                    PlayerPrefs.SetString("current_room_id", "");
                    LoadRooms();

                });

    }

    private void JoinGame()
    {

        string roomId = PlayerPrefs.GetString("current_room_id", "");
        int currentIsFull = PlayerPrefs.GetInt("current_is_full", -1);
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
                string.Format("Are you sure you want to join {0}?", RoomName),
                "inputDialog");
            isEnabled = true;
            FindObjectOfType<DialogManager>().IsEnabled = !isEnabled;

        }

    }

    private async void CheckPassword()
    {

        await Task.Delay(500);
        string roomPassword = PlayerPrefs.GetString("current_room_password", "");
        string password = FindObjectOfType<DialogManager>().Password;

        if (!isConnected)

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

    private void Join()
    {

        FindObjectOfType<GameManager>().GetAnimator.SetTrigger("ok");
        string playerId = PlayerPrefs.GetString("player_id", "");
        string roomId = PlayerPrefs.GetString("current_room_id", "");

        if (!playerId.Equals("")
            && !roomId.Equals(""))
        {

            Dictionary<string, object> player = new();
            player.Add("room_id", roomId);

            firebaseFirestore
                .Collection("Players")
                .Document(playerId)
                .UpdateAsync(player)
                .ContinueWithOnMainThread(async task =>
                {

                    FindObjectOfType<DialogManager>().OnDialog(
                     "SUCCESS",
                     "Welcome, you've successfully login!",
                     "dialog");

                    PlayerPrefs.SetString("room_id", roomId);
                    await Task.Delay(3000);
                    SceneManager.LoadScene(4);

                });

        }

    }

    private async void RecheckPassword()
    {

        await Task.Delay(500);
        FindObjectOfType<DialogManager>().Password = "";
        FindObjectOfType<DialogManager>().OnInputDialog(
            "JOIN GAME",
            string.Format("Are you sure you want to join {0}?", RoomName),
            "dialogToInputDialog");

    }

    private Sprite ActionUIButton
    {

        set => actionUIButton.sprite = value;

    }

    public string RoomName
    {

        get => roomNameUIText.text;
        set => roomNameUIText.text = value;

    }

    public void OnJoinGame() => JoinGame();

}
