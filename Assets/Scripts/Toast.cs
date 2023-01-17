using UnityEngine;
using TMPro;

public class Toast : MonoBehaviour
{

    private TextMeshProUGUI toastUIText;

    void Awake()
    {

        toastUIText = GetComponent<TextMeshProUGUI>();

    }

    public void OnToast(string text)
    {

        toastUIText.text = text;
        FindObjectOfType<GameManager>().GetAnimator.SetTrigger("toast");

    }

}
