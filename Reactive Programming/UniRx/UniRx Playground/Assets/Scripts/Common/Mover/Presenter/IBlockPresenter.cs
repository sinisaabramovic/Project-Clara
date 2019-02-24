﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockPresenter
{
    void Initialize(IBlockView view);
    void UnInitialize();
    void HandleInput();
    void HandleBaseRotation(DDirections dDirections);
}


