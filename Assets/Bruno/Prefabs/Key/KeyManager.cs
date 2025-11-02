using UnityEngine;

using System.Collections.Generic;
public class KeyManager : MonoBehaviour
{
    private static KeyManager _instance;
    public static KeyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindFirstObjectByType<KeyManager>();
                if (_instance == null)
                {
                    Debug.LogError("No hay un KeyManager en la escena.");
                }
            }
            return _instance;
        }
    }

    private HashSet<string> collectedKeys = new HashSet<string>();

    public void Collect(string keyType)
    {
        collectedKeys.Add(keyType);
        Debug.Log("Llave guardada: " + keyType);
    }

    public bool HasKey(string keyType)
    {
        return collectedKeys.Contains(keyType);
    }
}
