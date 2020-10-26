//author: Tim Bouwman
//Github: https://github.com/TimBouwman
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Grip : MonoBehaviour
{
    #region Variables
    private Transform index;

    #endregion

    #region Unity Methods
    private void Start()
    {
        index = this.transform.GetChild(0).transform;
    }

    private void Update()
    {
        
    }
    #endregion

    #region Custom Methods
    private void Input()
    {

    }
    #endregion
}