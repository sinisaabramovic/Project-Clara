using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Beam", menuName = "Inventory/Beam", order = 1)]

public class Component_Beam : Component 
{

    public Component_Beam()
    {
        base.name = "BEAM!";
        base.moneyCost = 10;
    }

}
