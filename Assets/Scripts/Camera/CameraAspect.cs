﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraModule
{
    public class CameraAspect : MonoBehaviour
    {
        Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
            cam.aspect = 2960 / 1440;
        }
    }
}
