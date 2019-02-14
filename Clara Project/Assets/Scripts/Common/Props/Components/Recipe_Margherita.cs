using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pizza-Margherita", menuName = "Recipe/Margherita", order = 1)]

public class Recipe_Margherita : Recipe
{
    public override void addCompound(int amount, Component component)
    {
        base.addCompound(amount, component);
    }

    public override void DebugPrintRecipe()
    {
        base.DebugPrintRecipe();
    }

    public override void MakeRecipe()
    {
        base.MakeRecipe();
    }
}
