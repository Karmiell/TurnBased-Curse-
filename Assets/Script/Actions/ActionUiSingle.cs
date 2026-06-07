using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionUiSingle : MonoBehaviour
{
    private TextMeshProUGUI uGUI;
    private BaseAction baseAction;
    private Button button;

    private void Awake()
    {
    uGUI = GetComponentInChildren<TextMeshProUGUI>();
    if(!TryGetComponent<Button>(out button))Debug.Log($"Componente Button não encontrado no : {gameObject.name}");
    }

    private void Start()
    {
        button.onClick.AddListener(()=> 
        {HandlerSelection.Instance.SetActionSelect(baseAction);});
        
        
    }

    public void SetBaseAction(BaseAction baseAction)
    {
      uGUI.text = baseAction.GetNameAction().ToUpper();
    
    this.baseAction = baseAction;
    }
    
}
