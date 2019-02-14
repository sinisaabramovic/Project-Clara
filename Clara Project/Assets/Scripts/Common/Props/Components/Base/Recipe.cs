using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Recipe : ScriptableObject
{

    public Dictionary<Component, int> recipe = new Dictionary<Component, int>();

    public virtual void addCompound(int amount, Component component)
    {
        recipe.Add(component, amount);
    }

    public virtual void DebugPrintRecipe()
    {
        throw new System.NotImplementedException();
    }

    public virtual void MakeRecipe()
    {
        throw new System.NotImplementedException();
    }
}
