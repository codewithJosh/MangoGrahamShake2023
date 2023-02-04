using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] UITexts;

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnOK"))

            FindObjectOfType<GameManager>()
                .GetAnimator
                .SetTrigger("ok");

    }

    private string Title
    {

        set { UITexts[0].text = value; }

    }

    private string Description
    {

        set { UITexts[1].text = value; }

    }

    public void OnDialog(string _title, string _description, string _mode)
    {

        Title = _title;
        Description = _description;
        FindObjectOfType<GameManager>()
            .GetAnimator
            .SetTrigger(_mode);

    }

}
