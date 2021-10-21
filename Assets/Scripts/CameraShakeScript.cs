using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShakeScript : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera camera;

    [SerializeField]
    private float amplitude;

    [SerializeField]
    private float frequency;

    [SerializeField]
    private float shakeTime;

    private float timeElapsed;

    private bool isShaking;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Keyboard.current.aKey.isPressed)
        {
            DoCameraShake();
        }

        if (isShaking)
        {
            timeElapsed -= Time.deltaTime;
            
            if (timeElapsed <= 0)
            {
                CinemachineBasicMultiChannelPerlin multiChannel = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                multiChannel.m_AmplitudeGain = 0;
                multiChannel.m_FrequencyGain = 0;
                this.isShaking = false;
            }
        }
    }

    public void DoCameraShake()
    {
        CinemachineBasicMultiChannelPerlin multiChannel = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        multiChannel.m_AmplitudeGain = amplitude;
        multiChannel.m_FrequencyGain = frequency;
        this.timeElapsed = shakeTime;
        this.isShaking = true;

    }
}
