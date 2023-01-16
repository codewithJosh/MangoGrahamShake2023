using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{

    [SerializeField] private Button loginUIButton;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {

        loginUIButton.interactable = Application.internetReachability != NetworkReachability.NotReachable;

    }
}
