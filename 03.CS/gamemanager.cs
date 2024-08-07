using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    static public bool pause;
    public player pc;
    public skills skills;
    // static public skills_manager skills_Manager;

    public GameObject player_c;
    public GameObject esc_menu;
    
    static public float pi = 3.141592f;
    public TextMeshProUGUI game_over_txt;

    public static bool GameOver = false;
    public float time;
    void Start()
    {
        player_c = GameObject.FindGameObjectWithTag("Player");
        
        pc = player_c.GetComponent<player>();
        skills = player_c.GetComponent<skills>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(pause){
            Time.timeScale = 0;
        }
        else Time.timeScale = time;
        // Debug.Log(pns.now_hp);
        // Debug.Log(pc);
    }
}
