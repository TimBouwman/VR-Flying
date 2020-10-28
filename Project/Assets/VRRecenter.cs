//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// 
/// </summary>
public class VRRecenter : MonoBehaviour
{
    #region Variables
    [SerializeField] private InputDeviceCharacteristics inputDeviceCharacteristics = InputDeviceCharacteristics.None;
    private InputDevice inputDevice;
    [SerializeField] private Transform xRRig = null;
    [SerializeField] private Transform xRCamera = null;
    #endregion

    #region Unity Methods
    private void Start()
    {
        if (!xRRig)
            xRRig = this.transform;
    }
    private void Update()
    {
        if (!inputDevice.isValid)
            TrySet();
        else
            Recenter();
    }
    #endregion

    #region Custom Methods
    private void TrySet()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics | InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
            inputDevice = devices[0];
    }
    private void Recenter()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue);

        if (secondaryButtonValue)
        {
            //center position
            xRRig.localPosition = xRCamera.localPosition * -1;

            //center rotation
            xRRig.localEulerAngles = new Vector3(0, xRCamera.localEulerAngles.y * -1, 0);
        }
    }
    #endregion
}