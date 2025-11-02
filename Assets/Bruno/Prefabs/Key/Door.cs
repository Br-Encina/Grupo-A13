using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredKeyType;

    private void OnCollisionEnter(Collision collision)
    {
        if (KeyManager.Instance.HasKey(requiredKeyType))
        {
            // Lógica para abrir la puerta
            gameObject.SetActive(false);
        }
        else
        {
            // Lógica para indicar que no se tiene la llave
            Debug.Log("Necesitas la llave: " + requiredKeyType);
        }
    }
}
