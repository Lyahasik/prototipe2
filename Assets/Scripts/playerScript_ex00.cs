using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour
{
    [Range(1, 3)]
    public int NumberPlayer = 1;
    [Space]
    
    public float Speed = 10.0f;
    public float PowerJump = 15.0f;
    [Space]
    
    public GameObject Camera;

    public float HeightCast = 1.0f;

    private Rigidbody2D _rigidbody2D;
    
    private Vector3 _startPosition;
    private bool _move = false;

    private Vector3 _vectorMove;
    
    void Start()
    {
        _vectorMove = Vector3.zero;
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        SwitchPlayer(1);
    }

    void Update()
    {
        InputKey();
        Move();
    }

    void Move()
    {
        if (_move)
        {
            _rigidbody2D.AddForce(_vectorMove, ForceMode2D.Force);
            Camera.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.transform.position.z);
            
            _vectorMove = Vector3.zero;
        }
    }

    void InputKey()
    {
        if (Input.GetKeyDown("r"))
        {
            transform.position = _startPosition;
            SwitchPlayer(1);
        }
        
        if (Input.GetKeyDown("1"))
        {
            SwitchPlayer(1);
        }
        if (Input.GetKeyDown("2"))
        {
            SwitchPlayer(2);
        }
        if (Input.GetKeyDown("3"))
        {
            SwitchPlayer(3);
        }

        if (_move)
        {
            if (Input.GetKey("left"))
            {
                _vectorMove += new Vector3(-Speed, 0, 0);
            }
            if (Input.GetKey("right"))
            {
                _vectorMove += new Vector3(Speed, 0, 0);
            }
            
            if (Input.GetKeyDown("space")
                && Physics2D.LinecastAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - HeightCast)).Length > 1)
            {
                _rigidbody2D.AddForce(new Vector2(0, PowerJump), ForceMode2D.Impulse);
            }
        }
    }

    void SwitchPlayer(int number)
    {
        if (NumberPlayer == number)
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _move = true;
        }
        else
        {
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            _move = false;
        }
    }
}
