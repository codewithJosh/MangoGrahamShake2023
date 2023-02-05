using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomAdapter : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject[] UIButtons;

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public bool RemoveUIButton
    {

        set { UIButtons[0].SetActive(value); }

    }

    public bool LeaveUIButton
    {

        set { UIButtons[1].SetActive(value); }

    }

    public string RoomName
    {

        set { UITexts[0].text = value; }

    }

    public int RoomSlots
    {

        set { UITexts[1].text = value.ToString(); }

    }

}
