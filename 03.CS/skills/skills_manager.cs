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
        public GameObject skills_obj;//스킬 오브젝트
        public Vector3 skills_size;//스킬 오브젝트 크기
        public bool none_attect;
        public int skill_type;//스킬 타입
        public float demege;//데미지
        public float speed;//속도
        public float life_time;//실행시간
        public float cool_time;//쿨타임
        public float before_delay;//전 딜레이
        public float after_delay;//후 딜레이
        public bool Penetrated; //관통 여부
        public int count;//갯수
        public int delay;//딜레이?
        public GameObject target_ting;//유동적으로 사용할 타켓
        public GameObject sp_pos;//사용할 오브젝트 or 스폰 위치
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
            if(skill_op.skill_type == 0){ //근접 스킬
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
            else if(skill_op.skill_type == 1){//원거리 스킬
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
