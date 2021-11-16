using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;
using System;
using UnityEngine.Events;

public class TrackedManager : DefaultObserverEventHandler
{
    private ObserverBehaviour myCustomObserver;
    protected TargetStatus mPreviousTargetStatus = TargetStatus.NotObserved;

    private TargetStatus targetStatus;

    private ARListManager arListManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ARListObj = new GameObject();
        ARListObj = GameObject.Find("ARList");
        arListManager = ARListObj.gameObject.GetComponent<ARListManager>();

        myCustomObserver = GetComponent<ObserverBehaviour>();

        if (myCustomObserver)
        {
            myCustomObserver.OnTargetStatusChanged += OnCustomObserverStatusChanged;
            myCustomObserver.OnBehaviourDestroyed += OnObserverDestroyed;

            OnCustomObserverStatusChanged(myCustomObserver, myCustomObserver.TargetStatus);
        }
    }
    private void OnCustomObserverStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        var name = myCustomObserver.TargetName;
  
        if (name.ToString() == "QR1" && (myCustomObserver.TargetStatus.Status.ToString() == "TRACKED" || myCustomObserver.TargetStatus.Status.ToString() == "DETECTED" 
            || myCustomObserver.TargetStatus.Status.ToString() == "EXTENDED_TRACKED"))
        {
            arListManager.CurrentTracking("Olival 1");
        }
        else if (name.ToString() == "QR2" && (myCustomObserver.TargetStatus.Status.ToString() == "TRACKED" || myCustomObserver.TargetStatus.Status.ToString() == "DETECTED"
            || myCustomObserver.TargetStatus.Status.ToString() == "EXTENDED_TRACKED"))
        {
            arListManager.CurrentTracking("Olival 2");
        }
        else if (myCustomObserver.TargetStatus.Status.ToString() == "NO_POSE")
        {
            arListManager.NoTracking();
        }

        HandleTargetStatusChanged(mPreviousTargetStatus.Status, targetStatus.Status);
        HandleTargetStatusInfoChanged(targetStatus.StatusInfo);

        mPreviousTargetStatus = targetStatus;
    }

    protected virtual void HandleTargetStatusChanged(Status previousStatus, Status newStatus)
    {
        var shouldBeRendererBefore = ShouldBeRendered(previousStatus);
        var shouldBeRendererNow = ShouldBeRendered(newStatus);
        if (shouldBeRendererBefore != shouldBeRendererNow)
        {
            if (shouldBeRendererNow)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }
        else
        {
            if (!mCallbackReceivedOnce && !shouldBeRendererNow)
            {
                // This is the first time we are receiving this callback, and the target is not visible yet.
                // --> Hide the augmentation.
                OnTrackingLost();
            }
        }

        mCallbackReceivedOnce = true;
    }

    void OnObserverDestroyed(ObserverBehaviour observer)
    {
        myCustomObserver.OnTargetStatusChanged -= OnCustomObserverStatusChanged;
        myCustomObserver.OnBehaviourDestroyed -= OnObserverDestroyed;
        myCustomObserver = null;
    }


}
