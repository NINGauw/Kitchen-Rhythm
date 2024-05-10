using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField]private PlateCounter plateCounter;
    [SerializeField]private Transform counterTopPoint;
    [SerializeField]private Transform plateVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;
    private void Awake(){
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start(){
        plateCounter.OnPlateSpawned += plateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += plateCounter_OnPlateRemoved;
    }

    private void plateCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void plateCounter_OnPlateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTranform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = .2f;
        plateVisualTranform.localPosition = new Vector3(0 ,plateOffsetY * plateVisualGameObjectList.Count ,0);
        plateVisualGameObjectList.Add(plateVisualTranform.gameObject);
    }
}
