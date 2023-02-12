using UnityEngine;

public class Sounds : MonoBehaviour
{

    void Awake()
    {

        AudioSource = GetComponent<AudioSource>();

    }

    public AudioSource AudioSource { get; set; }

}
