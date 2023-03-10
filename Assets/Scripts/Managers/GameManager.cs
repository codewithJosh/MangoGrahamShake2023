using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    /*
     * Let's privately declare an OBJECT field
     * where we can store our Animator INSTANCE later.
     */
    private Animator animator;

    private NowInformingUIText nowInformingUIText;

    /*
     * A predefined (built-in) method in UNITY
     * where is called when the script instance is being loaded.
     */
    void Awake()
    {

        DontDestroy();

    }

    /*
     * A predefined (built-in) method in UNITY
     * where is called every frame, if the MonoBehaviour is enabled.
     */
    void Update()
    {

        if (animator == null)

            animator = FindObjectOfType<Animator>();

        if (nowInformingUIText == null)

            nowInformingUIText = FindObjectOfType<NowInformingUIText>();

    }

    /*
     * Upon calling this method it must return a string value of current active toggle belong to the toggle group.
     */
    private string ToggleName(ToggleGroup _toggleGroup)
    {

        /*
         * Let's locally declare a TOGGLE field.
         * Also, let's initialize it's value to a PARAMETERIZED TOGGLE GROUP
         * that returns a value of current active TOGGLE belong to that TOGGLE GROUP.
         */
        Toggle toggle = _toggleGroup
            .ActiveToggles()
            .FirstOrDefault();

        // Lastly, let's returns a string value of current active toggle.
        return toggle.name.ToString();

    }

    private void DontDestroy()
    {

        if (FindObjectsOfType(GetType()).Length > 1)

            Destroy(gameObject);

        else

            DontDestroyOnLoad(gameObject);

    }

    /*
     * Upon calling this method the system will prompt the user with a dialog
     * that contains something went wrong.
     * Also, this method requires a parameter STRING field
     * where we can pass our message.
     */
    private void Failed(string _description)
    {

        /*
         * An error sound effects must be played.
         */
        FindObjectOfType<SoundsManager>().OnError();

        /*
         * Also, a dialog contains something went wrong must be displayed.
         */
        FindObjectOfType<DialogManager>().OnDialog(
            "FAILED",
            _description,
            "dialog");

    }

    private void NowInforming(string _text)
    {

        nowInformingUIText.Text = _text;
        OnTrigger("nowInforming");

    }

    public void OnNowInforming(string _text) => NowInforming(_text);

    public void OnNext() => FindObjectOfType<GameManager>().OnTrigger("next");

    /*
     * Let's publicly declare an OBJECT property
     * where we can only allow other classes to get the value/ referenced.
     */
    public Animator Animator => animator;

    /*
     * Let's publicly declare a GetToggle method that has a string value.
     * Also, let's add a publicly get method init.
     */
    public string GetToggleName(ToggleGroup _toggleGroup) => ToggleName(_toggleGroup);

    /*
     * Let's publicly declare a FAILED method
     * where we can only allow other classes to used.
     */
    public void OnFailed(string _description) => Failed(_description);

    public void OnTrigger(string _trigger) => animator.SetTrigger(_trigger);

}