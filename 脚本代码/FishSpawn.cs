using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour {
    public float timer = 0;  //计时
    public int max_fish = 30;   //最大鱼数
    public int fish_count = 0;   //当前数量
    public AudioClip m_moneyClip;
    protected AudioSource m_moneyAudio;
	// Use this for initialization
	void Start () {
        m_moneyAudio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        timer = timer - Time.deltaTime;
        if(timer<=0)
        {
            timer = 2.0f;
            if (fish_count >= max_fish)
                return;
            int index = 1 + (int)(Random.value * 3.0f);
            if (index > 3)
                index = 3;    //最大为3，防止溢出
            fish_count++;
            GameObject fishprefab = (GameObject)Resources.Load("fish" + index);  //读取鱼的prefab
            float cameraz = Camera.main.transform.position.z;
            Vector3 randpos = new Vector3(Random.value, Random.value, -cameraz);   //鱼的初始位置
            randpos = Camera.main.ViewportToWorldPoint(randpos);

            Fish.Target target = Random.value > 0.5f ? Fish.Target.Right : Fish.Target.Left;   //鱼的初始方向
            Fish f = Fish.Create(fishprefab, target, randpos);

            f.OnDeath = f.OnDeath + OnDeath;   //处理鱼的死亡信息
            
        }
	}

    public void OnDeath(Fish fish)
    {
        fish_count--;
        playMusic();
    }

    void playMusic()
    {
        m_moneyAudio.clip = m_moneyClip;
        m_moneyAudio.Play();
    }
}
