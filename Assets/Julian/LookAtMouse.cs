using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public LayerMask groundLayer;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, groundLayer))
        {
            Vector3 target = hit.point;

            target.y = transform.position.y;

            transform.LookAt(target);

            // Debug log just the XZ coordinates
            Debug.Log("Mouse on floor at: X=" + hit.point.x + " Z=" + hit.point.z);
        }
    }
    
}
