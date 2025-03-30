using UnityEngine;

[CreateAssetMenu(fileName = "SoundDataBase", menuName = "ScriptableObjects/SoundDataBase", order = 2)]
public class SoundDataBase : ScriptableObject
{

    public AudioClip Walk;
    public AudioClip jump;
    public AudioClip hit;
    public AudioClip explosion;
    public AudioClip Shoot;
    public AudioClip Market;
    public float VolumeContoller = 0.1f;
}
