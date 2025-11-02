using UnityEngine;
using System.Collections.Generic;


public class Door : MonoBehaviour
{
    [Tooltip("Lista de llaves necesarias para abrir la puerta.")]
    public List<string> requiredKeys = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
       

        // Verificamos si el jugador tiene todas las llaves necesarias
        bool hasAllKeys = true;
        foreach (string key in requiredKeys)
        {
            if (!KeyManager.Instance.HasKey(key))
            {
                hasAllKeys = false;
                Debug.Log("Falta la llave: " + key);
            }
        }

        if (hasAllKeys)
        {
            Debug.Log("¡Puerta abierta! Todas las llaves obtenidas.");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No puedes abrir esta puerta. Te faltan llaves.");
        }
    }
}
