using TMPro;
using UnityEngine;

public class NowPlayingUIText : MonoBehaviour
{

    private static TextMeshProUGUI nowPlayingUIText;

    void Awake()
    {

        nowPlayingUIText = GetComponent<TextMeshProUGUI>();

    }

    public static string Text { set => nowPlayingUIText.text = value; }

}
