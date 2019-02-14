using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fungi", menuName = "Inventory/Fungi", order = 1)]

public class Component_Fungi : Component {

    public Component_Fungi()
    {
        base.name = "Fungi";
        base.moneyCost = 5;
    }
}
