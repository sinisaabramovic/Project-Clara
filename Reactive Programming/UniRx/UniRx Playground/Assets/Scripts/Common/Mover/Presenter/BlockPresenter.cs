using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPresenter : MonoBehaviour, IBlockPresenter
{
    private IBlockView _view;

    public void Initialize(IBlockView view)
    {
        this._view = view;
        this._view.EnableInput();
    }

    public void UnInitialize()
    {
    }

    public void Update()
    {
        if (!_view.isInputEnabled()) return;

        if(Input.GetKey(KeyCode.UpArrow))
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

    private void EnableInput()
    {
        this._view.EnableInput();
    }
}
