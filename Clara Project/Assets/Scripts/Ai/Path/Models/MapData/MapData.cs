using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour {


    int width = 0;
    int height = 0;

    public Texture2D textureMap;
    public TextAsset textAssets;
    
    private GameObject floorInstance;

    public int[,] MakeMap()
    {

        floorInstance = GameObject.FindGameObjectWithTag("floor");

        this.width = (int)floorInstance.transform.localScale.x - 1;
        this.height = (int)floorInstance.transform.localScale.z - 1;
        
        int[,] map = new int[this.width, this.height];

        for (int y = 0; y < this.height; y++)
        {
            for(int x = 0; x < this.width; x++)
            {
                RaycastHit hit;
                Vector3 rayPosition = new Vector3(x, 5f, y);

                if (Physics.Raycast(rayPosition, Vector3.down, out hit))
                {                
                    if (hit.transform.gameObject.tag == "floor")
                    {
                        map[x, y] = 0;
                    }
                    else
                    {
                        map[x, y] = 1;
                    }

                }
                         
            }
        }

        return map;
    }

    private void Start () {
        int[,] mapInstance = MakeMap();
	}

    //public List<string> GetDataFromTextFile(TextAsset tAsset)
    //{
    //    List<string> lines = new List<string>();

    //    if(tAsset != null)
    //    {
    //        string textData = tAsset.text;
    //        string[] delimiters = { "\r\n", "\n" };
    //        lines.AddRange(textData.Split(delimiters, System.StringSplitOptions.None));
    //        lines.Reverse();
    //    }
    //    else
    //    {
    //        Debug.LogWarning("MapData GetDataFromTextFile error invalid ");
    //    }

    //    return lines;
    //}

    //public List<string> GetDataFromTextFile()
    //{
    //    return GetDataFromTextFile(this.textAssets);
    //}

    //public List<string> GetMapFromTexture(Texture2D texture)
    //{
    //    List<string> lines = new List<string>();

    //    if (texture != null)
    //    {
    //        for (int y = 0; y < texture.height; y++)
    //        {
    //            string newLine = "";

    //            for (int x = 0; x < texture.width; x++)
    //            {
    //                if (texture.GetPixel(x, y) == Color.black)
    //                {
    //                    newLine += '1';
    //                }
    //                else if (texture.GetPixel(x, y) == Color.white)
    //                {
    //                    newLine += '0';
    //                }
    //                else
    //                {
    //                    newLine += ' ';
    //                }
    //            }
    //            lines.Add(newLine);
    //        }
    //    }

    //    return lines;
    //}

    //public void SetDimensions(List<string> textLines)
    //{
    //    this.height = textLines.Count;

    //    foreach(string line in textLines)
    //    {
    //        if(line.Length > this.width)
    //        {
    //            this.width = line.Length;
    //        }
    //    }
    //}

}
