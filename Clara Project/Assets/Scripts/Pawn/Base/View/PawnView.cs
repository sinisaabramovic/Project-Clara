using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnView : MonoBehaviour
{

    private int currentWaypoint = 0;
    private Vector3 nextPosition;
    private float minObjectsDistance = 0.05f;

    private PawnState mementoAnimationState = PawnState.Idle;

    public Vector3 NextPosition { get { return nextPosition; } set { nextPosition = value; } }

    public float MinObjectsDistance { get { return minObjectsDistance; } set { minObjectsDistance = value; } }

    public int CurrentWaypoint { get { return currentWaypoint; } set { currentWaypoint = value; } }

    public PawnState MementoAnimationState { get { return mementoAnimationState; } set { mementoAnimationState = value; } }

    public virtual void Start () 
    {         

    }

    public virtual void RotateTo(Vector3 who, Action<bool> onComplete)
    {
        Vector3 lookDir = who - transform.position;

        lookDir.y = 0f;

        transform.rotation = Quaternion.Slerp(
        transform.rotation,
        Quaternion.LookRotation(lookDir),
        Time.deltaTime * 100);

        if (onComplete != null)
        {
            onComplete(true);
        }

    }

    public virtual IEnumerator Move(List<Node> path, Action<bool> onCompletion)
    {
        if (path == null)
        {
            Debug.Log("ERROR IN Move : Path is null!");
            if (onCompletion != null)
            {
                onCompletion(false);
            }
            yield return null;
        }

        StartAnimation(PawnState.Walk, GetComponent<PawnModel>().animationSpeed);
        GetComponent<PawnModel>().state = PawnState.Walk;
        while (CurrentWaypoint < path.Count)
        {
            NextPosition = new Vector3(path[CurrentWaypoint].xIndex, 0f, path[CurrentWaypoint].yIndex);
            Vector3 moveDirection = NextPosition - transform.position;

            if (Vector3.Distance(transform.position, NextPosition) < MinObjectsDistance)
            {
                transform.position = NextPosition;
                CurrentWaypoint++;
            }
            else
            {
                moveDirection.y = 0f;

                Rotate(moveDirection);

                float moveSpeedTemp = GetComponent<PawnModel>().moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, NextPosition, moveSpeedTemp);

            }

            yield return new WaitForFixedUpdate();

        }

        if (onCompletion != null)
        {
            StopMoving();
            onCompletion(true);
        }

    }

    public virtual void StopMoving()
    {
        GetComponent<PawnModel>().state = PawnState.Idle;
        currentWaypoint = 0;
        StopAnimation();
    }

    private void Rotate(Vector3 moveDirection)
    {
        transform.rotation = Quaternion.Slerp(
        transform.rotation,
        Quaternion.LookRotation(moveDirection),
        Time.deltaTime * 100);
    }

    private void Animation(PawnState animatonState, bool value, float speed = 1.0f)
    {
        if (GetComponent<PawnModel>().animator == null)
        {
            Debug.LogError("BotAnimation Animation: animator not set!");
            return;
        }

        GetComponent<PawnModel>().animator.SetBool(ExtensionMethods.ToStringEnums(animatonState), value);
        GetComponent<PawnModel>().animator.SetFloat("speed", speed);
    }

    public virtual void StartAnimation(PawnState animatonState, float speed = 1.0f)
    {
        Animation(animatonState, true, speed);
        MementoAnimationState = animatonState;
    }


    public virtual void StopAnimation()
    {
        Animation(MementoAnimationState, false);
    }
}
