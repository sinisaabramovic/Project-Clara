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

        move.addAction((ActionModel obj) => { Debug.Log(obj.count + "  " + obj.name); })
            .addAction((ActionModel obj) => { Debug.Log(obj.count + "  " + obj.name); })
            .addAction((ActionModel obj) => { Debug.Log(obj.count + "  " + obj.name); });

        move.Subscribe(provider);
        //for (int i=0; i < actionHandlers.Length; i++)
        //{
        //    actionHandlers[i].Subscribe(provider);
        //}
        //actionHandlers[0].Subscribe(provider);
        //provider.DoAction(() => { actionHandlers[1].Call(); });
        //provider.DoAction(actionHandlers[1].Call);

        //actionHandler.Unsubscribe();
        //provider.EndAction();

        //ActionHandler actionHandler = new ActionHandler(() => { dumpAction("1"); });
        //actionHandler.Subscribe(provider);

        //ActionHandler actionHandler2 = new ActionHandler(() => { dumpAction("2"); });
        //actionHandler2.Subscribe(provider);

        //provider.DoAction(() => { doAction("X"); });
        //actionHandler.Unsubscribe();
        //provider.DoAction(() => { doAction("Y"); });
        //provider.DoAction(null);

        //provider.EndAction();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            provider.DoNext(drek);
            provider.DoComplete(null);
        }

        if (Input.GetMouseButtonDown(1))
        {
            provider.DoNext(drek);
            provider.DoComplete();
        }


    }


}
