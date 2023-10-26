using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player_move : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _speed;
    [SerializeField] float _jump;
    private Rigidbody _rb;
    private bool _isGround;
    void Start()
    {   
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        move_player();
        move_jump();
        //rotation_player();
    }
    void move_player()
    {
        if ( _isGround)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            _rb.AddForce(new Vector3(h, 0, v) * _speed, ForceMode.VelocityChange);
        }
    }

    void rotation_player()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(
                0,
                Input.mousePosition.x, 
                0);
        }
    }
    void move_jump()
    {
        if (_isGround && (Input.GetAxis("Jump") > 0))
        {
            _rb.AddForce(Vector3.up * _jump,ForceMode.VelocityChange);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGround = value;
        }
    }
}
