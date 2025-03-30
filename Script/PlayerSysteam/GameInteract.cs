using UnityEngine;
using UnityEngine.UI;
using Tec.RayCheck;
using Tec.DistanceCheck;
using Tec.MovmentSysteam;
using static Tec.TecVector;

[System.Serializable]
public class PlayerSystem : IRayCastable, ISearchable
{

    [SerializeField] private int Health_;
    [SerializeField] private int MaxHealth_;

    [SerializeField] public Animator Anim;

    [SerializeField] public Slider HealthSlider;
    [SerializeField] public Slider ShootSlider;

    [SerializeField] public float Speed = 1;
    [SerializeField] public float JumpeSpeed = 10;

    [SerializeField] public Rigidbody2D Rig2d;

    [SerializeField] public GameObject GameObjectSelf;

    [SerializeField] public LayerMask EnemysLayers; // Assign this in the Inspector

    [SerializeField] public float RayOffset = 0.5f; // Assign this in the Inspector
    [SerializeField] public float RayDistance = 1f; // Assign this in the Inspector
    [SerializeField] public bool isdead = false; // Assign this in the Inspector
    [SerializeField] public GameObject Shoot_Efect;
    bool isTrgert = false;
    public PlayerSystem(int health, int maxhealth, Animator anim, Slider healthSlider, Slider shootSlider, float speed, float jumpeSpeed, Rigidbody2D rig2d, GameObject gameobjectself, LayerMask enemysLayers) 
    {
        Health = health;
        Anim = anim;
        HealthSlider = healthSlider;
        ShootSlider = shootSlider;
        Speed = speed;
        JumpeSpeed = jumpeSpeed;
        Rig2d = rig2d;
        GameObjectSelf = gameobjectself;
        EnemysLayers = enemysLayers;
    }
    public int Health
    {
        get 
        {
            return Health_;
        }
        set
        {
            Health_ = value;
            HealthSlider.value = Health_;
            if (Health <= 0) { Debug.Log("death"); Anim.SetBool("Death", true); }
        }

    }

