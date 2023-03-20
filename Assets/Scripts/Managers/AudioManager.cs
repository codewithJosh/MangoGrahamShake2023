using UnityEngine;

public class AudioManager : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private AudioClip[] playlist;

    private static AudioSource audioSource;
    private static NowPlayingUIText nowPlayingUIText;

    #endregion

    #region UPDATE_METHOD

    void Update()
    {

        if (audioSource == null)
        {

            STATUS.IS_AUDIO_ON = PlayerPrefs.GetFloat("is_audio_on", 0.5f);

            audioSource = Audio.AudioSource;
            audioSource.volume = STATUS.IS_AUDIO_ON;

        }

        if (nowPlayingUIText == null)

            nowPlayingUIText = FindObjectOfType<NowPlayingUIText>();

        if (!audioSource.isPlaying)
        {

            int index = Random.Range(0, playlist.Length);

            audioSource.clip = playlist[index];
            audioSource.Play();

            NowPlayingUIText.Text = ENV.PLAYLIST_TEXT[index];
            GameManager.OnTrigger(ENV.NOW_PLAYING);

        }

    }

    #endregion

    #region AUDIO_ON

    private static void AudioOn()
    {

        STATUS.IS_AUDIO_ON = STATUS.IS_AUDIO_ON != 0
            ? 0
            : 0.5f;

        audioSource.volume = STATUS.IS_AUDIO_ON;
        PlayerPrefs.SetFloat("is_audio_on", STATUS.IS_AUDIO_ON);

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public static void OnAudioOn() => AudioOn();

    #endregion

}
