using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CheckInternet : MonoBehaviour
{
    [SerializeField]
    private Text internetProvider;

    [SerializeField]
    private RectTransform rectPanelNotification;

    private float xValue = -22.0f;
    private float xValue2 = 44.0f;

    private float timer;
    private bool connected = false;
    private bool notConnected = false;
    private bool newStatus = true;
    private float waitTime = 3f;
    
    private void Start()
    {
        StartCoroutine(waitNotification());
    }

    private void Update()
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
            LeanTween.move(rectPanelNotification, new Vector3(0.0f, xValue, 0f), 1f).setCanvasMove().setDelay(0.2f);
            StartCoroutine(waitNotification());
        }

        else if (connected == true && newStatus == true)
        {
            LeanTween.move(rectPanelNotification, new Vector3(0.0f, xValue, 0f), 1f).setCanvasMove().setDelay(0.2f);
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
        yield return new WaitForSecondsRealtime(6.0f);
        LeanTween.move(rectPanelNotification, new Vector3(0.0f, xValue2, 0f), 1f).setCanvasMove().setDelay(0.2f); //Hide notification
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
