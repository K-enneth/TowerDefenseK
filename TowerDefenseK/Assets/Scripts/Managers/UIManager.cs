using System;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public Slider healthSlider;
    
    public void OnEnable()
    {
        PlayerBase.OnHurt += ChangeLife;
        CoinManager.CoinCountChanged += UpdateMoney;
    }

    public void OnDisable()
    {
        PlayerBase.OnHurt -= ChangeLife;
        CoinManager.CoinCountChanged -= UpdateMoney;
    }

    private void ChangeLife(int health)
    {
        healthSlider.value = health;
    }

    private void UpdateMoney(int money)
    {
        coinText.text = "$"+ money;
    }
}
