using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador cayó en la zona, respawneando...");
            other.transform.position = respawnPoint.position;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = Vector3.zero;
        }
    }
}