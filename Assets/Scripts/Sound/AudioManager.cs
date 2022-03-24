using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
    public class Aliase 
    {
        public string name;
        public AudioMixerGroup MixerGroup;
        public AudioClip[] audio;

        public bool bypassEffects;
        public bool bypassListenerEffects;
        public bool bypassReverbZones;
        [Range(0,256)]
        public float priority;
        [Range(0,1)]
        public float volume = 0.8f;
        public bool isLooping;
        [Range(-3,3)]
        public float minPitch = 1f;
        [Range(-3,3)]
        public float maxPitch = 1f;
        [Range(-1,1)]
        public float stereoPan = 0;
        [Range(0,1)]
        public float spatialBlend = 0;
        
        [Range(0,1.1f)]
        public float reverbZoneMix = 1;
        [Header("3D Sound Settings")]
        [Range(0,5)]
        public float dopplerLevel = 1;
        [Range(0,360)]
        public float Spread = 1;
        [Range(0,10000)]
        public float MinDistance = 1;
        [Range(0,10000)]
        public float MaxDistance = 500;
        public AudioRolloffMode CurveType = AudioRolloffMode.Logarithmic;
        public AnimationCurve distanceCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 0) });
    }

//[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Util;

    [SerializeField] static Aliases[] aliasesArray = new Aliases[0];
    [SerializeField] List<GameObject> audioSource;

    [SerializeField] int audioSourcePoolSize = 32; // 32 is a good start
    [Header("Debug")] 
     [SerializeField]  Aliases[] TableAliasesLoaded = new Aliases[0];

    // Add aliases to load
    public static void AddAliases(Aliases newAliases)
    {
        // Check if not already exist
        for(int i = 0 ; i < aliasesArray.Length;i++)
        {
            if(aliasesArray[i] == newAliases)
            {
                Debug.Log("AudioManager : L'aliases a déja était ajouté");
                return;
            }    
        }
        // We can add it
        Aliases[] TempAliasesArray = new Aliases[aliasesArray.Length+1];
        for(int i = 0 ; i < aliasesArray.Length;i++)
        {
            TempAliasesArray[i] = aliasesArray[i];
        }
        TempAliasesArray[TempAliasesArray.Length-1] = newAliases;
        aliasesArray = TempAliasesArray;
        Debug.Log("AudioManager : Aliases added");
    }

    void Awake()
    {
        if(Util == this || Util == null)
        {
            Util = this;
        }      
        else
            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);

        InitAudioSources();
    }

    void InitAudioSources()
    {
         for(int i = 0 ; i < audioSourcePoolSize; i++)
        {
            GameObject newAudioSource = new GameObject("Audio Source "+i);
            newAudioSource.transform.SetParent(transform);
            AudioSource audioS = newAudioSource.AddComponent<AudioSource>();
            Util.audioSource.Add(newAudioSource);
            newAudioSource.SetActive(false);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Util != this)
            Util = this;   

        TableAliasesLoaded = aliasesArray;    

        DisableInusedAudioSource();
    }

    void DisableInusedAudioSource()
    {
          foreach(GameObject aS in audioSource)
        {
            if(!aS.GetComponent<AudioSource>().isPlaying)
            {
                aS.SetActive(false);
            }     
        }
    }

    static Aliase GetSoundByAliase(string name)
    {
        for(int i = 0 ; i < aliasesArray.Length; i++)
        {
            foreach(Aliase alias in aliasesArray[i].aliases )
                if(alias.name == name)
                    return alias ;
        }

        Debug.LogWarning("AudioManager : Aliase: "+name+" not found.");
        return null;
    }

    static AudioSource GetAudioSource()
    {
        foreach(GameObject aS in Util.audioSource)
        {  
            AudioSource audio = aS.GetComponent<AudioSource>();
            if(!audio.isPlaying)
            {
                return audio;
            }  
        }
        return null;
    }

    public static void PlaySoundAtPosition(string aliaseName, Vector3 position)
    {
        Aliase clip = GetSoundByAliase(aliaseName);
        if( clip.audio.Length == 0)
        {
            Debug.LogWarning("AudioManager : Aliase: "+aliaseName+" contains no sounds.");
            return;
        }
        AudioSource audioS = GetAudioSource();
        if(audioS == null)
        {
            Debug.LogWarning($"AudioManager : Limits exceded for audioSource, maybe you need to increase your audioSourcePoolSize (Size = {Util.audioSourcePoolSize})");
            return;
        }
        audioS.volume = clip.volume;
        audioS.loop = clip.isLooping;
        audioS.pitch = Random.Range(clip.minPitch, clip.maxPitch);
        audioS.outputAudioMixerGroup = clip.MixerGroup;
        switch(clip.CurveType)
        {
            case AudioRolloffMode.Logarithmic:
            case AudioRolloffMode.Linear:
                audioS.rolloffMode = clip.CurveType;
            break;
            case AudioRolloffMode.Custom:
                audioS.rolloffMode = clip.CurveType;
               audioS.SetCustomCurve(AudioSourceCurveType.CustomRolloff, clip.distanceCurve);  
            break;

        }
        audioS.gameObject.transform.position = position;
        audioS.gameObject.SetActive(true);
        if(clip.isLooping)
        {
            audioS.clip = clip.audio[Random.Range(0,clip.audio.Length)];
            audioS.Play();
        }
        else
            audioS.PlayOneShot(clip.audio[Random.Range(0,clip.audio.Length)]);
        

    } 
    void PlayLoopSound(string aliaseName, Vector3 position, GameObject attachedTo)
    {
        
    }
}



