using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Actions : MonoBehaviour 
{

    public Dictionary<string, List<Action<ActionData, Action<bool>>>> actionData = new Dictionary<string, List<Action<ActionData, Action<bool>>>>();

    public void SetAction(string actionKey, Action<ActionData, Action<bool>> action)
    {
        if(!actionData.ContainsKey(actionKey))
        {
            actionData.Add(actionKey, new List<Action<ActionData, Action<bool>>>());
        }

        actionData[actionKey].Add(action);
    }

    public void CallAction(string actionKey, ActionData data, Action<bool> action)
    {
        if (actionData.ContainsKey(actionKey))
        {
            for(int i = 0; i < actionData[actionKey].Count; i++)
            {
                actionData[actionKey][i].Invoke(data, action);
            }
        }
    }


}
