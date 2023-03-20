using TMPro;
using UnityEngine;

public class TitleUIText : MonoBehaviour
{

    private TextMeshProUGUI titleUIText;

    void Awake()
    {

        titleUIText = GetComponent<TextMeshProUGUI>();

    }

    public string Text { set => titleUIText.text = value; }

}
