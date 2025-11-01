using UnityEngine;

public class PlayerLookAtMouse : MonoBehaviour
{
  [SerializeField] private LayerMask layerMask;
  [SerializeField] private Transform transformPos;
  
  public void PlayerAtMouse()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit, 100f, layerMask))
    {
      Vector3 target = hit.point;

      target.y = transformPos.position.y;

      transformPos.LookAt(target);
    }
  }
  
}
