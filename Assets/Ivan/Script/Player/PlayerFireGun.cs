using UnityEngine;

public class PlayerFireGun : MonoBehaviour
{
  private Animator animator;
  [SerializeField] private Transform SpawnPointBullet;
  [SerializeField] private GameObject bulletPrefab;

  private void Start()
  {
    animator = GetComponent<Animator>();
  }
  
  public void FireBullet()
  {
    Vector3 BulletDir = SpawnPointBullet.right;
    
    Quaternion rot = Quaternion.LookRotation(BulletDir, Vector3.up);

    GameObject Bullet = Instantiate(bulletPrefab, SpawnPointBullet.position, rot);

    if (Bullet.TryGetComponent<Rigidbody>( out var rb))
    {
      rb.linearVelocity = BulletDir * 20f;
    }
  }
  
  
  public void IsGunFireAttackTrue()
  {
    animator.SetLayerWeight(1, 1);
    animator.SetTrigger("FireGun");
  }
}
