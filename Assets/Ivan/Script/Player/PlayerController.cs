using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
  private PlayerMovement playerMovement;
  private PlayerLookAtMouse playerLookAtMouse;
  private void Start()
  {
    playerMovement = GetComponent<PlayerMovement>();
    playerLookAtMouse = GetComponent<PlayerLookAtMouse>();
  }

  private void Update()
  {
    
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
