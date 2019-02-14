using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayOrder : MonoBehaviour 
{

    // Use this for initialization
    public GameObject mark;
    public GameObject parent;
    public List<GameObject> marks = new List<GameObject>();
	
    public void Display(UserOrder forOreder)
    {
        List<Component> listComp = forOreder.OrdersInfo();

        for (int i=0; i < listComp.Count; i++)
        {
            GameObject gameObj = Instantiate(mark, parent.transform.position, parent.transform.rotation);
            gameObj.GetComponent<Renderer>().material = listComp[i].m_Material;
            gameObj.transform.localScale = new Vector3(0.025f, 0.05f, 0.05f);
            gameObj.transform.parent = parent.transform;

            marks.Add(gameObj);

            Vector3 positionOfNewObject = i - 1 < 0 ? marks[0].transform.localPosition : marks[i - 1].transform.localPosition;

            positionOfNewObject.x = -2.5f + (i * 2.5f);

            positionOfNewObject.y = 0.5f;

            gameObj.transform.localPosition = positionOfNewObject;

        }

    }
}
