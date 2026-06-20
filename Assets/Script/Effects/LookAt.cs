using System.Diagnostics;
using UnityEngine;

public class LookAt : MonoBehaviour
{
private enum LookType{
    ForCamera,
    FarWayCamera,
    Straith,
    StraithInverted
    }

    [SerializeField]private LookType look;

    private void Update()
    {
        switch (look)
        {
            case LookType.ForCamera:
            LookAtCamera();
            break;

            case LookType.FarWayCamera:
            LookAtCameraInvented();
            break;

            case LookType.Straith:
            transform.LookAt(transform.forward);
            break;

            case LookType.StraithInverted:
            transform.LookAt(transform.forward - transform.forward);
            break;
        }
    }

    private void LookAtCameraInvented()
    {
        var moveDir = transform.position - Camera.main.transform.position;
        transform.LookAt(-moveDir);
        
    }
    private void LookAtCamera()
    {
        var moveDir = transform.position - Camera.main.transform.position;
        transform.LookAt(moveDir);
    }
}
