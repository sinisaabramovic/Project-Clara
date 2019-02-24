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
            StartCoroutine(_view.Move(Vector3.right, EnableInput));

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            _view.DisableInput();
            StartCoroutine(_view.Move(Vector3.left, EnableInput));

        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            _view.DisableInput();
            StartCoroutine(_view.Move(Vector3.forward, EnableInput));

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            _view.DisableInput();
            StartCoroutine(_view.Move(Vector3.back, EnableInput));

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

    private void EnableInput()
    {

        this._view.EnableInput();
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

}
