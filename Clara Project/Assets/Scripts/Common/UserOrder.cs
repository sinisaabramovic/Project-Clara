using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserOrder 
{

    // Use this for initialization
    List<Component> orderList = new List<Component>();
    public Reward reward;

    public GameObject dialogScreen;

    public UserOrder(Reward reward)
    {
        this.reward = reward;
    }

    public virtual void AddToOrder(Component component)
    {
        orderList.Add(component);
    }

    public virtual List<Component> OrdersInfo()
    {
        return this.orderList;
    }

    public virtual void DisplayOrder()
    {
        foreach(Component component in orderList)
        {
            Debug.Log("Component: " + component.name);
        }
    }

    public virtual Reward CheckOrder(List<Component> components)
    {
        if(components == null)
        {
            reward.SetResult(false);
            return reward;
        }

        if(components.Count != orderList.Count)
        {
            reward.SetResult(false);
            return reward;
        }

        int numberOfMatches = 0;
        int idx = 0;

        // Ovaj while je u slucaju ako zelim da se i redoslijed podudara!!!
        //while (idx < components.Count && idx < orderList.Count)
        //{
        //    if (components[idx].GetType() == orderList[idx].GetType())
        //    {
        //        numberOfMatches++;
        //        idx++;
        //    }
        //    else
        //    {
        //        break;
        //    }
        //}

        foreach(Component componentOrder in orderList)
        {
            foreach(Component componentSent in components)
            {
                if(componentSent.GetType() == componentOrder.GetType())
                {
                    numberOfMatches++;
                }
            }
        }

        if (numberOfMatches == orderList.Count)
        {

            reward.SetResult(true);
            return reward;
        }
        else
        {
            reward.SetResult(false);
            return reward;
        }
    }

}
