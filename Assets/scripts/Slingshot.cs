using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [SerializeField]
    private GameObject launchPoint;

    private void Awake()
    {
        launchPoint.SetActive(false);
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
        launchPoint.SetActive(false);
    }
}
