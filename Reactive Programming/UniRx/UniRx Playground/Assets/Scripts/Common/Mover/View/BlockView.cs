using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockView : MonoBehaviour, IBlockView {

    // Use this for initialization
    private BlockPresenter _presenter;
    private BlockModel _model;
    private bool _isInputEnabled = false;

    public void Initialize()
    {
        _presenter = GetComponent<BlockPresenter>();
        _presenter.Initialize(this);
        _model = new BlockModel(this.gameObject, 6, 0.01f);
    }

    void Awake ()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        _presenter.UnInitialize();
    }

    public void EnableInput()
    {
        this._isInputEnabled = true;
    }

    public void DisableInput()
    {
        this._isInputEnabled = false;
    }

    public bool isInputEnabled()
    {
        return this._isInputEnabled;
    }

    public IEnumerator Move(Vector3 byVector, Action onComplete)
    {
        for (int i = 0; i < 90 / _model.Step; i++)
        {
            _model.Rotate(byVector);
            yield return new WaitForSeconds(_model.Speed * Time.deltaTime);
        }

        _model.ResetPivotPoint();

        if (onComplete != null)
        {
            onComplete();
        }

    }


}
