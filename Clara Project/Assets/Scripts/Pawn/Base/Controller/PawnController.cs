using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : Actions 
{
    // Update is called once per frame
    public GraphController graphController;
    public Camera camera;
    public float countDownTimeInSeconds;


    public virtual void Update()
    {
        if (graphController == null)
        {
            Debug.Log("ERROR in PawnController: graphController not set!");
            return;
        }
    }

    public virtual Node GetNode(GameObject forObject)
    {
        if(forObject.GetComponent<Prop>() != null)
        {
            return forObject.GetComponent<Prop>().getNode();
        }

        if (forObject.GetComponent<NPCController>() != null)
        {
            return forObject.GetComponent<NPCController>().positionToServe();
        }

        return null;
    }
}
