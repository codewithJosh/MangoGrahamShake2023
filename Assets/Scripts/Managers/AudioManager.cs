using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] playlist;

    private AudioSource audioSource;
    private NowPlayingUIText nowPlayingUIText;

    private string[] playlistTexts;

    private float isAudioOn;

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

            isAudioOn = PlayerPrefs.GetFloat("is_audio_on", 0.5f);

            audioSource = FindObjectOfType<Audio>().AudioSource;
            audioSource.loop = false;
            audioSource.volume = isAudioOn;

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

    private void IsAudioOn()
    {

        isAudioOn = isAudioOn != 0
            ? 0
            : 0.5f;

        audioSource.volume = isAudioOn;
        PlayerPrefs.SetFloat("is_audio_on", isAudioOn);

    }

    public bool IsAudioMuted => isAudioOn == 0;

    public void OnIsAudioOn() => IsAudioOn();

}
