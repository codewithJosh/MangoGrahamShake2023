using TMPro;
using UnityEngine;

public class NowInformingUIText : MonoBehaviour
{

    #region DECLARATION

    private static TextMeshProUGUI nowInformingUIText;

    #endregion

    #region AWAKE_METHOD

    void Awake()
    {

        nowInformingUIText = GetComponent<TextMeshProUGUI>();

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public string Text
    {

        set => nowInformingUIText.text = value;

    }

    #endregion

}
