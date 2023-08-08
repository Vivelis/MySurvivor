using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;

    [SerializeField]
    private GameObject tooltip;

    private void Awake()
    {
        instance = this;
    }

    public void Show()
    {
        tooltip.SetActive(true);
    }

    public void Hide()
    {
        tooltip.SetActive(false);
    }
}
