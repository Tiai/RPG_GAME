using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private float sfxMinDistance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool isPlayBGM;
    private int bgmIndex;

    private bool canPlaySFX;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }

        Invoke("AllowSFX", 0.2f);
    }

    private void Update()
    {
        if (!isPlayBGM)
        {
            StopAllBGM();
        }
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }

    public void PlaySFX(int _sfxIndex, Transform _source)
    {
        if (canPlaySFX == false)
        {
            return;
        }

        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > sfxMinDistance)
        {
            return;
        }

        //if (sfx[_sfxIndex].isPlaying)
        //{
        //    return;
        //}

        if (_sfxIndex < sfx.Length)
        {
            //sfx[_sfxIndex].pitch = Random.Range(0.85f, 1.0f);
            sfx[_sfxIndex].Play();
        }
    }

    public void StopSFX(int _index) => sfx[_index].Stop();

    public void StopSFXWithTime(int _index) => StartCoroutine(DecreaseVolume(sfx[_index]));

    private IEnumerator DecreaseVolume(AudioSource _audio)
    {
        float defaultVolume = _audio.volume;

        while (_audio.volume > .1f)
        {
            _audio.volume -= _audio.volume * .2f;
            yield return new WaitForSeconds(.15f);

            if (_audio.volume <= .1f)
            {
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }
    }

    public void PlayBGM(int _bgmIndex)
    {
        bgmIndex = _bgmIndex;

        StopAllBGM();

        bgm[bgmIndex].Play();
    }

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; ++i)
        {
            bgm[i].Stop();
        }
    }

    private void AllowSFX() => canPlaySFX = true;
}

//using System.Collections;
//using UnityEngine;

//public class AudioManager : MonoBehaviour
//{
//    public static AudioManager instance;

//    [SerializeField] private float sfxMinDistance;
//    [SerializeField] private AudioSource[] sfx;
//    [SerializeField] private AudioSource[] bgm;

//    public bool isPlayBGM;
//    private int bgmIndex;

//    private bool canPlaySFX;
//    private void Awake()
//    {
//        if (instance != null)
//        {
//            Destroy(instance.gameObject);
//        }
//        else
//        {
//            instance = this;
//        }

//        Invoke("AllowSFX", 0.2f);
//    }

//    private void Update()
//    {
//        if (!isPlayBGM)
//        {
//            StopAllBGM();
//        }
//        else
//        {
//            if (!bgm[bgmIndex].isPlaying)
//            {
//                PlayBGM(bgmIndex);
//            }
//        }
//    }

//    public void PlaySFX(int _sfxIndex, Transform _source)
//    {
//        if (!canPlaySFX || _source == null)
//        {
//            return;
//        }

//        if (PlayerManager.instance == null || PlayerManager.instance.player == null)
//        {
//            Debug.LogWarning("PlayerManager or player is null.");
//            return;
//        }

//        float distance = Vector3.Distance(PlayerManager.instance.player.transform.position, _source.position);

//        if (distance > sfxMinDistance)
//        {
//            return;
//        }

//        if (_sfxIndex >= 0 && _sfxIndex < sfx.Length)
//        {
//            sfx[_sfxIndex].Play();
//        }
//        else
//        {
//            Debug.LogWarning("Invalid audio index.");
//        }
//    }

//    public void StopSFX(int _index) => sfx[_index].Stop();

//    public void StopSFXWithTime(int _index)
//    {
//        if (instance == null)
//        {
//            Debug.LogWarning("AudioManager instance is null.");
//            return;
//        }

//        if (_index >= 0 && _index < sfx.Length && sfx[_index] != null)
//        {
//            StartCoroutine(DecreaseVolume(sfx[_index]));
//        }
//        else
//        {
//            Debug.LogWarning("Invalid audio index or audio source is null.");
//        }
//    }

//    private IEnumerator DecreaseVolume(AudioSource _audio)
//    {
//        if (_audio == null)
//        {
//            yield break;
//        }

//        float defaultVolume = _audio.volume;

//        while (_audio.volume > .1f)
//        {
//            _audio.volume -= _audio.volume * .2f;
//            yield return new WaitForSeconds(.15f);

//            if (_audio.volume < .1f)
//            {
//                _audio.Stop();
//                _audio.volume = defaultVolume;
//                break;
//            }
//        }
//    }

//    public void PlayBGM(int _bgmIndex)
//    {
//        bgmIndex = _bgmIndex;

//        StopAllBGM();

//        bgm[bgmIndex].Play();
//    }

//    public void PlayRandomBGM()
//    {
//        bgmIndex = Random.Range(0, bgm.Length);
//        PlayBGM(bgmIndex);
//    }

//    public void StopAllBGM()
//    {
//        for (int i = 0; i < bgm.Length; ++i)
//        {
//            bgm[i].Stop();
//        }
//    }

//    private void AllowSFX() => canPlaySFX = true;
//}
