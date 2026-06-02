using UnityEngine;
using Unity.Cinemachine;


public class CameraMoveHandler : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float rotateSpeed = 5f;
    [SerializeField]private CinemachineCamera cinemachineCamera;
    private float rotationFixedValue = 0f;

 


    private void LateUpdate()
    {       
    var inputMove = CameraInputHandler.Instance.GetCameraMovimentInputNormalized();
    float inputRotate = CameraInputHandler.Instance.GetCameraRotateInput();

    var position = transform.forward * inputMove.y + transform.right * inputMove.x;
    var rotateInput = new Vector3(rotationFixedValue, inputRotate,rotationFixedValue);

    ChangeFOV(CameraInputHandler.Instance.GetCameraFOVModifier());
    Move(position);
    Rotate(rotateInput);
    
        
    }

    private void ChangeFOV(float modifier)
    {
        cinemachineCamera.Lens.FieldOfView += modifier;
    }
    private void Move(Vector3 position)
    {
        
        transform.position += position * Time.deltaTime * moveSpeed;
    }
    private void Rotate(Vector3 rotation)
    {
        transform.Rotate(rotation * Time.deltaTime * rotateSpeed);
        
    }
}
