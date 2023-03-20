using TMPro;
using UnityEngine;

public class DescriptionUIText : MonoBehaviour
{

    private TextMeshProUGUI descriptionUIText;

    void Awake()
    {

        descriptionUIText = GetComponent<TextMeshProUGUI>();

    }

    public string Text { set => descriptionUIText.text = value; }

}
