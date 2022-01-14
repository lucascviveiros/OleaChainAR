using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class NextToTree : MonoBehaviour
{
    [SerializeField]
   // private Text debugNextTree;

    private bool waitLoad;

    private void Start()
    {
        StartCoroutine(WaitLoad());
    }

    private IEnumerator WaitLoad()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        waitLoad = !waitLoad;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "myCustomMarker(Clone)" || other.gameObject.name == "Sphere")
        {
            //Debug.Log("User next to Tree!");
            //debugNextTree.color = Color.green;
            //debugNextTree.text = "Next to a tree!";

        }

        else if (other.gameObject.name == "tree_0" || other.gameObject.name == "Sphere")
        {
            //Debug.Log("Next Tree 0");
            //debugNextTree.text = "Next Tree 0";
        }

        else
        {
            //Debug.Log("No tree around");
            //debugNextTree.text = "";
        }
    }

    private void OnTriggerExit(Collider other)
    {
       // debugNextTree.text = "";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "myCustomMarker(Clone)" || other.gameObject.name == "Sphere")
        {
            //debugNextTree.color = Color.white;
        }

        if (other.gameObject.name == "Collider_-1") 
        {
          //  debugNextTree.text = "Next Tree 0";
           // Debug.Log("Tree 0");

            if (waitLoad)
            {
                //StartCoroutine(CountTime());
            }
        }

        if (other.gameObject.name == "Collider_0")
        {
           // debugNextTree.text = "Next Tree 1";
           // Debug.Log("Tree 1");

            if (waitLoad)
            {
               // StartCoroutine(CountTime());
            }
            
        }

        if (other.gameObject.name == "Collider_1")
        {
            //debugNextTree.text = "Next Tree 2";
        }
    }

    private IEnumerator CountTime()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("XRInteraction");

    }

}
