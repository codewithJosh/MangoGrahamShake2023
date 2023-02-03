using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] 
    private float rotationSpeed;

    private void Update()
    {

        transform.Rotate(
            0, 
            0, 
            rotationSpeed * (Time.deltaTime)
            );

    }

}