using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public abstract class Bot : MonoBehaviour
{

    public BotStates botState = BotStates.Idle;
    public Animator animator;
    [SerializeField]
    public float Speed { get { return speed; } set { speed = value; } }

    //public delegate void OnBotAction();
    //public event OnBotAction botAction;

    private CharacterController character;
    private int currentWaypoint = 0;
    private float speed = 1.0f;
    private Vector3 nextPosition;
    private List<Node> nodes;

    // This is a stash 


    public virtual void Start()
    {
        nodes = new List<Node>();
        character = GetComponent<CharacterController>();
    }

    public virtual void Update()
    {
        if (this.nodes == null) return;

    }

    public void setState(BotStates botState)
    {
        this.botState = botState;
    }

    public virtual void Move(List<Node> nodes, GameObject selectedObject = null)
    {
        setState(BotStates.Walk);

        this.nodes = nodes;
        currentWaypoint = 0;

    }

    public virtual void StopMoving()
    {
        setState(BotStates.Idle);
        this.nodes.Clear();
        currentWaypoint = 0;
    }

    public Vector2 getPosition()
    {
        return new Vector2(transform.position.x, transform.position.z);
    }

    public virtual void Moving(Action<bool> onCompletion)
    {
        if (currentWaypoint < nodes.Count && botState == BotStates.Walk)
        {

            nextPosition = new Vector3(nodes[currentWaypoint].xIndex, 0f, nodes[currentWaypoint].yIndex);
            Vector3 moveDirection = nextPosition - transform.position;

            if (Vector3.Distance(transform.position, nextPosition) < 0.05f)
            {
                transform.position = nextPosition;
                currentWaypoint++;
            }
            else
            {
                moveDirection.y = 0f;

                transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(moveDirection),
                Time.deltaTime * 100);

                float moveSpeed = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed);

            }
        }
        else
        {
            StopMoving();

            if (onCompletion != null)
            {
                onCompletion(true);
            }
        }
    }

    private void Animation(BotStates animatonState, bool value, float speed = 1.0f)
    {
        if (animator == null)
        {
            Debug.LogError("BotAnimation Animation: animator not set!");
            return;
        }

        animator.SetBool(ExtensionMethods.ToStringEnums(animatonState), value);
        animator.SetFloat("speed", speed);
    }

    public virtual void StartAnimation(BotStates animatonState, float speed = 1.0f)
    {
        Animation(animatonState, true, speed);

    }


    public virtual void StopAnimation(BotStates animatonState)
    {
        Animation(animatonState, false);
    }


}
