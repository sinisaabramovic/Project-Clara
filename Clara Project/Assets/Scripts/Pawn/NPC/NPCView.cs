using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCView : PawnView
{
    private float timeToWaitForExitAnimation = 3.0f;

    public override IEnumerator Move(List<Node> path, Action<bool> onCompletion)
    {
        return base.Move(path, onCompletion);
    }

    public override void RotateTo(Vector3 who, Action<bool> onComplete)
    {
        base.RotateTo(who, onComplete);
    }

    public override void Start()
    {

        base.Start();

        GetComponent<NPCController>().SetAction("Move", (ActionData actionData, System.Action<bool> callback) =>
        {
            StartCoroutine(Move(actionData.getPathData(), (bool returnValue) => {
                if (returnValue)
                {
                    if (callback != null)
                    {
                        callback(returnValue);
                    }
                }

            }));
        });

        GetComponent<NPCController>().SetAction("TakeOrder", (ActionData actionData, System.Action<bool> callback) =>
        {
            enableCollider();
            DisplayOrdersInfo(true);

            if (callback != null)
            {
                callback(true);
            }

        });

        GetComponent<NPCController>().SetAction("RotateToOrder", (ActionData actionData, System.Action<bool> callback) =>
        {
            Vector3 lookTo = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
            RotateTo(lookTo, null);

            if (callback != null)
            {
                callback(true);
            }
        });

        GetComponent<NPCController>().SetAction("StartWaiting", (ActionData actionData, System.Action<bool> callback) =>
        {
            StartCoroutine(GetComponent<Timer>().StartCountdown((bool returnVal) =>
            {

                disableCollider();
                StopAnimation();
                StartAnimation(PawnState.BadOrder);

                DisplayOrdersInfo(false);

                StartCoroutine(GetComponent<Timer>().StartCountdown((bool retObj) =>
                {
                    if(callback != null)
                    {
                        callback(true);
                    }

                    StartAnimation(PawnState.Walk);

                }, timeToWaitForExitAnimation));

            }, actionData.getTimeout()));


        });

    }

    public override void StartAnimation(PawnState animatonState, float speed = 1)
    {
        base.StartAnimation(animatonState, speed);
    }

    public override void StopAnimation()
    {
        base.StopAnimation();
    }

    public override void StopMoving()
    {
        base.StopMoving();
    }


    public void enableCollider()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    public void disableCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    public Reward GetReward(List<Component> recieved)
    {
        Reward reward = GetComponent<NPCModel>().userOrder.CheckOrder(recieved);
        GetComponent<NPCModel>().served = reward.GetResult();
        return reward;
    }

    public void DisplayOrdersInfo(bool value)
    {
        GetComponent<NPCModel>().displayOrder.SetActive(value);
    }
}
