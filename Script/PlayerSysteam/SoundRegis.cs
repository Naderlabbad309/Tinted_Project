using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundRegis : MonoBehaviour
{
    [SerializeField] private SoundDataBase m_SoundData;
    private AudioSource m_AudioSource;


    private void Start()
    { 
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        m_AudioSource.volume = m_SoundData.VolumeContoller; 
    }

    private void Walk() { m_AudioSource.clip = m_SoundData.Walk; m_AudioSource.Play(); }
    private void Jump() { m_AudioSource.clip = m_SoundData.jump; m_AudioSource.Play(); }
    private void hit() { m_AudioSource.clip = m_SoundData.hit; m_AudioSource.Play(); }
    private void explosion() { m_AudioSource.clip = m_SoundData.explosion; m_AudioSource.Play(); }
    private void Shoot() { m_AudioSource.clip = m_SoundData.Shoot; m_AudioSource.Play(); }
    public void Market() { m_AudioSource.clip = m_SoundData.Market; m_AudioSource.Play(); }

}
