using TMPro;
using UnityEngine;

public class RoomAdapter : MonoBehaviour
{

    [SerializeField]
    private GameObject[] UIButtons;

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    private string currentRoomId;

    private void RemoveItem()
    {

        FindObjectOfType<DialogManager>().OnDialog(
            "WARNING",
            string.Format("Are you sure you want to remove {0}?", RoomName),
            "optionPane1");
        PlayerPrefs.SetString("current_room_id", currentRoomId);

    }

    private void Tap()
    {

        FindObjectOfType<LobbyManager>().RoomName = RoomName;
        PlayerPrefs.SetString("current_room_id", currentRoomId);

    }

    public bool RemoveUIButton
    {

        set => UIButtons[0].SetActive(value);

    }

    public bool LeaveUIButton
    {

        set => UIButtons[1].SetActive(value);

    }

    public string RoomId
    {

        set => currentRoomId = value;

    }

    public string RoomName
    {

        get => UITexts[0].text;
        set => UITexts[0].text = value;

    }

    public string RoomSlots
    {

        set => UITexts[1].text = value;

    }

    public void OnRemoveItem() { RemoveItem(); }

    public void OnTap() { Tap(); }

}
