using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour {
    float m_moveSpeed = 7.0f;  //速度
  

    public static shoot Create(Vector3 pos,Vector3 angle)    //创建子弹实例
    {
        GameObject prefab = Resources.Load<GameObject>("fire");   //读取子弹prefab
        GameObject shootSprite = (GameObject)Instantiate(prefab, pos, Quaternion.Euler(angle));  //创建子弹sprite实例
        shoot f = shootSprite.AddComponent<shoot>();
        Destroy(shootSprite, 2.0f);
        return f;
    }
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(new Vector3(0, m_moveSpeed * Time.deltaTime));  //更新子弹位置
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        Fish f = other.GetComponent<Fish>();
        if (f == null)
            return;
        else
            f.SetDamage(1);
        Destroy(this.gameObject);
    }
}
