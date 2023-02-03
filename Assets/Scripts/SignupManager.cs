using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignupManager : MonoBehaviour
{

    [SerializeField] private Button signupUIButton;
    [SerializeField] private List<TMP_InputField> valueUIText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        signupUIButton.interactable = !PlayerLastname.Equals("") 
            && !PlayerFirstname.Equals("")
            && !PlayerValue.Equals("");

        if (SimpleInput.GetButtonDown("OnSubmit"))
        {

            if (signupUIButton.IsInteractable())
                IsValid();
            else
                FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please fill out all the fields first"
                        );

        }

    }

    private bool IsValid()
    {

        bool isStudent = FindObjectOfType<ToggleManager>().IsStudent;

        if (isStudent)
        {

            if (PlayerValue.Length != 13)
                return false;

            string validCharacters = "0123456789-";

            for (int i = 0; i < PlayerValue.Length; i++)
            {

                if (i < 2 || i > 2 && i < 7 || i > 7)
                {

                    if (!validCharacters.Contains(PlayerValue[i]))
                    return false;

                }
                else
                    if (!PlayerValue[i].Equals('-'))
                    return false;

            }

            return true;

        }
        else
        {

            CheckVerificationCode();

        }

        return false;
    
    }

    private bool CheckVerificationCode()
    {



        return false;

    }

    private string PlayerLastname
    {

        get { return valueUIText[0].text.Trim().ToUpper(); }

    }

    private string PlayerFirstname
    {

        get { return valueUIText[1].text.Trim().ToUpper(); }

    }

    private string PlayerValue
    {

        get { return valueUIText[2].text; }

    }

}
