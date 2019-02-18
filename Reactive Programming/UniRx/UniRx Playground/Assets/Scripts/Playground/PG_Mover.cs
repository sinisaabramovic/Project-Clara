using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PG_Mover : MonoBehaviour {

    // Use this for initialization
    void Start () {
        ConformDoubleTap();
    }

    IEnumerator RoutineA()
    {
        Debug.Log("CALLED!");
        yield return new WaitForSeconds(1);
        MoveMe();
    }

    private void ConformDoubleTap()
    {
        var clickStream = Observable
            .EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0));

        clickStream
            .Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
            .Where(xs => xs.Count >= 2)
            .Subscribe(xs => MoveInTime());
    }

    void MoveInTime()
    {
         var cancel = Observable.FromCoroutine(RoutineA)
            .SelectMany(RoutineA)
            .Subscribe();

        cancel.Dispose();
    }

    private void MoveMe()
    {
        Debug.Log("MOVE");
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
    }
}
