using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBot : Bot 
{

    public float characterMoveSpeed = 5.0f;
    public override void Move(List<Node> nodes, GameObject selectedObject)
    {
        base.Move(nodes, null);
    }

    public override void StopMoving()
    {
        base.StopMoving();
    }

    public override void Update()
    {
        base.Update();

        switch (this.botState) {
            case BotStates.Idle:
                break;

            case BotStates.Walk:
                Moving((bool returnValue) => {
                    setState(BotStates.Idle);
                    StopAnimation(BotStates.Walk);
                    RotateTo(null);
                });
                break;
        }
    }

    public override void Start()
    {
        base.Start();
        Speed = this.characterMoveSpeed;
    }

    public override void Moving(Action<bool> onCompletion)
    {
        base.Moving(onCompletion);
    }

    public override void StartAnimation(BotStates animatonState, float speed = 1)
    {
        base.StartAnimation(animatonState, speed);
    }

    public override void StopAnimation(BotStates animatonState)
    {
        base.StopAnimation(animatonState);
    }

    private void RotateTo(GameObject directedObject)
    {
        if (directedObject == null)
        {
            return;
        }
    }

}
