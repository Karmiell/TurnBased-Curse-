using Unity.VisualScripting;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance;
    [SerializeField] private LayerMask layerMaskGround;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public RaycastHit GetWorldMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMaskGround);
        return hit;
    }
}
