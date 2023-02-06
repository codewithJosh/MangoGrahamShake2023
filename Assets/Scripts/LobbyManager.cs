using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{

    [SerializeField] private Image actionUIButton;
    [SerializeField] private Sprite[] resources;

    private FirebaseFirestore firebaseFirestore;
    private bool isStudent;

    void Start()
    {

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);
        isStudent = playerIsStudent == 1;
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

        ActionUIButton = resources[isStudent ? 0 : 1];

        if (SimpleInput.GetButton("OnAction"))

            ActionUIButton = resources[isStudent ? 2 : 3];

        if (SimpleInput.GetButtonDown("OnAction"))

            if (isStudent)

                Debug.Log("I AM STUDENT");

            else

                CreateGame();

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

        if (SimpleInput.GetButtonDown("OnOK"))
        {

            string roomId = PlayerPrefs.GetString("current_room_id", "");

            if (roomId.Equals(""))
                
                LoadRooms();

        }
            
    }

    private void LoadRooms()
    {

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

                PlayerPrefs.SetString("current_room_id", "");

            });

    }

    private Sprite ActionUIButton
    {

        set { actionUIButton.sprite = value; }

    }

}
