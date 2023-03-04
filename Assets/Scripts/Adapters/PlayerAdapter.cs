using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAdapter : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI[] playerUITexts;

    [SerializeField]
    private Image playerImageHUD;

    public string PlayerName
    {

        set => playerUITexts[0].text = value;

    }

    public string PlayerStudentID
    {

        set => playerUITexts[1].text = value;

    }

    public string PlayerReputation
    {

        set => playerUITexts[2].text = value;

    }

    public Image PlayerImage
    {

        get => playerImageHUD;
        set => playerImageHUD = value;

    }

}
