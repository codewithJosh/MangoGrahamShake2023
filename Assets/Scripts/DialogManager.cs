using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{

    [SerializeField]
    private List<TextMeshProUGUI> UIText;

    void Update()
    {

        if (SimpleInput.GetButtonDown("OnOK"))

            FindObjectOfType<GameManager>()
                .GetAnimator
                .SetTrigger("ok");

    }

    private string Title
    {

        set { UIText[0].text = value; }

    }

    private string Description
    {

        set { UIText[1].text = value; }

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
