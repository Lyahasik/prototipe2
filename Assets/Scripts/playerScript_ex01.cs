using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript_ex01 : MonoBehaviour
{
    [Range(1, 3)] public int NumberPlayer = 1;
    
    [Space] public float Speed = 10.0f;
    public float PowerJump = 15.0f;
            
    [Space] public GameObject Camera;
    
    [Space] public float HeightCast = 1.0f;
    public float WidthCast = 1.0f;
    
    [Space] public GameObject[] Players;
    public GameObject FinishObject;
    public float FinishLine = 0.1f;

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
        SettingIgnore();
    }

    void Update()
    {
        CheckFinish();
        InputKey();
        Move();
    }

    void CheckFinish()
    {
        if (NumberPlayer == 1)
        {
            if (Finish()
                && Players[1].GetComponent<playerScript_ex01>().Finish()
                && Players[2].GetComponent<playerScript_ex01>().Finish())
            {
                Debug.Log("Finished");
                if (SceneManager.GetActiveScene().name == "ex03")
                {
                    SceneManager.LoadScene("ex02");
                }
                if (SceneManager.GetActiveScene().name == "ex02")
                {
                    SceneManager.LoadScene("ex01");
                }
            }
        }
    }

    public bool Finish()
    {
        if (FinishObject.transform.position.x - transform.position.x < FinishLine
            && FinishObject.transform.position.x - transform.position.x > -FinishLine
            && FinishObject.transform.position.y - transform.position.y < FinishLine
            && FinishObject.transform.position.y - transform.position.y > -FinishLine)
        {
            return true;
        }

        return false;
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
 
            Debug.DrawLine(new Vector2(transform.position.x - WidthCast, transform.position.y - HeightCast), new Vector2(transform.position.x + WidthCast, transform.position.y - HeightCast), Color.red);
            if (Input.GetKeyDown("space")
                && Physics2D.LinecastAll(new Vector2(transform.position.x - WidthCast, transform.position.y - HeightCast),
                                        new Vector2(transform.position.x + WidthCast, transform.position.y - HeightCast)).Length > 0)
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

    void SettingIgnore()
    {
        GameObject[] IgnoreList1;
        GameObject[] IgnoreList2;

        if (NumberPlayer == 1)
        {
            IgnoreList1 = GameObject.FindGameObjectsWithTag("yellow");
            IgnoreList2 = (GameObject.FindGameObjectsWithTag("blue"));
        }
        else if (NumberPlayer == 2)
        {
            IgnoreList1 = GameObject.FindGameObjectsWithTag("red");
            IgnoreList2 = (GameObject.FindGameObjectsWithTag("blue"));
        }
        else
        {
            IgnoreList1 = GameObject.FindGameObjectsWithTag("red");
            IgnoreList2 = (GameObject.FindGameObjectsWithTag("yellow"));
        }
             
        foreach (GameObject obj in IgnoreList1)
        {
            Physics2D.IgnoreCollision(obj.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
             
        foreach (GameObject obj in IgnoreList2)
        {
            Physics2D.IgnoreCollision(obj.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
