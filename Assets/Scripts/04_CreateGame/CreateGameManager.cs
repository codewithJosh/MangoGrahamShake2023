using Firebase.Extensions;
using Firebase.Firestore;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGameManager : MonoBehaviour
{

    // At the beginning, let's privately declare some SERIALIZED field for later use.
    [SerializeField]
    private Button createUIButton;

    [SerializeField]
    private TextMeshProUGUI roomSlotsUIText;

    [SerializeField]
    private TMP_InputField[] valueUIInputFields;

    // Also, let's privately declare some NON-SERIALIZED -or- OBJECT -or- REFERENCE field for later use.
    private DocumentReference documentRef;
    private FirebaseFirestore firebaseFirestore;
    private Query query;

    void Start()
    {

        /*
         * First, let's locally declare a INT field.
         * Also, let's initialize it's value to preference INT last_room_slots field.
         */
        int lastRoomSlots = PlayerPrefs.GetInt("last_room_slots", 25);

        // Then, let's initialize the value of isLoading to FALSE and RoomSlots to lastRoomSlots value.
        IsLoading = false;
        RoomSlots = lastRoomSlots;
        firebaseFirestore = FindObjectOfType<FirebaseFirestoreManager>().FirebaseFirestore;

    }

    void Update()
    {

        /*
         * A field that continuously holds a boolean value.
         * If it's value is TRUE, then the system is connected to the internet. Else, FALSE.
         */
        IsConnected = Application.internetReachability != NetworkReachability.NotReachable;

        /*
         * An instance of Animator that continuously set the boolean value of isLoading parameter
         * by isLoading argument.
         * If it's value is TRUE, then 
         */
        FindObjectOfType<GameManager>()
            .Animator
            .SetBool("isLoading", IsLoading);

        if (!IsLoading)
        {

            bool hasSomeEmpty = RoomName.Equals("")
            && Password.Equals("")
            && ConfirmPassword.Equals("");
            bool isEmpty = RoomName.Equals("")
                || Password.Equals("")
                || ConfirmPassword.Equals("");

            createUIButton.interactable = IsConnected && !isEmpty;

            if (SimpleInput.GetButtonDown("OnCancel"))
            {

                FindObjectOfType<SoundsManager>().OnClicked();

                if (!hasSomeEmpty)

                    FindObjectOfType<DialogManager>().OnDialog(
                        "WARNING",
                        "Are you sure you're no longer want to create a game?",
                        "optionPane1");

                else

                    Lobby();

            }
                

            if (SimpleInput.GetButtonDown("OnYes"))
            {

                FindObjectOfType<SoundsManager>().OnGrahamCrack();
                Lobby();

            }
                
            if (SimpleInput.GetButtonDown("OnIncrementMaxPlayers")
                && RoomSlots < 50)
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                RoomSlots += 1;

            }
                

            if (SimpleInput.GetButtonDown("OnDecrementMaxPlayers")
                && RoomSlots > 25)
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                RoomSlots -= 1;

            }

            if (SimpleInput.GetButtonDown("OnCreate"))
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
                else if (Password.Length < 4)
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Password must be at least (4) four characters",
                        "dialog");

                }
                else if (!Password.Equals(ConfirmPassword))
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Password doesn't match",
                        "dialog");

                }
                else
                {

                    FindObjectOfType<SoundsManager>().OnClicked();
                    CheckRoomName();

                } 

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
                {

                    FindObjectOfType<SoundsManager>().OnError();
                    FindObjectOfType<DialogManager>().OnDialog(
                        "SORRY",
                        "The Room Name is Unavailable",
                        "dialog");

                }
                    
            });

    }

    private void CreateGame()
    {

        FindObjectOfType<SoundsManager>().OnClicked();
        IsLoading = true;

        string roomId = firebaseFirestore
            .Collection("Rooms")
            .Document()
            .Id;
        string playerId = PlayerPrefs.GetString("player_id", "");

        if (!playerId.Equals(""))
        {

            firebaseFirestore
                .Collection("Players")
                .Document(playerId)
                .GetSnapshotAsync()
                .ContinueWithOnMainThread((System.Action<Task<DocumentSnapshot>>)(task =>
                {

                    DocumentSnapshot doc = task.Result;

                    if (doc != null && doc.Exists)
                    {

                        PlayerStruct player = doc.ConvertTo<PlayerStruct>();

                        string playerLastName = player.player_last_name;
                        string playerFirstName = player.player_first_name;
                        string playerName = string.Format("{0}, {1}", playerLastName, playerFirstName);

                        RoomStruct firebaseRoomModel = new()
                        {

                            room_slots = RoomSlots,
                            room_id = roomId,
                            room_name = RoomName,
                            room_password = Password,
                            room_player_id = playerId,
                            room_player_name = playerName

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
                                            "dialog");

                                        PlayerPrefs.SetInt("last_room_slots", RoomSlots);

                                        await Task.Delay(3000);
                                        SceneManager.LoadScene(2);

                                    });

                            });

                    }

                }));

        }

    }

    private async void Lobby()
    {

        await Task.Delay(500);
        SceneManager.LoadScene(2);

    }


    /*
     * Let's privately declare a IsConnected property that has an boolean value.
     * Also, let's add both privately get and set method init.
     */
    private bool IsConnected { get; set; }

    /*
     * Let's privately declare a IsLoading property that has an boolean value.
     * Also, let's add both privately get and set method init.
     */
    private bool IsLoading { get; set; }

    private string RoomName => valueUIInputFields[0].text.Trim().ToUpper();

    private int RoomSlots
    {

        get => int.Parse(roomSlotsUIText.text);
        set => roomSlotsUIText.text = value.ToString();

    }

    private string Password => valueUIInputFields[1].text;

    private string ConfirmPassword => valueUIInputFields[2].text;

}
