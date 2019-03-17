using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Octree;
using System;

public class OctreeComponent : MonoBehaviour {
    public float size = 5;
    public int depth = 2;
    public GameObject lookupObject;
    public GameObject baseNodePosition;

    BaseOctree<int> baseOctree;
    // Use this for initialization
    void Start () {
         baseOctree = new BaseOctree<int>(this.transform.position, size, depth);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log((OctreeIndex)baseOctree.GetIndexPosition(lookupObject.transform.position, baseNodePosition.transform.position));
	}

    private void OnDrawGizmos()
    {
        if (baseOctree == null) return;

        DrawNode(baseOctree.GetRoot(), 0);
    }

    private Color minColor = new Color(1, 1, 1, 1f);
    private Color maxColor = new Color(0, 0.5f, 1, 0.25f);

    private void DrawNode(BaseOctreeNode<int> node, int nodeDepth = 0)
    {
        if (!node.isLeaf())
        {
            foreach (var subnode in node.Nodes)
            {
                DrawNode(subnode, nodeDepth + 1);
            }
        }
        Gizmos.color = Color.Lerp(minColor, maxColor, nodeDepth / (float)depth);
        Gizmos.DrawWireCube(node.Position, Vector3.one * node.Size);
    }
}
