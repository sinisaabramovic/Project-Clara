using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TestReact : MonoBehaviour {

    ActionHandlerManager provider = new ActionHandlerManager();
    Dictionary<Action<ActionModel>, IEnumerator> corutines = new Dictionary<Action<ActionModel>, IEnumerator>();
    List<IEnumerator> corutinesList = new List<IEnumerator>();
    ActionHandler[] actionHandlers;

    ActionMove move;
    ActionModel drek = new ActionModel();
    private delegate void someDumpAction(ActionModel action);
    // Use this for initialization
    void Start () {

        drek.count = 10;
        drek.name = "Drek!";

        move = GetComponent<ActionMove>();
        //actionHandlers = GetComponents<ActionHandler>();

        move.addAction(HandleAction1).addAction(HandleAction2).addAction(HandleAction3);

        corutines.Add(HandleAction1, Example(1, ""));
        corutines.Add(HandleAction2, Example(1, ""));
        corutines.Add(HandleAction3, Example(1, ""));

        //move.removeAction(HandleAction1);

        removeAction(HandleAction1);

        move.Subscribe(provider);

    }

    void removeAction(Action<ActionModel> action)
    {
        StopCoroutine(corutines[action]);
        StopCoroutine(corutinesList[0]);
        move.removeAction(action);

    }

    void HandleAction3(ActionModel obj)
    {
        Debug.Log("ACTION 1: " + obj.count + " " + obj.name);
        Action3();
    }


    void HandleAction2(ActionModel obj)
    {
        Action2();
    }


    void HandleAction1(ActionModel obj)
    {
        Action1();       
    }


    IEnumerator Example(float time, string someDumpText)
    {
        Debug.Log("TICK CALLED! " + someDumpText);
        yield return new WaitForSeconds(time);
        Debug.Log("TICK ENDED! " + someDumpText);
    }

    private void Action1()
    {
        Debug.Log("Action 1");
        StartCoroutine(Example(1, "1"));
    }

    private void Action2()
    {
        Debug.Log("Action 2");
        StartCoroutine(Example(2, "2"));
    }

    private void Action3()
    {
        Debug.Log("Action 3");
        StartCoroutine(Example(4, "3"));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            provider.Execute(drek);
            provider.DoComplete(null);
        }

        if (Input.GetMouseButtonDown(1))
        {
            provider.Execute(drek);
            provider.DoComplete();
        }


    }

}
