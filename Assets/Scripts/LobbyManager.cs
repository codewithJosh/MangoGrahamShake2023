using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{

    [SerializeField] private Image actionUIButton;
    [SerializeField] private Sprite[] resources;

    private bool isStudent;

    void Start()
    {

        int playerIsStudent = PlayerPrefs.GetInt("player_is_student", -1);
        isStudent = playerIsStudent == 1;

    }

    
    void Update()
    {

        ActionUIButton = resources[isStudent ? 0 : 1];

        if (SimpleInput.GetButton("OnAction"))

            ActionUIButton = resources[isStudent ? 2 : 3];

    }

    private Sprite ActionUIButton
    {

        set { actionUIButton.sprite = value; }

    }

}
