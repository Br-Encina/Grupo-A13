using System.Collections;
using UnityEngine;

public class EnemyScavenger : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float explosionRange = 2f;

    [Header("Daño")]
    [SerializeField] private int explosionDamage = 40;
    [SerializeField] private float explosionDelay = 0.5f;

    [Header("Referencias")]
    private Transform target;
    private Rigidbody rb;
    private bool isExploding = false;
    private HealthManager healthManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        healthManager = GetComponent<HealthManager>();
    }

    private void FixedUpdate()
    {
        if (target == null || isExploding) return;

        float distancia = Vector3.Distance(transform.position, target.position);

        if (distancia <= detectionRange)
        {
            // Seguir al jugador
            Vector3 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;

            // Si está lo suficientemente cerca, inicia la explosión
            if (distancia <= explosionRange)
            {
                StartCoroutine(Explode());
            }
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private IEnumerator Explode()
    {
        isExploding = true;
        rb.linearVelocity = Vector3.zero;

       
        yield return new WaitForSeconds(explosionDelay);

       
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<IHealth>(out var health))
            {
                health.TakeDamage(explosionDamage);
            }
        }

        // Efecto visual o de sonido (si tenés uno)
        Debug.Log($"{name} explota causando {explosionDamage} de daño.");

       
        healthManager.Death();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
