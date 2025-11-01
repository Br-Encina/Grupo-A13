using Unity.Cinemachine;
using UnityEngine;

public class CameraFollowIvan : MonoBehaviour
{
  private CinemachineCamera _cinemachineCamera;
  private Transform _cameraFollow;

  private void Start()
  {
    StartCoroutine(WaitForPlayer());
  }
  
  private System.Collections.IEnumerator WaitForPlayer()
  {
    while (_cinemachineCamera == null)
    {
      _cinemachineCamera = FindFirstObjectByType<CinemachineCamera>(); // Busca la CinemachineCamera en la escena
      yield return null;
    }
    GameObject player = null;
    while (player == null)
    {
      player = GameObject.FindWithTag("Player");
      yield return null;
    }

    while (_cameraFollow == null)
    {
      _cameraFollow = player.transform.Find("CameraFollow");
      yield return null;
    }

    _cinemachineCamera.Follow = _cameraFollow;
    
    
  }
  
}
