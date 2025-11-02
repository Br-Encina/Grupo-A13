using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Llave recolectada: " + keyType);
            KeyManager.Instance.Collect(keyType); // Guarda la llave como recolectada
            Destroy(gameObject);
        }
    }
}