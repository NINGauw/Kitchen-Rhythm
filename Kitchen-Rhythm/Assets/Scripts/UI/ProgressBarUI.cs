using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField]private GameObject progressBarGameObject;
    [SerializeField]private Image barImage;
    private IProgressBar progressBar;
    private void Start(){
        progressBar = progressBarGameObject.GetComponent<IProgressBar>();        
        progressBar.OnProgressChange += ProgressBar_OnProgressChange;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void ProgressBar_OnProgressChange(object sender, IProgressBar.OnProgressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNomalized;
        //Debug.Log(e.progressNomalized);
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
