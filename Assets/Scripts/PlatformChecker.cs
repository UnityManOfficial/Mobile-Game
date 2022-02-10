using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    public GameObject Touch;


    public void Start()
    {
#if UNITY_STANDALONE_WIN
        Touch.SetActive(false);

#elif UNITY_ANDROID
        Touch.SetActive(true);

#elif UNITY_IOS
        Touch.SetActive(true);
#endif

    }
}
