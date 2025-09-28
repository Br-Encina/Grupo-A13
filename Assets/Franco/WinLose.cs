using UnityEngine;

public  class WinLose : MonoBehaviour
{
    [SerializeField]GameObject player;
    HealthManager healthManager;
    [SerializeField] GameObject gameOverCanvas;
    void Start()
    {
        healthManager = player.GetComponent<HealthManager>();
        
    }
    private void Update()
    {
       GameOver();
    }

    private void GameOver()
    {
        if (healthManager.Health <= 0)
        {
            Cursor.visible = true;
            gameOverCanvas.SetActive(true);
        }
    }


}
