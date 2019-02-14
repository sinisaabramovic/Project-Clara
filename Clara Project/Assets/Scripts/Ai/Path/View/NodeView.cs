using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour {

    // Use this for initialization

    public GameObject tile;
    public GameObject arrow;
    Node m_node;

    [Range(0, 0.5f)]
    public float borederSize = 0.15f;

    public void Init(Node node)
    {
       if(tile != null) {
            gameObject.name = "Node (" + node.xIndex + "," + node.yIndex + ")";
            gameObject.transform.position = node.position;
            tile.transform.localScale = new Vector3(1f - borederSize, 1f, 1f - borederSize);
            m_node = node;
            EnableObject(arrow, false);
        }
    }

    void ColorNode(Color color, GameObject goObject)
    {
        Renderer goRender = goObject.GetComponent<Renderer>();

        if (goObject != null) {        
            if(goRender != null) {
                goRender.material.color = color;
            }
        } else {
            if(goRender != null) {
                goRender.enabled = false;
            }
        }
    }

    public void ColorNode(Color color)
    {
        ColorNode(color, tile);
    }

    void EnableObject(GameObject go, bool state)
    {
        if(go != null)
        {
            go.SetActive(state);
        }
    }

    public void ShowArrow(Color color)
    {
        if(this.m_node != null && arrow != null && m_node.previous != null) {
            EnableObject(arrow, true);

            Vector3 dirToPrevious = (m_node.previous.position - m_node.position).normalized;
            arrow.transform.rotation = Quaternion.LookRotation(dirToPrevious);

            Renderer arrowRenderer = arrow.GetComponent<Renderer>();

            if(arrowRenderer != null) {
                arrowRenderer.material.color = color;
            }
        }
    }

}
