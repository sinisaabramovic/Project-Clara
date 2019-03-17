using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Octree
{
    public enum OctreeIndex
    {
        BottomLeftFront = 0,     //000,
        BottomRightFront = 2,    //010,
        BottomRightBack = 3,     //011,
        BottomLeftBack = 1,      //001,
        TopLeftFront = 4,    //100,
        TopRightFront = 6,   //110,
        TopRightBack = 7,     //111,
        TopLeftBack = 5,     //101,
    }
    public class BaseOctree<TType>
    {
        private BaseOctreeNode<TType> node;
        private int depth;

        public BaseOctree(Vector3 position, float size, int depth)
        {
            this.node = new BaseOctreeNode<TType>(position, size);
            this.node.Subdivide(depth);

        }

        public BaseOctreeNode<TType> GetRoot()
        {
            return node;
        }

        public int GetIndexPosition(Vector3 lookupPosition, Vector3 nodePosition)
        {
            int index = 0;

            index |= lookupPosition.y > nodePosition.y ? (int)OctreeIndex.TopLeftFront : (int)OctreeIndex.BottomLeftFront;
            index |= lookupPosition.x > nodePosition.x ? (int)OctreeIndex.BottomRightFront : (int)OctreeIndex.BottomLeftFront;
            index |= lookupPosition.z > nodePosition.z ? (int)OctreeIndex.BottomLeftBack : (int)OctreeIndex.BottomLeftFront;

            return index;
        }
    }

    
}



