using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonHandler : MonoBehaviour
{
    public void OnRestarLevel() {
        SceneManager.LoadScene("LevelTutorial");

    }

    public void OnExit()
    {
        SceneManager.LoadScene("MainMenu");

    }

}
