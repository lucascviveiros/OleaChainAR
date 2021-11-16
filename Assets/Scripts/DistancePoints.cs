using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DistancePoints : MonoBehaviour
{
    //ARvore 1 IPB
    private double lat1 = 41.797185;
    private double lon1 = -6.770038;

    //ARvore 2 IPB
    private double lat2 = 41.796782;
    private double lon2 = -6.769779;

    //Dist AR1 AR2 49metros

    private void Start()
    {
        //Debug.Log("DISTANCIA:: " +
        //CalculateDistance(lat1, lon1, lat2, lon2)
        //);
    }

    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return 0;
        }
        else
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
        
            dist = dist * 1.609344;
            
            dist = dist / 0.001; //to metro

            Debug.Log("DISTANCIA CALCULADA:: " + dist);
            return (dist);
        }
    }

    //converts decimal degrees to radians             
    private double deg2rad(double deg)  
    {
        return (deg * Math.PI / 180.0);
    }

    //converts radians to decimal degrees             
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }

}
