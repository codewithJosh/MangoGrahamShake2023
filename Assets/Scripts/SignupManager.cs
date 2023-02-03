using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignupManager : MonoBehaviour
{

    [SerializeField] private Button signupUIButton;
    [SerializeField] private List<TMP_InputField> valueUIText;

    private bool isLoading;

    void Start()
    {

        isLoading = false;

    }

    void Update()
    {

        FindObjectOfType<GameManager>().GetAnimator.SetBool("isLoading", isLoading);

        if (!isLoading)
        {

            bool isConnected = Application.internetReachability != NetworkReachability.NotReachable;
            bool isEmpty = PlayerLastname.Equals("")
            || PlayerFirstname.Equals("")
            || PlayerValue.Equals("");

            signupUIButton.interactable = isConnected && !isEmpty;

            if (SimpleInput.GetButtonDown("OnSubmit"))
            {

                if (!isConnected)
                    FindObjectOfType<DialogManager>().OnDialog(
                        "NOTICE",
                        "Please check your internet connection first"
                        );

                else if (isEmpty)
                    FindObjectOfType<DialogManager>().OnDialog(
                        "REQUIRED",
                        "Please fill out all the fields first"
                        );

                else
                    IsValid();

            }

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

    public bool IsLoading
    {
        
        get { return isLoading; }

    }

}
