using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [Header("Configuración de Puntos")]
    [Tooltip("El GameObject que define el punto A (inicio).")]
    public Transform pointA;
    [Tooltip("El GameObject que define el punto B (fin).")]
    public Transform pointB;

    [Header("Configuración de Movimiento")]
    [Tooltip("La velocidad de movimiento de la plataforma.")]
    public float speed = 2f;
    [Tooltip("La distancia mínima para considerar que se ha llegado al destino.")]
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

        // Configuración del Rigidbody (esencial para plataformas)
        rb.isKinematic = true;

        // Validar que los puntos estén asignados
        if (pointA == null || pointB == null)
        {
            Debug.LogError("¡Faltan puntos de referencia! Asigna Point A y Point B en el Inspector.");
            enabled = false;
            return;
        }

        // Establecer el primer objetivo como la posición del Point B
        nextTarget = pointB.position;
    }

    // Se usa FixedUpdate para manejar Rigidbody (física)
    void FixedUpdate()
    {
        // El movimiento se detiene si falta un punto, aunque ya debería estar deshabilitado.
        if (pointA == null || pointB == null) return;

        // 1. Calcular la dirección y el movimiento deseado
        Vector3 moveDirection = (nextTarget - rb.position).normalized;
        Vector3 movement = moveDirection * speed * Time.fixedDeltaTime;

        // 2. Mover la plataforma usando MovePosition
        rb.MovePosition(rb.position + movement);

        // 3. Comprobar si se ha llegado al objetivo
        float distanceToTarget = Vector3.Distance(rb.position, nextTarget);

        if (distanceToTarget < threshold)
        {
            // Cambiar el objetivo: 
            // Si el objetivo actual es B, el siguiente es A. Si es A, el siguiente es B.
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