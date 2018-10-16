using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool goldMet = false;

    private void OnTriggerEnter(Collider other)
    {
       // 当其他物体撞道触发器时
       
        // 检查是否第弹丸
        if(other.gameObject.tag == GameConst.Tag_Projectile)
        {
            //如果是弹丸，设置goalMet 为 true
            Goal.goldMet = true;

            //
            Color c = this.GetComponent<Renderer>().material.color;
            c.a = 1;
            this.GetComponent<Renderer>().material.color = c;
        }
    }
}
