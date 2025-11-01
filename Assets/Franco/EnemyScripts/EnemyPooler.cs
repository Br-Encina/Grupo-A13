using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int size = 5;
    public bool expandable = true;
}

public class EnemyPooler : MonoBehaviour
{
    public static EnemyPooler Instance;

    [Header("Pools Config")]
    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            if (pool.prefab == null)
            {
                Debug.LogError($"Pool '{pool.tag}' prefab vacío.");
                continue;
            }

            Transform parentFolder = new GameObject(pool.tag + "_Pool").transform;
            parentFolder.SetParent(transform);

            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, parentFolder);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"Pool '{tag}' no existe.");
            return null;
        }

        if (poolDictionary[tag].Count == 0)
        {
            Pool originalPool = pools.Find(p => p.tag == tag);

            if (originalPool != null && originalPool.expandable)
            {
                // ----- INICIO DE CORRECCIÓN -----
                // 1. Buscar el transform padre correcto (creado en Start)
                Transform parentFolder = transform.Find(originalPool.tag + "_Pool");

                // 2. Instanciar el objeto en el padre correcto
                GameObject extra = Instantiate(originalPool.prefab, parentFolder);
                // ----- FIN DE CORRECCIÓN -----
                extra.SetActive(false);
                Debug.LogWarning($"Pool '{tag}' vacío → expandiendo.");
                return ActivateObject(extra, position, rotation);
            }

            Debug.LogWarning($"Pool '{tag}' vacío y NO expandible.");
            return null;
        }

        GameObject obj = poolDictionary[tag].Dequeue();

        return ActivateObject(obj, position, rotation);
    }

    private GameObject ActivateObject(GameObject obj, Vector3 position, Quaternion rotation)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        IPooledObject pooled = obj.GetComponent<IPooledObject>();
        pooled?.OnObjectSpawn();

        return obj;
    }

    public void ReturnToPool(string tag,GameObject obj)
    {
        if (obj == null) return;

       

        if (string.IsNullOrEmpty(tag) || !poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"El objeto '{obj.name}' no pertenece a ningún pool, destruyendo.");
            Destroy(obj);
            return;
        }

        IPooledObject pooled = obj.GetComponent<IPooledObject>();
        pooled?.OnObjectDespawn();

        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}
