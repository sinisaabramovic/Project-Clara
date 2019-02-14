using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : PawnController
{

    public Node startNode;
    public Node destinationNode;
    public Node exitNodde;
    private Node serveNode;

    private Node currentNode;
    private bool initialized = false;

    private float countDownTimer = 4.0f;

    public float CountDownTimer { get { return countDownTimer; } set { countDownTimer = value; } }

    NPCView nPCView;
    NPCModel nPCModel;

    private void Awake()
    {
        nPCView = GetComponent<NPCView>();
        nPCModel = GetComponent<NPCModel>();

        nPCModel.animationSpeed = 2.0f;
    }

    public void SetOrder(UserOrder order)
    {
        nPCModel.userOrder = order;
        nPCModel.displayOrder.GetComponent<DisplayOrder>().Display(order);
    }

    public override Node GetNode(GameObject forObject)
    {
        return base.GetNode(forObject);
    }

    public Reward GetReward(List<Component> recieved)
    {
        Reward reward = nPCView.GetReward(recieved);
        nPCView.StopAnimation();

        if (reward.GetResult())
        {
            nPCView.StartAnimation(PawnState.GoodOreder);

            HandleCourutines(2);

        }
        else
        {
            nPCView.StartAnimation(PawnState.BadOrder);
            // TODO treba vidjeti da li se kupac vraca tj. odlazi ukoliko mu ponudimo krivu narudzbu
            HandleCourutines(3);
        }

        return reward;
    }

    private void HandleCourutines(float timer = 3)
    {
        StopAllCoroutines();

        nPCView.DisplayOrdersInfo(false);

        StartCoroutine(GetComponent<Timer>().StartCountdown((bool retObj) =>
        {
            currentNode = exitNodde;
            nPCView.StartAnimation(PawnState.Walk);

        }, timer));
    }

    public void Init(Node destinationNode, Node exitNode, Camera camera, float countDownTimer = 5f)
    {
        this.startNode = new Node((int)transform.position.x, (int)transform.position.z);
        this.destinationNode = destinationNode;
        this.exitNodde = exitNode;
        this.camera = camera;
        currentNode = destinationNode;
        nPCView.enableCollider();
        this.countDownTimer = countDownTimer;
        serveNode = new Node(destinationNode.xIndex, destinationNode.yIndex - 2);
        initialized = true;
    }

    public Node positionToServe()
    {
        return serveNode;
    }

    public override void Update()
    {
        if (!initialized)
        {
            return;
        }

        base.Update();

        //if(nPCModel.state == PawnState.Idle && currentNode != null)
        //{
        //    nPCView.disableCollider();           
        //    graphController.StartSearch(new Vector2(transform.position.x, transform.position.z),
        //        currentNode.toVector2(),
        //            (List<Node> path) => {
        //                if (path == null) return;
        //                CallAction("Move", new PathData(path), (bool result) => {
        //                    if (result)
        //                    {
        //                        nPCView.enableCollider();

        //                        nPCView.DisplayOrdersInfo(true);

        //                        Vector3 lookTo = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);

        //                        nPCView.RotateTo(lookTo,(bool obj) => {
        //                            nPCModel.userOrder.DisplayOrder();
        //                            currentNode = null;
        //                            StartCoroutine(GetComponent<Timer>().StartCountdown((bool returnVal) => {

        //                                nPCView.disableCollider();
        //                                nPCView.StopAnimation();
        //                                if (nPCModel.served)
        //                                {
        //                                    nPCView.StartAnimation(PawnState.GoodOreder);
        //                                }
        //                                else
        //                                {
        //                                    nPCView.StartAnimation(PawnState.BadOrder);
        //                                }

        //                                nPCView.DisplayOrdersInfo(false);

        //                                StartCoroutine(GetComponent<Timer>().StartCountdown((bool retObj) => {
        //                                    currentNode = exitNodde;
        //                                    nPCView.StartAnimation(PawnState.Walk);

        //                                }, 3));

        //                            }, CountDownTimer));

        //                          });
        //                    }
        //                });
        //            });
        //}

        if (nPCModel.state == PawnState.Idle && currentNode != null)
        {
            nPCView.disableCollider();
            graphController.StartSearch(new Vector2(transform.position.x, transform.position.z),
                currentNode.toVector2(),
                    (List<Node> path) => {
                        if (path == null) return;
                        CallAction("Move", new PathData(path), (bool result) => {
                            if (result)
                            {
                                currentNode = null;
                                CallAction("TakeOrder", null, (bool takeOrderResult) => {
                                    if (takeOrderResult)
                                    {
                                        CallAction("RotateToOrder", null, (bool rotateToOrderResult) => { 
                                            if (rotateToOrderResult)
                                            {
                                                TimerData timerData = new TimerData(null, countDownTimer);
                                                CallAction("StartWaiting", timerData, (bool startWaitingResult) => {
                                                    if (startWaitingResult)
                                                    {
                                                        currentNode = exitNodde;
                                                    }
                                                });
                                            }
                                        });
                                    }
                                }
                                );
                            }
                        });
                    });
        }
    }
}
