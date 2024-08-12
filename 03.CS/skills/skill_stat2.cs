using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill_stat2 : MonoBehaviour
{
    // Start is called before the first frame update
    public skills_stat.skill skill_op;
    private void Start() {
        Destroy(this,skill_op.life_time);
    }
}
