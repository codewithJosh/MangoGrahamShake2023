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

    }

    private void LoadRooms()
    {

        firebaseFirestore
                .Collection("Rooms")
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(task =>
                {

                    QuerySnapshot documentSnapshots = task.Result;

                    if (documentSnapshots != null && documentSnapshots.Count != 0)
                    {

                        List<FirebaseRoomModel> rooms = new();

                        foreach (DocumentSnapshot doc in documentSnapshots)
                        {

                            FirebaseRoomModel room = doc.ConvertTo<FirebaseRoomModel>();
                            rooms.Add(room);

                        }

                        FindObjectOfType<LoadRoomsManager>().OnLoadRooms(isStudent, rooms);

                    }

                    else

                        FindObjectOfType<DialogManager>().OnDialog(
                            "EMPTY",
                            "To get started, let's create a game and invite your students",
                            "dialog");

                });

    }

    private async void CreateGame()
    {

        await Task.Delay(500);
        SceneManager.LoadScene(3);

    }

    private Sprite ActionUIButton
    {

        set { actionUIButton.sprite = value; }

    }

}
