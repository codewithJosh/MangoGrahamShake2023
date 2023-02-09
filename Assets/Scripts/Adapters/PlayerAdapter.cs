using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAdapter : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI playerNameUIText;

    [SerializeField]
    private Image playerImageHUD;

    public string PlayerName
    {

        set => playerNameUIText.text = value;

    }

    public Image PlayerImage
    {

        get => playerImageHUD;
        set => playerImageHUD = value;

    }

}
