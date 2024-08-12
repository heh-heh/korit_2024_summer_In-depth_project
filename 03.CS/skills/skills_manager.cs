using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;



public class skills_manager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [Serializable] 
    public struct skill{
        public string ID;
        public GameObject skills_obj;//��ų ������Ʈ
        public Vector3 skills_size;//��ų ������Ʈ ũ��
        public bool none_attect;
        public int skill_type;//��ų Ÿ��
        public float demege;//������
        public float speed;//�ӵ�
        public float life_time;//����ð�
        public float cool_time;//��Ÿ��
        public float before_delay;//�� ������
        public float after_delay;//�� ������
        public bool Penetrated; //���� ����
        public int count;//����
        public int delay;//������?
        public GameObject target_ting;//���������� ����� Ÿ��
        public GameObject sp_pos;//����� ������Ʈ or ���� ��ġ
        public Vector3 sp_pos2;
        public string next_at_ID;
    }
    public List<Vector3> temp = new List<Vector3>();
    [Header("skill_parameter")]
    public List<skill> skills = new List<skill>();

    public List<skills_stat.skill> skill_list = new List<skills_stat.skill>();
    public Dictionary<string,skills_stat.skill> skill_dict = new Dictionary<string,skills_stat.skill>();
    [Space(20f)]
    private Dictionary<string, float> cooldownTimers;
    // public skiils skiils_option;
    void Start()
    {
        cooldownTimers = new Dictionary<string, float>();
        foreach(skills_stat.skill skill in skill_list){
            skill_dict.Add(skill.ID, skill);
            cooldownTimers.Add(skill.ID,0f);
            // Debug.Log(skill_dict[skill.ID].ID);
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach(skill skill in skills){
            if(cooldownTimers[skill.ID]>0){
                cooldownTimers[skill.ID] -= Time.deltaTime;
            }
        }
    }
    public int use_skill(string ID, GameObject use){
        skills_stat.skill skill_op = skill_dict[ID];
        GameObject skill_obj;
        if(cooldownTimers[skill_op.ID] <= 0){
            cooldownTimers[skill_op.ID] = skill_op.cool_time;
            if(skill_op.skill_type == 0){ //���� ��ų
                if(skill_op.sp_pos.GetComponent<sword_aura>() == null && skill_op.skills_obj == null){
                    sword_aura sw_test = skill_op.sp_pos.AddComponent<sword_aura>();
                    skill_stat2 skill_Stat = sw_test.AddComponent<skill_stat2>();
                    skill_Stat.skill_op = skill_op;
                } 
                else{
                    float agl = 0;
                    if(skill_op.count > 1) agl = 360 / skill_op.count;
                    for(int i=0; i<skill_op.count; i++){
                        Quaternion t_rot = Quaternion.Euler(new Vector3(
                            0f,
                            skill_op.sp_pos.transform.rotation.eulerAngles.y+(agl*i),
                            0
                        ));
                        Vector3 t_pos = new Vector3(
                            skill_op.sp_pos.transform.position.x+(skill_op.sp_pos2.x * MathF.Cos((t_rot.eulerAngles.y-90)*(Mathf.PI / 180))),
                            skill_op.sp_pos.transform.position.y + skill_op.sp_pos2.y,
                            skill_op.sp_pos.transform.position.z+(skill_op.sp_pos2.z * Mathf.Sin((t_rot.eulerAngles.y+90)*(Mathf.PI / 180)))
                        );
                        skill_obj = Instantiate(skill_op.skills_obj,  t_pos, t_rot);
                        skill_stat2 skill_Stat2 = skill_obj.AddComponent<skill_stat2>(); 
                        sword_aura sword_Aura = skill_obj.AddComponent<sword_aura>();
                    }
                }
                // sw_test.skill_op = skills[skill_index];
            }
            else if(skill_op.skill_type == 1){//���Ÿ� ��ų
                Quaternion t_rot = Quaternion.Euler(new Vector3(
                    0f,
                    skill_op.sp_pos.transform.rotation.eulerAngles.y,
                    0
                ));
                Vector3 t_pos = new Vector3(
                    skill_op.sp_pos.transform.position.x+(skill_op.sp_pos2.x * MathF.Cos((t_rot.eulerAngles.y-90)*(Mathf.PI / 180))),
                    skill_op.sp_pos.transform.position.y + skill_op.sp_pos2.y,
                    skill_op.sp_pos.transform.position.z+(skill_op.sp_pos2.z * Mathf.Sin((t_rot.eulerAngles.y+90)*(Mathf.PI / 180)))
                );
                skill_obj = Instantiate(skill_op.skills_obj, t_pos, t_rot);
                bullet bullet_sp = skill_obj.AddComponent<bullet>();
                bullet_sp.skill_op = skill_op;
                skill_obj.transform.localScale = skill_op.skills_size;
            }
            else if(skill_op.skill_type == 2){
                dash dash = skill_op.sp_pos.AddComponent<dash>();
                skill_stat2 skill_Stat = dash.AddComponent<skill_stat2>();
                skill_Stat.skill_op = skill_op; dash.dash_type = 0;
            }
            else if(skill_op.skill_type == 3){
                if(skill_op.sp_pos.GetComponent<dash>() == null){
                    dash dash = skill_op.sp_pos.AddComponent<dash>();
                    dash.dash_type = 1;
                    dash.pc = skill_op.target_ting.transform;
                    dash.TeleportAreaCenter = temp[0];
                    dash.TeleportAreaSize = temp[1];
                }
            }
        }
        else{
            Debug.Log("is_cooldwon");
            return 1;
        }
        return 0;
    }
}
