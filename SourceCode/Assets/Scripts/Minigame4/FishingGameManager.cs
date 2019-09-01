using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class FishingGameManager : MonoBehaviour
{
    public static FishingGameManager fgm;

    [Header("Camera")]
    public PostProcessingBehaviour ppb;
    public CameraController cc;
    public float focusZoom = 30;
    public float zoomTime = 2f;

    [Header("Control position")]
    public BaitControl baitPoint;

    [Header("Control ripple appear time")]
    public float maxDistanceTime = 10f;
    public float minDistanceTime = 5f;

    [Header("Control fish")]
    public float minTimeFishingCome = 15f;
    public float maxTimeFishingCome = 60f;
    public FishAttack fa;
    
    [Header("Control water")]
    public GameObject waterEffect;
    public WaterController waterController;

    [Header("Sound")]
    public AudioClip rippleWaterSound;

    [Header("End scene")]
    public Animator levelChanger;
    public float endTime = 5f;

    [Header("Guide")]
    public GameObject guide;
    public float guideTime = 5f;

    float appearTime = -1f;
    float _rdAppearTime = -1f;
    float _fishTime = -1f;
    float newzoom, initzoom;
    
    Vector3 initForward;
    bool isFishing;

    

    private void Awake()
    {
        fgm = this;

        DialogueSystem.ds.Display(new int[] { 0, 1, 2});
        Invoke("EnabledGuide", guideTime);
        
    }

    private void Start()
    {
        initForward = Camera.main.transform.forward;
        initzoom = Camera.main.fieldOfView;
        newzoom = initzoom;

        _rdAppearTime = Random.Range(minDistanceTime, maxDistanceTime);
    }


    private void Update()
    {
 
        //Debug.Log(_fishTime);
        if (_fishTime >= 0) _fishTime -= Time.deltaTime;
        appearTime += Time.deltaTime;

        if (appearTime > _rdAppearTime){
            // make ripple wateer appear && reset time 
            MakeRippleWater();
            appearTime = -1f;
            _rdAppearTime = Random.Range(minDistanceTime, maxDistanceTime);
        }

        if (_fishTime < 0 && isFishing) {
            // Fish Attack
            ppb.enabled = true;
            newzoom = focusZoom;
            fa.enabled = true;
        }

        // control camera
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, newzoom, Time.deltaTime * zoomTime);
       
    }

    public void StartFishing()
    {
        // start fishing activity
        if (isFishing) return;
        isFishing = true;
        _fishTime = maxTimeFishingCome;       
    }

    public void EndFishing()
    {
        // end fishing activity
        Debug.Log("End fishing");

        EndCatchFishing(false);
        isFishing = false;

        // reset all time
        appearTime = -1f;
        _rdAppearTime = -1f;
        _fishTime = -1f;
    }


    public void EndCatchFishing(bool direct)
    {
        // fish attack end
        // if end when fish attack need to return to forward pos otherwise no need
        if (direct) Camera.main.transform.forward = Vector3.forward;
        fa.enabled = false;

        _fishTime = maxTimeFishingCome;
        newzoom = initzoom;
        ppb.enabled = false;

        SoundManager.sm.audioSource.Stop();
    }

    public void CheckWater()
    {
        // reduce the time of fishing
        //Debug.Log("Check Water");
        if (_fishTime > minTimeFishingCome && isFishing) _fishTime = minTimeFishingCome;
        //Debug.Log(_fishTime);
    }

    void MakeRippleWater()
    {
        // make ripple water appear 
        Vector3 pos = randomFishAppearPos();
        Instantiate(waterEffect, pos, Quaternion.identity);

        SoundManager.sm.PlayOneShot(rippleWaterSound);
    }

    Vector3 randomFishAppearPos() {

        // return a random position depend on the range
        float randomPos = Random.Range(-cc.horizontalRange, cc.horizontalRange);
        var rotation = Quaternion.AngleAxis(randomPos, Vector3.up);
        var right = rotation * Vector3.forward;

        return Camera.main.transform.position + right * baitPoint.throwDistance;
    }

    public void End()
    {
        Invoke("EndScene", endTime);
    }

    void EndScene()
    {

        levelChanger.SetTrigger("FadeOut");
    }

    void EnabledGuide()
    {
        guide.SetActive(true);
    }





}
