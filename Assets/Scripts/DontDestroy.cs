using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    private void Awake()
    {

        if (FindObjectsOfType(GetType()).Length > 1)

            Destroy(gameObject);

        else

            DontDestroyOnLoad(gameObject);

    }

}
