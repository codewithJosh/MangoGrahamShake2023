using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAdapter : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private TextMeshProUGUI playerRankUIText;

    [SerializeField]
    private TextMeshProUGUI playerNameUIText;

    [SerializeField]
    private TextMeshProUGUI playerEmailUIText;

    [SerializeField]
    private TextMeshProUGUI playerReputationUIText;

    [SerializeField]
    private Image playerImageHUD;

    #endregion

    #region AUTOMATED_PROPERTIES

    public string PlayerRank
    {

        set => playerRankUIText.text = value;

    }

    public string PlayerName
    {

        set => playerNameUIText.text = value;

    }

    public string PlayerEmail
    {

        set => playerEmailUIText.text = value;

    }

    public double PlayerReputation
    {

        set => playerReputationUIText.text = $"{value * 100.0:0.00}%";

    }

    public Image PlayerImage
    {

        set => playerImageHUD = value;

    }

    #endregion

}
