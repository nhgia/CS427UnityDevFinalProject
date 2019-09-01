using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float speed = 2f;
    public GameObject chooseCanvas;

    public SpriteRenderer meleeWeapon;
    public SpriteRenderer rangeWeapon;
    public SpriteRenderer pet;

    [HideInInspector] public bool move = true;

    private void Start()
    {
        DialogueSystem.ds.Display(new int[] {0,1,2});
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }



    private void Update()
    {
        if (move) transform.Translate(transform.right * speed * Time.deltaTime);
    }

    public void ChangeMeleeWeapon(Sprite sprite)
    {
        meleeWeapon.sprite = sprite;
        meleeWeapon.gameObject.SetActive(true);
    }

    public void ChangeRangeWeapon(Sprite sprite)
    {
        rangeWeapon.sprite = sprite;
        rangeWeapon.gameObject.SetActive(true);
    }

    public void ChangePet(Sprite sprite)
    {
        pet.sprite = sprite;
        pet.gameObject.SetActive(true);
    }

    public void TryToTest()
    {
        //gameObject.SetActive(false);
        DialogueSystem.ds.DoSomething();
    }

}
