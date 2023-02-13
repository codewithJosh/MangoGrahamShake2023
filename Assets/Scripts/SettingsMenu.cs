using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField]
    private Button logoutUIButton;

    [SerializeField]
    private Sprite[] resources;

    [SerializeField]
    private Toggle[] UIButtons;

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
    private int itemCount;

    void Start()
    {

        IsExpanded = false;
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

        isAudioMuted = FindObjectOfType<AudioManager>().IsAudioMuted;
        isSoundsMuted = FindObjectOfType<SoundsManager>().IsSoundsMuted;
        isConnected = Application.internetReachability != NetworkReachability.NotReachable;

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

            if (isConnected)
            {

                FindObjectOfType<SoundsManager>().OnClicked();
                FindObjectOfType<DialogManager>().OnDialog(
                    "WARNING",
                    "Are you sure you want to logout?",
                    "optionPane3");

            }
            else
            {

                FindObjectOfType<SoundsManager>().OnError();
                FindObjectOfType<DialogManager>().OnDialog(
                    "NOTICE",
                    "Please check your internet connection first",
                    "dialog");

            }

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

    private bool IsSettingsOpened
    {

        set => SettingsUIButton.isOn = value;

    }

    private bool IsLogoutUIButtonInteractable
    {

        set => logoutUIButton.interactable = value;

    }

    private Toggle SettingsUIButton => UIButtons[0];

    private Toggle AudioUIButton => UIButtons[1];

    private Toggle SoundsUIButton => UIButtons[2];

}
