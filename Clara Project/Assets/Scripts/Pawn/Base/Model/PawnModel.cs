using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PawnState
{
    Idle = 0,
    Walk,
    GoodOreder,
    BadOrder,
}

public class PawnModel : MonoBehaviour 
{
    public float moveSpeed = 3f;
    // Use this for initialization
    public float animationSpeed = 1.8f;
    public Animator animator;

    public PawnState state;

    public virtual void Start()
    {
        state = PawnState.Idle;
        animator = GetComponent<Animator>();
    }

}
