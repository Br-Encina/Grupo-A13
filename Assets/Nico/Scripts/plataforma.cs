using UnityEngine;

public class plataforma : MonoBehaviour
{
    [SerializeField] private Transform passengerAnchor;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(passengerAnchor);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}
