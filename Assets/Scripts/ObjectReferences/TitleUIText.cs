using TMPro;
using UnityEngine;

public class TitleUIText : MonoBehaviour
{

    #region DECLARATION

    private TextMeshProUGUI titleUIText;

    #endregion

    #region AWAKE_METHOD

    void Awake()
    {

        titleUIText = GetComponent<TextMeshProUGUI>();

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public string Text
    {

        set => titleUIText.text = value;

    }

    #endregion

}
