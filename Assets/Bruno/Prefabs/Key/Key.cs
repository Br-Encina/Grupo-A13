using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyType;

    private void Update()
    {
        transform.Rotate(Vector3.up * 50f * Time.deltaTime);
    }

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