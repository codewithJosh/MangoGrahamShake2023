using UnityEngine;

public class SoundsManager : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private AudioClip[] soundFX;

    private static AudioSource audioSource;

    #endregion

    #region AWAKE_METHOD

    void Awake()
    {

        audioSource = GetComponent<AudioSource>();

        STATUS.IS_SOUNDS_ON = PlayerPrefs.GetFloat("is_sounds_on", 1);

        audioSource.volume = STATUS.IS_SOUNDS_ON;

    }

    #endregion

    #region SOUND_ON_METHOD

    private static void SoundsOn()
    {

        STATUS.IS_SOUNDS_ON = STATUS.IS_SOUNDS_ON != 0
            ? 0
            : 1;

        audioSource.volume = STATUS.IS_SOUNDS_ON;
        PlayerPrefs.SetFloat("is_sounds_on", STATUS.IS_SOUNDS_ON);

    }

    #endregion

    #region CLICK_METHOD

    private void Click(int _index)
    {

        audioSource.clip = soundFX[_index];
        audioSource.Play();

    }

    #endregion

    #region AUTOMATED_PROPERTIES

    public static void OnSoundsOn() => SoundsOn();

    public void OnClicked() => Click(0);

    public void OnGrahamCrack() => Click(1);

    public void OnError() => Click(2);

    #endregion

}
