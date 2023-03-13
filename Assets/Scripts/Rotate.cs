using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField]
    private float rotationSpeed;

    void Update()
    {

        transform.Rotate(0, 0, rotationSpeed * (Time.deltaTime));

    }

}