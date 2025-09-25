using UnityEngine;

public class EnemyScavenger : Enemy
{
    [SerializeField] private float speed = 5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override void StateIdle()
    {
        // El Scavenger no patrulla, permanece inactivo en estado Idle
        rb.linearVelocity = Vector3.zero;
        // Si el jugador est� en rango, cambia a estado de seguimiento
        if (distancia < distanciaSeguir)
        {
            ChangeState(States.follow);
        }
    }
    public override void StateFollow()
    {
        // Persigue al jugador
        ChasePlayer();
        if (distancia < distanciaAtacar)
        {
            ChangeState(States.atack);
        }
        else if (distancia > distanciaVolver)
        {
            // Si el jugador se aleja, vuelve a estado Idle
            ChangeState(States.idle);
        }
    }
    public override void StateAtacar()
    {
        Debug.Log("Enemy Scavenger ataca");
        // Ataque cuerpo a cuerpo: deslizamiento corto hacia el jugador
        if (PuedeAtacar())
        {
            Vector3 direccionAtaque = (target.position - transform.position).normalized;
            float distanciaDeslizamiento = 2f; // distancia corta del golpe
            float fuerzaDeslizamiento = 15f;   // fuerza del impulso

            // Solo desliza si la distancia al jugador es mayor que la distancia mínima de golpe
            if (Vector3.Distance(transform.position, target.position) > distanciaDeslizamiento)
            {
                rb.AddForce(direccionAtaque * fuerzaDeslizamiento, ForceMode.Impulse);
            }
            else
            {
                // Si ya está cerca, solo aplica un pequeño impulso
                rb.AddForce(direccionAtaque * (fuerzaDeslizamiento * 0.5f), ForceMode.Impulse);
            }
            ReiniciarCooldown();
        }
    }

    public override void StateDead()
    {
        if (!live) return;
        live = false;
        rb.linearVelocity = Vector3.zero;
        // Aqu puedes implementar la l�gica de muerte del Scavenger
        // Por ejemplo, reproducir una animaci�n de muerte o desactivar el enemigo
        gameObject.SetActive(false);
        // Despu�s de morir, podr�as reutilizar el objeto para un pool de enemigos
    }
    private void ChasePlayer()
    {
        if (!live) return;
        Vector3 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    
    
}
