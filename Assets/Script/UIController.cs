using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour
{
    RectTransform pos;
    Vector2 targetPos;
    private void Start()
    {
        pos = GetComponent<RectTransform>();
        targetPos = new Vector2(-400, -100);
        Replacer();
    }

    void Replacer()
    {
        pos.DOAnchorPos(targetPos , 0.4f);
#if UNITY_ANDROID
        Handheld.Vibrate();
#elif UNITY_IOS
        Handheld.Vibrate();
#endif

    }
}
