using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class TestLocationService : MonoBehaviour
{
    public Text debug;
    //private GameObject dialog = null;

    [SerializeField]
    private TextMeshProUGUI tMod1;
    [SerializeField]
    private TextMeshProUGUI tMod1sub;

    [SerializeField]
    private TextMeshProUGUI tMod2;
    [SerializeField]
    private TextMeshProUGUI tMod2sub;

    [SerializeField]
    private Button bMod1;

    [SerializeField]
    private Button bMod2;

    private Color activeColor;
    private Color unableColor;
    private Color blackColor;

    private bool startListening;

    public void SetGPSlistening(bool value)
    {
        startListening = value;
    }

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#323232", out blackColor);
        ColorUtility.TryParseHtmlString("#C39B26", out activeColor);
        ColorUtility.TryParseHtmlString("#C39B26", out unableColor);

        unableColor.a = 0.43f;
        activeColor.a = 0.43f;
        bMod1.GetComponent<Image>().color = activeColor;
        bMod2.GetComponent<Image>().color = activeColor;
    }

    private void Start()
    {
        OnEnableGPSbyUser();
    }

    private void Update()
    {
        if (startListening)
        {
#if UNITY_EDITOR
            StartCoroutine(LoadSceneAR());
#endif
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                StartCoroutine(LoadSceneAR());
            }

            if (Input.location.isEnabledByUser)
            {
                debug.text = "Localização de GPS reconhecida";
                StartCoroutine(LoadSceneGPS());
            }
            else
            {
                debug.text = "Habilite sua localização de GPS";
            }
        }
    }

    private IEnumerator LoadSceneGPS()
    {
        EnableMod1();
        yield return new WaitForSecondsRealtime(4.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("myZoomableMap");
    }

    private IEnumerator LoadSceneAR()
    {
        EnableMod2();
        yield return new WaitForSecondsRealtime(4.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("BasicAR2");
    }

    public void OnEnableGPSbyUser()
    {
        //1 REQUEST de permi��o de GPS 
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation))
        {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
            debug.text = "Android Permission";
        }
        //2 Se j� deu permiss�o verifica se usuario tem o servico de GPS habilitado nas funcoes do celular
        if (!UnityEngine.Input.location.isEnabledByUser)
        {
            //debug.text = "Android and Location not enabled";
            debug.text = "Habilite sua localização de GPS";
            debug.color = blackColor;
            DisableMod();
        }
        else
        {
            debug.text = "Localização de GPS reconhecida";
            EnableMod1();
        }
    }
    
    private void DisableMod()
    {
        unableColor.a = 0.43f;
        tMod1.color = unableColor;
        tMod1sub.color = unableColor;
        tMod2.color = unableColor;
        tMod2sub.color = unableColor;
    }

    private void EnableMod1()
    {
        activeColor.a = 1f;
        tMod1.color = activeColor;
        tMod1sub.color = activeColor;
        bMod1.GetComponent<Image>().color = activeColor;
    }

    private void EnableMod2()
    {
        activeColor.a = 1f;
        tMod2.color = activeColor;
        tMod2sub.color = activeColor;
        bMod2.GetComponent<Image>().color = activeColor;
    }

    IEnumerator CheckGPS()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location False :/");
            debug.text = "Precisa Ligar GPS! False";
            //yield break;
        }
        else 
        {
            Debug.Log("GPS ligado! TRUE");
            debug.text = "GPS Ligado!!";
        }

        // Starts the location service.
        Input.location.Start();

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            debug.text = "Unable to determine device location";
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    IEnumerator LocationCoroutine()
    {

        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            Debug.LogFormat("Android and Location not enabled");
            debug.text = "Android and Location not enabled";
            yield break;
        }

        
#if UNITY_IOS
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            yield break;
        }
#endif
        
        // Start service before querying location
        UnityEngine.Input.location.Start(500f, 500f);

        // Wait until service initializes
        int maxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            debug.text = "LocationService Initializing";

            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        int editorMaxWait = 15;
        while (UnityEngine.Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            debug.text = "LocationService Stopped";

            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            // TODO Failure
            Debug.LogFormat("Timed out");
            debug.text = "Time out";

            yield break;
        }

        // Connection has failed
        if (UnityEngine.Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", UnityEngine.Input.location.status);
            debug.text = "Não determinou a localizacao do disp.";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            Debug.LogFormat("Location: "
                + UnityEngine.Input.location.lastData.latitude + " "
                + UnityEngine.Input.location.lastData.longitude + " "
                + UnityEngine.Input.location.lastData.altitude + " "
                + UnityEngine.Input.location.lastData.horizontalAccuracy + " "
                + UnityEngine.Input.location.lastData.timestamp);

            var _latitude = UnityEngine.Input.location.lastData.latitude;
            var _longitude = UnityEngine.Input.location.lastData.longitude;
            // TODO success do something with location
        }

        // Stop service if there is no need to query location updates continuously
        UnityEngine.Input.location.Stop();
    }

}

