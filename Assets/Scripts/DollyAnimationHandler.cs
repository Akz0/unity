using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class DollyAnimationHandler : MonoBehaviour
{
    public CinemachineVirtualCamera dollyTrackCamera;
    public CinemachineVirtualCamera freeLookCamera;
    public void PlayAnimation()
    {
        freeLookCamera.enabled = false;
        dollyTrackCamera.enabled = true;
    }

    public void ResetAnimation()
    {
        freeLookCamera.enabled = true;
        dollyTrackCamera.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        dollyTrackCamera.Priority = 9;
        freeLookCamera.Priority = 10;
        ResetAnimation();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
