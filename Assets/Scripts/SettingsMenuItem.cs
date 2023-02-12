using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{

    [SerializeField]
    private Image image;

    void Awake()
    {

        Transform = transform;

    }

    public Transform Transform { get; private set; }

    public Image UIButton => image;

}
