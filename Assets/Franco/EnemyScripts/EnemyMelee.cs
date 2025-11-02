using System.Collections;
using UnityEngine;

public class EnemyMelee : Enemy
{
  [SerializeField] private float forceAtack = 25f;
  private Animator animator;
  private Vector3 direction;
  [SerializeField] private float retroceso = 5f;

  [Header("Warning Area")]
  [SerializeField] private GameObject warning;
  [SerializeField] private float warningDuration = 0.5f;
    
  private void Start()
  {
    warning.SetActive(false);
    animator = GetComponent<Animator>();
  }

  public override void StateAtacar()
  {

    if (!live) return;
    StartCoroutine(ShowWarningArea(target.transform.position));
    if (PuedeAtacar())
    {
      // La direcci�n se sigue usando para el impulso
      direction = (target.transform.position - transform.position).normalized;
      // �IMPORTANTE! Ahora pasamos la posici�n del objetivo (target.transform.position)
      animator.SetTrigger("IsAttack");
      rb.AddForce((direction * forceAtack), ForceMode.Impulse);

      ReiniciarCooldown();
    }
  }
  private void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      target.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
      Debug.Log("Jugador golpeado por el enemigo cuerpo a cuerpo");
      healthManager.TakeDamage(10);
    }
    else if (collision.gameObject.CompareTag("Pared"))
    {
      Debug.Log("Enemigo cuerpo a cuerpo toc� pared");

      Vector3 normal = collision.contacts[0].normal;

      Vector3 bounceDirection = Vector3.Reflect(direction, normal).normalized;

      rb.linearVelocity = Vector3.zero;

      rb.AddForce(bounceDirection * retroceso, ForceMode.Impulse);

      ChangeState(States.idle);

      StartCoroutine(DelayStun(0.1f));
    }
  }
    // abria que hacer una interfaz IStunnable
    private IEnumerator DelayStun(float delay)
    {
        yield return new WaitForSeconds(delay);
        ApplyStun();
    }

  // abria que hacer una interfaz ShowWarning
  private IEnumerator ShowWarningArea(Vector3 targetPosition)
  {

    Vector3 direccionAlObjetivo = targetPosition - warning.transform.position;


    Quaternion rotacionDeseada = Quaternion.LookRotation(direccionAlObjetivo);


    float anguloY = rotacionDeseada.eulerAngles.y;


    warning.transform.rotation = Quaternion.Euler(0f, anguloY, 0f);


    warning.SetActive(true);

    yield return new WaitForSeconds(warningDuration);

    warning.SetActive(false);
  }
}