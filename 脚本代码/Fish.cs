using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    protected float m_moveSpeed = 2.0f;  //速度
    protected int m_life = 5;    //生命值

  
    public enum Target
    {
        Left=0,
        Right=1
    }
    public Target m_target = Target.Right;   //当前移动方向（目标方向）
    public Vector3 m_targetPosition;  //目标位置

    public delegate void VoidDelegate(Fish fish);
    public VoidDelegate OnDeath;   //死亡回调

    public static Fish Create(GameObject prefab,Target target,Vector3 pos)    //创建实例
    {
        GameObject go = (GameObject)Instantiate(prefab, pos, Quaternion.identity);   //给实例生成函数提供参数
        Fish fish = go.AddComponent<Fish>();
        fish.m_target = target;
        return fish;
    }

    public void SetDamage(int damage)
    {
        m_life = m_life - damage;
        GameObject prefab = Resources.Load<GameObject>("boom");
        GameObject boom = (GameObject)Instantiate(prefab, this.transform.position, this.transform.rotation); //创建爆炸
        Destroy(boom, 1.0f);
        if(m_life<=0)
        {
  
            GameObject money = Resources.Load<GameObject>("money");
            GameObject m = (GameObject)Instantiate(money, this.transform.position, this.transform.rotation);
            Destroy(m, 2.0f);
       //     OnDeath(this);
            Destroy(this.gameObject);
        }
    }
	// Use this for initialization
	void Start () {
       
     //   m_moneyAudio = Resources.Load<AudioClip>("moneyMusic");
	}
	
    public void SetTarget()
    {
        float rand = Random.value;  //随机值
        Vector3 scale = this.transform.localScale;   //翻转方向
        scale.x = Mathf.Abs(scale.x) * (m_target == Target.Right ? 1 : -1);
        this.transform.localScale = scale;

        float cameraz = Camera.main.transform.position.z;   //获得摄像机z轴坐标
        print(cameraz);
        m_targetPosition = Camera.main.ViewportToWorldPoint(new Vector3((int)m_target, 1 * rand, -cameraz));  //设置目标位置
        print(m_targetPosition);
    }

	// Update is called once per frame
	void Update () {
        m_targetPosition.z = -(Random.value*3);
        Vector3 pos = Vector3.MoveTowards(this.transform.position, m_targetPosition, m_moveSpeed * Time.deltaTime); //移动
	    if(Vector3.Distance(pos,m_targetPosition)<0.1f)  //如果到达目标位置
        {
            m_target = m_target == Target.Left ? Target.Right : Target.Left;
            SetTarget();   //设置方向
        }
        this.transform.position = pos;
	}
}
