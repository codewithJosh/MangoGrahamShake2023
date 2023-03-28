using TMPro;
using UnityEngine;

public class DescriptionUIText : MonoBehaviour
{

    #region DECLARATION

    private static TextMeshProUGUI descriptionUIText;

    #endregion

    #region AWAKE_METHOD

    void Awake()
    {

        descriptionUIText = GetComponent<TextMeshProUGUI>();

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public string Text
    {

        set => descriptionUIText.text = value;

    }

    #endregion

}
