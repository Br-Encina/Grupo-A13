using UnityEngine;

public class EnemyScavenger : MonoBehaviour, IPooledObject
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private int explosionDamage = 30;
    [SerializeField] private float lifetime = 5f;

    private Transform player;
    private Rigidbody rb;
    private float timer;

    // 1. Variable de seguro para evitar doble explosión
    private bool isExploding = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
        // 2. Resetear el seguro y el timer
        isExploding = false;
        timer = lifetime;

        // Buscar player (Optimización: Si tienes un GameManager, pide la referencia desde ahí)
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Debug.Log(player);

        rb.linearVelocity = Vector3.zero; // Usar .velocity es más estándar que .linearVelocity
        rb.angularVelocity = Vector3.zero;
    }

    // 3. Mover la lógica de físicas a FixedUpdate
    private void FixedUpdate()
    {
        if (player == null || isExploding)
        {
            rb.linearVelocity = Vector3.zero; // Detenerse si no hay jugador o si está explotando
            return;
        }

        // Mover hacia el player
        Vector3 dir = (player.position - transform.position).normalized;
        rb.linearVelocity = dir * speed;
    }

    // 4. Update solo se encarga del timer
    private void Update()
    {
        if (isExploding) return; // Si ya estamos explotando, no descontar timer

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Explode();
        }
    }

    // 5. Filtrar la colisión
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // 6. Activar el seguro
        if (isExploding) return;
        isExploding = true;
        Debug.Log("Scavenger exploding!");

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var col in hits)
        {
            // Buscar el transform raíz (puede que el collider sea de un hijo del jugador)
            Transform root = col.transform.root;

            // Comprobar si la raíz tiene la etiqueta Player
            if (!root.CompareTag("Player"))
            {
                // Alternativamente, si el collider tiene un Rigidbody y su gameObject está etiquetado:
                if (col.attachedRigidbody == null || !col.attachedRigidbody.gameObject.CompareTag("Player"))
                    continue;
            }

            // Intentar obtener HealthManager primero en la raíz, si no, en los padres del collider
            var health = root.GetComponent<HealthManager>() ?? col.GetComponentInParent<HealthManager>();

            if (health != null)
            {
                health.TakeDamage(explosionDamage);
            }
            else
            {
                Debug.LogWarning($"Explode: se detectó un objeto con tag Player ({root.name}) pero no tiene HealthManager attached.", root);
            }
        }

        // Volver al pool
        EnemyPooler.Instance.ReturnToPool("Scavenger", this.gameObject);
    }

    public void OnObjectDespawn()
    {
        // Resetear velocidad por si acaso
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}