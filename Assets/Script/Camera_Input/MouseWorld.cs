using Unity.VisualScripting;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld Instance;
    [SerializeField]public LayerMask layerMaskDefaut;

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

    public RaycastHit GetWorldMousePosition( LayerMask layerMask = default)
    {
        if(layerMask == default)layerMask = layerMaskDefaut;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask);
        return hit;
    }
}
