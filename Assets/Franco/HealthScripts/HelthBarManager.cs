using UnityEngine;
using UnityEngine.UI;

public class HelthBarManager : MonoBehaviour
{
    
     
    Slider _sliderHealthBar;

    Camera _camara;

    HealthManager _healthManager;

    int _halthbarValue;

    void Start()
    {
        _camara = Camera.main; 
        _sliderHealthBar = GetComponentInChildren<Slider>();
        _healthManager = GetComponentInParent<HealthManager>();
    }

    void Update()
    {
       
       transform.LookAt(_camara.transform);
       _sliderHealthBar.value = _healthManager.Health/100f;
    }
}
