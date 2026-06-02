using Unity.Mathematics;
using UnityEngine;


public class CameraMoveHandler : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 5f;
    [SerializeField]private float rotateSpeed = 5f;
    private float rotationFixedValue = 0f;

 


    private void LateUpdate()
    {       
    var inputMove = CameraInputHandler.Instance.GetCameraMovimentInputNormalized();
    float inputRotate = CameraInputHandler.Instance.GetCameraRotateInput();     
    var moveValue = new Vector3(inputMove.x,transform.position.y,inputMove.y);
    var rotateInput = new Vector3(rotationFixedValue, inputRotate,rotationFixedValue);

    Move(moveValue);
    Rotate(rotateInput);
    
        
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
