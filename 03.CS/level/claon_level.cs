using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class claon_level : MonoBehaviour
{
    // Start is called before the first frame update
    public enum obj_type{
        wall,
        plane,
        tower
    };
    public obj_type cloan_type;

    public GameObject clone_obj;
    public List<int> claon_count = new List<int>();
    public Vector2 claon_pos;
    public Vector3 clzon_size;
    void Start()
    {
        // Quaternion [] t_rot = new 
        int size = claon_count[0] * claon_count[1];
        Vector3 [] t_pos = new Vector3[size];
        Quaternion [] t_rot = new Quaternion[size];
        int ii=0;
        if(cloan_type == obj_type.plane){
            for(int i=0; i<claon_count[0]; i++){
                for(int j=0; j<claon_count[1]; j++){
                    t_pos[ii++] = new Vector3(
                        clone_obj.transform.position.x + (claon_pos.x * i),
                        clone_obj.transform.position.y,
                        clone_obj.transform.position.z + (claon_pos.y * j));
                }
            }
        }
        else if(cloan_type == obj_type.wall){
            for(int i=0; i<claon_count[0]; i++){
                for(int j=0; j<claon_count[1]; j++){
                    t_pos[ii++] = new Vector3(
                        clone_obj.transform.position.x + (claon_pos.x * i),
                        clone_obj.transform.position.y+ (claon_pos.y * j),
                        clone_obj.transform.position.z );
                }
            }
        }
        else if(cloan_type == obj_type.tower){
            float agl = 360 / claon_count[0];
            for(int i=0; i<claon_count[0]; i++){
                t_rot[i] = Quaternion.Euler(new Vector3(0f,gameObject.transform.rotation.eulerAngles.y+(agl*i),0));
                t_pos[i] = new Vector3(
                    gameObject.transform.position.x+(claon_pos.x * MathF.Cos((t_rot[i].eulerAngles.y-90)*(Mathf.PI / 180))),
                    gameObject.transform.position.y,
                    gameObject.transform.position.z+(claon_pos.x * Mathf.Sin((t_rot[i].eulerAngles.y+90)*(Mathf.PI / 180)))
                );
            }
        }
            // Debug.Log(size);
        for(int i=0; i<size; i++){
            Debug.Log(t_pos[i]);
            Debug.Log(t_rot[i]);
            GameObject test = Instantiate(clone_obj,  t_pos[i], t_rot[i]);
            test.gameObject.transform.localScale = clzon_size;
        }
        
    }
}
