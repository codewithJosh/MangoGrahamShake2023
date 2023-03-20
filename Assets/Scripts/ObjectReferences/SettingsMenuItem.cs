using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private Image image;

    #endregion

    #region AWAKE_METHOD

    void Awake()
    {

        Transform = transform;

    }

    #endregion

    #region AUTOMATED_PROPERTIES

    public Transform Transform { get; private set; }

    public Image UIButton => image;

    #endregion

}
