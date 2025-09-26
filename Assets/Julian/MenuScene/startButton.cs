using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class startButton : MonoBehaviour
{       
    public void OnClick ()
    {
        SceneManager.LoadScene("LevelMain");
    }
}
