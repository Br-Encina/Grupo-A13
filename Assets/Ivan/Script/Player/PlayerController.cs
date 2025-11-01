using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
  private PlayerMovement playerMovement;
  private PlayerLookAtMouse playerLookAtMouse;
  private PlayerFireGun playerFireGun;
  private void Start()
  {
    playerMovement = GetComponent<PlayerMovement>();
    playerLookAtMouse = GetComponent<PlayerLookAtMouse>();
    playerFireGun = GetComponentInChildren<PlayerFireGun>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      playerFireGun.IsGunFireAttackTrue();
      //playerFireGun.FireBullet();
    }
  }

  private void LateUpdate()
  {
    playerLookAtMouse.PlayerAtMouse();
  }

  private void FixedUpdate()
  {
    if (playerMovement == null)
    {
      return;
    }

    playerMovement.MovementPlayer();
  }

}
