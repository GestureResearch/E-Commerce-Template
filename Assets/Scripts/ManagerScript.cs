using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {

    public int distance = 300;

    public GameObject contentPrefab;

    public GameObject container;

    object[] images;

    // Use this for initialization
    void Start ()
    {
        images = Resources.LoadAll("amazon-pantry", typeof(Sprite));
        PopulateView();        
    }

    void PopulateView()
    {
        int len = images.Length;
        for (int i = 0; i < len; i++)
        {
            GameObject content = Instantiate(contentPrefab, container.transform);
            content.transform.GetChild(0).GetComponent<Image>().sprite = (Sprite) images[i];
            content.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i*-distance);
        }
    }
}
