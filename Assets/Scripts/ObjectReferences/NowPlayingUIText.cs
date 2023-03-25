using TMPro;
using UnityEngine;

public class NowPlayingUIText : MonoBehaviour
{

    #region DECLARATION

    private static TextMeshProUGUI nowPlayingUIText;

    #endregion

    #region AWAKE_METHOD

    void Awake()
    {

        nowPlayingUIText = GetComponent<TextMeshProUGUI>();

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public static string Text 
    { 
        
        set => nowPlayingUIText.text = value;
    
    }

    #endregion

}
