using UnityEngine;
using static Tec.TecVector;

public class AI_Wizard : MonoBehaviour,IPlayerable
{
    [SerializeField] private PlayerSystem playersystem;
    public PlayerSystem PlayerSystem => playersystem;

    [SerializeField] private AISystem aIsystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersystem.GamePlayerData(GameDataBase.Instance.Ai_Wizard_info);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playersystem.isdead) { return; }
        playersystem.JumbCheck();
        playersystem.Shoot(aIsystem.DistanceCheck(aIsystem.Petrol(playersystem.RayCastCheck(aIsystem.LayerEtc, new DuoVector(Vector2.left, Vector2.right)), new DuoVector3(Vector2.left, Vector2.right)), 0.8f).Target);
    }

    void IPlayerable.FlipPlayer(bool XFlip)
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = XFlip;
    }
    void IPlayerable.DeathEvent() { playersystem.isdead = true; GameEventManager.Instance.PlayerDied(1); }
    void IPlayerable.SlashStopEvent() { playersystem.Anim.SetBool("Slash", false); }
    void IPlayerable.ShootStopEvent() { playersystem.Anim.SetBool("Shoot", false); }
}
