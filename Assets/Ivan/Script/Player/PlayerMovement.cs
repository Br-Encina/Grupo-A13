using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
  [Header("Movimiento")]
  [SerializeField] private float speed = 5.0f;

  [Header("Raycast al suelo")]
  [SerializeField] private LayerMask groundMask;
  [SerializeField] private float maxRayDistance = 500f;

  private Rigidbody rb;
  private Animator playerAnim;
  private Camera cam;

  private void Start()
  {
    rb = GetComponent<Rigidbody>();
    playerAnim = GetComponent<Animator>();
    cam = Camera.main; // la cámara debe tener tag MainCamera
  }

  public void MovementPlayer()
{
  // 1) Input WASD
  Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
  input = Vector3.ClampMagnitude(input, 1f);

  // 2) Forward desde el mouse
  if (!TryGetMouseForwardXZ(out Vector3 mouseFwd))
  {
    playerAnim?.SetBool("isMoving", false);
    return;
  }

  // 3) Right estable (según cámara, sin inclinación)
  Vector3 camFwd = cam.transform.forward; camFwd.y = 0f; camFwd.Normalize();
  Vector3 camRight = cam.transform.right; camRight.y = 0f; camRight.Normalize();

  // 4) Dirección de movimiento: W hacia el mouse, A/D según cámara
  Vector3 moveDir = mouseFwd * input.z + camRight * input.x;

  // (opcional) normalizar para evitar “diagonales más rápidas”
  if (moveDir.sqrMagnitude > 1e-6f) moveDir.Normalize();

  // 5) Aplicar desplazamiento
  Vector3 movePos = moveDir * speed * Time.fixedDeltaTime;
  rb.MovePosition(rb.position + movePos);

  // 6) Animación
  playerAnim?.SetBool("isMoving", input.sqrMagnitude > 0.0001f);
}


  private bool TryGetMouseForwardXZ(out Vector3 forward)
  {
    forward = Vector3.zero;
    if (cam == null) return false;

    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    Vector3 lookPoint;

    if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, groundMask, QueryTriggerInteraction.Ignore))
    {
      lookPoint = hit.point;
    }
    else
    {
      // Plano horizontal a la altura del jugador como respaldo
      Plane plane = new Plane(Vector3.up, new Vector3(0f, transform.position.y, 0f));
      if (!plane.Raycast(ray, out float enter)) return false;
      lookPoint = ray.GetPoint(enter);
    }

    Vector3 dir = lookPoint - transform.position;
    dir.y = 0f;
    if (dir.sqrMagnitude < 0.0001f) return false;

    forward = dir.normalized;
    return true;
  }
}
