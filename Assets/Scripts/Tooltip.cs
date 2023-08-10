using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private RectTransform rectTransform;

    public void SetText(string header, string content = "")
    {
        headerField.text = header;
        if (content == "")
        {
            contentField.gameObject.SetActive(false);
        }
        else
        {
            contentField.gameObject.SetActive(true);
            contentField.text = content;
        }
        UpdateSize();
    }

    private void UpdateSize()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength >= maxCharacters || contentLength >= maxCharacters) ? true : false;
    }

    private void Update()
    {
        MoveProportionaly();
    }

    private void MoveProportionaly()
    {
        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        transform.position = mousePosition;
    }
}
