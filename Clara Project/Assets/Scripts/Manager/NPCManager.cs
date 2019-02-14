using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour 
{
    public GameObject npcPrefab;
    public Camera mainCamera;

    NPCManagerModel nPCManagerModel;

    private void Awake()
    {
        nPCManagerModel = GetComponent<NPCManagerModel>();
    }

    void Start ()
    {

        BaseTestMethod();              
    }

    private void BaseTestMethod()
    {
        nPCManagerModel.destinationPositions.Add(new Node(6, 6));
        nPCManagerModel.destinationPositions.Add(new Node(8, 6));
        nPCManagerModel.destinationPositions.Add(new Node(10, 6));

        nPCManagerModel.destinationPositions.Add(new Node(8, 16));

        nPCManagerModel.spawnPoints.Add(new Vector3(8, 0, 16));
        nPCManagerModel.spawnPoints.Add(new Vector3(12, 0, 16));
        nPCManagerModel.spawnPoints.Add(new Vector3(14, 0, 16));

        GameObject npc = Instantiate(this.npcPrefab, nPCManagerModel.spawnPoints[0], Quaternion.identity);
        npc.gameObject.GetComponent<NPCController>().Init(nPCManagerModel.destinationPositions[0], nPCManagerModel.destinationPositions[3], this.mainCamera, 20.0f);

        UserOrder order = new UserOrder(new Reward(1, 1, true));
        order.AddToOrder(nPCManagerModel.Beam);
        order.AddToOrder(nPCManagerModel.Component_TomatoSouce);
        order.AddToOrder(nPCManagerModel.onion);

        npc.gameObject.GetComponent<NPCController>().SetOrder(order);

        Destroy(npc, 40.0f);
    }

    // Update is called once per frame
    void Update () 
    {

	}
}
