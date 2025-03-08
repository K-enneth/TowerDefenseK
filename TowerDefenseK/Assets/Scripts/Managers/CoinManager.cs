using UnityEngine;

namespace Managers
{
    public class CoinManager : MonoBehaviour
    {
        public static CoinManager instance;
        public int coinCount;

        #region Delegate and event
            public delegate void OnCoinCount(int count);
            public static event OnCoinCount CoinCountChanged;
        #endregion
        

        private void Awake()
        {
            if(instance == null)instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            //Sends event to update Canvas and to check if all bombs can be placed
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
