using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class VirtualCameraHandler : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private CinemachineFreeLook _virtualCamera;
    private float _camXValue;

    private int _inputMode;
    
    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = GetComponent<CinemachineFreeLook>();
        if(_virtualCamera == null) Debug.Log("VirtualCamera not found");
        else _camXValue = _virtualCamera.m_XAxis.Value;
    }

    public int GetVirtXValue()
    {
        _camXValue = _virtualCamera.m_XAxis.Value;
        //virtual camera is set tro wrap X value between 0 and 360
        // between 45 and 0 or 360 and 315	= default wasd
        // between 315 and 225 			    = left is front key, back is left key
        // between 225 and 135 			    = back is front key, right is left key
        // between 135 and 45			    = right is front key, front is left key
        if (_camXValue is < 45 and >= 0 or < 360 and >= 315)
        {
            //This is sent as a int to the PlayerController, use numpad as reference:
            //8 forward, 4 left, 6 right, 2 back for "forward" direction mapping.
            _inputMode = 8;
        }
        else if (_camXValue is < 315 and >= 225)
        {
            _inputMode = 4;
        }
        else if (_camXValue is < 225 and >= 135)
        {
            _inputMode = 2;
        }
        else if (_camXValue is < 135 and >= 45)
        {
            _inputMode = 6;
        }

        // Debug.Log("_camXValue is: " + _camXValue + ", inputmode sent is: " + _inputMode);

        return _inputMode;
    }
}