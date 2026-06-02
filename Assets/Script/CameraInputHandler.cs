using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{
public static CameraInputHandler Instance;
  InputSystem_Actions inputActionCamera;

    private void Awake()
    {
        if(!Instance.IsUnityNull() && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        inputActionCamera = new InputSystem_Actions();
    }
    private void Start()
    {
        inputActionCamera.Enable();
    }
    void Update()
    {
        Debug.Log(GetCameraFOVModifier());
    }

    public float GetCameraFOVModifier()
    {
        return inputActionCamera.Camera.Zoom_InOut.ReadValue<float>();
    }
    public Vector2 GetCameraMovimentInputNormalized()
    {
        return inputActionCamera.Camera.Moviment.ReadValue<Vector2>();
    }
    public float GetCameraRotateInput()
    {
        return inputActionCamera.Camera.Rotate.ReadValue<float>();
    }
}

