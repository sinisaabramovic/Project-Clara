using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : PawnView
{

    public override void RotateTo(Vector3 who, Action<bool> onComplete)
    {
        base.RotateTo(who, onComplete);
    }

    public override void Start()
    {
        base.Start();

        GetComponent<PawnController>().SetAction("MoveToGet", (ActionData actionData, System.Action<bool> callback) =>
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

        GetComponent<PawnController>().SetAction("MoveToServe", (ActionData actionData, System.Action<bool> callback) =>
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
    }

    public override void StopMoving()
    {
        base.StopMoving();
    }

    public override IEnumerator Move(List<Node> path, Action<bool> onCompletion)
    {
        return base.Move(path, onCompletion);
    }

    public void CommitOrder(GameObject userObject, Action<Reward> onComplete)
    {
        if(userObject.GetComponent<NPCController>() == null)
        {
            if (onComplete != null)
                onComplete(null);

            return;
        }

        Reward reward = userObject.GetComponent<NPCController>().GetReward(GetComponent<PlayerModel>().inventory.GetInventoryComponents());
        GetComponent<PlayerModel>().inventory.CleanInventory();
        onComplete(reward);

    }

    public void GetComponentFromProp(GameObject propObject, int amount)
    {
        if (GetComponent<PlayerModel>().inventory != null)
        {

            GetComponent<PlayerModel>().inventory.Deposit(propObject.GetComponent<Prop>().WithdrawComponent(amount, (bool returnValue) => {
                if (returnValue)
                {
                    SuccessGet();
                }
                else
                {
                    FailGet("ERROR!");
                }
            }), null);
        }
    }

    public void SuccessGet()
    {
        Debug.Log("SUCCESS GETED!!!");

    }

    public void FailGet(string message)
    {
        Debug.Log("FAIL GET!!! " + message);
    }

}
