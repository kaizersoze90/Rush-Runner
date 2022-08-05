using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] CinemachineVirtualCamera danceCam;
    [SerializeField] CinemachineVirtualCamera paintCam;

    public void SetDanceCam()
    {
        danceCam.Priority = 2;
    }

    public void SetPaintCam()
    {
        paintCam.Priority = 3;
    }
}
