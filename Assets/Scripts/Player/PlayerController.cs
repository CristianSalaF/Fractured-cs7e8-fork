// ReSharper disable RedundantUsingDirective

using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //unused due to bug, check GameManager.cs for details.
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SetLanguage _setLanguageScript;

    [SerializeField] private float _speed = 20;
    [SerializeField] private VirtualCameraHandler _freeLookCamera;
    
    //audio sources
    private AudioSource _sndFragmentPickup;
    private AudioSource _sndGong;

    private Rigidbody _rigidbodyPlayer;

    private int _playerInputMode = 8;

    private InputMaster _inputMaster;
    private Vector3 _playerMovement = Vector3.zero;
    
    private void Awake()
    {
        //Using Unity's new input manager, and adding method subscribers to the event calls.
        //(InputMaster asset in /Assets)
        
        _inputMaster = new InputMaster();

        _inputMaster.Player.MoveHorizontal.performed += mInput 
            => MovePlayer(mInput.ReadValue<float>(), true);
        _inputMaster.Player.MoveVertical.performed += mInput 
            => MovePlayer(mInput.ReadValue<float>(), false);
        _inputMaster.Player.BrakeBall.performed += mInput => StopMoving();
        
        //check GameManager.cs for details about the bug.
        //handling the calls from player's input instead so it at least is playable
        // _inputMaster.Player.PauseGame.performed += mInput => _gameManager.PauseGame();
        _inputMaster.Player.ToggleLang.performed += mInput => _setLanguageScript.ToggleLang();
        _inputMaster.Player.Retry.performed += mInput => ReloadScene();
        _inputMaster.Player.QuitGame.performed += mInput => Application.Quit();
    }

    void Start()
    { 
        _rigidbodyPlayer = GetComponent<Rigidbody>();
        
        _sndFragmentPickup = GameObject.Find("sndFragment").GetComponent<AudioSource>();
        _sndGong = GameObject.Find("sndGong").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //Inputs will modify the _playerMovement v3 in the appropriate axis
        //Then this will add it as torque *10 * _speed (by default 50) 
        if (_inputMaster.Player.MoveHorizontal.inProgress || _inputMaster.Player.MoveVertical.inProgress)
        {
            //TODO: Change .AddTorque for .Velocity += (same vector input, multiplied by 1 to 5 max)
            //TODO: Input directions will become messed up, re-sort the directions.
            //Debug.Log("Trying to move! X :" + _playerMovement.x + ", Z: " + _playerMovement.z);
                _rigidbodyPlayer.AddTorque(_playerMovement * _speed, ForceMode.Acceleration);
        }
    }

    private void MovePlayer(float mInput, bool horizontal)
    {
        if (horizontal)
        {
            _playerMovement = new Vector3(_playerMovement.x, _playerMovement.y, mInput);
        }
        else
        {
            _playerMovement = new Vector3(mInput, _playerMovement.y, _playerMovement.z);
        }

        //Debug.Log("X: " + _playerMovement.x + ", Y: " + _playerMovement.y + ", Z: " + _playerMovement.z);
        
        _playerInputMode = _freeLookCamera.GetVirtXValue();
        _playerMovement = ParseInput();
    }

    private Vector3 ParseInput()
    {
        //using bloq num as reference: 4=Left, 6=Right, 8=Up, 2=down), obtained from CameraController script
        //This is sent as a int from the VirtualCameraHandler, use numpad as reference:
        //8 forward, 4 left, 6 right, 2 back for "forward" camera/movement direction mapping.
        Vector3 parsedInput = Vector3.zero;
        
        switch (_playerInputMode)
        {
            //x default is vertical axis, Z is horizontal
            //x+ forward, x- back;
            //z+ = left, z- = right;
            
            case 4:
                //up is left
                parsedInput = new Vector3((_playerMovement.z * -1),_playerMovement.y,_playerMovement.x);
                break;
            case 6:
                //up is right
                parsedInput = new Vector3(_playerMovement.z,_playerMovement.y,(_playerMovement.x * -1));
                break;
            case 8:
                //up is up
                parsedInput = _playerMovement;
                break;
            case 2:
                //up is down
                parsedInput = new Vector3((_playerMovement.x * -1), _playerMovement.y, (_playerMovement.z * -1));
                break;
            
        }
        
        // Debug.Log("inputmode (_forwardDirection) is: " + _playerInputMode);
        
        return parsedInput;
    }

    private void StopMoving()
    {
        _playerInputMode = _freeLookCamera.GetVirtXValue();
        
        //Debug.Log("Stopping movement");
        _playerMovement = Vector3.zero;
        
        //Stop rotating
        _rigidbodyPlayer.angularVelocity = Vector3.zero;
    }

    public void PlayPickupSFX()
    {
        _sndFragmentPickup.Play();
    }

    public void PlayGongSFX()
    {
        _sndGong.Play();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Disable();
    }
}