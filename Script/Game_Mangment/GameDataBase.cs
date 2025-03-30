using UnityEngine;

[CreateAssetMenu(fileName = "GameDataBase", menuName = "ScriptableObjects/GameDataBase", order = 1)]
public class GameDataBase : ScriptableObject
{
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraSmoothness = 1;
    [Space][Space]
    [SerializeField] public Playerinfo playerinfo;
    [SerializeField] public Playerinfo Ai_Skelton_info;
    [SerializeField] public Playerinfo Ai_Skelton_Boom_info;
    [SerializeField] public Playerinfo Ai_Wizard_info;
    [Space][Space]
    [SerializeField] public Objectinfo Ice_Bucket_info;
    [SerializeField] public Objectinfo Waterkiller_info;
    [SerializeField] public Objectinfo PlayerShoot_info;
    // Singleton instance
    private static GameDataBase instance;


    // Property to access the singleton instance
    public static GameDataBase Instance
    {
        get
        {
            if (instance == null)
            {
                // Load the instance if it hasn't been set
                instance = Resources.Load<GameDataBase>("GameDataBase"); // Make sure to change the path if needed
            }
            return instance;
        }
    }

    public Vector3 GetCameraOffset()
    {
        return cameraOffset;
    }

    public float GetCameraSmoothness()
    {
        return cameraSmoothness;
    }
}
[System.Serializable]
public class Playerinfo
{
    public int Health_info;
    public int MaxHealth_info;
    public int Damage_info;
    public int Speed_info;
    public float JumbSpeed_info;
    public Playerinfo(int Health_, int MaxHealth_, int Damage_, int Speed_, float JumbSpeed_)
    {
        Damage_info = Damage_;
        Health_info = Health_;
        JumbSpeed_info = JumbSpeed_;
        Speed_info = Speed_;
        MaxHealth_info = MaxHealth_;
    }
}
[System.Serializable]
public class Objectinfo
{
    public int Damage_info;
    public int Speed_info;
    public Objectinfo(int Damage_, int speed_)
    {
        Damage_info = Damage_;
        Speed_info = speed_;
    }
}

