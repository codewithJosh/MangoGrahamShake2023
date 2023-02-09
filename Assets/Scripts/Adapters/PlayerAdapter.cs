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

    public Sprite PlayerImage
    {

        set => playerImageHUD.sprite = value;

    }

}
