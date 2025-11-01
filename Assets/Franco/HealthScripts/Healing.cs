using UnityEngine;

public class Healing : MonoBehaviour
{

    private void Update()
    {
        //rotar el objeto de curación lentamente
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager healthManager = other.GetComponentInParent<HealthManager>();
            if (healthManager != null)
            {
                healthManager.Heal(20); // Cura 20 puntos de salud
                Destroy(gameObject); // Destruye el objeto de curación después de usarlo
            }
        }
    }
}
