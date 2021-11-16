using Mapbox.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPointInfo : MonoBehaviour
{
    [SerializeField]
    private Camera myCamera;
    private Vector3 touchPosWorld;
    private Vector3 clickPosWorld;
    private bool isHit;
    private GameObject touchedObject;
    private float xValue = 0.5f;

    [SerializeField]
    private Text myTouchText;

    [SerializeField]
    private RectTransform rectPanelNotification;

    [SerializeField]
    private Button closeInfo;

    private LabelTextSetter labelTextSetter;

    private LocalDatabase localDatabase;

    private bool openPanel;

    private Vector2 touchPosWorld2D;

    private RaycastHit2D hitInformation;

    private void Start()
    {
        labelTextSetter = GetComponent<LabelTextSetter>();
        localDatabase = GetComponent<LocalDatabase>();
        closeInfo.onClick.AddListener(ClosePanelInfo);
    }

    void Update()
    {
        /*
        if (Input.touchCount > 0)
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);
            touchedObject = hitInformation.transform.gameObject;
            labelTextSetter = touchedObject.GetComponentInParent<LabelTextSetter>();

            if (touchedObject.name.ToString() != "UserSurroundings")
            {
                UpdateInfo(labelTextSetter);
            }
        }*/

        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Stationary)
            {
                // Construct a ray from the current touch coordinates
                Ray ray = myCamera.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    touchedObject = hit.transform.gameObject;
                    labelTextSetter = touchedObject.GetComponentInParent<LabelTextSetter>();

                    if (touchedObject.name.ToString() != "UserSurroundings")
                    {
                        UpdateInfo(labelTextSetter);
                    }

                }
            }
        }      
        
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) )
        {
            //Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            GameObject hitObj;

            if (Physics.Raycast(ray, out hit))
            {
                //isHit = false;
                //Debug.Log("Clicked: " + hit.transform.name);

                touchedObject = hit.transform.gameObject;
                labelTextSetter = touchedObject.GetComponentInParent<LabelTextSetter>();

                if (touchedObject.name.ToString() != "UserSurroundings")
                {
                    UpdateInfo(labelTextSetter);
                }

            }
        }    
#endif
    }

    private void UpdateInfo(LabelTextSetter labelText)
    {
        myTouchText.text = "Obj ID:" + labelTextSetter.GetId();
        string ID = labelTextSetter.GetId();

        StartCoroutine(ShowPanelInfo());
    }

    private IEnumerator ShowPanelInfo()
    {
        openPanel = true;
        LeanTween.move(rectPanelNotification, new Vector3(xValue, 0.0f, 0f), 1f).setCanvasMoveX().setDelay(0.2f);
        yield return new WaitForSecondsRealtime(1.5f);
        openPanel = false;
    }

    private void ClosePanelInfo()
    {
        if(openPanel != true)
        {
            Debug.Log("Close Clicked");
            LeanTween.move(rectPanelNotification, new Vector3(-190.0f, 0.0f, 0f), 1f).setCanvasMoveX();
        }
    }
}
