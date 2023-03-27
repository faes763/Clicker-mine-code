using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Rock rock;
    public Pickaxe pickaxe;

    public int countHit = 0;
    public int countMiss = 0;

    public TargetMove moveBlock;
    private string tagCollider;
    public bool target;

    private void OnTriggerEnter2D(Collider2D other) {
        target = true;
        tagCollider = other.tag;
    }
     void OnTriggerExit2D(Collider2D other) {
        target= false;
    }   
    public void dealingDamageRock() {
        rock.TakeDamage(pickaxe.damage);
            if(pickaxe._double == 1) {
                countHit++;
                if(countHit%5==0) rock.TakeDamage(pickaxe.damage);
            }
        moveBlock.speed*=1.01f;
    }
    public void lossStability() {
        countMiss++;
            if(countMiss%3 == 0 && pickaxe.hitStability == 1) {
                Debug.Log("Прочность не потратилась");
            } else {
                pickaxe.currentStability-=1;
                pickaxe.checkStability();
                moveBlock.speed=180f;
            }
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && target) {
            dealingDamageRock();
        } else if(Input.GetKeyUp(KeyCode.Space) && !target) {
            lossStability();
        }
    }
}
