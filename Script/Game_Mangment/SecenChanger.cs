using UnityEngine;
using UnityEngine.SceneManagement;

public class SecenChanger : MonoBehaviour
{
    [SerializeField] private int SecenIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadSecen()
    {
        SceneManager.LoadScene(SecenIndex);
    }
}
