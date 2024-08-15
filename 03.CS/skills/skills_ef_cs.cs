using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class skills_ef : MonoBehaviour
{
    public player pc;
    public skills_manager.skill sk_op;
    public bool truk;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        
        transform.localScale = sk_op.skills_size;
        Destroy(gameObject,sk_op.life_time);
    }

    // Update is called once per frame
    void Update()
    {
        if(truk){
            transform.rotation = pc.transform.rotation;
            Vector3 t_pos = new Vector3(
                sk_op.sp_pos.transform.position.x+(sk_op.sp_pos2.x * MathF.Cos((pc.transform.rotation.eulerAngles.y-90)*(Mathf.PI / 180))),
                sk_op.sp_pos.transform.position.y + sk_op.sp_pos2.y,
                sk_op.sp_pos.transform.position.z+(sk_op.sp_pos2.z * Mathf.Sin((pc.transform.rotation.eulerAngles.y+90)*(Mathf.PI / 180)))
            );
            transform.position = t_pos;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "monster"){
            demege_test test = other.gameObject.GetComponent<demege_test>();
            test.on_damage(sk_op.demege);
        }
    }
}
