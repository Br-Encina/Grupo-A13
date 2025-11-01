using UnityEngine;

public class Healing : MonoBehaviour
{
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
