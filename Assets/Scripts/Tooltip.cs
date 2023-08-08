using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private Text headerField;
    [SerializeField]
    private Text contentField;

    [SerializeField]
    private LayoutElement layoutElement;

    [SerializeField]
    private int maxCharacters;

    private void Update()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength >= maxCharacters || contentLength >= maxCharacters) ? true : false;
    }
}
