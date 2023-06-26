using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currencyUI;
    [SerializeField] private Animator animator;
    [SerializeField] private bool MenuActive = false;

    public void ToggleShopMenu()
    {
        MenuActive = !MenuActive;
        animator.SetBool("MenuActive", MenuActive);
    }

    private void OnGUI()
    {
        currencyUI.text = "$" + GameManager.Instance.currency.ToString() + " / $" + GameManager.Instance.goal.ToString();
    }
}
