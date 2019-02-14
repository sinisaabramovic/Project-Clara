using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Controller : MonoBehaviour 
{
    public GraphController graphController;
    public Bot character;
    public Camera camera;

    public virtual void Awake () 
    {
        if (graphController == null) return;
        if (camera == null) camera = new Camera();
    }

    public virtual void Update () 
    {
		
	}
}
