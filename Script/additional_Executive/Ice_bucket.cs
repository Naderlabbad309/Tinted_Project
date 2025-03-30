using UnityEngine;
using static Tec.TecVector;

public class Ice_bucket : MonoBehaviour
{
    [SerializeField] private PlayerSystem playersystem;
    [SerializeField] private AISystem aIsystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playersystem.GameobjectData(GameDataBase.Instance.Ice_Bucket_info);

    }
    void Start()
    {
        
    }
    void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update()
    {
        playersystem.DestorySelf(playersystem.Hit(playersystem.MoveINTO(gameObject,aIsystem.DistanceCheck(playersystem.RayCastCheck(playersystem.EnemysLayers, new DuoVector(Vector2.up, Vector2.down)), 0.5f),false,playersystem.Speed, new DuoVector3(Vector2.up, Vector2.down)), GameDataBase.Instance.Ice_Bucket_info.Damage_info,"Ground").Found);
    }
}
