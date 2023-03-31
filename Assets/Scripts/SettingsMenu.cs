using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private Button logoutUIButton;

    [SerializeField]
    private Button leaderboardUIButton;

    [SerializeField]
    private Sprite[] resources;

    [SerializeField]
    private Toggle settingsUIButton;

    [SerializeField]
    private Image audioUIButton;

    [SerializeField]
    private Image soundsUIButton;

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

    private static SettingsMenuItem[] settingsMenuItems;
    private static Vector2 settingsUIButtonPosition;
    private bool isExpanded;
    private static int itemCount;

    #endregion

    #region START_METHOD

    void Start()
    {

        isExpanded = false;
        itemCount = transform.childCount - 1;
        settingsMenuItems = new SettingsMenuItem[itemCount];

        for (int i = 0; i < itemCount; i++)

            settingsMenuItems[i] = transform
                .GetChild(i + 1)
                .GetComponent<SettingsMenuItem>();

        settingsUIButton
            .transform
            .SetAsLastSibling();

        settingsUIButtonPosition = settingsUIButton
            .transform
            .position;

        Reset();

    }

    #endregion

    #region UPDATE_METHOD

    void Update()
    {

        logoutUIButton.interactable = STATUS.IS_CONNECTED;
        
        if (SceneManager.GetActiveScene().buildIndex == 3)

            leaderboardUIButton.interactable = STATUS.IS_CONNECTED;

        audioUIButton.sprite = SimpleInput.GetButton("OnAudio")
            ? !STATUS.IS_AUDIO_MUTED
            ? resources[0]
            : resources[1]
            : !STATUS.IS_AUDIO_MUTED
            ? resources[2]
            : resources[3];

        soundsUIButton.sprite = SimpleInput.GetButton("OnSounds")
            ? !STATUS.IS_SOUNDS_MUTED
            ? resources[4]
            : resources[5]
            : !STATUS.IS_SOUNDS_MUTED
            ? resources[6]
            : resources[7];

        if (SimpleInput.GetButtonDown("OnSettings"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            Settings();

        }

        if (SimpleInput.GetButtonDown("OnAudio"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            AudioManager.OnAudioOn();

        }

        if (SimpleInput.GetButtonDown("OnSounds"))
        {

            FindObjectOfType<SoundsManager>().OnClicked();
            SoundsManager.OnSoundsOn();

        }

        if (SimpleInput.GetButtonDown("OnLogout"))
        {

            if (!STATUS.IS_CONNECTED)
            {

                FindObjectOfType<SoundsManager>().OnError();
                DialogManager.OnDialog(
                    "NOTICE",
                    "Please check your internet connection first",
                    ENV.DIALOG);

            }
            else
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                DialogManager.OnDialog(
                    "WARNING",
                    "Are you sure you want to logout?",
                    ENV.OPTION_PANE);
                STATUS.STATE = STATUS.STATES.LOG_OUT;

            }

        }

        if (SimpleInput.GetButtonDown("OnLeaderboard"))
        {

            if (!STATUS.IS_CONNECTED)
            {

                FindObjectOfType<SoundsManager>().OnError();
                DialogManager.OnDialog(
                    "NOTICE",
                    "Please check your internet connection first",
                    ENV.DIALOG);

            }
            else
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                GameManager.OnTrigger(ENV.LEADERBOARD);

            }

        }

        if (SimpleInput.GetButtonDown("OnYes")
            && STATUS.STATE == STATUS.STATES.LOG_OUT)

            Signout();

    }

    #endregion

    #region RESET_METHOD

    private void Reset()
    {

        for (int i = 0; i < itemCount; i++)

            settingsMenuItems[i]
                .Transform
                .position = settingsUIButtonPosition;

    }

    #endregion

    #region SETTINGS_METHOD

    private void Settings()
    {

        settingsUIButton.isOn = isExpanded;
        isExpanded = !isExpanded;

        if (isExpanded)

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

        else

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

    #endregion

    #region SIGNOUT_METHOD

    private async void Signout()
    {

        STATUS.STATE = STATUS.STATES.IDLE;
        FirebaseAuthManager.OnSignout();
        GoogleAuthManager.OnSignout();
        PlayerPrefs.DeleteAll();
        await Task.Delay(3000);
        SceneManager.LoadScene(0);

    }

    #endregion

}
