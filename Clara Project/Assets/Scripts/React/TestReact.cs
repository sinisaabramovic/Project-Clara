using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TestReact : MonoBehaviour {

    ActionHandlerManager provider = new ActionHandlerManager();
    ActionHandler[] actionHandlers;

    ActionMove move;
    ActionModel drek = new ActionModel();
    // Use this for initialization
    void Start () {

        drek.count = 10;
        drek.name = "Drek!";

        move = GetComponent<ActionMove>();
        //actionHandlers = GetComponents<ActionHandler>();
        move.Add(HandleAction1).Add(HandleAction2).Add(HandleAction3);

        move.Remove(HandleAction2);
        
        move.Subscribe(provider);

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //move.Unsubscribe();
            provider.Execute(drek);
            provider.DoComplete(null);
        }

        if (Input.GetMouseButtonDown(1))
        {
            provider.Execute(drek);
            provider.DoComplete();
        }
    }

    void HandleAction3(ActionModel obj)
    {
        Debug.Log("CALL FOR !" + obj.count);
        Action3();
    }


    void HandleAction2(ActionModel obj)
    {
        Debug.Log("CALL FOR !" + obj.count);
        Action2();
        StopAllCoroutines();
    }

    void HandleAction1(ActionModel obj)
    {
        Debug.Log("CALL FOR !" + obj.count);
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
        StartCoroutine(Example(1,"1"));
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


}
