using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component : ScriptableObject {

    public float moneyCost;
    public string name;

    public GameObject gameObject;
    public Sprite sprite;
    public Material m_Material;

    public Component()
    {
        this.name = "NON SET!";
        this.moneyCost = -1;
    }

    public Component(string name, float moneyCost)
    {
        this.name = name;
        this.moneyCost = moneyCost;
    }

    public virtual string toString()
    {
        return "Component name: " + name + " COST: " + moneyCost;
    }

    public virtual void setTableService(GameObject gameObjectTable)
    {
        gameObjectTable.GetComponent<Renderer>().material = m_Material;
    }
}
