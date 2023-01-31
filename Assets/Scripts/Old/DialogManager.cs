using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> UIText;

    private void Update()
    {
        
        if (SimpleInput.GetButtonDown("OnOK"))
        {

            FindObjectOfType<GameManager>().GetAnimator.SetTrigger("ok");

        }
            
    }

    private string Title
    {
        set { UIText[0].text = value; }
    }

    private string Description
    {
        set { UIText[1].text = value; }
    }

    public void OnDialog(string _title, string _description)
    {

        Title = _title;
        Description = _description;
        FindObjectOfType<GameManager>().GetAnimator.SetTrigger("dialog");

    }

}
