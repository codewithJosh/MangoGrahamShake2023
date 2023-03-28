using UnityEngine;

public class Rotate : MonoBehaviour
{

    #region DECLARATION

    [SerializeField]
    private float rotationSpeed;

    #endregion

    #region UPDATE_METHOD

    void Update()
    {

        transform.Rotate(0, 0, rotationSpeed * (Time.deltaTime));

    }

    #endregion

}