    public void DoDamage(int damage)
    {
        Health -= damage;
    }
    public DuoGameObject Hit(DuoGameObject Target_,int Damage,string LayerDestroyer)
    {
        if (Target_.Target && DistanceCheckSystem.DistanceObject(Target_.Target,GameObjectSelf,1) && Target_.Target.layer == LayerMask.NameToLayer(LayerDestroyer)) {DestorySelf(true);} 
        if (Target_.Target && Target_.Found)
        {
            Target_.Target.gameObject.GetComponent<IPlayerable>().PlayerSystem.DoDamage(Damage);
            if(Damage != 999) RigForceSide(Target_.Target);
        }
        return new DuoGameObject(Target_.Target, Target_.Found);
    }
    public DuoGameObject RayToHit(GameObject Target)
    {
        bool Value;
        if (!Target) return new DuoGameObject(null, false);
        Value = true;
        return new DuoGameObject(Target, Value);
    }
    public void DestorySelf(bool Value)
    {
        if(!Value) return;
        GameObject.Destroy(GameObjectSelf);
    }
    public void KillSelf(bool Value)
    {
        if (!Value) return;
        GameObjectSelf.GetComponent<IPlayerable>().PlayerSystem.DoDamage(999);
    }
    public DuoGameObject MoveINTO(GameObject MovingTarget, DuoGameObject Target, bool Direction, float speed, DuoVector3 duoVector3)
    {
        Vector3 vectorDirection;
        if (isTrgert)
        {
            vectorDirection = Direction ? duoVector3.Vector1 : duoVector3.Vector2;
            MovingTarget.transform.Translate(vectorDirection * speed * Time.deltaTime, Space.World);
        }
        if (!Target.Target) return new DuoGameObject(null, false);
        vectorDirection = Direction ? duoVector3.Vector1 : duoVector3.Vector2;
        MovingTarget.transform.Translate(vectorDirection * speed * Time.deltaTime, Space.World);
        isTrgert = true;
        return new DuoGameObject(Target.Target, Target.Found);
    }
    public void Slash(DuoVector duoVectorLR,int DamageAmount)
    {

        if (Anim.GetBool("Slash"))return;
        Anim.SetBool("Slash", true);
        if (RayCastCheck(EnemysLayers, duoVectorLR))
        {
            Debug.Log("HitTarget");
            IPlayerable playerable = RayCastCheck(EnemysLayers, duoVectorLR).gameObject.GetComponent<IPlayerable>();
            playerable.PlayerSystem.DoDamage(DamageAmount);
            if (!playerable.PlayerSystem.Rig2d) return;
            RigForceSide(RayCastCheck(EnemysLayers, duoVectorLR));
        }
    }
    public void RigForceSide(GameObject Target)
    {
        if(GameObjectSelf.GetComponent<SpriteRenderer>().flipX)
        {
            Target.GetComponent<Rigidbody2D>().AddForce(new Vector2(-2 * 100,2*100));
        }
        if (!GameObjectSelf.GetComponent<SpriteRenderer>().flipX)
        {
            Target.GetComponent<Rigidbody2D>().AddForce(new Vector2(2 * 100, 2 * 100));
        }
    }
    public bool JumbCheck()
    {
        if (!Rig2d) return false;
        if (Rig2d.linearVelocityY > 0.01f) // Falling threshold
        {
            //Debug.Log("We are going up!");
            Anim.SetBool("Jumb", true);
            return true;
        }
        else if (Rig2d.linearVelocityY < -0.01f)
        {
            //Debug.Log("We are falling down!");
            Anim.SetBool("Jumb", true);
            return true;
        }
        Anim.SetBool("Jumb", false);
        return false;
    }
    public GameObject RayCastCheck(LayerMask enemysLayers,DuoVector duoV)
    {
        if (!RayCheckSystem.RayCheck(GameObjectSelf.transform, enemysLayers, RayOffset, RayDistance, duoV)) return null;

        if (!GameObjectSelf.GetComponent<SpriteRenderer>().flipX)
        {
            if (RayCheckSystem.GetRayCastRight())
            {
                return RayCheckSystem.GetRayCastRight().collider.gameObject;
            }
        }
        else
        {
            if (RayCheckSystem.GetRayCastLeft())
            {
                return RayCheckSystem.GetRayCastLeft().collider.gameObject;
            }
        }
        return null;
    }
    public bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }
    public void Jumb() { Rig2d.AddForceY(JumpeSpeed * 100); }
    public void MoveWay(GameObject MovedTarget,DuoVector3 _duoVector3,bool side)
    {        
        MovmentSysteam.MoveTo(MovedTarget, JumbCheck() ? Speed * 2.5f : Speed, _duoVector3, side);
    }
    public void Shoot()
    {
        if (Anim.GetBool("Shoot")) return;
        if (ShootSlider && ShootSlider.value == 0) return;
        Anim.SetBool("Shoot", true);
        if (ShootSlider)ShootSlider.value--;
        GameObject SE = GameObject.Instantiate(Shoot_Efect,GameObjectSelf.transform.position, Quaternion.identity);
        SE.GetComponent<SpriteRenderer>().flipX = GameObjectSelf.GetComponent<SpriteRenderer>().flipX;
    }
    public GameObject Shoot(GameObject Target)
    {
        if (!Target) {return null;}
        Shoot();
        Debug.Log("ReRe");
        return Target;
    }
    public void GamePlayerData(Playerinfo Playerinfo_)
    {
        Health = Playerinfo_.Health_info;
        MaxHealth_ = Playerinfo_.MaxHealth_info;
        HealthSlider.maxValue = Playerinfo_.MaxHealth_info;
        Speed = Playerinfo_.Speed_info;
        JumpeSpeed = Playerinfo_.JumbSpeed_info;
    }
    public void GameobjectData(Objectinfo objectinfo_)
    {
        Speed = objectinfo_.Speed_info;
    }
    public void Heal(int Amount)
    {
        if(Health >= MaxHealth_) return;
        Health += Amount;
    }
}

