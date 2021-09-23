using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatMove : MonoBehaviour
{
    [SerializeField] private List<AudioClip> AudioClipList;
    public AudioSource AudioPlayer;

    private float Speed;
    //private Vector3 Target;

    public int Index = 0;

    private Animator Ani;
    public bool isAni_Sound1;

    void Awake()
    {
        //AudioClip = GetComponent<AudioClip>();
        AudioPlayer = GetComponent<AudioSource>();
        Ani = GetComponent<Animator>();
    }

    void Start()
    {
        Speed = 5.0f;
        //Target = new Vector3(0.0f, 0.0f, 0.0f);

        object[] AudioObject = Resources.LoadAll("Sound");
        //AudioPlayer.clip = AudioClip;
        AudioPlayer.playOnAwake = false;

        for(int i = 0; i < AudioObject.Length; i++)
        {
            AudioClipList.Add(AudioObject[i] as AudioClip);
        }
    }
    void Update()
    {
        float Hor = Input.GetAxisRaw("Horizontal");
        float Ver = Input.GetAxisRaw("Vertical");

        transform.Translate(Hor * Time.deltaTime * Speed, 0.0f, Ver * Time.deltaTime * Speed);

        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit Hit;

        //    if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
        //    {
        //        Vector3 Dir = Hit.point - transform.position;
        //        Dir.Normalize();

        //        Target = Hit.point;

        //        transform.position = Vector3.Lerp(transform.position, Target, 0.5f);
        //    }
        //}

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlaySound(Index);
        }

        if(Ani.GetCurrentAnimatorStateInfo(0).IsName("BoxTmpAni0") && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Ani.SetBool("Sound1", false);
        }

        if (Ani.GetCurrentAnimatorStateInfo(0).IsName("BoxAni2") && Ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Ani.SetBool("Sound15", false);
        }
    }

    public void PlaySound(int _Index, float _Volume = 1.0f, bool _Loop = false)
    {
        if (_Index < 0 || AudioClipList.Count < _Index)
            return;

        AudioPlayer.Stop();
        AudioPlayer.mute = false;
        AudioPlayer.clip = AudioClipList[_Index];
        AudioPlayer.loop = _Loop;
        AudioPlayer.time = 0.0f;
        AudioPlayer.volume = _Volume;

        AudioPlayer.Play();
    }
    public void AniPlaySound(int _SoundIndex)
    {
        if(_SoundIndex == 1)
            Ani.SetBool("Sound1", true);

        if (_SoundIndex == 15)
            Ani.SetBool("Sound15", true);

        Index = _SoundIndex;
    }

    public void AudioPlay()
    {
        PlaySound(Index);
    }
}
