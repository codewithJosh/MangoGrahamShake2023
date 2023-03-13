using TMPro;
using UnityEngine;

public class DialogTutorialManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    private string Title
    {

        set => UITexts[0].text = value;

    }

    private string Description
    {

        set => UITexts[1].text = value;

    }

    public void OnDialog(string _title, string _description)
    {

        Title = _title;
        Description = _description;
        FindObjectOfType<GameManager>().OnTrigger("dialogTutorial");

    }

}
