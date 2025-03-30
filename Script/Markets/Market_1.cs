using UnityEngine;
using static Tec.TecVector;

[RequireComponent(typeof(SoundRegis))]
public class Market_1 : MonoBehaviour,IMarketable
{
    [SerializeField] private PlayerSystem playersystem;
    public PlayerSystem PlayerSystem => playersystem;

    [SerializeField] private MarketSystem marketsystem;
    public MarketSystem Marketsystem => marketsystem;

    public Animator Dooranim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LateUpdate()
    {
        ((IMarketable)this).DoSomethink(marketsystem.CheckCoin(marketsystem.IFGameObject(playersystem.RayCastCheck(playersystem.EnemysLayers,new DuoVector(Vector2.left,Vector2.right))),4));
    }
    void IMarketable.DoSomethink(GameObject Target)
    {
        if (!Target)return;
        Dooranim.SetBool("Door_Open",true);
        GameEventManager.Instance.SubtractCoinCount(4);
        gameObject.GetComponent<SoundRegis>().Market();
    }
}
