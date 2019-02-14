using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Prop_FungiStash : Prop
{
    public override void DepositComponent(Component component)
    {
        base.DepositComponent(component);
    }

    public override List<Component> WithdrawComponent(int amount, Action<bool> onComplete)
    {
        return base.WithdrawComponent(amount, onComplete);
    }
}
