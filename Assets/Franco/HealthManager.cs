using UnityEngine;

public class HealthManager : MonoBehaviour, IHealth
{
    [SerializeField]int health = 100;

    public int Health { get {return health; } set { health = value;}}
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(transform.name + " health " + health);

        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        // Animate deadth
        Destroy(gameObject,5);
    }

    /*
     private void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("Enemies"))
        {
          healthManager = other.transform.GetComponent<HealthManager>();//make damage
          if(healthManager != null) healthManager.TakeDamage(_bulletDamage);
        }
        Destroy(gameObject);
    }
    */
}
