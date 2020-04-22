using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private float v;
        private float h;

        [SerializeField]
        private AudioSource source;
        private float topSpeed;
  

        public float V {
            get { return v; }
        }
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            topSpeed = m_Car.m_Topspeed;
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
             h = CrossPlatformInputManager.GetAxis("Horizontal");
             v = CrossPlatformInputManager.GetAxis("Vertical");

             //h = Input.GetAxis("Horizontal");
             //v = Input.GetAxis("Vertical");

            h = Mathf.Clamp(h, -0.6f, 0.6f);



#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);

#endif
            source.pitch = 0.75f + 0.3f / topSpeed * Mathf.Abs(v) + m_Car.m_Topspeed;
        }
    }
}
