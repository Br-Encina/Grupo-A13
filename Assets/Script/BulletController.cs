using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float bulletPower = 20f; // velocidad de la bala
    [SerializeField] private float lifeTime = 4f; // duración antes de destruirse
    [SerializeField] private int damage = 50;
    
    HealthManager healthManager;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ////// Mover la bala hacia adelante con cierta velocidad
        rb.linearVelocity = transform.forward * bulletPower;

        // Destruir automáticamente después de cierto tiempo
        Destroy(gameObject, lifeTime);
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemies"))
        {
            Debug.Log("Impacto con el enemigo");
            healthManager = other.transform.GetComponentInParent<HealthManager>();//make damage
            if (healthManager != null) healthManager.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
    

