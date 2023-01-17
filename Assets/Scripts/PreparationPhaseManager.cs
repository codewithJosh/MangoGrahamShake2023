using UnityEngine;
using UnityEngine.UI;

public class PreparationPhaseManager : MonoBehaviour
{
    
    [SerializeField] private Sprite[] actionNormalUIButtons;
    [SerializeField] private Sprite[] actionPressedUIButtons;
    [SerializeField] private Image actionUIButton;

    void Start()
    {


    }

    void Update()
    {

        actionUIButton.sprite = 
            SimpleInput.GetButton("OnAction") 
            ? actionPressedUIButtons[1]
            : actionNormalUIButtons[1];

    }

}
