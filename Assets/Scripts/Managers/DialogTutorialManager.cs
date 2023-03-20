using TMPro;
using UnityEngine;

public class DialogTutorialManager : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private TextMeshProUGUI titleUIText;

    [SerializeField]
    private TextMeshProUGUI descriptionUIText;

    #endregion

    #region DIALOG_METHOD

    private void Dialog(string _title, string _description)
    {

        titleUIText.text = _title;
        descriptionUIText.text = _description;
        GameManager.OnTrigger(ENV.DIALOG_TUTORIAL);

    }

    #endregion

    #region AUTOMATED_PROPERTY

    public void OnDialog(string _title, string _description) => Dialog(_title, _description);

    #endregion

}
