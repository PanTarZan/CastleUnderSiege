
using UnityEngine;

[System.Serializable]
public class Sound{

    public string name;
    public AudioClip clip;
    public bool loop = false;
    public bool oneShot = false;

    [Range(0f,1f)]
    public float volume = 0.7f;
    [Range(0f, 1f)]
    public float pitch = 1f;

    [Range(0f, 1f)]
    public float volumeRandom = 0.1f;
    [Range(0f, 1f)]
    public float pitchRandom = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        Debug.Log("Playing" + name);
        source.volume = volume * (1 + Random.Range(-volumeRandom * 0.5f, volumeRandom * 0.5f));
        source.pitch = pitch * (1 + Random.Range(-pitchRandom * 0.5f, pitchRandom * 0.5f));
        source.loop = loop;
        if (oneShot)
        {
            source.PlayOneShot(source.clip);
        }
        else
        {
            source.Play();
        }
    }
    public void Stop()
    {
        source.Stop();
    }

}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than one audio manager in scene");
        }
        else
        {
            instance = this;
        }
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
          
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("No sound with specified name exist: " + _name);
    } 
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        Debug.LogWarning("No sound with specified name exist: " + _name);
    } 
}
