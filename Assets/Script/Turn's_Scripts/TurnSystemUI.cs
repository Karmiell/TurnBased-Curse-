using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI numberTurn;
    [SerializeField]private Button endTurnButton;

    private void Awake()
    {
        endTurnButton = GetComponentInChildren<Button>();
    }
    private void Start()
    {
    endTurnButton.onClick.AddListener(() =>
    TurnSystem.Instance.NextTurn()
    );
    TurnSystem.Instance.OnTurnChange += TurnSystem_OnTurnChange;
    UpdateVisual();
    }

    private void TurnSystem_OnTurnChange(object sender, EventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        if(TurnSystem.Instance.GetUnitTurnBool())endTurnButton.gameObject.SetActive(true);
        else endTurnButton.gameObject.SetActive(false);
        numberTurn.text = $"Turno Atual: {TurnSystem.Instance.GetCurrentTurnInt()}";
    }

}
