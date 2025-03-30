using System;
using UnityEngine;
using UnityEngine.UI;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance;
    public event Action OnPlayerDeath;
    public event Action<int> OnHealthEvent;

    [SerializeField] private int Coin = 0;
    [SerializeField] private Text TextCoin;
    [SerializeField] private GameObject Menu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(TextCoin){ TextCoin.text = Coin.ToString();}

        if (Application.isEditor) return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerDied(int coin_)
    {
        Coin += coin_;
        Debug.Log("+Coin " + Coin);
        TextCoin.text = Coin.ToString();
        // Notify all listeners (e.g., UI)
        OnPlayerDeath?.Invoke();
    }

    public int GetCoinCount()
    {
        return Coin;
    }
    public void SubtractCoinCount(int amount)
    {
        Coin -= amount;
        TextCoin.text = Coin.ToString();
    }
    public void FloorPass(PlayerSystem playerSystem_)
    {
        if(playerSystem_.Health < 3) { playerSystem_.Health++; }   
         playerSystem_.ShootSlider.value++;
    }
    public void MainPlayerDied()
    {
        Menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void TriggerHealthEvent(int healthAmount)
    {
        OnHealthEvent?.Invoke(healthAmount);
    }
}
