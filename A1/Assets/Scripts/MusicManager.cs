using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            
            Destroy(gameObject);
        }
    }


    void Start()
    {
        PlayMusic();
    }
    // Update is called once per frame
    public void PlayMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
