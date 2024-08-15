using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effect_player : MonoBehaviour
{
    List<GameObject> effect_list = new List<GameObject>();
    public Animator animator;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        // test = animator.GetBool("s_at");
        // Debug.Log(test);
    }
}
