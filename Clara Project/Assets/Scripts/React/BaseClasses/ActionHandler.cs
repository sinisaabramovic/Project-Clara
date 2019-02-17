using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActionHandler : MonoBehaviour, IObserver<ActionModel>
{

    private Action<ActionModel> action;
    private IDisposable unsubscriber;

    public ActionHandler(Action<ActionModel> action)
    {
        this.action = action;
    }


    public virtual ActionHandler Add(Action<ActionModel> action)
    {
        this.action += action;
        return this;
    }

    public virtual ActionHandler Remove(Action<ActionModel> action)
    {
        this.action -= action;
        return this;
    }

    public virtual void OnError(Exception error)
    {

    }

    public virtual void OnNext(ActionModel actionData)
    {
        action(actionData);
    }

    public virtual void OnCompleted()
    {

    }

    public virtual void Subscribe(IObservable<ActionModel> provider)
    {
        if (provider == null) return;
        unsubscriber = provider.Subscribe(this);
    }

    public virtual void Unsubscribe()
    {
        unsubscriber.Dispose();
    }

    public virtual void Call()
    {
        //this.action();
    }


}
