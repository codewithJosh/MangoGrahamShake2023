using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    [SerializeField]
    private TMP_InputField passwordUIInputField;

    void Start()
    {

        IsEnabled = true;

    }

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnOK") && IsEnabled)
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            FindObjectOfType<GameManager>().OnTrigger("ok");

        }

    }

    private string Title
    {

        set => UITexts[0].text = value;

    }

    private string Description
    {

        set => UITexts[1].text = value;

    }

    private string InputDescription
    {

        set => UITexts[2].text = value;

    }

    public string Password
    {

        get => passwordUIInputField.text;
        set => passwordUIInputField.text = value;

    }

    public void OnDialog(string _title, string _description, string _mode)
    {

        Title = _title;
        Description = _description;
        FindObjectOfType<GameManager>().OnTrigger(_mode);

    }

    public void OnInputDialog(string _title, string _description, string _mode)
    {

        Title = _title;
        InputDescription = _description;
        FindObjectOfType<GameManager>().OnTrigger(_mode);

    }

    public bool IsEnabled { private get; set; }

}
