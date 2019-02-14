using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Prop : MonoBehaviour 
{

    public int nodeX;
    public int nodeY;

    public int amount;

    public Component componentType;
    public GameObject tableService;

    private Node nodeSet = new Node(0, 0);
    
    private Inventory inventory;

    public void Awake()
    {
        setNode(new Node(nodeX, nodeY));
        inventory = GetComponent<Inventory>();
        componentType.setTableService(tableService);
    }

    public void Start()
    {
        if (amount == 0) return;

        for (int i = 0; i < amount; i++) {
            DepositComponent(componentType);
        }
    }

    public virtual void DepositComponent(Component component)
    {
        if(component == null) {
            return;
        }

        if (inventory != null)
        {
            inventory.Deposit(component, null);
        }
    }

    public virtual List<Component> WithdrawComponent(int amount, Action<bool> onComplete)
    {
        if (inventory == null)
        {
            onComplete(false);
            return null;
        }

        return inventory.Withdraw(componentType, amount, onComplete);
    }

    public virtual Node getNode()
    {
        return nodeSet;
    }

    public virtual void setNode(Node n)
    {
        this.nodeSet = n;
    }

    public virtual bool isEqual(Node n)
    {
        bool result = false;

        if(n.xIndex == nodeSet.xIndex && n.yIndex == nodeSet.yIndex) {
            result = true;
        }

        return result;
    }

    public virtual void Rotate(GameObject who, Action<bool> onComplete)
    {

        Vector3 lookDir = transform.position - who.transform.position;

        lookDir.y = 0f;

        who.transform.rotation = Quaternion.Slerp(
        who.transform.rotation,
        Quaternion.LookRotation(lookDir),
        Time.deltaTime * 100);

        if (onComplete != null) {
            onComplete(true);
        }

    }

}
