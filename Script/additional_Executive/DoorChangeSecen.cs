using UnityEngine;
using static Tec.TecVector;
using UnityEngine.SceneManagement;

public class DoorChangeSecen : MonoBehaviour, IMarketable
{
    [SerializeField] private PlayerSystem playersystem;
    public PlayerSystem PlayerSystem => playersystem;

    [SerializeField] private MarketSystem marketsystem;
    public MarketSystem Marketsystem => marketsystem;
    [SerializeField] private int SecenIndex = 0;

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
        if (!playersystem.Anim.GetBool("Door_Open")) { return; }
     ((IMarketable)this).DoSomethink(marketsystem.IFGameObject(playersystem.RayCastCheck(playersystem.EnemysLayers, new DuoVector(Vector2.left, Vector2.right))));
    }
    void IMarketable.DoSomethink(GameObject Target)
    {
        if (!Target) return;
        if (playersystem.Anim.GetBool("Door_Open"))
        {
            SceneManager.LoadScene(SecenIndex);

        }
    }
}
