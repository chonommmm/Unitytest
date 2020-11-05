﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileControl : MonoBehaviour
{
    enum State {
        stay,search,move,delete
    };
    GameObject target = null;
    Rigidbody m_rigidbody;
    [SerializeField]
    float thurust = 1000.0f;
    [SerializeField]
    Vector3 addforce;
    State state;


    // Start is called before the first frame update
    void Start()
    {
        // state = State.stay;
        // target = GameObject.Find("Player");
        // m_rigidbody = GetComponent<Rigidbody>();
    }

    

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case State.stay:
                break;
            case State.search:
                this.transform.LookAt(target.transform);
                Vector3 velo = m_rigidbody.velocity;
                velo.y = (-velo.y)+5;
                m_rigidbody.AddForce(velo *10);
                if(m_rigidbody.velocity.y >0){
                    state = State.move;
                    GetComponentInChildren<Collider>().enabled = true;
                    Debug.Log("State.search");
                }
                break;
            case State.move:
                StartCoroutine(statectl());

                m_rigidbody.AddForce(transform.forward * thurust);
                
                break;
            case State.delete:
                Destroy(this.gameObject);
                break;
        }

    }
    private IEnumerator statectl(){
        yield return new WaitForSeconds(2);
        state = State.delete;
    }
    public void Fire(){
        target = GameObject.Find("Player");
        m_rigidbody = GetComponent<Rigidbody>();
        state = State.search;
        this.transform.LookAt(target.transform.position);
        m_rigidbody.velocity = (Vector3.down * 10);
        m_rigidbody.AddForce(m_rigidbody.velocity);
    }
}