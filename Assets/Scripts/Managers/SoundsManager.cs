using UnityEngine;

public class SoundsManager : MonoBehaviour
{

    [SerializeField]
    private AudioClip[] soundFX;

    private AudioSource audioSource;

    private int isSoundsOn;

    void Update()
    {

        if (audioSource == null)
        {

            isSoundsOn = PlayerPrefs.GetInt("is_sounds_on", 1);

            audioSource = FindObjectOfType<Sounds>().AudioSource;
            audioSource.loop = false;
            audioSource.volume = isSoundsOn;

        }

    }

    private void IsSoundsOn()
    {

        isSoundsOn = isSoundsOn != 0
            ? 0
            : 1;

        audioSource.volume = isSoundsOn;
        PlayerPrefs.SetInt("is_sounds_on", isSoundsOn);

    }

    private void Click(int _index)
    {

        audioSource.clip = soundFX[_index];
        audioSource.Play();

    }

    public bool IsSoundsMuted => isSoundsOn == 0;

    public void OnIsSoundsOn() => IsSoundsOn();

    public void OnClicked() => Click(0);

    public void OnGrahamCrack() => Click(1);

    public void OnError() => Click(2);

}
