using UnityEngine;

public class Audio : MonoBehaviour
{

    void Awake()
    {

        AudioSource = GetComponent<AudioSource>();

    }

    public AudioSource AudioSource { get; set; }

}
