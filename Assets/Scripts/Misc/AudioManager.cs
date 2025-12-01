using UnityEngine;

public class AudioManager : MonoBehaviour
{
   #region Singleton
    private static AudioManager instance;
    public static AudioManager Instance => instance ? instance : FindFirstObjectByType<AudioManager>();
    #endregion
    [SerializeField] private AudioSource musicSource;

    private bool isMuted = false;

    private void Awake()
    {
        isMuted = PlayerPrefs.GetInt("musicMuted", 0) == 1;
        musicSource.mute = isMuted;
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        musicSource.mute = isMuted;
        PlayerPrefs.SetInt("musicMuted", isMuted ? 1 : 0);
    }

    public bool IsMuted() => isMuted;
}