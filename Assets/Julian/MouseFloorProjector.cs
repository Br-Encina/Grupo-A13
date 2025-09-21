using UnityEngine;

public class MouseFloorProjector : MonoBehaviour
{
    public LayerMask groundLayer; // Assign floor layer
    public float maxSlope = 15f;  // Maximum tilt angle allowed (degrees)


    private void Start()
    {
        Cursor.visible = false;

        // Optional: lock cursor to the game window
        Cursor.lockState = CursorLockMode.Confined; // or Locked
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, groundLayer))
        {
            // Check if surface is "horizontal enough"
            float angle = Vector3.Angle(hit.normal, Vector3.up);

            if (angle <= maxSlope)
            {
                // Move the projector/quad to hit point
                transform.position = hit.point + Vector3.up * 0.01f; // Slight offset to avoid z-fighting

                // Align rotation to the floor normal
                transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                // Make sure it's visible
                if (!gameObject.activeSelf) gameObject.SetActive(true);
            }
            else
            {
                // Hide if too tilted
                if (gameObject.activeSelf) gameObject.SetActive(false);
            }
        }
    }
}
