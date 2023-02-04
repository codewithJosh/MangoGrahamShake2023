using System.Collections.Generic;
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

            Title = isStudent
                ? "Student ID"
                : "Verification Code";

            Hint = isStudent
                ? "03-2023-01234"
                : "********";

            valueUIText[2].inputType = isStudent
                ? TMP_InputField.InputType.Standard
                : TMP_InputField.InputType.Password;

            valueUIText[2].characterLimit = isStudent
                ? 13
                : 16;

            if (SimpleInput.GetButtonUp("OnToggle")
                && lastToggle != toggle)
            {

                lastToggle = toggle;
                OnClear();
                FindObjectOfType<GameManager>().GetAnimator.SetInteger("toggleState", isStudent
                    ? 0
                    : 1);

            }

        }

    }

    private void OnClear()
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
