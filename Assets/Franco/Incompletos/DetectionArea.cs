using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    private BoxCollider detectionCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        detectionCollider = GetComponent<BoxCollider>();
        if (detectionCollider == null)
        {
            Debug.LogError("DetectionArea requiere BoxCollider");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado!");

            Vector3 searchHalfExtents = Vector3.Scale(detectionCollider.size, transform.lossyScale)/2;

            Collider[] hitColliders = Physics.OverlapBox(transform.position, searchHalfExtents, transform.rotation);

            foreach (var hitCollider in hitColliders)
            {
                EnemyController enemy = hitCollider.GetComponent<EnemyController>();
                if(enemy != null)
                {
                    enemy.StarAttacking(other.gameObject);
                }
            }

        }
    }

    private void OnDrawGizmos()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if(collider !=null)
        {
            Gizmos.color = Color.red;

        }
    }
}
