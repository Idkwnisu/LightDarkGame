using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float RayLenght;
    public float jumpForce;
    public float FallingGravity;
    

    private Rigidbody _rb;
    private CapsuleCollider _cc;
    private bool _grounded = false;
    private bool _justJumped = false;
    private float _gravity = 1;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cc = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 point1 = transform.position + _cc.center + Vector3.up * (_cc.height / 2 - _cc.radius);
        Vector3 point2 = point1 - Vector3.up * (_cc.height - 2 * _cc.radius);
        point1 += Vector3.up * RayLenght * 0.5f;
        point2 += Vector3.up * RayLenght * 0.5f;

        if (Physics.CapsuleCast(point1, point2, _cc.radius, transform.TransformDirection(Vector3.down), out hit, RayLenght) && !_justJumped)
        {
            _grounded = true;
        }
        if(_grounded)
        { 
        float _moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        _rb.AddForce(_moveX * transform.right);

            if (Input.GetButtonDown("Jump"))
            {
                _grounded = false;
                _justJumped = true;
                Invoke("EndJump", 0.5f);
                _rb.AddForce(Vector3.up * jumpForce);
            }
        }
        else
        {
            float _moveX = Input.GetAxis("Horizontal") * speed/5 * Time.deltaTime;
            _rb.AddForce(_moveX * transform.right);
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


        _rb.AddForce(Physics.gravity * Time.deltaTime * 100 * _gravity * (_rb.mass * _rb.mass));
    }

    public void EndJump()
    {
        _justJumped = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
