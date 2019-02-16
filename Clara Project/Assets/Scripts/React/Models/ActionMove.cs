using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMove : ActionHandler
{
    public ActionMove(Action action) : base(action)
    {
        //base.SetAction(Move);
    }

    public override void Call()
    {
        base.Call();
    }

    public override void OnCompleted()
    {
        base.OnCompleted();
        Debug.Log("COMPLETED OBSERVE!");
    }

    public override void OnNext(ActionModel actionModel)
    {
        Debug.Log("START NEXT!");
        base.OnNext(actionModel);
    }

    private void Move()
    {
        Debug.Log("I MOVE!!!");
    }

}
