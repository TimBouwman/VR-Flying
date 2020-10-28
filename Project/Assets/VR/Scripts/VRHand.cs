//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// 
/// </summary>
public class VRHand : MonoBehaviour
{
    #region Variables
    [SerializeField] private InputDeviceCharacteristics inputDeviceCharacteristics = InputDeviceCharacteristics.None;
    private InputDevice inputDevice;
    [Header("Model")]
    [SerializeField] private GameObject handModelPrefab = null;
    private GameObject handModel = null;
    [Header("Animations")]
    [SerializeField] private bool animated = false;
    private Animator handAnimator = null;
    [SerializeField] private string gripParameter = "";
    [SerializeField] private string triggerParameter = "";
    #endregion

    #region Unity Methods
    private void Update()
    {
        if (!inputDevice.isValid)
            TrySetup();
        else
            AnimationHandler();
    }
    #endregion

    #region Custom Methods
    private void TrySetup()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics | InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
            inputDevice = devices[0];
        
        handModel = Instantiate(handModelPrefab, this.transform);
        if (animated)
            handAnimator = handModel.GetComponent<Animator>();
    }

    private void AnimationHandler()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);

        handAnimator.SetFloat(gripParameter, gripValue);
        handAnimator.SetFloat(triggerParameter, triggerValue);
    }
    #endregion
}