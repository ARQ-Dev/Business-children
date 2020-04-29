using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    [SerializeField]
    private ScreenOrientation _orientation = ScreenOrientation.Portrait;

    private void Start()
    {
        Screen.orientation = _orientation;
    }

}
