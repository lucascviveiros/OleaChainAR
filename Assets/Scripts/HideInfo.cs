using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideInfo : MonoBehaviour
{
    [SerializeField]
    private GameObject panelVideo;

    [SerializeField]
    private GameObject panelApp;


    [SerializeField]
    private Button closeVideo;

    [SerializeField]
    private TestLocationService testLocation;

    private void Awake()
    {
        panelVideo = GameObject.Find("PanelVideo");
        panelApp = GameObject.Find("PanelModulos");
    }

    void Start()
    {
        closeVideo.GetComponent<Button>();
        closeVideo.onClick.AddListener(CloseVideo);
        StartCoroutine("WaitVideo");
       // testLocation.startListening = false;
    }

    IEnumerator WaitVideo()
    {
        yield return new WaitForSecondsRealtime(69.0f);
        panelVideo.SetActive(false);
        panelApp.SetActive(true);
        testLocation.SetGPSlistening(true);
    }

    private void CloseVideo()
    {
        panelVideo.SetActive(false);
        panelApp.SetActive(true);
        testLocation.SetGPSlistening(true);
    }
}
