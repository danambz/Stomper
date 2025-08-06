using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Mixer Groups (drag your groups here)")]
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    [Header("SFX Clips")]
    public AudioClip clickSfx;
    public AudioClip startButtonSfx;
    public AudioClip jumpSfx;
    public AudioClip coinSfx;
    public AudioClip powerupSfx;
    public AudioClip damageSfx;
    public AudioClip stompSfx;
    public AudioClip winSfx;
    public AudioClip newHighSfx;

    AudioSource musicSource;
    AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // → Create your two AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.5f;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.volume = 1f;

        // ← Route them into the mixer
        musicSource.outputAudioMixerGroup = musicGroup;
        sfxSource.outputAudioMixerGroup = sfxGroup;
    }

    public void PlayMusic(string which)
    {
        var clip = (which == "menu") ? menuMusic : levelMusic;
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        AudioClip clip = name switch
        {
            "click" => clickSfx,
            "start" => startButtonSfx,
            "jump" => jumpSfx,
            "coin" => coinSfx,
            "powerup" => powerupSfx,
            "damage" => damageSfx,
            "stomp" => stompSfx,
            "win" => winSfx,
            "newHigh" => newHighSfx,
            _ => null
        };
        if (clip != null) sfxSource.PlayOneShot(clip);
    }
}
