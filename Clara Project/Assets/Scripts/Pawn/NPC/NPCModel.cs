using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCModel : PawnModel
{
    public GameObject displayOrder;
    public UserOrder userOrder;
    public bool served = false;

    public override void Start()
    {
        base.Start();
        displayOrder.SetActive(false);

    }

    public Node DestinationNode()
    {
        return null;
    }
}
