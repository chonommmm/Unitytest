﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//test、確認用
public class popEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject aircraft = null;
    [SerializeField] Vector3 offset = new Vector3(0,100,100);

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            Vector3 appearancepos = this.transform.position;
            appearancepos.z += 50;
            GameObject airCraft = Instantiate
            (aircraft,this.transform.position + offset,this.transform.rotation) as GameObject;
        }
    }
}
