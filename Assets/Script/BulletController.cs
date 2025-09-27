using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float bulletPower = 20f; // velocidad de la bala
    [SerializeField] private float lifeTime = 4f;     // duración antes de destruirse

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ////// Mover la bala hacia adelante con cierta velocidad
        rb.linearVelocity = transform.forward * bulletPower;

        // Destruir automáticamente después de cierto tiempo
        Destroy(gameObject, lifeTime);
       
    }
}
