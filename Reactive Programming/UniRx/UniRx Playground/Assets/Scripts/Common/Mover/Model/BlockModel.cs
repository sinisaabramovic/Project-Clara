using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockModel : MonoBehaviour
{

    private DDirections dDirections = DDirections.Neutral;

    private GameObject centerPivot;
    private GameObject parentObject;

    private GameObject up;
    private GameObject down;
    private GameObject left;
    private GameObject right;

    private int step = 4;
    private float speed = 0.01f;

    public int Step { get { return step; } }
    public float Speed { get { return speed; } }
    public GameObject ParentObject { get { return parentObject; }  set { parentObject = value; } }
    public GameObject CenterPivot { get { return centerPivot; }  set { centerPivot = value; } }

    public Dictionary<Vector3, Vector3> point = new Dictionary<Vector3, Vector3>();

    public BlockModel(GameObject parent, int step, float speed)
    {

        this.ParentObject = parent;
        this.step = step;
        this.speed = speed;
        CenterPivot = new GameObject();
        CenterPivot.transform.position = ParentObject.transform.position;
        CenterPivot.name = "CENTER PIVOT";
        Init();
    }

    private void Init()
    {

        up = new GameObject();
        down = new GameObject();
        left = new GameObject();
        right = new GameObject();

        up.name = "UP";
        down.name = "DOWN";
        left.name = "LEFT";
        right.name = "RIGHT";

        float baseSizeX = ParentObject.transform.localScale.x / 2;
        float baseSizeY = ParentObject.transform.localScale.y / 2;
        float baseSizeZ = ParentObject.transform.localScale.z / 2;

        up.transform.position = new Vector3(0.0f, -baseSizeY, baseSizeZ);
        down.transform.position = new Vector3(0.0f, -baseSizeY, -baseSizeZ);
        left.transform.position = new Vector3(-baseSizeX, -baseSizeY, 0.0f);
        right.transform.position = new Vector3(baseSizeX, -baseSizeY, 0.0f);

        up.transform.SetParent(CenterPivot.transform, true);
        down.transform.SetParent(CenterPivot.transform, true);
        left.transform.SetParent(CenterPivot.transform, true);
        right.transform.SetParent(CenterPivot.transform, true);

        ResetRotationPoints();
    }

    private void ResetRotationPoints()
    {

        if (point == null) return;

        point.Clear();

        point.Add(Vector3.right, up.transform.position);
        point.Add(Vector3.left, down.transform.position);
        point.Add(Vector3.forward, left.transform.position);
        point.Add(Vector3.back, right.transform.position);
    }

    public void ResetPivotPoint()
    {

        this.CenterPivot.transform.position = this.ParentObject.transform.position;
        ResetRotationPoints();
    }

    public void Rotate(Vector3 byVector)
    {

        ParentObject.transform.RotateAround(this.point[byVector], byVector, this.Step);
    }

    public void RotateBaseUp()
    {

    }

    public void RotateBaseDown()
    {

    }

    public void RotateBaseNorth()
    {
        CenterPivot.transform.localEulerAngles = new Vector3(90, 0, 0);
    }

    public void RotateBaseSouth()
    {
        CenterPivot.transform.localEulerAngles = new Vector3(-90, 0, 0);
    }

    public void RotateBaseEast()
    {
        CenterPivot.transform.localEulerAngles = new Vector3(0, 0, -90);
    }

    public void RotateBaseWest()
    {
        CenterPivot.transform.localEulerAngles = new Vector3(0, 0, 90);
    }

    public void RotateBaseDefault()
    {
        CenterPivot.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
