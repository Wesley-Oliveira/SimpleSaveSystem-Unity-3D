using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //public delegate void CollectedCoin(int value);
    //public static event CollectedCoin OnCoinCollected;

    public static Action<int> OnCoinCollected;
    public UnityEvent ButtonPressed;

    public float moveSpeed;
    public float turnSpeed;

    void Start()
    {
        float x = PlayerPrefs.GetFloat("PlayerX", 0);
        float z = PlayerPrefs.GetFloat("PlayerZ", 0);

        transform.position = new Vector3(x, transform.position.y, z);
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        transform.Translate(0, 0, moveSpeed * z * Time.deltaTime, Space.Self);
        transform.Rotate(0, turnSpeed * x * Time.deltaTime, 0);

        if(Input.GetKeyDown(KeyCode.Return))
        {
            PlayerPrefs.SetFloat("PlayerX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerZ", transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            var coin = other.GetComponent<Coin>();
            if(OnCoinCollected != null)
            {
                OnCoinCollected.Invoke(coin.value);
            }

            Destroy(other.gameObject);
        }

        if(other.CompareTag("Button"))
        {
            if (ButtonPressed != null)
            {
                Debug.Log("Fim de jogo");
                ButtonPressed.Invoke();
            }
        }
    }
}
