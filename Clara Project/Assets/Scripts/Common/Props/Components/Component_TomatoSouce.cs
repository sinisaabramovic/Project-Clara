using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TomatoSouce", menuName = "Inventory/TomatoSouce", order = 1)]

public class Component_TomatoSouce : Component 
{

    public Component_TomatoSouce()
    {
        base.name = "TOMATO SOUCE!";
        base.moneyCost = 10;
    }

}
