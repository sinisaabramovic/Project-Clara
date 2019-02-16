using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ActionHandlerManager : IObservable<ActionModel>
{
    private List<IObserver<ActionModel>> observers = new List<IObserver<ActionModel>>();

    public ActionHandlerManager()
    {
        observers = new List<IObserver<ActionModel>>();
    }

    public IDisposable Subscribe(IObserver<ActionModel> observer)
    {
        this.observers.Add(observer);

        return new Unsubscriber(this.observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        private List<IObserver<ActionModel>> _observers;
        private readonly IObserver<ActionModel> _observer;

        public Unsubscriber(List<IObserver<ActionModel>> observers, IObserver<ActionModel> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }

    public void DoNext(ActionModel actionModel)
    {
        foreach(var observer in observers)
        {
            if (actionModel == null)
                observer.OnError(new Exception());
            else
                observer.OnNext(actionModel);
        }
    }

    public void DoComplete(Action action = null)
    {
        foreach(var observer in observers.ToArray())
        {
            if (observers.Contains(observer))
                observer.OnCompleted();

            observers.Clear();
        }
    }
}


