using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    #region DECLARATION

    /*
     * Let's privately declare an OBJECT field
     * where we can store our Animator INSTANCE later.
     */
    private static Animator animator;

    private static NowInformingUIText nowInformingUIText;

    #endregion

    #region AWAKE_METHOD

    /*
     * A predefined (built-in) method in UNITY
     * where is called when the script instance is being loaded.
     */
    void Awake()
    {

        DontDestroy();

    }

    #endregion

    #region UPDATE_METHOD

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

        if (SimpleInput.GetButtonDown("OnOK")
            || SimpleInput.GetButtonDown("OnYes")
            || SimpleInput.GetButtonDown("OnNo"))
        {

            FindObjectOfType<SoundsManager>().OnGrahamCrack();
            OnTrigger("ok");

            if (!SimpleInput.GetButtonDown("OnOK"))
                
                STATUS.STATE = STATUS.STATES.IDLE;

        }

    }

    #endregion

    #region DONT_DESTROY_METHOD

    private void DontDestroy()
    {

        if (FindObjectsOfType(GetType()).Length > 1)

            Destroy(gameObject);

        else

            DontDestroyOnLoad(gameObject);

    }

    #endregion

    #region TOGGLE_NAME_METHOD

    /*
     * Upon calling this method it must return a string value of current active toggle belong to the toggle group.
     */
    private static string ToggleName(ToggleGroup _toggleGroup)
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

    #endregion

    #region NOW_INFORMING_METHOD

    private static void NowInforming(string _text)
    {

        nowInformingUIText.Text = _text;
        OnTrigger(ENV.NOW_INFORMING);

    }

    #endregion

    #region AUTOMATED_PROPERTIES

    /*
     * Let's publicly declare an OBJECT property
     * where we can only allow other classes to get the value/ referenced.
     */
    public static Animator Animator => animator;

    /*
     * Let's publicly declare a GetToggle method that has a string value.
     * Also, let's add a publicly get method init.
     */
    public static string GetToggleName(ToggleGroup _toggleGroup) => ToggleName(_toggleGroup);

    public static void OnTrigger(string _trigger) => animator.SetTrigger(_trigger);

    public static void OnBool(string _field, bool _value) => animator.SetBool(_field, _value);

    public static void OnNowInforming(string _text) => NowInforming(_text);

    public void OnNext() => OnTrigger(ENV.NEXT);

    #endregion

}