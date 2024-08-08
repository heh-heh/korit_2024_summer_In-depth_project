using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class claon_level : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject clone_obj;
    public int claon_count_x;
    public int claon_count_y;
    public Vector2 claon_pos;
    public bool is_claon = true;
    void Start()
    {
        Vector3 [] t_pos = new Vector3[claon_count_x*claon_count_y];
        int ii=0;
        if(is_claon){
            for(int i=0; i<claon_count_x; i++){
                for(int j=0; j<claon_count_y; j++){
                    t_pos[ii++] = new Vector3(
                        clone_obj.transform.position.x + (claon_pos.x * i),
                        clone_obj.transform.position.y,
                        clone_obj.transform.position.z + (claon_pos.y * j));
                }
            }
            for(int i=1; i<claon_count_x*claon_count_y; i++){
                    GameObject test = Instantiate(clone_obj,  t_pos[i], clone_obj.transform.rotation);
                    claon_level test2 = test.GetComponent<claon_level>();
                    test2.is_claon = false;
            }
        }
    }

    // Update is called once per frame
}
