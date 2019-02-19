using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

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

    void Start () {
        //ConformDoubleTap();

        //baseObject.transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }

    private void Update()
    {
        if(input == true)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                input = false;
                StartCoroutine(moveUp());
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

    //IEnumerator RoutineA()
    //{
    //    Debug.Log("CALLED!");
    //    yield return new WaitForSeconds(1);
    //    MoveMe();
    //}

    //private void ConformDoubleTap()
    //{
    //    var clickStream = Observable
    //        .EveryUpdate()
    //        .Where(_ => Input.GetMouseButtonDown(0));

    //    clickStream
    //        .Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
    //        .Where(xs => xs.Count >= 2)
    //        .Subscribe(xs => MoveInTime());
    //}

    //void MoveInTime()
    //{
    //     var cancel = Observable.FromCoroutine(RoutineA)
    //        .SelectMany(RoutineA)
    //        .Subscribe();

    //    cancel.Dispose();
    //}

    //private void MoveMe()
    //{
    //    Debug.Log("MOVE");
    //    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
    //}

    IEnumerator moveUp()
    {


        for (int i = 0; i < 90 / step; i++)
        {
            mover.transform.RotateAround(up.transform.position, Vector3.right, step);
            yield return new WaitForSeconds(speed * Time.deltaTime);
        }

        baseObject.transform.position = mover.transform.position;
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
}
