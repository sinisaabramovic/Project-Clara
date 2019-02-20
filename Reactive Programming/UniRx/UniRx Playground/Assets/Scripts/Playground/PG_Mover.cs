using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public enum Directions
{
    Up,
    Down,
    South,
    North,
    Top,
    Neutral
}

public class PG_Mover : MonoBehaviour {

    // Use this for initialization
    private Vector3 offset;
    public GameObject mover;
    public GameObject baseObject;

    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;

    public int step = 4;

    public float speed = 0.01f;

    bool input = true;
    private Directions directions = Directions.Neutral;
    //public bool canExecute = false;

    void Start () {
        //ConformDoubleTap();

        //baseObject.transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }

    private void Update()
    {

        //if (directions == Directions.Neutral)
        //{
        //    baseObject.transform.localEulerAngles = setBaseObjectRotationNeutral();
        //}

        if (input == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                input = false;
                StartCoroutine(moveUp(() => {
                    if (directions == Directions.Up)
                    {
                        baseObject.transform.localEulerAngles = setBaseObjectRotationUp();
                        Debug.Log("SETS!");
                    }else if(directions == Directions.Neutral)
                    {
                        baseObject.transform.localEulerAngles = setBaseObjectRotationNeutral();
                    }
                }));
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                input = false;
                StartCoroutine(moveDown());
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                input = false;
                StartCoroutine(moveLeft());
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                input = false;
                StartCoroutine(moveRight());
            }
        }
    }

    IEnumerator moveUp(Action onComplete)
    {    
        for (int i = 0; i < 90 / step; i++)
        {
            mover.transform.RotateAround(up.transform.position, Vector3.right, step);
            yield return new WaitForSeconds(speed * Time.deltaTime);
        }

        baseObject.transform.position = mover.transform.position;

        onComplete();

        input = true;
    }

    IEnumerator moveDown()
    {
        for (int i = 0; i < 90 / step; i++)
        {
            mover.transform.RotateAround(down.transform.position, Vector3.left, step);
            yield return new WaitForSeconds(speed * Time.deltaTime);
        }

        baseObject.transform.position = mover.transform.position;
        input = true;
    }

    IEnumerator moveLeft()
    {
        for (int i = 0; i < 90 / step; i++)
        {
            mover.transform.RotateAround(left.transform.position, Vector3.forward, step);
            yield return new WaitForSeconds(speed * Time.deltaTime);
        }

        baseObject.transform.position = mover.transform.position;
        input = true;
    }

    IEnumerator moveRight()
    {
        for (int i = 0; i < 90 / step; i++)
        {
            mover.transform.RotateAround(right.transform.position, Vector3.back, step);
            yield return new WaitForSeconds(speed * Time.deltaTime);
        }

        baseObject.transform.position = mover.transform.position;
        input = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.name == "South")
        {
            Debug.Log("TRIGGER ENTER!");
            Debug.Log(other.gameObject.name);
            directions = Directions.Up;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "South")
        {
            Debug.Log("TRIGGER EXIT!");
            Debug.Log(other.gameObject.name);
            directions = Directions.Neutral;
        }
    }

    private Vector3 setBaseObjectRotationUp()
    {
        return new Vector3(-90, 0, 0);
    }

    private Vector3 setBaseObjectRotationNeutral()
    {
        return new Vector3(0, 0, 0);
    }
}
