using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Octree
{

    public class BaseOctreeNode<TType>
    {
        private Vector3 position;
        float size;
        BaseOctreeNode<TType>[] subNodes;
        List<TType> value;

        public IEnumerable<BaseOctreeNode<TType>> Nodes { get { return subNodes; } }

        public Vector3 Position { get { return position; } }
        public float Size { get { return size; } }

        public BaseOctreeNode(Vector3 position, float size)
        {
            this.position = position;
            this.size = size;
        }

        public void Subdivide(int depth = 1)
        {
            subNodes = new BaseOctreeNode<TType>[8];
            for(int i =0; i < subNodes.Length; i++)
            {
                Vector3 newPosition = this.position;
                if((i & 4) == 4)
                {
                    newPosition.y += size * 0.25f;
                }
                else
                {
                    newPosition.y -= size * 0.25f;
                }

                if ((i & 1) == 1)
                {
                    newPosition.z += size * 0.25f;
                }
                else
                {
                    newPosition.z -= size * 0.25f;
                }

                if ((i & 2) == 2)
                {
                    newPosition.x += size * 0.25f;
                }
                else
                {
                    newPosition.x -= size * 0.25f;
                }


                subNodes[i] = new BaseOctreeNode<TType>(newPosition, size * 0.5f);
                if (depth > 0)
                {
                    subNodes[i].Subdivide(depth - 1);
                }
            }
          
        }

        public bool isLeaf()
        {
            return subNodes == null;
        }
    }
}

