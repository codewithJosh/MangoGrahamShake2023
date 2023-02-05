using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateGameManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI maxPlayersUIText;

    [SerializeField]
    private TMP_InputField[] valueUITexts;

    private int maxPlayers;

    void Start()
    {

        maxPlayers = PlayerPrefs.GetInt("max_players", 25);
        MaxPlayers = maxPlayers;

    }

    
    void Update()
    {

        bool hasSomeEmpty = RoomName.Equals("")
            && Password.Equals("")
            && ConfirmPassword.Equals("");

        if (SimpleInput.GetButtonDown("OnCancel"))

            if (!hasSomeEmpty)

                FindObjectOfType<DialogManager>().OnDialog(
                    "WARNING",
                    "Are you sure you're no longer want to create a game?",
                    "optionPane1"
                    );

            else
                
                SceneManager.LoadScene(2);

        if (SimpleInput.GetButtonDown("OnYes"))
            
            SceneManager.LoadScene(2);

        if (SimpleInput.GetButtonDown("OnIncrementMaxPlayers") 
            && MaxPlayers < 50)
            
            MaxPlayers += 1;

        if (SimpleInput.GetButtonDown("OnDecrementMaxPlayers")
            && MaxPlayers > 25 )

            MaxPlayers -= 1;

    }

    private string RoomName
    {

        get { return valueUITexts[0].text.Trim().ToUpper(); }

    }

    private int MaxPlayers
    {

        get { return int.Parse(maxPlayersUIText.text); }
        set { maxPlayersUIText.text = value.ToString(); }

    }

    private string Password
    {

        get { return valueUITexts[1].text; }

    }

    private string ConfirmPassword
    {

        get { return valueUITexts[2].text; }

    }

}
