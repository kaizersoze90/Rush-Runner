using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] PaintWallManager wallManager;
    [SerializeField] GameObject rankDisplay;
    [SerializeField] Slider paintedSlider;


    void Update()
    {
        paintedSlider.value = wallManager.CurrentProgress();
    }

    public float GetPaintPercentage()
    {
        return paintedSlider.value;
    }

    public void SetPaintSlider()
    {
        paintedSlider.gameObject.SetActive(true);
    }

    public void HideRank()
    {
        rankDisplay.SetActive(false);
    }
}
