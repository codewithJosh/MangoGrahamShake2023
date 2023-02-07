using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomAdapter : MonoBehaviour
{

    // At the beginning, let's privately declare some SERIALIZED field for later use.
    [SerializeField]
    private GameObject[] UIButtons;

    [SerializeField]
    private Image roomHUD;

    [SerializeField]
    private Sprite[] resources;

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    // Also, let's privately declare some PRIMITIVE field for later use.
    private bool isRoomFull;
    private string roomId;
    private string roomPassword;

    /*
     * Upon calling this method the system must inform the teacher is about
     * to remove a selected room.
     */
    private void RemoveItem()
    {

        /*
         * First, let's display an option pane that informs the teacher is about
         * to remove a selected room.
         */
        FindObjectOfType<DialogManager>().OnDialog(
            "WARNING",
            string.Format("Are you sure you want to remove {0}?", RoomName),
            "optionPane1");

        // Finally, let's store the selected room id in a preference.
        PlayerPrefs.SetString("selected_room_id", roomId);

    }

    /*
     * Upon calling this method the system must set the values of current selected room for later use.
     */
    private void Tap()
    {

        // At the Lobby, let's set the value of RoomNameUIText into the current selected RoomName.
        FindObjectOfType<LobbyManager>().RoomName = RoomName;

        /*
         * Also, let's store the selected string value of room id, room password, and
         * boolean value if the room is already full in a preference.
         * If it's value is 1, then the room is already full. Else, 0.
         */
        PlayerPrefs.SetInt("selected_is_room_full", isRoomFull ? 1 : 0);
        PlayerPrefs.SetString("selected_room_id", roomId);
        PlayerPrefs.SetString("selected_room_password", roomPassword);

    }

    /*
     * Upon calling this method this adapter must change the roomHUD sprite depends upon the PARAMETERIZED BOOLEAN field.
     * If it's value is TRUE, then roomHUD sprite is set to RoomHUDNormalUIButton. Else, RoomHUDDisabledUIButton.
     */
    private void SetRoomHUDInteractable(bool _isInteractable)
    {

        roomHUD.sprite = resources[_isInteractable
            ? 0 // Background-6 (Normal)
            : 1 // Background-8 (Disabled)
            ];

    }

    /*
     * Let's publicly declare IsRemoveUIButtonVisible property that has an boolean value.
     * Also, let's add a publicly set method init.
     * Upon setting this property, if it's TRUE, then the RemoveUIButton is visible. Else, FALSE.
     */
    public bool IsRemoveUIButtonVisible
    {

        set => UIButtons[0].SetActive(value);

    }

    /*
     * Let's publicly declare IsLeaveUIButtonVisible property that has an boolean value.
     * Also, let's add a publicly set method init.
     * Upon setting this property, if it's TRUE, then the LeaveUIButton is visible. Else, FALSE.
     */
    public bool IsLeaveUIButtonVisible
    {

        set => UIButtons[1].SetActive(value);

    }

    /*
     * Let's publicly declare a IsRoomFull property that has a boolean value.
     * Also, let's add a publicly set method init.
     * Upon setting this property, if it's TRUE, then the value of private isRoomFull field is true. Else, FALSE.
     */
    public bool IsRoomFull
    {

        set => isRoomFull = value;

    }

    /*
     * Let's publicly declare a RoomId property that has a string value.
     * Also, let's add a publicly set method init.
     * Upon setting this property, the value of private roomId field is change.
     */
    public string RoomId
    {

        set => roomId = value;

    }

    /*
     * Let's publicly declare a RoomName property that has a string value.
     * Also, let's add a privately get and publicly set method init.
     */
    public string RoomName
    {

        private get => UITexts[0].text;
        set => UITexts[0].text = value;

    }

    /*
     * Let's publicly declare a RoomSubtitleUIText property that has a string value.
     * Also, let's add a publicly set method init.
     */
    public string RoomSubtitleUIText
    {

        set => UITexts[1].text = value;

    }

    /*
     * Let's publicly declare a RoomPassword property that has a string value.
     * Also, let's add a publicly set method init.
     */
    public string RoomPassword
    {

        set => roomPassword = value;

    }

    //Let's publicly declare a RemoveItem method.
    public void OnRemoveItem() { RemoveItem(); }

    //Let's publicly declare a Tap method.
    public void OnTap() { Tap(); }

    //Let's publicly declare a SetRoomHUDInteractable method.
    public void OnSetRoomHUDInteractable(bool _isInteractable) => SetRoomHUDInteractable(_isInteractable);

}
