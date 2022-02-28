using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTextParser : MonoBehaviour
{

    #region Singlton
    public static EventTextParser instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public struct EventTextInfo
    {
        public string id;
        public string state;
        public string spritePass;
        public string speakerName;
        public string textMesse;
    }

    public static EventTextInfo[] textInfo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
