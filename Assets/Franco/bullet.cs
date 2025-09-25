using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] protected float _bulletSpeed;
    [SerializeField] protected float _bulletLifeTime;
    [SerializeField] protected int _bulletDamage;

    Rigidbody _rb;
    HealthManager healthManager;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        Destroy(gameObject, _bulletLifeTime);
    }

   public void Launch(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        _rb.linearVelocity = direction * _bulletSpeed;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Enemies"))
        {
            healthManager = other.transform.GetComponent<HealthManager>();//make damage
            if (healthManager != null) healthManager.TakeDamage(_bulletDamage);
        }
        Destroy(gameObject);
    }
}
