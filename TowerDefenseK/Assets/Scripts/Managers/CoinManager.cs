using UnityEngine;

namespace Managers
{
    public class CoinManager : MonoBehaviour
    {
        public static CoinManager instance;
        public delegate void OnCoinCount(int count);
        public static event OnCoinCount CoinCountChanged;
        
        public int coinCount;

        private void Awake()
        {
            if(instance == null)instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            CoinCountChanged?.Invoke(coinCount);
        }
        

        public void AddCoin(int amount)
        {
            coinCount += amount;
            CoinCountChanged?.Invoke(coinCount);
        }

        public void WasteCoin(int amount)
        {
            coinCount -= amount;
            CoinCountChanged?.Invoke(coinCount);
        }
    }
}
