using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttack : MonoBehaviour
{
    [Header("Control time, force and health")]
    public float attackTime = 3f;
    public float escapeTime = 7f;
    public float waterInterval = 0.2f;
    public float attackForce = 3f;
    public float pullForce = 0.5f;
    public float health = 50f;

    [Header("Object's inputs")]
    public GameObject waterSource;
    public Transform lake;
    public Transform head;
    public GameObject guide;

    [Header("Sound")]
    public AudioClip fishingRopeSound;

    float _escapeTime = -1f;
    float _health, waterTime, _attackTime;
    int dir = -1;
    bool isFistTime = true;

    // Update is called once per frame
    private void OnEnable()
    {
        guide.SetActive(true);

        waterTime = waterInterval;
        _health = health;
        _attackTime = attackTime;
        _escapeTime = -1f;
        transform.position += transform.up * (-3);
    }

    void Update()
    {
        waterTime -= Time.deltaTime;
        _attackTime -= Time.deltaTime;
        _escapeTime += Time.deltaTime;

        if (_escapeTime > escapeTime)
        {
            // end catch fish
            _escapeTime = -1;
            FishingGameManager.fgm.EndCatchFishing(true);
        }

        if (_attackTime < 0) {
            // attack another direction
            _attackTime = attackTime;
            dir *= -1;
        }

        if (waterTime < 0) {
            // create water but not continously
            CreateWater();
            waterTime = waterInterval;
        }

        if (Input.GetAxisRaw("Mouse X") * dir > 0)
        {
            // if attack the opposite direction => fish health decrease
            //Debug.Log("Health" + _health);
            transform.Translate(transform.right * pullForce * Time.deltaTime * dir);
            _health -= Time.deltaTime; 
        }
        else transform.Translate(transform.right * attackForce * Time.deltaTime * dir);

        Camera.main.transform.forward = (transform.position - Camera.main.transform.position).normalized;

        if (_health < 0) // if fish is catch
        {
            if (isFistTime) // catch first = fail by story line
            {
                transform.GetComponent<BaitControl>().Return();
                isFistTime = false;

                DialogueSystem.ds.Display(new int[] {3});
            }
            else // catch second = success
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetComponent<BaitControl>().Return();

                DialogueSystem.ds.Display(new int[] {4, 5});
                FishingGameManager.fgm.End();
            }
        }


    }

    void CreateWater()
    {
        // fish attack water
        LayerMask layer = 1 << 4;
        RaycastHit hit;
        
        if (Physics.Linecast(head.position, transform.position, out hit, layer))
        {
            //Debug.Log(hit.point);
            Instantiate(waterSource, hit.point, Quaternion.identity);

            SoundManager.sm.PlayOneShot(fishingRopeSound);
        }


    }

    
}
