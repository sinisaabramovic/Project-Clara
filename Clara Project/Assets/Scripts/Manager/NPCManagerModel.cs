using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManagerModel : MonoBehaviour
{

    // Use this for initialization
    public List<Node> destinationPositions = new List<Node>();
    public List<Vector3> spawnPoints = new List<Vector3>();
          

    public Component_Beam Beam;
    public Component_TomatoSouce Component_TomatoSouce;
    public Component_Fungi fungi;
    public Component_Onion onion;
}
