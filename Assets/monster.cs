using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class MonsterAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float rot_speed = 5.0f; 
    public Transform target;
    public Rigidbody monsterrigid;
    public Animator animator;


    private void Start()
    {
        monsterrigid = GetComponent<Rigidbody>();
        animator = GetComponent <Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rot_speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
        }
    }
}
