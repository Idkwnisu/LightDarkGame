using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float RayLenght;
    public float AnimationRayLenght;
    public float jumpForce;
    public float FallingGravity;
    public Transform feet;
    public Transform feetMesh;

    private Transform feetMeshNoJump;
    

    private Rigidbody _rb;
    private CapsuleCollider _cc;
    public bool _grounded = false;
    private bool _justJumped = false;
    private bool _doubleJump = false;
    private bool _blockAnimation = false;
    private float _gravity = 1;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cc = GetComponent<CapsuleCollider>();
        _animator = GetComponent<Animator>();
        feetMeshNoJump = feetMesh;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 point1 = transform.position + _cc.center + Vector3.up * (_cc.height / 2 - _cc.radius);
        Vector3 point2 = point1 - Vector3.up * (_cc.height - 2 * _cc.radius);
        point1 += Vector3.up * RayLenght * 0.5f;
        point2 += Vector3.up * RayLenght * 0.5f;
        float _moveX;
        if (Physics.CapsuleCast(point1, point2, _cc.radius, transform.TransformDirection(Vector3.down), out hit, RayLenght) && !_blockAnimation)
        {
            _grounded = true;
            if (_animator.GetBool("Jumping") && !_blockAnimation)
            {
                _justJumped = false;
                _doubleJump = false;
                _animator.SetBool("Jumping", false);
                _animator.SetBool("DoubleJump", false);
            }
        }
        point1 = transform.position + _cc.center + Vector3.up * (_cc.height / 2 - _cc.radius);
        point2 = point1 - Vector3.up * (_cc.height - 2 * _cc.radius);
        point1 += Vector3.up * AnimationRayLenght * 0.5f;
        point2 += Vector3.up * AnimationRayLenght * 0.5f;
        if (Physics.CapsuleCast(point1, point2, _cc.radius, transform.TransformDirection(Vector3.down), out hit, AnimationRayLenght))
        {
            if (_animator.GetBool("Jumping") && !_blockAnimation)
            {
                _justJumped = false;
                _doubleJump = false;
                _animator.SetBool("Jumping", false);
                _animator.SetBool("DoubleJump", false);
            }
        }
        if(_grounded || _justJumped)
        {
        _moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _rb.AddForce(_moveX * Vector3.right);

            if (Input.GetButtonDown("Jump"))
            {
                _grounded = false;
                if (_justJumped)
                {
                    _doubleJump = true;
                    _justJumped = false;
                    _animator.SetBool("DoubleJump", true);
                }
                else
                {
                    _justJumped = true;
                    _blockAnimation = true;
                    Invoke("EndJump", 0.5f);
                    _animator.SetBool("Jumping", true);
                }
                _rb.velocity = Vector3.zero + Vector3.right * _rb.velocity.x;
                _rb.AddForce(Vector3.up * jumpForce);
            }
            
            transform.LookAt(transform.position + Vector3.right*_moveX);
        }
        else
        {
             _animator.SetBool("DoubleJump", false);


            _moveX = Input.GetAxis("Horizontal") * speed/5 * Time.deltaTime;
            _rb.AddForce(_moveX * Vector3.right);
        }
        if (_rb.velocity.y < -.5f)
        {
            _gravity = FallingGravity;
        }
        else
        {
            _gravity = 1f;
        }
        if (_rb.velocity.x > maxSpeed)
        {
            _rb.velocity = new Vector3(maxSpeed, _rb.velocity.y, 0);
        }
        else if (_rb.velocity.x < (-1) * maxSpeed)
        {
            _rb.velocity = new Vector3((-1) * maxSpeed, _rb.velocity.y, 0);
        }
        
        if(Mathf.Abs(_moveX) > 0.5f)
        {
           _animator.SetBool("Running", true);
        }
        else
        {
            _animator.SetBool("Running", false);
        }

        _rb.AddForce(Physics.gravity * Time.deltaTime * 100 * _gravity * (_rb.mass * _rb.mass));

        if(_moveX == 0)
        {
            if(Mathf.Abs(_rb.velocity.x) > 0.1f)
            {
                _rb.velocity = new Vector3(_rb.velocity.x / 3.0f, _rb.velocity.y, _rb.velocity.z);
            }
        }
    }

    public void EndJump()
    {
        _blockAnimation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
