using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Transform shootSpawn;      // Punto de salida del disparo (la boca del arma)
    [SerializeField] GameObject bulletPrefab;   // Prefab de la bala
    [SerializeField] float bulletSpeed = 20f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click izquierdo
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Dirección = hacia donde está mirando el arma
        Vector3 shootDir = shootSpawn.forward;

        // Instanciamos la bala en la posición y orientación del arma
        GameObject bullet = Instantiate(bulletPrefab, shootSpawn.position, Quaternion.LookRotation(shootDir));

        // Aplicamos velocidad
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDir * bulletSpeed;
        }
    }
}