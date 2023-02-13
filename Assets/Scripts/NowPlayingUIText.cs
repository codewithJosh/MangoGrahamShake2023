using TMPro;
using UnityEngine;

public class NowPlayingUIText : MonoBehaviour
{

    private TextMeshProUGUI nowPlayingUIText;

    void Awake()
    {

        nowPlayingUIText = GetComponent<TextMeshProUGUI>();

    }

    public string Text
    {

        set => nowPlayingUIText.text = value;

    }

}
