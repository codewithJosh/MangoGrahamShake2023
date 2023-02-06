using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomAdapter : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject[] UIButtons;

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    private string currentRoomId;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void RemoveItem()
    {

        FindObjectOfType<DialogManager>().OnDialog(
            "WARNING",
            "Are you sure you want to remove the room?",
            "optionPane1");
        PlayerPrefs.SetString("current_room_id", currentRoomId);

    }

    public bool RemoveUIButton
    {

        set { UIButtons[0].SetActive(value); }

    }

    public bool LeaveUIButton
    {

        set { UIButtons[1].SetActive(value); }

    }

    public string RoomId
    {

        set { currentRoomId = value; }

    }

    public string RoomName
    {

        set { UITexts[0].text = value; }

    }

    public string RoomSlots
    {

        set { UITexts[1].text = value; }

    }

    public void OnRemoveItem() { RemoveItem(); }

}