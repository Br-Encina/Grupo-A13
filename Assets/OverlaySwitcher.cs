using UnityEngine;

public class OverlaySwitcher : MonoBehaviour
{

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject looseScreen;

    public void LoadWinLooseScene (bool status)
    {
        if (status)
        {
            winScreen.SetActive(true);
            looseScreen.SetActive(false);
        }
        else {
            winScreen.SetActive (false);
            looseScreen.SetActive (true);
        }
    }

}
