using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBlockView
{
    void Initialize();
    IEnumerator Move(Vector3 byVector, Action onComplete);
    void EnableInput();
    void DisableInput();
    bool isInputEnabled();
}


