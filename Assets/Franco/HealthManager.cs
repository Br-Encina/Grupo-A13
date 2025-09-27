using UnityEngine;

public class HealthManager : MonoBehaviour, IHealth
{
    [SerializeField]int health = 100;

    //Necesario para hacer una llama al controlador del enemigo
    EnemyController enemyController;
    public int Health { get {return health; } set { health = value;}}
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(transform.name + " health " + health);

        if (health <= 0)
        {
            Death();
        }
        // hay que adaptar para que en lugar de usar EnemyControler use el script del enemigo que toque
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        enemyController = GetComponent<EnemyController>();

        enemyController.StarAttacking(player);
    }

    public void Death()
    {
        // Animate deadth
        Destroy(gameObject,5);
    }

    /*
    
    */
}
