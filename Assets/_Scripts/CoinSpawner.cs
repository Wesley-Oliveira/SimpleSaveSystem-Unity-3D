using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinSpawner : MonoBehaviour
{
    public int maxCoinValue;
    public int numberOfCoins;
    public Coin coinPrefab;
    private List<Coin> coins = new List<Coin>();
    
    public UnityEvent OnEndGame;
    private int aux = 0;

    void Start()
    {
        if(PlayerPrefs.HasKey("SavedCoins"))
        {
            GetSavedInstantiate();
            Debug.Log("Instantiate saved");
        }
        else
        {
            InstantiateCoins();
            Debug.Log("Instantiate random");
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetInt("SavedCoins", 1);
            foreach(Coin coin in coins)
            {
                PlayerPrefs.SetFloat("CoinX" + aux, coin.transform.position.x);
                PlayerPrefs.SetFloat("CoinZ" + aux, coin.transform.position.z);
                aux++;
            }
            PlayerPrefs.SetInt("TotalCoins", aux);
            Debug.Log("Save all keys");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("All keys deleted");
        }
    }

    public void GetSavedInstantiate()
    {
        if(PlayerPrefs.GetInt("SavedCoins") == 1)
        {
            for(var i = 0; i < PlayerPrefs.GetInt("TotalCoins"); i++)
            {
                var position = new Vector3(PlayerPrefs.GetFloat("CoinX" + aux), 0.5f, PlayerPrefs.GetFloat("CoinZ" + aux));
                var coin = Instantiate(coinPrefab, position, Quaternion.identity);
                aux++;

                var value = Random.Range(1, maxCoinValue);
                coin.SetupCoin(value, OnCoinDestroyed);
                coins.Add(coin);
            }
        }
    }

    public void InstantiateCoins()
    {
        for (var i = 0; i < numberOfCoins; i++)
        {
            var position = new Vector3(Random.Range(-6f, 6f), 0.5f, Random.Range(-12f, 2f));
            var coin = Instantiate(coinPrefab, position, Quaternion.identity);

            var value = Random.Range(1, maxCoinValue);
            coin.SetupCoin(value, OnCoinDestroyed);
            coins.Add(coin);
        }
    }

    void OnCoinDestroyed(Coin coin)
    {
        coins.Remove(coin);
        if(coins.Count == 0)
        {
            if(OnEndGame != null)
            {
                //Debug.Log("Fim de jogo");
                OnEndGame.Invoke();
            }
        }
    }
}
