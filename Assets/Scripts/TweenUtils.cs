using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class TweenUtils
{
    public static void KillAndNull(ref Tween tween)
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }
    }
}
