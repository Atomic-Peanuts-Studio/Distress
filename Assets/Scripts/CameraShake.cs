using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance { get; private set; } 
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;
    // Start is called before the first frame update

    private void Awake()
    {
     
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        instance = this;
    }
   
    public void ShakeCamera (float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin m = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        m.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin m = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                m.m_AmplitudeGain = 0;
            }
        }

    }
}
