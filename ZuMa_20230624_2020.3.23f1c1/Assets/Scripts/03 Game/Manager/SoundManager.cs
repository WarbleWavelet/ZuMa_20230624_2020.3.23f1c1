using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class SoundManager : MonoSingleton<SoundManager>
{
    static AudioSource m_BgAudio;


    public void Init()
    {
        m_BgAudio = gameObject.GetOrAddComponent<AudioSource>();
    }


    private static void PlaySound(string clipName)
    {
      //  AudioSource.PlayClipAtPoint(GetAudioClip(clipName), Vector3.zero);
        AudioKit.PlaySound("resources://Sound/"+clipName );
    }

    public static void PlayMusic(string name, float volume = 0.3f)
    {
        //bgAudio.clip = SoundManager.GetAudioClip(name);//*-
        //bgAudio.volume = volume;
        //bgAudio.loop = true;
        //bgAudio.Play();
        AudioKit.PlayMusic("resources://Sound/" + name, volume: volume);
    }


    public static AudioClip GetAudioClip(string clipName)
    {
        return Resources.Load("Sound/" + clipName, typeof(AudioClip)) as AudioClip;
    }


    public static void PlayDestroy() { PlaySound("Eliminate");  }
    public static void PlayShoot()   { PlaySound("Shoot"); }
    public static void PlayInsert()  { PlaySound("BallEnter"); }
    public static void PlayClick() { PlaySound("BallEnter"); } //没有合适的
    public static void PlayBomb()    { PlaySound("Bomb"); }
    public static void PlayFail()    { PlaySound("Fail"); }
    public static void PlayFastMove(){ PlaySound("FastMove"); }
    public static void PlayBg(){ PlayMusic("Bg"); }




}
