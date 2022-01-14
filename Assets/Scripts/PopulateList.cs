using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopulateList : MonoBehaviour
{
    private GameObject myContentManager;
    [SerializeField]
    private GameObject myImagePrefab;

    private bool listFilled = false;
//    private double[] myActualDistanceMatrix = new double[5];

    // Start is called before the first frame update
    void Start()
    {
        myContentManager = GameObject.FindGameObjectWithTag("myContentManager");

        /*int x = 0;
        while (x <= 20)
        {
            var myNewImage = Instantiate(myImagePrefab);

            myNewImage.transform.SetSiblingIndex(x);

            myNewImage.transform.SetParent(myContentManager.transform);

            TextMeshProUGUI imageText = myNewImage.GetComponentInChildren<TextMeshProUGUI>();
            //imageText.text = "x: " + x;
            imageText.text = "";
            x++;
        } */  

    }

    public void Populate(int listLength, double[] myActualDistanceMatrix)
    {
        /*
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        */
        if (listFilled == false) 
        { 
            Debug.Log("Populate List");
            int x = 0;
            while (x <= listLength - 1)
            {
                var myNewImage = Instantiate(myImagePrefab);

                myNewImage.transform.SetSiblingIndex(x);

                myNewImage.transform.SetParent(myContentManager.transform);

                TextMeshProUGUI imageText = myNewImage.GetComponentInChildren<TextMeshProUGUI>();
                //imageText.text = "x: " + x;
                imageText.text = myActualDistanceMatrix[x].ToString();
                x++;
            }

            listFilled = !listFilled;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
