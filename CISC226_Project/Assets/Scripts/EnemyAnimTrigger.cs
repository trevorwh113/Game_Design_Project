using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimTrigger : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Activate the enemy animation with a delay.
        if (anim != null && Input.GetMouseButtonDown(1)) {
            StartCoroutine("AnimateEnemy");
        }
    }

    private IEnumerator AnimateEnemy() {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("Echo");
    }
}
