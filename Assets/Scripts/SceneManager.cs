using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private Button arSceneButton;
    private Button vrSceneButton;
    private Button listSceneButton;
    private Button buttonMod1;
    private Button buttonMod2;

    //[SerializeField]
    //private Button buttonGPStest;

    private GameObject panelVideo;    
    private GameObject panelMod;

    private Scene currentScene;

    private bool firsLoad;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string sceneName = currentScene.name;
            if (sceneName == "myZoomableMap" || sceneName == "BasicSwiper" || sceneName == "Main")
            {
                MinimizeApp();
            }   

            else if (sceneName == "BasicAR")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
            }

            else if (sceneName == "BasicAR2")
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("BasicSwiper");
            }
        }
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
            arSceneButton = GameObject.Find("ButtonAR").GetComponent<Button>();

            vrSceneButton = GameObject.Find("ButtonGPS").GetComponent<Button>();

            listSceneButton = GameObject.Find("ButtonList").GetComponent<Button>();

            //buttonGPStest.onClick.AddListener(TesteGPS);

            arSceneButton.onClick.AddListener(ARScene);
            vrSceneButton.onClick.AddListener(VRScene);
            listSceneButton.onClick.AddListener(ListScene);

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

    private void TesteGPS()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CheckGPS");
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

        if (sceneName == "myZoomableMap")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("BasicAR");
        }
        
        if(sceneName == "BasicAR")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
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
}
