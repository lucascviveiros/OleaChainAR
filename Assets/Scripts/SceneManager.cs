using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    private Button arSceneButton;
    [SerializeField]
    private Button arSceneButton2;
    [SerializeField]
    private Button arSceneButton3;
    [SerializeField]
    private Button vrSceneButton;
    [SerializeField]
    private Button listSceneButton;
    [SerializeField]
    private Button listSwiperButton;
    [SerializeField]
    private Button backMapButton;
    [SerializeField]
    private Button buttonMod1;
    [SerializeField]
    private Button buttonMod2;

    private GameObject panelVideo;    
    private GameObject panelMod;
    
    private GameObject panelListSwiper;

    private GameObject panelMap;

    private Scene currentScene;
    private bool firsLoad;

    private void Update()
    {
#if !UNITY_EDITOR
  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = currentScene.name;
            if (sceneName == "myZoomableMap" || sceneName == "Main")
            {
                MinimizeApp();
            }   

            else if (sceneName == "BasicAR" || sceneName == "XRInteraction2" || sceneName == "ARFoundation" || sceneName == "ARFace")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
            }

            else if (sceneName == "BasicAR2")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BasicSwiper");
            }
            else if (sceneName == "BasicSwiper")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
            }
        }
#endif
    }

    private void MinimizeApp()
    {
#if UNITY_ANDROID
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack", true);
#endif
    }

    private void Start()
    {
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Main")
        {
            panelMod = GameObject.Find("PanelModulos");
            panelVideo = GameObject.Find("PanelVideo");

            panelMod.SetActive(true);
            //buttonMod1 = GameObject.Find("ButtonMod1").GetComponent<Button>();
            buttonMod2 = GameObject.Find("ButtonMod2").GetComponent<Button>();

            //buttonMod1.onClick.AddListener(Mod1);
            buttonMod2.onClick.AddListener(Mod2);
            panelMod.SetActive(false);
            panelVideo.SetActive(true);
          
        }

        else if (sceneName == "myZoomableMap") 
        {
            panelListSwiper = GameObject.Find("PanelListSwiper");
            //panelListSwiper.SetActive(false);

            panelMap = GameObject.Find("PanelMap");

            arSceneButton = GameObject.Find("ButtonAR").GetComponent<Button>();
            arSceneButton.onClick.AddListener(ARScene);

            arSceneButton2 = GameObject.Find("ButtonList").GetComponent<Button>();
            arSceneButton2.onClick.AddListener(ARScene2);

            backMapButton.onClick.AddListener(ShowMap);

            listSwiperButton.onClick.AddListener(ShowListPanel);

        }
        else if (sceneName == "BasicAR")
        {
            arSceneButton = GameObject.Find("ButtonMap").GetComponent<Button>();
            arSceneButton.onClick.AddListener(ARScene);

        }
        else if (sceneName == "VRMobile")
        {
            vrSceneButton = GameObject.Find("ButtonBack").GetComponent<Button>();
            vrSceneButton.onClick.AddListener(VRScene);
        }
        else if (sceneName == "BasicAR2")
        {
            listSceneButton = GameObject.Find("ButtonList").GetComponent<Button>();
            listSceneButton.onClick.AddListener(ListScene);
        }
    }

    private void Mod1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
    }
    private void Mod2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BasicAR2");
    }

    private void ListScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BasicSwiper");
        
    }

    private void ARScene()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
       
        UnityEngine.SceneManagement.SceneManager.LoadScene("XRInteraction2");
        
    }
    private void ARScene2()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "myZoomableMap")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ARFace"); //Vuforia
        }

        if (sceneName == "BasicAR")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
        }
    }

    private void ARScene3()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "myZoomableMap")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("ARFoundation");
 //           UnityEngine.SceneManagement.SceneManager.LoadScene("AreaTargetTest");
        }

    }
    private void VRScene()
    {
        Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if(sceneName == "myZoomableMap")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("VRMobile");
        }

        else if(sceneName == "VRMobile")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
        }
    }
    [SerializeField]
    private RectTransform rectPanelList;

    private float xValue = 0.5f;
    private float xValue2 = -440.0f;

    private void ShowListPanel()
    {
        //panelListSwiper.SetActive(true);
        //Lewen tween
        LeanTween.move(rectPanelList, new Vector3(xValue, 0.0f, 0f), 1f).setCanvasMove();
        //panelMap.SetActive(false);
    }

    private void ShowMap()
    {
        LeanTween.move(rectPanelList, new Vector3(xValue2, 0.0f, 0f), 1f).setCanvasMove(); //Hide List Panel       
        //panelMap.SetActive(true);
    }
}
