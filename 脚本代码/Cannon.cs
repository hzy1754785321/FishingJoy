using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    float m_shootTimer = 0;  //射击计时
    public AudioClip m_shootClip;    //背景音乐
    protected AudioSource m_Audio;   //声源       
	void Start () {
        m_Audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateInput();
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    void UpdateInput()
    {
        m_shootTimer = m_shootTimer - Time.deltaTime;

        Vector3 ms = Input.mousePosition;  //获得鼠标位置
         print(ms);
         ms.z = 1;      //使用Screentoworldpoint时z不能为0值
        ms = Camera.main.ScreenToWorldPoint(ms);

        Vector3 mypos = this.transform.position;  //炮台的位置

        if(Input.GetMouseButton(0))
        {
        //    print("test shoot");
            Vector2 targetDir = ms - mypos;   //计算鼠标与炮塔位置的角度
            float angle = Vector2.Angle(targetDir, Vector3.up);
            if (ms.x > mypos.x)
                angle = -angle;
            this.transform.eulerAngles = new Vector3(0, 0, angle);   //转向

            if(m_shootTimer<=0)
            {
                m_Audio.PlayOneShot(m_shootClip);

                m_shootTimer = 0.3f;   //射击间隔
                shoot.Create(this.transform.TransformPoint(0, 1, 0), new Vector3(0, 0, angle));  //射击.创建子弹
            }
        }
    }
}
