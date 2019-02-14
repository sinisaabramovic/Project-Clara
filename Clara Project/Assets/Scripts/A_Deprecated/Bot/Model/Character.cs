using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Bot
{
    private GameObject selectedObject;
    public float characterMoveSpeed = 5.0f;

    private Inventory inventory;

    public override void Update()
    {
        base.Update();

        switch (this.botState)
        {
            case BotStates.Idle:
                break;

            case BotStates.Walk:
                Moving((bool returnValue) =>
                {
                    setState(BotStates.Idle);
                    StopAnimation(BotStates.Walk);
                    RotateTo(this.selectedObject);
                });
                break;
        }
    }

    public override void Start()
    {
        base.Start();

        Speed = this.characterMoveSpeed;
        this.inventory = GetComponent<Inventory>();
    }

    public override void Move(List<Node> nodes, GameObject selectedObject)
    {
        base.Move(nodes, selectedObject);
        this.selectedObject = selectedObject;
    }

    private void RotateTo(GameObject directedObject)
    {
        if (directedObject == null)
        {
            return;
        }

        if (directedObject.GetComponent<Prop>() == null)
        {
            return;
        }

        directedObject.GetComponent<Prop>().Rotate(this.gameObject, (bool returnValue) =>
        {
        
            if (this.inventory != null)
            {
            
                inventory.Deposit(directedObject.GetComponent<Prop>().WithdrawComponent(15, (bool obj) => { 
                    if (obj)
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
        );

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
