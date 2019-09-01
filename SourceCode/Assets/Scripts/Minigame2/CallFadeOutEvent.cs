using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallFadeOutEvent : MonoBehaviour
{
    public Animator sceneChangeAnimator;

    public void FadeOut() {

        sceneChangeAnimator.SetTrigger("FadeOut");
    }


}
