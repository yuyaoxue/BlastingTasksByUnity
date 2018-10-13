using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{

    [SerializeField]
    private GameObject launchPoint;
    [SerializeField]
    private GameObject prefabProjectile;

    private GameObject projectile;

    private Vector3 launchPos;
    private bool aimingMode;


    private void Awake()
    {
        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;
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

    private void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;
    }
}
