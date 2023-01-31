using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Animator animator;

    public Animator GetAnimator
    {

        get { return animator; }

    }
    public string GetToggle(ToggleGroup _toggleGroup)
    {

        Toggle toggle = _toggleGroup.ActiveToggles().FirstOrDefault();
        return toggle.name.ToString();

    }

}