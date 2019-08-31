using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSelect : MonoBehaviour
{
    public Color hightlightColor;
    public bool isLife;

    TextMeshPro textMeshPro;
    Color initOne;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        initOne = textMeshPro.color;
    }


    private void OnMouseOver()
    {
        textMeshPro.color = hightlightColor;

        if (Input.GetMouseButtonDown(0))
        {
            if (isLife)
            {
                FinalRoomManager.frm.ChooseLife();
            }
            else
            {
                FinalRoomManager.frm.ChooseMom();
            }


        }



    }

    private void OnMouseExit()
    {
        textMeshPro.color = initOne;
    }
}
