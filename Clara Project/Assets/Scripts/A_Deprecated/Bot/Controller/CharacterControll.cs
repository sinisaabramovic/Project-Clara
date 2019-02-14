using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : Controller
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButtonDown(0))
        {
            if (graphController == null) return;

            RaycastHit hit;
            Ray mouseCameraPos = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mouseCameraPos, out hit, 100))
            {
                character.StopMoving();
                Vector2 hitPoint = new Vector2(hit.point.x, hit.point.z);

                if (hit.transform.gameObject.tag == "prop")
                {
                    Prop prop = hit.transform.gameObject.GetComponent<Prop>();

                    hitPoint = new Vector2(prop.getNode().xIndex, prop.getNode().yIndex);
                }


                graphController.StartSearch(character.getPosition(), hitPoint, (List<Node> path) =>
                {

                    if (path == null)
                    {
                        return;
                    }

                    if (path.Count > 1)
                    {

                        if (hit.transform.gameObject.tag == "prop")
                        {
                            character.StartAnimation(BotStates.Walk, 1.8f);
                            character.Move(path, hit.transform.gameObject);
                        }
                        else
                        {
                            var foundObjects = FindObjectsOfType<Prop>();

                            GameObject associatedNodeObject = null;

                            foreach (Prop prop in foundObjects)
                            {
                                Node propNode = new Node((int)hitPoint.x, (int)hitPoint.y);
                                if (prop.isEqual(propNode))
                                {
                                    associatedNodeObject = prop.gameObject;
                                    break;
                                }
                            }
                            character.StartAnimation(BotStates.Walk, 1.8f);
                            character.Move(path, associatedNodeObject);
                        }

                    }

                });
            }

        }
    }
}
