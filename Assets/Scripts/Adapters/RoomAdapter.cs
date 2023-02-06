using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomAdapter : MonoBehaviour
{

    [SerializeField]
    private Image UIButton;

    [SerializeField]
    private GameObject[] UIButtons;

    [SerializeField]
    private Sprite[] resources;

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    private string roomId;

    private void RemoveItem()
    {

        FindObjectOfType<DialogManager>().OnDialog(
            "WARNING",
            string.Format("Are you sure you want to remove {0}?", RoomName),
            "optionPane1");
        PlayerPrefs.SetString("current_room_id", roomId);

    }

    private void Tap()
    {

        FindObjectOfType<LobbyManager>().RoomName = RoomName;
        PlayerPrefs.SetString("current_room_id", roomId);
        PlayerPrefs.SetInt("current_is_full", !IsFull ? 0 : 1 );
        PlayerPrefs.SetString("current_room_password", RoomPassword);

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);
        bool isStudent = playerIsStudent == 1;

        if (Input.touchCount > 1 && isStudent)

            FindObjectOfType<LobbyManager>().OnJoinGame();

    }

    private void Interactable(bool _isInteractable)
    {

        UIButton.sprite = resources[_isInteractable
            ? 0 
            : 1 ];

    }

    public bool RemoveUIButton
    {

        set => UIButtons[0].SetActive(value);

    }

    public bool LeaveUIButton
    {

        set => UIButtons[1].SetActive(value);

    }

    public bool IsFull
    {

        private get => UIButtons[2];
        set => UIButtons[2].SetActive(value);

    }

    public string RoomId
    {

        set => roomId = value;

    }

    public string RoomName
    {

        private get => UITexts[0].text;
        set => UITexts[0].text = value;

    }

    public string RoomSlots
    {

        set => UITexts[1].text = value;

    }

    public string RoomPassword
    {

        private get => UITexts[2].text;
        set => UITexts[2].text = value;

    }

    public void OnRemoveItem() { RemoveItem(); }

    public void OnTap() { Tap(); }

    public void OnInteractable(bool _isInteractable) => Interactable(_isInteractable);

}
