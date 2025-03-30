using UnityEngine;
using static Tec.TecVector;


public interface ISearchable
{
    public bool IsInLayerMask(GameObject obj, LayerMask mask);
}
public interface IPlayerable
{
    PlayerSystem PlayerSystem { get; }  // Interface Property
    public void DeathEvent();
    public void SlashStopEvent();
    public void ShootStopEvent();
    public void FlipPlayer(bool XFlip = false);
}
public interface IRayCastable
{
    public GameObject RayCastCheck(LayerMask enemysLayers, DuoVector duo);
}
public interface IAIable
{
    public GameObject Petrol(GameObject RayCast, DuoVector3 duoVector3LR);
    public DuoGameObject DistanceCheck(GameObject Target, float Distance_);
    public GameObject Pursuit(DuoGameObject Target_, DuoVector duoVectorLR, int Damageamount);

}

public interface IMarketable
{
   public void DoSomethink(GameObject Target);
   PlayerSystem PlayerSystem { get; }
   MarketSystem Marketsystem { get; }

}

