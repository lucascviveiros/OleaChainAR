using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Events;

public class CheckInternet : MonoBehaviour
{
    [SerializeField]
    private Text internetProvider;

    [SerializeField]
    private RectTransform rectPanelNotification;

    private float xValue = -90.0f;
    private float timer;
    private bool connected = false;
    private bool notConnected = false;
    private bool newStatus = true;
    public float waitTime = 3f;

    private void Start()
    {
        StartCoroutine(waitNotification());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                connected = false;
                internetProvider.text = "No Internet";
                internetProvider.color = Color.red;
                if (notConnected == false)
                {
                    notConnected = true;
                    newStatus = true;
                }

            }
            else
            {
                notConnected = false;
                internetProvider.text = "Internet Connection Successful";
                internetProvider.color = Color.white;
                if (connected == false)
                {
                    connected = true;
                    newStatus = true;
                }
            }

            timer = 0f;
        }

        if (notConnected == true && newStatus == true) //No internet
        {
            LeanTween.move(rectPanelNotification, new Vector3(xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.2f);
            StartCoroutine(waitNotification());
        }

        else if (connected == true && newStatus == true)
        {
            LeanTween.move(rectPanelNotification, new Vector3(xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.2f);
            StartCoroutine(waitNotification());
        }

        if (notConnected == true && connected == true || notConnected == false && connected == false)
        {
            newStatus = false;
        }
    }


    IEnumerator waitNotification()
    {
        newStatus = false;
        yield return new WaitForSecondsRealtime(5.0f);
        LeanTween.move(rectPanelNotification, new Vector3(-xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.2f); //Hide notification
    }

    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest request = new UnityWebRequest("http://www.google.com");
        yield return request.SendWebRequest();

        if (request.error != null)
        {
            internetProvider.text = "No Internet connection";
            internetProvider.color = Color.red;
        }

        else
        {
            internetProvider.text = "Internet connection successful";
            internetProvider.color = Color.green;
        }
    }
}
