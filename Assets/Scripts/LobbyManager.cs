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

    void Start()
    {

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);

        isConnected = FindObjectOfType<GameManager>().IsConnected;
        isStudent = playerIsStudent == 1;
        isRoomLoading = true;
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

                    Debug.Log("I AM STUDENT");

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

        if (SimpleInput.GetButtonDown("OnNo"))

            FindObjectOfType<GameManager>().GetAnimator.SetTrigger("ok");

    }

    private void LoadRooms()
    {

        isRoomLoading = true;
        string playerId = PlayerPrefs.GetString("player_id", null);

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

        string roomId = PlayerPrefs.GetString("current_room_id", null);

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

                PlayerPrefs.SetString("current_room_id", null);
                LoadRooms();

            });

    }

    private Sprite ActionUIButton
    {

        set => actionUIButton.sprite = value;

    }

    public string RoomName
    {

        set => roomNameUIText.text = value;

    }

}
