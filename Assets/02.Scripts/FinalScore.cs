﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    [SerializeField]
    Text killText;

    void Start()
    {
        killText.text = "Kill :" + "<color=#ff0000>" + G_Manager.instance.total.ToString()+ "</color>";
    }
}
