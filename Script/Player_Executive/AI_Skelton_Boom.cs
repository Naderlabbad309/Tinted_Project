using UnityEngine;
using static Tec.TecVector;

public class AI_Skelton_Boom : MonoBehaviour, IPlayerable
{

    [SerializeField] private PlayerSystem playersystem;
    public PlayerSystem PlayerSystem => playersystem;

    [SerializeField] private AISystem aIsystem;
    [SerializeField] private GameObject Boom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Awake()
    {
        playersystem.GamePlayerData(GameDataBase.Instance.Ai_Skelton_Boom_info);
    }
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (playersystem.isdead) { return; }
        playersystem.JumbCheck();
        playersystem.KillSelf(aIsystem.Pursuit(aIsystem.DistanceCheck(aIsystem.Petrol(playersystem.RayCastCheck(aIsystem.LayerEtc, new DuoVector(Vector2.left, Vector2.right)), new DuoVector3(Vector2.left, Vector2.right)), 0.8f), new DuoVector(Vector2.left, Vector2.right), GameDataBase.Instance.Ai_Skelton_Boom_info.Damage_info));
    }
    
    void LateUpdate()
    {

    }
    void IPlayerable.FlipPlayer(bool XFlip)
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = XFlip;
    }
    void IPlayerable.DeathEvent() {playersystem.isdead = true; GameEventManager.Instance.PlayerDied(1); Boom.SetActive(false); }
    void IPlayerable.SlashStopEvent() { playersystem.Anim.SetBool("Slash", false); }
    void IPlayerable.ShootStopEvent() {}

}
