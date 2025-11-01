using UnityEngine;
public class PlayerMovementV2 : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] float _speedSideways = 5f;
    [SerializeField] float _rotationSpeed = 100f;
    Rigidbody rb;
    float _moveH, _moveV; Vector3 _movement;
    Vector3 _moveDirection; Vector3 _moveSideways;
    float _rotationAcount; Quaternion _turnOffset;
    private Animator _animator;

    private bool useGun = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        _moveH = Input.GetAxis("Horizontal");
        _moveV = Input.GetAxis("Vertical");
        _moveDirection = transform.forward * _moveV * _speed * Time.deltaTime;

        _moveSideways = transform.right * _moveH * _speedSideways * Time.deltaTime;
        
       // _moveSideways = Vector3.zero;
       // _rotationAcount = _moveH * _rotationSpeed * Time.deltaTime;
       // _turnOffset = Quaternion.Euler(0, _rotationAcount, 0);
       // rb.MoveRotation(rb.rotation * _turnOffset);
      
        _movement = rb.position + _moveDirection + _moveSideways;
        rb.MovePosition(_movement);
        _animator.SetFloat("VelX", _moveH);
        _animator.SetFloat("VelY", _moveV);
        //Esto controla la animacion de disparo!!!!
        if (Input.GetMouseButtonDown(0))
        {
            useGun = true;
            _animator.SetLayerWeight(1, 1);
            _animator.SetTrigger("Shoot"); // lanza la animaci�n de disparo
        }
    }
}
 