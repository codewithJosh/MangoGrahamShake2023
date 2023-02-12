using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField]
    private Vector2 spacing;

    Button settingsUIButton;
    SettingsMenuItem[] settingsMenuItems;
    bool isExpanded = false;

    Vector2 settingsUIButtonPosition;
    int itemCount;

    void Start()
    {

        itemCount = transform.childCount - 1;
        settingsMenuItems = new SettingsMenuItem[itemCount];
        
        for (int i = 0; i < itemCount; i++)
        {

            settingsMenuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();

        }

        settingsUIButton = transform.GetChild(0).GetComponent<Button>();
        settingsUIButton.transform.SetAsLastSibling();

        settingsUIButtonPosition = settingsUIButton.transform.position;

        ResetPositions();

    }

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnSettings"))
            
            ToggleMenu();

    }

    private void ResetPositions()
    {

        for (int i = 0; i < itemCount; i++)
        {

            settingsMenuItems[i].trans.position = settingsUIButtonPosition;

        }

    }

    private void ToggleMenu()
    {

        isExpanded = !isExpanded;

        if (isExpanded)
        {

            for (int i = 0; i < itemCount; i++)
            {

                settingsMenuItems[i].trans.position = settingsUIButtonPosition + spacing * (i + 1);

            }

        }
        else
        {

            for (int i = 0; i < itemCount; i++)
            {

                settingsMenuItems[i].trans.position = settingsUIButtonPosition;

            }

        }

    }

}
