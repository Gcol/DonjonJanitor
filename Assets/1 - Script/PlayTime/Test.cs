using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    public TMP_Text textArea;

    public void SubmitName()
    {

        Debug.Log(textArea.text);
    }
}