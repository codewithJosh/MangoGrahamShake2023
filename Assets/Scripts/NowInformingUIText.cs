using TMPro;
using UnityEngine;

public class NowInformingUIText : MonoBehaviour
{

    private TextMeshProUGUI nowInformingUIText;

    void Awake()
    {

        nowInformingUIText = GetComponent<TextMeshProUGUI>();

    }

    public string Text
    {

        set => nowInformingUIText.text = value;

    }

}
