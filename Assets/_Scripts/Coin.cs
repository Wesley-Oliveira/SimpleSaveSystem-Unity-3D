using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void CoinCallback(Coin coin);
    private CoinCallback callback;

    public float turnSpeed = 150;
    public int value;

    public void SetupCoin(int value, CoinCallback callback = null)
    {
        this.value = value;
        this.callback = callback;
    }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * turnSpeed, 0);
    }

    void OnDestroy()
    {
        if(callback != null)
        {
            callback.Invoke(this);
        }
    }
}
