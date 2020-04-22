using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
public class Axelerator : MonoBehaviour
{

    [SerializeField] private float topSpeed = 1;
    [SerializeField] private float delay = 2;
    private float oldTopSpeed = 0.25f;
    private GameObject car;
    private CarUserControl carControll;
    private void Start()
    {
        car = GameObject.Find("Dodge");
        carControll = car.GetComponent<CarUserControl>();
    }

    private void OnTriggerStay(Collider other)
    {
        //car = other.gameObject;
        float angle = Vector3.Angle(car.transform.forward, transform.forward);
        if (angle > 90 || carControll.V <= 0) return;
        Debug.Log(angle);
        Debug.Log(car.name);
        car.GetComponent<CarController>().m_Topspeed = topSpeed;
        Invoke("SetOldTopSpeed", delay);

       
    }

    private void SetOldTopSpeed()
    {
        car.GetComponent<CarController>().m_Topspeed = oldTopSpeed;
    }

}
