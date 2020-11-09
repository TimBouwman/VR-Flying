//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class FlightControllerV2 : MonoBehaviour
{
    #region Variables
    [Header("Steering")]
    [SerializeField] private Transform diraction = null;
    [SerializeField] private Transform inputObject = null;
    [SerializeField] private Transform camera = null;
    [Range(0f, 0.1f)]
    [SerializeField] private float lerpPosSpeed = 0.03f;
    [Range(0f, 0.1f)]
    [SerializeField] private float lerpRotSpeed = 0.06f;
    [Header("Speed")]
    [SerializeField] private float movementSpeed = 1f;
    private Rigidbody rb;
    #endregion

    #region Unity Methods
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }
    #endregion
     
    #region Custom Methods
    private void Movement()
    {
        diraction.Lerp(inputObject, lerpPosSpeed, lerpRotSpeed);

        //rb.AddForce(diraction.forward);
    }
    #endregion
}