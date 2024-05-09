using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]private CuttingCounter cuttingCounter;
    [SerializeField]private Image barImage;

    private void Start(){
        cuttingCounter.OnProgressChange += CuttingCounter_OnProgressChange;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void CuttingCounter_OnProgressChange(object sender, CuttingCounter.OnProgressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNomalized;
        Debug.Log(e.progressNomalized);
        if(e.progressNomalized == 0f || e.progressNomalized == 1f){
            Hide();
        }
        else{
            Show();
        }
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}
