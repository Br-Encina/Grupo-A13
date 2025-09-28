using UnityEngine;

public class HealthManager : MonoBehaviour, IHealth
{
    [SerializeField]int health = 100;

    //Necesario para hacer una llama al controlador del enemigo
    //EnemyController enemyController;
    Enemy enemy;
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
       // GameObject player = GameObject.FindGameObjectWithTag("Player");
      //enemyController = GetComponent<EnemyController>();

        //enemyController.StarAttacking(player);
    }

    public void Death()
    {
        // Animate deadth
        Destroy(gameObject,1);
        if(gameObject.tag=="Player")
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
        else
        {
           
            Debug.Log(transform.name + " is dead");
          
            //enemyController.enabled = false;
            //GetComponent<Collider>().enabled = false;
            //GetComponent<Rigidbody>().isKinematic = true;
            //GetComponent<Enemy>().enabled = false;
        }
    }

    /*
    
    */
}
