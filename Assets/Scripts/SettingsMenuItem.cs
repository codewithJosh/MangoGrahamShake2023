using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuItem : MonoBehaviour
{

    [HideInInspector] public Image image;
    [HideInInspector] public Transform trans;

    void Awake()
    {

        image = GetComponent<Image>();
        trans = transform;

    }

}
