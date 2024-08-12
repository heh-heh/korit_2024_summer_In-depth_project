using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class skills_stat : MonoBehaviour
{
    [Serializable] 
    public struct skill{
        public string ID;
        public GameObject skills_obj;//스킬 오브젝트f
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
        public bool is_stun;
        public string next_at_ID;
    }
    
}
