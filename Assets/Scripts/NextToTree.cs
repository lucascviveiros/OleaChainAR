using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class NextToTree : MonoBehaviour
{
    [SerializeField]
    private Text debugNextTree;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "myCustomMarker(Clone)" || other.gameObject.name == "Sphere")
        {
            //Debug.Log("User next to Tree!");
            debugNextTree.color = Color.white;
            //debugNextTree.text = "Next to a tree!";

        }

        else if (other.gameObject.name == "tree_0" || other.gameObject.name == "Sphere")
        {
            Debug.Log("Next Tree 0");
           // debugNextTree.text = "Next Tree 0";
        }

        //else
        //{
            //Debug.Log("No tree around");
            //debugNextTree.text = "";
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("No tree around");
        debugNextTree.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "myCustomMarker(Clone)" || other.gameObject.name == "Sphere")
        {
            //debugNextTree.color = Color.white;
           // debugNextTree.text = "Keep next!";

        }

        if (other.gameObject.name == "Collider_0") 
        {
            //Debug.Log("Next to 0");
            debugNextTree.text = "Next Tree 0";
        }

        if (other.gameObject.name == "Collider_1")
        {
            //Debug.Log("Next to 1");
            debugNextTree.text = "Next Tree 1";
        }

        if (other.gameObject.name == "Collider_2")
        {
            //Debug.Log("Next to 2");
            debugNextTree.text = "Next Tree 2";
        }

    }

}
