using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextBlock
{
    [TextArea(2,10)]
    [SerializeField]
    public string Text;
}
