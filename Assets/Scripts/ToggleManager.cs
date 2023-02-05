using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] UIText;

    [SerializeField]
    private TMP_InputField[] valueUIText;

    [SerializeField]
    private ToggleGroup togglePanel;

    private string lastToggle;
    private bool isStudent;

    void Start()
    {

        lastToggle = "StudentUIButton";
        isStudent = false;

    }

    void Update()
    {

        bool isLoading = FindObjectOfType<SignupManager>().IsLoading;

        if (!isLoading)
        {

            string toggle = FindObjectOfType<GameManager>().GetToggle(togglePanel);
            isStudent = toggle.Equals("StudentUIButton");

            if (SimpleInput.GetButtonUp("OnToggle")
                && lastToggle != toggle)
            {

                lastToggle = toggle;
                Clear();
                FindObjectOfType<GameManager>().GetAnimator.SetInteger("toggleState", isStudent
                    ? 0
                    : 1);
                Toggle(isStudent);

            }

        }

    }

    private async void Toggle(bool _isStudent)
    {

        await Task.Delay(500);

        Title = _isStudent
                ? "Student ID"
                : "Verification Code";

        Hint = _isStudent
            ? "03-2023-01234"
            : "********";

        valueUIText[2].inputType = _isStudent
            ? TMP_InputField.InputType.Standard
            : TMP_InputField.InputType.Password;

        valueUIText[2].characterLimit = _isStudent
            ? 13
            : 16;

    }

    private void Clear()
    {

        valueUIText[0].text = "";
        valueUIText[1].text = "";
        valueUIText[2].text = "";

    }

    private string Title
    {

        set { UIText[0].text = value; }

    }

    private string Hint
    {

        set { UIText[1].text = value; }

    }


    public bool IsStudent
    {

        get { return isStudent; }

    }

}
