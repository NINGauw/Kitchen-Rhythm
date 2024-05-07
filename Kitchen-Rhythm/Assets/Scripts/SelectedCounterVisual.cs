using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private Counter counter;
    [SerializeField] private GameObject visualGameObject;
    private void Start(){
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        Hide();
    }
    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == counter){
            Show();
        }
        else{
            Hide();
        }
    }
    private void Show(){
        visualGameObject.SetActive(true);
    }
    private void Hide(){
        visualGameObject.SetActive(false);
    }
}
