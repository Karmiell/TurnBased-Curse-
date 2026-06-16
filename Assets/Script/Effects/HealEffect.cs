using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HealEffect : MonoBehaviour
{
private Light lightEffect;
private float timerMax = 5f;
private float timerAtual = 0f;
private bool activeHeling;
private GridPosition gridPosition;

    private void Awake()
    {
        lightEffect = GetComponentInChildren<Light>();
        lightEffect.intensity = 5f;
         gridPosition = LevelGrid.Instance.meuGrid.GetGridPosition(transform.position);
    }


    private void Update()
    {
    timerAtual += Time.deltaTime;
    if(timerAtual <= timerMax)
        {
            var modifier = timerAtual / timerMax;
            lightEffect.intensity = math.lerp(0f,5f,modifier);
            
            if(timerAtual > timerMax-1)
            {
                activeHeling = true;
                ApllyHeling();
            }
        }else Destroy(gameObject);
    }

    private void ApllyHeling()
    {
        if (activeHeling)
        {
            
            LevelGrid.Instance.meuGrid.GetGridObject(gridPosition).GetDamageblesList()[0].TakeDamage(-10);
            timerAtual++;
            activeHeling = false;
          
        }
    
    }
    
}
