using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("winScene");
        }
        
    }
}
