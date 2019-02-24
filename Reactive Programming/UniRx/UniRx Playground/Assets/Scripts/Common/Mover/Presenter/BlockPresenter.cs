using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Async;
using UniRx.Triggers;
using System;

public class BlockPresenter : MonoBehaviour, IBlockPresenter
{
    private IBlockView _view;
    private IObservable<bool> _movement;
    private IObservable<bool> _trigger;

    public IObservable<bool> Movement { get { return _movement; } private set { _movement = value; } }
    public IObservable<bool> Trigger { get { return _trigger; } private set { _trigger = value; } }

    public void HandleBaseRotation(DDirections dDirections)
    {
        this._view.RotateBase(dDirections);
    }

    public void HandleInput()
    {

        if (!_view.isInputEnabled()) return;

        if (Input.GetKey(KeyCode.UpArrow))
        {

            _view.DisableInput();
            StartCoroutine(_view.Move(Vector3.right, ()=> { HandleOnMove(Vector3.right); }));

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            _view.DisableInput();
            StartCoroutine(_view.Move(Vector3.left, () => { HandleOnMove(Vector3.left); }));

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            _view.DisableInput();
            StartCoroutine(_view.Move(Vector3.forward, () => { HandleOnMove(Vector3.forward); }));

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            _view.DisableInput();
            StartCoroutine(_view.Move(Vector3.back, () => { HandleOnMove(Vector3.back); }));

        }
    }

    public void Initialize(IBlockView view)
    {

        this._view = view;

        SetupInput();
        SetupTriggers();
    }

    public void UnInitialize()
    {
    }

    private void SetupInput()
    {

        this._view.EnableInput();

        // Rx Setup, subscribe on Update
        Movement = this.UpdateAsObservable().Select(_ =>
        {
            HandleInput();
            return true;
        });

        Movement.Subscribe(inputMovement => { }).AddTo(this);
    }

    private void SetupTriggers()
    {

        Trigger = (UniRx.IObservable<bool>)this.OnTriggerEnterAsObservable()
            .Where(component => component.GetComponent<BuildingBlockSide>() != null)
            .Select(component => {
                HandleBaseRotation(component.GetComponent<BuildingBlockSide>().sideDirection);
                return true;
            });


        Trigger.Subscribe(triggerEnter => { }).AddTo(this);
    }

    #region private methods
    private void HandleOnMove(Vector3 moverDirections)
    {
        //StartCoroutine(_view.Move(Vector3.right, null));
        this._view.EnableInput();
        Seeker(moverDirections);
    }

    // TODO - treba vidjeti kako napraviti seeker koji ce pretrazivati sljedeci blok
    private void Seeker(Vector3 moverDirections)
    {

        GameObject[] blocks;
        blocks = GameObject.FindGameObjectsWithTag("Block");
        Vector3 nextPosition = _view.getPosition();
        nextPosition += moverDirections;

        Debug.Log(_view.getPosition());
        Debug.Log(nextPosition);
    }
    #endregion
}
