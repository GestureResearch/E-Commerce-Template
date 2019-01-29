using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScriptSupermart : MonoBehaviour
{

    public int distance = 289;

    public int rowSize = 86, colSize = 11;

    public GameObject bannerPrefab;

    public GameObject contentPrefab;

    public GameObject container;

    string[,] data;

    // Use this for initialization
    void Start()
    {
        LoadCSV();
        PopulateView();
    }

    void LoadCSV()
    {
        data = new string[rowSize, colSize];
        TextAsset dataAsset = Resources.Load<TextAsset>("data");
        string[] lines = dataAsset.text.Split(new char[] { '\n' });
        for (int i = 0; i < rowSize; i++)
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
        string[] category = { "PET", "Can", "Carton" };
        for (int j = 0; j < 3; j++)
        {
            GameObject banner = Instantiate(bannerPrefab, container.transform);

            // Set Image
            object[] bannerImages = Resources.LoadAll("banners/" , typeof(Sprite));
            banner.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite)bannerImages[j];
            banner.transform.GetChild(0).GetComponent<Image>().preserveAspect = true;

            // Correct Position            
            banner.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, startAt);
            startAt -= distance;


            for (int i = 1; i < rowSize; i++)
            {
                if (data[i, 3] == category[j])
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
                    content.transform.GetChild(1).GetComponent<Text>().text = data[i, 1];

                    // Set Quantity and Type
                    content.transform.GetChild(2).GetComponent<Text>().text = data[i, 2] + ", " + data[i, 3];

                    // Set Price
                    content.transform.GetChild(3).GetComponent<Text>().text += data[i, 8];

                    // Set MRP
                    content.transform.GetChild(4).GetComponent<Text>().text = data[i, 4];

                    // Set Discount
                    content.transform.GetChild(5).GetComponent<Text>().text += ((int)(float.Parse(data[i, 4]) - float.Parse(data[i, 9]))).ToString() + " off";

                    // Set Rate
                    content.transform.GetChild(6).GetComponent<Text>().text += Random.Range(50, 65).ToString() + "/L";

                    // Correct Position            
                    content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, startAt);
                    startAt -= distance;
                }
            }
        }
    }
}