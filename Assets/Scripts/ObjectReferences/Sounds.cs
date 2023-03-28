using UnityEngine;

public class Sounds : MonoBehaviour
{

    #region AWAKE_METHOD

    void Awake()
    {

        AudioSource = GetComponent<AudioSource>();

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public static AudioSource AudioSource { get; set; }

    #endregion

}
