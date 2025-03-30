using UnityEngine;
using static Tec.TecVector;

public class Waterkiller : MonoBehaviour
{
    [SerializeField] private PlayerSystem playersystem;
    [SerializeField] private AISystem aIsystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Awake()
    {
        playersystem.GameobjectData(GameDataBase.Instance.Ice_Bucket_info);

    }
    // Update is called once per frame
    void Update()
    {
        playersystem.Hit(playersystem.RayToHit(playersystem.RayCastCheck(playersystem.EnemysLayers, new DuoVector(Vector2.up, Vector2.down))), GameDataBase.Instance.Waterkiller_info.Damage_info, "Ground");
    }
}
