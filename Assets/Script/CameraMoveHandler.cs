using UnityEngine;
using Unity.Cinemachine;
using Unity.Mathematics;


public class CameraMoveHandler : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float rotateSpeed = 5f;
    [SerializeField]private float zoomSpeed = 50f;
    [SerializeField]private CinemachineCameraOffset cinemachineCamera;
    private float rotationFixedValue = 0f;

    private float zoomInMaxZ = 8f;
    private float zoomOutMaxZ = -10f;

 


    private void LateUpdate()
    {       
    var inputMove = CameraInputHandler.Instance.GetCameraMovimentInputNormalized();
    float inputRotate = CameraInputHandler.Instance.GetCameraRotateInput();

    var position = transform.forward * inputMove.y + transform.right * inputMove.x;
    var rotateInput = new Vector3(rotationFixedValue, inputRotate,rotationFixedValue);

    ChangeZoom(CameraInputHandler.Instance.GetCameraFOVModifier());
    Move(position);
    Rotate(rotateInput);
    
        
    }

    private void ChangeZoom(float modifier)
    {  
        if(modifier == 0f)return;
       cinemachineCamera.Offset.z -= modifier * Time.deltaTime * zoomSpeed;
       
        cinemachineCamera.Offset.z = math.clamp(cinemachineCamera.Offset.z,zoomOutMaxZ,zoomInMaxZ);   
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
