//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class FlightController : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform inputObject = null;
    [Header("Pitch")]
    [SerializeField] private float minPitchInputRecognition = 0.03f;
    [SerializeField] private float maxPitchInputRecognition = 0.2f;
    [SerializeField] private float pitchSpeed = 5f;
    [Header("Roll")]
    [SerializeField] private float minRollInputRecognition = 5f;
    [SerializeField] private float maxRollInputRecognition = 30f;
    private float maxRollInputQuaternion = 0f;
    [SerializeField] private float rotateSpeed = 5f;
    #endregion

    #region Unity Methods
    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        Pitch();
        Roll();
    }
    #endregion

    #region Custom Methods
    private void Setup()
    {
        inputObject.localEulerAngles = new Vector3(0, 0, maxRollInputRecognition);
        maxRollInputQuaternion = inputObject.localRotation.z;
        inputObject.localEulerAngles = Vector3.zero;
    }

    private void Pitch()
    {
        float y = Mathf.Clamp(inputObject.localPosition.y, -0.2f, 0.2f);
        float multiplier = 1 / maxPitchInputRecognition * y;

        if (y > minPitchInputRecognition || y < -minPitchInputRecognition)
            transform.Rotate(Vector3.right * (-pitchSpeed * multiplier) * Time.deltaTime);
    }
    private void Roll()
    {
        float z = inputObject.localEulerAngles.z;
        float multiplier = 1 / maxRollInputQuaternion * inputObject.localRotation.z;

        if ((z > minRollInputRecognition && z < 180) || (z < 360 - minRollInputRecognition && z > 180))
            transform.Rotate(Vector3.forward * (rotateSpeed * multiplier) * Time.deltaTime);
    }
    #endregion
}