[System.Serializable]
public class AISystem : IAIable
{
    [SerializeField] public GameObject GameObjectSelf;
    //Wall etc.....
    [SerializeField] public LayerMask LayerEtc;
    bool RandomePetrol = false;
    public AISystem(GameObject gameObjectSelf, LayerMask layeretc)
    {
        LayerEtc = layeretc;
        GameObjectSelf = gameObjectSelf;
    }
    public GameObject Petrol(GameObject RayCast, DuoVector3 duoVector3LR)
    {
        IPlayerable playerable = GameObjectSelf.GetComponent<IPlayerable>();

        if (RayCast)
        {

            if (RayCast.GetComponent<IPlayerable>() != null && playerable.PlayerSystem.IsInLayerMask(RayCast, playerable.PlayerSystem.EnemysLayers) && !RayCast.GetComponent<IPlayerable>().PlayerSystem.isdead)
            {
                return RayCast;
            }
            else if(RayCast.layer == LayerMask.NameToLayer("Ground"))
            {
                if (playerable.PlayerSystem.JumbCheck() == false) { playerable.PlayerSystem.Jumb(); Debug.Log("jumb"); }        
            }
            else
            {               
                playerable.PlayerSystem.MoveWay(GameObjectSelf, new DuoVector3(Vector3.left, Vector3.right), true);
                playerable.PlayerSystem.Anim.SetBool("Walk", true);
                playerable.FlipPlayer(!GameObjectSelf.GetComponent<SpriteRenderer>().flipX);
            }

        }
        else
        {
            if (!RandomePetrol)
            {
                bool RandomValue = Random.Range(0, 2) == 1;
                playerable.FlipPlayer(RandomePetrol);
                RandomePetrol = true;
            }
            if(RandomePetrol)
            {
                playerable.PlayerSystem.MoveWay(GameObjectSelf, new DuoVector3(Vector3.left, Vector3.right), true);
                playerable.PlayerSystem.Anim.SetBool("Walk", true);

            }

        }

        return null;
    }
    public DuoGameObject DistanceCheck(GameObject Target,float Distance_)
    {
        if(Target == null) return new DuoGameObject(null, false); ;
        if(DistanceCheckSystem.DistanceObject(Target, GameObjectSelf, Distance_))
        {
            return new DuoGameObject(Target, DistanceCheckSystem.DistanceObject(Target, GameObjectSelf, Distance_));
        }
        else
        {
            return new DuoGameObject(Target, false);
        }
    }
    public GameObject Pursuit(DuoGameObject Target_, DuoVector duoVectorLR,int Damageamount)
    {
        if (Target_.Target == null) return null;
        IPlayerable IPlayerable_ = GameObjectSelf.GetComponent<IPlayerable>();
        if (Target_.Found)
        {
            IPlayerable_.PlayerSystem.Slash(duoVectorLR,Damageamount);
            return Target_.Target;
        }
        else
        {
            GameObjectSelf.GetComponent<IPlayerable>().PlayerSystem.Anim.SetBool("Walk", true);
            GameObjectSelf.transform.position = Vector2.MoveTowards
            (GameObjectSelf.transform.position, Target_.Target.transform.position,
            IPlayerable_.PlayerSystem.Speed * Time.deltaTime);
        }
        return null;
    } 
}
[System.Serializable]
public class MarketSystem
{
    public MarketSystem()
    {

    }
    public GameObject IFGameObject(GameObject Target)
    {
        if (!Target) return null;
        if (Input.GetKeyDown(KeyCode.F)) { return Target; }
        return null;
    }
    public GameObject CheckCoin(GameObject Target,int AmountNeeded)
    {
        if (!Target) return null; 
        if(AmountNeeded <= GameEventManager.Instance.GetCoinCount())
        {
            return Target;
        }       
        return null;
    }
}
