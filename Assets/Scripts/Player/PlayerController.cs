// ReSharper disable RedundantUsingDirective

using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private SetLanguage _setLanguageScript;

    [SerializeField] private float _speed = 20;
    
    private AudioSource _sndFragmentPickup;
    private AudioSource _sndGong;

    private Rigidbody _rigidbodyPlayer;
    private Vector3 _mainCamDir;

    private float _mInputHorizontal;
    private float _mInputVertical;

    private InputMaster _inputMaster;
    private Vector3 _playerMovement = Vector3.zero;
    
    private void Awake()
    {
        //Using Unity's new input manager, and adding method subscribers to the event calls.
        //(InputMaster asset in /Assets)
        
        _inputMaster = new InputMaster();

        _inputMaster.Player.MoveHorizontal.performed += mInputHor 
            => SetHorMovement(mInputHor.ReadValue<float>());
        _inputMaster.Player.MoveVertical.performed += mInputVer 
            => SetVerMovement(mInputVer.ReadValue<float>());
        _inputMaster.Player.BrakeBall.performed += mInput => StopMoving();
        
        _inputMaster.Player.PauseGame.performed += mInput => _gameManager.PauseGame();
        _inputMaster.Player.ToggleLang.performed += mInput => _setLanguageScript.ToggleLang();
        _inputMaster.Player.Retry.performed += mInput => ReloadScene();
    }

    void Start()
    { 
        _rigidbodyPlayer = GetComponent<Rigidbody>();

        if (Camera.main != null)
        {
            _mainCamDir = Camera.main.transform.forward;
        }
        
        _sndFragmentPickup = GameObject.Find("sndFragment").GetComponent<AudioSource>();
        _sndGong = GameObject.Find("sndGong").GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //Inputs will modify the _playerMovement v3 in the appropriate axis
        //Then this will add it as torque *10 * _speed (by default 50) 
        if (_playerMovement != Vector3.zero)
        {
            _rigidbodyPlayer.AddTorque(new Vector3(_playerMovement.x, _playerMovement.y, 
                _playerMovement.z) * _speed, ForceMode.Acceleration);
        }
        
        if (Camera.main != null)
        {
            _mainCamDir = Camera.main.transform.forward;
            
            Vector3 controlDirection = new Vector3(_mInputVertical, 0, _mInputHorizontal);
            _playerMovement = Camera.main.transform.TransformDirection(controlDirection);
        }

        //Debug.DrawLine (pos, pos + dir * 10, Color.blue, 1f);
        //Debug.DrawLine(pos, pos + _mainCamDir * 10, Color.red, 1f);
    }

    private void SetHorMovement(float mInputHor)
    {
        _mInputHorizontal = mInputHor;
    }
    
    private void SetVerMovement(float mInputVer)
    {
        _mInputVertical = mInputVer;
    }

    private void StopMoving()
    {
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