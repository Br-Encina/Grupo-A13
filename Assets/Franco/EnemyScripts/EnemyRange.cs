using UnityEngine;
using System.Collections;

public class EnemyRange : Enemy
{
    private Vector3 direction;
    private LineRenderer lineRenderer;
    private bool isAttacking = false;

    [SerializeField] private float rayDistance = 20f;
    [SerializeField] private float laserDuration = 0.2f;
    [SerializeField] private float postAttackDelay = 0.5f;
    [SerializeField] private Color laserColor = Color.cyan;
    [SerializeField] private GameObject startLaserPoint;


    private void Start()
    {
        // Configurar LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = laserColor;
        lineRenderer.endColor = laserColor;
        lineRenderer.enabled = false;
    }


    public override void StateAtacar()
    {
        if (!live || isAttacking) return;
        if (PuedeAtacar())
        {

            StartCoroutine(RangedAttackSequence());

        }
    }


    private IEnumerator RangedAttackSequence()
    {
        isAttacking = true;

        Vector3 origin;
        if (startLaserPoint != null)
        {
            origin = startLaserPoint.transform.position;
        }
        else
        {
            origin = transform.position;
        }
        Vector3 playerPivot = target.position + new Vector3(0, 1, 0);
        direction = (playerPivot - origin).normalized;
        RaycastHit hit;
        Vector3 endPosition;


        if (Physics.Raycast(origin, direction, out hit, rayDistance))
        {
            endPosition = hit.point;
            Debug.Log("Rayo impactó en: " + hit.collider.name);

            if (hit.collider.CompareTag("Player"))
            {
                healthManager.TakeDamage(10);
            }
        }
        else
        {
            endPosition = origin + direction * rayDistance;
        }


        DrawLaser(origin, endPosition);


        yield return new WaitForSeconds(laserDuration);


        lineRenderer.enabled = false;


        yield return new WaitForSeconds(postAttackDelay);


        ReiniciarCooldown();

        isAttacking = false;
    }


    private void DrawLaser(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;

    }


}