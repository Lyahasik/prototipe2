using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformMove : MonoBehaviour
{
    public bool Vertical;

    public float Speed = 0.1f;
    public float Amplitude = 1.0f;
    public bool Rezus = true;

    private Vector3 _startPosition;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _startPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Vertical)
        {
            _rb.MovePosition(_rb.position + new Vector2(0, Speed * Time.deltaTime * ((Rezus) ? 1 : -1)));
            
            if (Mathf.Abs(_rb.position.y) < Mathf.Abs(_startPosition.y) + -Amplitude)
            {
                _rb.MovePosition(new Vector2(_rb.position.x, _startPosition.y + -Amplitude));
                Rezus = true;
            }
            if (Mathf.Abs(_rb.position.y) > Mathf.Abs(_startPosition.y) + Amplitude)
            {
                _rb.MovePosition(new Vector2(_rb.position.x, _startPosition.y + Amplitude));
                Rezus = false;
            }
        }
        else
        {
            if (Mathf.Abs(_rb.position.x) < Mathf.Abs(_startPosition.x) + -Amplitude)
            {
                _rb.MovePosition(new Vector2(_startPosition.x + -Amplitude, _rb.position.y));
                Rezus = true;
            }
            if (Mathf.Abs(_rb.position.x) > Mathf.Abs(_startPosition.x) + Amplitude)
            {
                _rb.MovePosition(new Vector2(_startPosition.x + Amplitude, _rb.position.y));
                Rezus = false;
            }
            
            _rb.MovePosition(_rb.position + new Vector2(Speed * Time.deltaTime * ((Rezus) ? 1 : -1), 0));
        }
    }
}
