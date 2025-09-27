using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Configuración de Puntos")]
    
    public Transform pointA;
    
    public Transform pointB;

    [Header("Configuración de Movimiento")]
    
    public float speed = 2f;
    
    public float threshold = 0.1f;

    private Rigidbody rb;
    private Vector3 nextTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("PlatformMover requiere un componente Rigidbody en el mismo GameObject.");
            enabled = false;
            return;
        }

       
        rb.isKinematic = true;

       
        if (pointA == null || pointB == null)
        {
            Debug.LogError("¡Faltan puntos de referencia! Asigna Point A y Point B en el Inspector.");
            enabled = false;
            return;
        }

     
        nextTarget = pointB.position;
    }

  
    void FixedUpdate()
    {
        // El movimiento se detiene si falta un punto, aunque ya debería estar deshabilitado.
        if (pointA == null || pointB == null) return;

        
        Vector3 moveDirection = (nextTarget - rb.position).normalized;
        Vector3 movement = moveDirection * speed * Time.fixedDeltaTime;

        
        rb.MovePosition(rb.position + movement);

    
        float distanceToTarget = Vector3.Distance(rb.position, nextTarget);

        if (distanceToTarget < threshold)
        {
            
            if (nextTarget == pointB.position)
            {
                nextTarget = pointA.position;
            }
            else
            {
                nextTarget = pointB.position;
            }
        }
    }
}