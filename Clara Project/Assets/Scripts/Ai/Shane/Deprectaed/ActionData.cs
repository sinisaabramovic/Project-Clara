using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData 
{
    private bool success;
    private List<Node> path;
    private string message;
    private float timeOut;

    public ActionData(bool success, List<Node> path, string message, float timeOut = 0)
    {
        this.success = success;
        this.path = path;
        this.message = message;
        this.timeOut = timeOut;
    }

    public virtual bool getSuccessResult()
    {
        return this.success;
    }

    public virtual void setSuccessResult(bool value)
    {
        this.success = value;
    }

    public virtual List<Node> getPathData()
    {
        return this.path;
    }

    public virtual void setPathData(List<Node> path)
    {
        this.path = path;
    }

    public virtual void setMessageData(string message)
    {
        this.message = message;
    }

    public virtual string getMessage()
    {
        return this.message;
    }

    public virtual float getTimeout()
    {
        return this.timeOut;
    }
}


public class PathData : ActionData
{

    public PathData(List<Node> path):base(true,path,"")
    {

    }

    public override List<Node> getPathData()
    {
        return base.getPathData();
    }

    public override void setPathData(List<Node> path)
    {
        base.setPathData(path);
    }

    public override void setMessageData(string message)
    {
        base.setMessageData(message);
    }

    public override string getMessage()
    {
        return base.getMessage();
    }

    public override bool getSuccessResult()
    {
        return base.getSuccessResult();
    }

    public override void setSuccessResult(bool value)
    {
        base.setSuccessResult(value);
    }
         
}

public class TimerData : ActionData
{
    public TimerData(List<Node> path, float timeOut) : base(true, path, "", timeOut)
    {

    }

    public override float getTimeout()
    {
        return base.getTimeout();
    }
}