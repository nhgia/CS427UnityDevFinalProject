using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent (typeof(TextMeshPro))]
public class TextEffect : MonoBehaviour
{
    TextMeshPro _txtMesh;
    string[] _txtChar;
    public bool isActive;
    public float time;
    float timer;
    int charCount;
    void Start()
    {
        _txtMesh = GetComponent<TextMeshPro>();
        _txtChar = new string[_txtMesh.text.Length];
        for (int i = 0; i< _txtMesh.text.Length; i++)
        {
            _txtChar[i] = _txtMesh.text.Substring(i, 1);
        }
        _txtMesh.text = "";
        charCount = 0;
        timer = 0;
    }

    void Update()
    {
        if (isActive)
        {
            
            if (charCount < _txtChar.Length)
            {
                timer += Time.deltaTime;
                if (timer >= time)
                {
                    _txtMesh.text += _txtChar[charCount];
                    charCount++;
                    timer = 0;
                }
            }

            /*if (charCount == _txtChar.Length)
            {
                                if (transform.childCount > 0)
                                {
                                    transform.GetChild(0).GetComponent<TextEffect>().isActive = true;
                                    charCount++;
                                }
                
            }*/
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
            isActive = true;
    }
}

//public class Typewriter : MonoBehaviour
//{

//    public float delay = 0.1f;
//    public string fullText;
//    private string currentText = "";

//    // Use this for initialization
//    void Start()
//    {
//        StartCoroutine(ShowText());
//    }

//    IEnumerator ShowText()
//    {
//        for (int i = 0; i < fullText.Length; i++)
//        {
//            currentText = fullText.Substring(0, i);
//            this.GetComponent<Text>().text = currentText;
//            yield return new WaitForSeconds(delay);
//        }
//    }

//}
