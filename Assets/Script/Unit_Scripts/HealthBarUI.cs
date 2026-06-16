
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{

[SerializeField]private Image bar;
[SerializeField]private TextMeshProUGUI textHealth;


private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponentInParent<HealthSystem>();
        healthSystem.OnHealthChange += healthSystem_OnHealthChange;
    }

     private void healthSystem_OnHealthChange(int max, int min)
    {
        textHealth.text = $"{min}/{max}";
        
        bar.fillAmount = (float)min/max;
    }
}
