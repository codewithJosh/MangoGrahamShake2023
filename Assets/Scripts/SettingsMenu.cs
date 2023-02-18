using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField]
    private Button[] UIButtons;

    [SerializeField]
    private GameObject LeaveUIButton;

    [SerializeField]
    private Sprite[] resources;

    [SerializeField]
    private Toggle[] UIToggles;

    [SerializeField]
    private Vector2 spacing;

    [SerializeField]
    private float expandDuration;

    [SerializeField]
    private float collapseDuration;

    [SerializeField]
    private float expandFadeDuration;

    [SerializeField]
    private float collapseFadeDuration;

    [SerializeField]
    private Ease expandEase;

    [SerializeField]
    private Ease collapseEase;

    private SettingsMenuItem[] settingsMenuItems;
    private Vector2 settingsUIButtonPosition;
    private bool IsExpanded { get; set; }
    private bool isAudioMuted;
    private bool isSoundsMuted;
    private bool isConnected;
    private bool hasRoomId;
    private bool isStudent;
    private bool isLoggingout;
    private bool isLeaving;
    private bool isGoingToLobby;
    private int itemCount;

    void Start()
    {

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);

        IsExpanded = false;
        Init();
        isStudent = playerIsStudent == 1;
        itemCount = transform.childCount - 1;
        settingsMenuItems = new SettingsMenuItem[itemCount];

        for (int i = 0; i < itemCount; i++)
        {

            settingsMenuItems[i] = transform
                .GetChild(i + 1)
                .GetComponent<SettingsMenuItem>();

        }

        SettingsUIButton
            .transform
            .SetAsLastSibling();

        settingsUIButtonPosition = SettingsUIButton
            .transform
            .position;

        Reset();

    }

    void Update()
    {

        string roomId = PlayerPrefs.GetString("room_id", "");

        isAudioMuted = FindObjectOfType<AudioManager>().IsAudioMuted;
        isSoundsMuted = FindObjectOfType<SoundsManager>().IsSoundsMuted;
        isConnected = Application.internetReachability != NetworkReachability.NotReachable;
        hasRoomId = !roomId.Equals("");

        AudioUIButton.image.sprite = SimpleInput.GetButton("OnAudio")
            ? !isAudioMuted
            ? resources[0]
            : resources[1]
            : !isAudioMuted
            ? resources[2]
            : resources[3];

        SoundsUIButton.image.sprite = SimpleInput.GetButton("OnSounds")
            ? !isSoundsMuted
            ? resources[4]
            : resources[5]
            : !isSoundsMuted
            ? resources[6]
            : resources[7];

        IsLogoutUIButtonInteractable = isConnected;
        IsLeaveUIButtonVisible = isStudent;

        if (isStudent)
            IsLeaveUIButtonInteractable = hasRoomId && isConnected;

        if (SimpleInput.GetButtonDown("OnSettings"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            Settings();

        }

        if (SimpleInput.GetButtonDown("OnAudio"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            FindObjectOfType<AudioManager>().OnIsAudioOn();

        }

        if (SimpleInput.GetButtonDown("OnSounds"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            FindObjectOfType<SoundsManager>().OnIsSoundsOn();

        }

        if (SimpleInput.GetButtonDown("OnLogout"))
        {

            if (!isConnected)
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
                FindObjectOfType<DialogManager>().OnDialog(
                    "WARNING",
                    "Are you sure you want to logout?",
                    "optionPane1");
                isLoggingout = !isLoggingout;

            }

        }

        if (SimpleInput.GetButtonDown("OnLeave"))
        {

            if (!isConnected)
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                    "NOTICE",
                    "Please check your internet connection first",
                    "dialog");

            }
            else if (!hasRoomId)
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                    "REQUIRED",
                    "Please choose a room to join first",
                    "dialog");

            }
            else
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                FindObjectOfType<DialogManager>().OnDialog(
                    "WARNING",
                    "Are you sure you want to leave game?",
                    "optionPane1");
                isLeaving = !isLeaving;
            }

        }

        if (SimpleInput.GetButtonDown("OnYes") && isLoggingout)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            Signout();
            isLoggingout = !isLoggingout;

        }

        if (SimpleInput.GetButtonDown("OnYes") && isLeaving)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            LeaveGame();
            isLeaving = !isLeaving;

        }

        if (SimpleInput.GetButtonDown("OnLobby"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            FindObjectOfType<DialogManager>().OnDialog(
                "WARNING",
                "Are you sure you want to go to the lobby?",
                "optionPane1");
            isGoingToLobby = !isGoingToLobby;

        }

        if (SimpleInput.GetButtonDown("OnYes") && isGoingToLobby)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("ok");
            OnLobby();
            isGoingToLobby = !isGoingToLobby;

        }

        if (SimpleInput.GetButtonDown("OnOK"))
        {

            Init();

        }

    }

    private void Reset()
    {

        for (int i = 0; i < itemCount; i++)
        {

            settingsMenuItems[i]
                .Transform
                .position = settingsUIButtonPosition;

        }

    }

    private void Settings()
    {

        IsSettingsOpened = IsExpanded;
        IsExpanded = !IsExpanded;

        if (IsExpanded)
        {

            for (int i = 0; i < itemCount; i++)
            {

                settingsMenuItems[i]
                    .Transform
                    .DOMove(settingsUIButtonPosition + spacing * (i + 1), expandDuration)
                    .SetEase(expandEase);

                settingsMenuItems[i]
                    .UIButton
                    .DOFade(1f, expandFadeDuration)
                    .From(0f);

            }

        }
        else
        {

            for (int i = 0; i < itemCount; i++)
            {

                settingsMenuItems[i]
                    .Transform
                    .DOMove(settingsUIButtonPosition, collapseDuration)
                    .SetEase(collapseEase);

                settingsMenuItems[i]
                    .UIButton
                    .DOFade(0f, collapseFadeDuration);

            }

        }

    }

    private async void Signout()
    {

        FindObjectOfType<FirebaseAuthManager>().OnSignout();
        FindObjectOfType<GoogleAuthManager>().OnSignout();
        PlayerPrefs.DeleteAll();
        await Task.Delay(1000);
        SceneManager.LoadScene(0);

    }

    private async void LeaveGame()
    {

        FindObjectOfType<Player>().RoomId = "";
        FindObjectOfType<Player>().OnAutoSave(isConnected);
        FindObjectOfType<DialogManager>().OnDialog(
                 "SUCCESS",
                 "You've successfully left the game!",
                 "dialog");

        PlayerPrefs.SetString("room_id", "");

        if (SceneManager.GetActiveScene().buildIndex == 2)

            FindObjectOfType<LobbyManager>().OnLoadRooms();
            
        else
        {

            await Task.Delay(3000);
            SceneManager.LoadScene(2);

        }
            
    }

    private async void OnLobby()
    {

        await Task.Delay(3000);
        SceneManager.LoadScene(2);

    }

    private void Init()
    {

        isLoggingout = false;
        isLeaving = false;
        isGoingToLobby = false;

    }

    private bool IsSettingsOpened
    {

        set => SettingsUIButton.isOn = value;

    }

    private bool IsLeaveUIButtonVisible
    {

        set => LeaveUIButton.SetActive(value);

    }

    private bool IsLeaveUIButtonInteractable
    {

        set => UIButtons[0].interactable = value;

    }

    private bool IsLogoutUIButtonInteractable
    {

        set => UIButtons[1].interactable = value;

    }

    private Toggle SettingsUIButton => UIToggles[0];

    private Toggle AudioUIButton => UIToggles[1];

    private Toggle SoundsUIButton => UIToggles[2];

}
