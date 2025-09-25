using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform playerTarget;
    private bool isAttacking = false;

    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if(navMeshAgent == null)
        {
            Debug.LogError("EnemyControler requiere NavMeshAgent.");

        }
    }

    public void StarAttacking(GameObject player)
    {
        playerTarget = player.transform;
        isAttacking = true;
        Debug.Log(gameObject.name +" Está atacando");
    }

    private void Update()
    {
        if (isAttacking && playerTarget != null)
        {
            navMeshAgent.SetDestination(playerTarget.position);
        }
    }
}

