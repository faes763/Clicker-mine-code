using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobileTouch : MonoBehaviour
{
    public Target target;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
               if(target.target) {
                target.dealingDamageRock();
               } else {
                target.lossStability();
               }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // Действия при окончании нажатия на экран
            }
        }
    }
}
