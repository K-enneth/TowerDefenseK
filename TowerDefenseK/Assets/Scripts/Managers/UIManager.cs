using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
            public TextMeshProUGUI coinText;
            public Slider healthSlider;
        #endregion
    
        //Manages UI updates of life and money
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
}
