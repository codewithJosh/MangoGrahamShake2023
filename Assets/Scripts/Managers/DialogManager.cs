using UnityEngine;

public class DialogManager : MonoBehaviour
{

    #region DIALOG_METHOD

    public static void Dialog(string _title, string _description, string _mode)
    {

        FindObjectOfType<TitleUIText>().Text = _title;
        FindObjectOfType<DescriptionUIText>().Text = _description;
        GameManager.OnTrigger(_mode);

    }

    #endregion

    #region FAILED_METHOD

    /*
     * Upon calling this method the system will prompt the user with a dialog
     * that contains something went wrong.
     * Also, this method requires a parameter STRING field
     * where we can pass our message.
     */
    private static void Failed(string _description)
    {

        /*
         * An error sound effects must be played.
         */
        FindObjectOfType<SoundsManager>().OnError();

        /*
         * Also, a dialog contains something went wrong must be displayed.
         */
        Dialog(
            "FAILED",
            _description,
            ENV.DIALOG);

    }

    #endregion

    #region AUTOMATED_PROPERTIES

    public static void OnDialog(string _title, string _description, string _mode) => Dialog(_title, _description, _mode);

    public static void OnFailed(string _description) => Failed(_description);

    #endregion

}
