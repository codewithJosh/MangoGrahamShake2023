using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateGameManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI maxPlayersUIText;

    [SerializeField]
    private TMP_InputField[] valueUITexts;

    void Start()
    {
        
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

    }

    private string RoomName
    {

        get { return valueUITexts[0].text.Trim().ToUpper(); }

    }

    private int MaxPlayers
    {

        get { return int.Parse(maxPlayersUIText.text); }

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
