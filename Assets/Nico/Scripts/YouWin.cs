using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
           winScreen.SetActive(true);
        }
        
    }
}
