using UnityEngine;

public class MyPlayerMovement : MonoBehaviour
{

    public float speed = 4;
    public float rotationspeed = 10;

    private Vector3 forward, right;
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Camera.main.transform.right;
        right.y = 0;
        right = Vector3.Normalize(right);
    }

    
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = horizontalInput * right + verticalInput * forward;

        if (direction.magnitude > 0.1f)
        {
            transform.position += direction * speed * Time.deltaTime;
        }


    }
}
