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

        move.addAction((ActionModel obj) => { Debug.Log(obj.count + " 1 " + obj.name); })
            .addAction((ActionModel obj) => { Debug.Log(obj.count + " 2 " + obj.name); })
            .addAction((ActionModel obj) => { Debug.Log(obj.count + " 3 " + obj.name); });

        
        move.Subscribe(provider);
  

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //move.Unsubscribe();
            provider.DoNext(drek);
            provider.DoComplete(null);
        }

        if (Input.GetMouseButtonDown(1))
        {
            provider.DoNext(drek);
            provider.DoComplete();
        }


    }

    IEnumerator Example(float time)
    {
        Debug.Log("TICK CALLED!");
        yield return new WaitForSeconds(time);
        Debug.Log("TICK ENDED!");
    }


}
