using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PawnController
{
    PlayerView playerView;
    PlayerModel playerModel;

    public void Awake()
    {
        playerView = GetComponent<PlayerView>();
        playerModel = GetComponent<PlayerModel>();
    }

    public override void Update()
    {
        base.Update();
       
        if (Input.GetMouseButtonDown(0) && playerModel.state == PawnState.Idle)
        {
            RaycastHit hit;
            Ray mouseCameraPos = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseCameraPos, out hit, 100))
            {            

                Node node = GetNode(hit.transform.gameObject);
                if (node == null)
                {
                    return;
                }

                graphController.StartSearch(new Vector2(transform.position.x, transform.position.z),
                 node.toVector2(),
                     (List<Node> path) => {
                         CallAction("MoveToGet", new PathData(path), (bool result) => {
                             if (result)
                             {
                                 playerView.RotateTo(hit.transform.position, (bool obj) => {
                                     if (hit.transform.gameObject.GetComponent<Prop>() != null)
                                     {
                                         playerView.GetComponentFromProp(hit.transform.gameObject, 1);
                                     }

                                     if(hit.transform.gameObject.GetComponent<NPCController>() != null)
                                     {
                                         playerView.CommitOrder(hit.transform.gameObject, (Reward returnReward) => { 
                                            if(returnReward == null)
                                             {
                                                 return;
                                             }

                                             if (returnReward.GetResult())
                                             {
                                                 Debug.Log("SUCCESS!!! ORDER!!!");
                                             }
                                             else
                                             {
                                                 Debug.Log("ERROR UNSUCCESS!!!!!");
                                             }
                                         });
                                     }

                                 });
                             }
                         });
                     });
            }
        }
    }
}
