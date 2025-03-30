using UnityEngine;
using static Tec.TecVector;

public class Player_Shoot : MonoBehaviour
{
    [SerializeField] private PlayerSystem playersystem;
    [SerializeField] private AISystem aIsystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersystem.GameobjectData(GameDataBase.Instance.PlayerShoot_info);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        playersystem.DestorySelf(playersystem.Hit(aIsystem.DistanceCheck(playersystem.RayCastCheck(playersystem.EnemysLayers, new DuoVector(Vector2.left, Vector2.right)),0.5f),GameDataBase.Instance.PlayerShoot_info.Damage_info,"Wall").Found);
        playersystem.MoveWay(gameObject, new DuoVector3(Vector3.left, Vector3.right), gameObject.GetComponent<SpriteRenderer>().flipX);
    }
}
