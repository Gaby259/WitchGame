using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager Instance;
    private static AudioSource _audioSource;
    private static SoundLibrary _soundLibrary;
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _audioSource = GetComponent<AudioSource>();
            _soundLibrary = GetComponent<SoundLibrary>();
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate{OnVolumeChanged();});
    }     

    public static void Play(string soundName)
    {
        AudioClip audioClip = _soundLibrary.GetRandom(soundName);
        if (audioClip != null)
        {
            _audioSource.PlayOneShot(audioClip);
        }
    }

    private static void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    private void OnVolumeChanged()
    {
        SetVolume(volumeSlider.value);
    }
}
