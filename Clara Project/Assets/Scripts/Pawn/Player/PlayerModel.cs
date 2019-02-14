using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : PawnModel
{

    public Inventory inventory;

    public override void Start()
    {
        base.Start();
        this.inventory = GetComponent<Inventory>();
    }
}
