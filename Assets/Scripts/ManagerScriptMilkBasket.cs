using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScriptMilkBasket : MonoBehaviour {

    public int distance = 300;

    public int rowSize = 86, colSize = 11;

    public GameObject contentPrefab;

    public GameObject container;

    string[,] data;

    // Use this for initialization
    void Start ()
    {   
        LoadCSV();
        PopulateView();
    }

    void LoadCSV()
    {
        data = new string[rowSize, colSize];
        TextAsset dataAsset = Resources.Load<TextAsset>("data");
        string[] lines = dataAsset.text.Split(new char[] { '\n' });
        for(int i = 0; i < rowSize; i++)
        {
            string[] temp = lines[i].Split(new char[] { ',' });
            for (int j = 0; j < colSize; j++)
            {
                data[i, j] = temp[j];
            }            
        }
    }

    void PopulateView()
    {
        int startAt = 0;
        for(int i = 1; i < rowSize; i++)
        {
            // Load Content Prefab
            GameObject content;

            // Set Image
            object[] image = Resources.LoadAll("images/" + data[i, 0], typeof(Sprite));
            if (image.Length != 0)
            {
                content = Instantiate(contentPrefab, container.transform);
                content.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)image[0];
            }                
            else
            {
                continue;             
            }

            // Set Name
            content.transform.GetChild(1).GetComponent<Text>().text = data[i,1];

            // Set Quantity
            content.transform.GetChild(2).GetComponent<Text>().text = data[i, 2];

            // Set Price
            content.transform.GetChild(3).GetComponent<Text>().text += data[i, 10];

            // Correct Position            
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, startAt);
            startAt -= distance;
        }
    }
}
