using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public List<Component> components = new List<Component>();

    public void CleanInventory()
    {
        components.Clear();
    }

    public List<Component> GetInventoryComponents()
    {
        return components;
    }

    public void Deposit(Component component, Action<bool> onComplete)
    {
        if (components == null || component == null)
        {
            if (onComplete != null)
            {
                onComplete(false);
            }
            return;
        }

        this.components.Add(component);

        if (onComplete != null)
        {
            onComplete(true);
        }
    }

    public void Deposit(List<Component> depositComponents, Action<bool> onComplete)
    {
        if (components == null || depositComponents == null || depositComponents.Count == 0)
        {
            if (onComplete != null)
            {
                onComplete(false);
            }
            return;
        }

        foreach(Component component in depositComponents)
        {
            components.Add(component);
        }

        if (onComplete != null)
        {
            onComplete(true);
        }
    }

    public List<Component> Withdraw(Component component, int amount, Action<bool> onComplete)
    {

        if ((this.components == null || this.components.Count == 0))
        {
            if (onComplete != null)
            {
                onComplete(false);
            }

            return null;
        }

        int componentsCount = getComponentsCount(component);

        if (componentsCount < amount)
        {
            if (onComplete != null)
            {
                onComplete(false);
            }

            return null;
        }

        int tempCounter = 0;
        int withdrawCounter = 0;

        List<Component> withDrawList = new List<Component>();

        while (tempCounter <= components.Count && withdrawCounter < amount)
        {
            if (components[tempCounter].GetType() == component.GetType())
            {
                withDrawList.Add(components[tempCounter]);
                withdrawCounter++;
            }
            tempCounter++;
        }

        foreach (Component forcomponent in withDrawList)
        {
            components.Remove(forcomponent);
        }

        if (onComplete != null)
        {
            onComplete(true);
        }

        return withDrawList;
    }

    public int getComponentsCount(Component forComponent)
    {

        return FilterCount(forComponent);
    }

    private int FilterCount(Component type)
    {
        List<Component> displayList = (from item in components where item.GetType() == type.GetType() select item).ToList();
        return displayList.Count;
    }
       
}
