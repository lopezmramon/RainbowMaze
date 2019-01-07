using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;

    private void Awake()
    {
        CodeControl.Message.AddListener<SwitchCameraRequestEvent>(OnSwitchCameraRequested);
        CodeControl.Message.AddListener<LevelIntroRequestEvent>(OnLevelIntroRequested);
    }

    private void OnLevelIntroRequested(LevelIntroRequestEvent obj)
    {
        CinemachineBrain.SoloCamera = virtualCameras[0];
    }

    private void OnSwitchCameraRequested(SwitchCameraRequestEvent obj)
    {
        CinemachineBrain.SoloCamera = virtualCameras[(object)CinemachineBrain.SoloCamera == virtualCameras[0] ? 1 : 0];
    }
}
