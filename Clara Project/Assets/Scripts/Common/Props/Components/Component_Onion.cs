using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Onion", menuName = "Inventory/Onion", order = 1)]

public class Component_Onion : Component 
{

    public Component_Onion()
    {
        base.name = "ONION";
        base.moneyCost = 10;
    }

}
