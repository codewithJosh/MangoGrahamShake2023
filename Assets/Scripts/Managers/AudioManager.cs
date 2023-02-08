using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] playlist;

    private AudioSource audioSource;
    private NowPlayingUIText nowPlayingUIText;

    private string[] playlistTexts;

    void Start()
    {

        playlistTexts = new string[]
        {

            "ASHAMALUEV MUSIC\n\"Cooking\"\nHappy Music",
            "ASHAMALUEV MUSIC\n\"Funny\"\nHappy Music",
            "ASHAMALUEV MUSIC\n\"Nature\"\nAcoustic Music",
            "ASHAMALUEV MUSIC\n\"Quirky\"\nHappy Music",
            "ASHAMALUEV MUSIC\n\"Upbeat Acoustic\"\nAcoustic Music"

        };

    }

    void Update()
    {
        
        if (audioSource == null)
        {

            audioSource = FindObjectOfType<AudioSource>();
            audioSource.loop = false;

        }

        if (nowPlayingUIText == null)
        {

            nowPlayingUIText = FindObjectOfType<NowPlayingUIText>();

        }

        if (!audioSource.isPlaying)
        {

            int index = Random.Range(0, playlist.Length);

            audioSource.clip = playlist[index];
            audioSource.Play();

            nowPlayingUIText.Text = playlistTexts[index];
            FindObjectOfType<GameManager>()
                .Animator
                .SetTrigger("nowPlaying");

        }

    }

}
