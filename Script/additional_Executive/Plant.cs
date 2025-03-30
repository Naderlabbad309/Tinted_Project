using UnityEngine;

public class Plant : MonoBehaviour, IPlayerable
{
    [SerializeField] private PlayerSystem playersystem;
    public PlayerSystem PlayerSystem => playersystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IPlayerable.FlipPlayer(bool XFlip) { }
    void IPlayerable.DeathEvent() { GameEventManager.Instance.TriggerHealthEvent(1);}
    void IPlayerable.SlashStopEvent() { }
    void IPlayerable.ShootStopEvent() { }
}
