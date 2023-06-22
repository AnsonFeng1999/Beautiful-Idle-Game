using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyUI;

    private void OnGUI()
    {
        currencyUI.text = GameManager.Instance.currency.ToString();
    }
}
