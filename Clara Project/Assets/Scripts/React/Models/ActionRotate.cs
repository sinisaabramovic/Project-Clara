using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotate : ActionHandler
{
    public ActionRotate(Action action) : base(action)
    {
    }

    public override void Call()
    {
        Rotate();
    }

    public override void OnCompleted()
    {
        base.OnCompleted();
        Debug.Log("Rotate completed!!!");
    }

    public override void OnNext(ActionModel actionModel)
    {
        base.OnNext(actionModel);
        Debug.Log("NEXT ROTATE!");
    }

    // Use this for initialization
    private void Rotate () {
        Debug.Log("I ROTATE!!!");
	}
	
	
}
