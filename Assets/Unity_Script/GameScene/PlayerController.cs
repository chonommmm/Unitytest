﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // speedを制御する
    public float speed = 10;
    public float moveForceMultiplier;

    // 水平移動時に機首を左右に向けるトルク
    public float yawTorqueMagnitude = 30.0f;

    // 垂直移動時に機首を上下に向けるトルク
    public float pitchTorqueMagnitude = 60.0f;

    // 水平移動時に機体を左右に傾けるトルク
    public float rollTorqueMagnitude = 30.0f;

    // バネのように姿勢を元に戻すトルク
    public float restoringTorqueMagnitude = 100.0f;

    private Vector3 Player_pos;
    private new Rigidbody rigidbody;
    public bool rb_freezepos_left = false;
    public bool rb_freezepos_right = false;
    public bool rb_freezepos_top = false;
    public bool rb_freezepos_bottom = false;

    public float multiplay_x;
    public float multiplay_y;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "camera_limit_left" ){
            rb_freezepos_left = true;
        }
        if(other.gameObject.tag == "camera_limit_right" ){
            rb_freezepos_right = true;
        }
        if(other.gameObject.tag == "camera_limit_top" ){
            rb_freezepos_top = true;
        }
        if(other.gameObject.tag == "camera_limit_bottom" ){
            rb_freezepos_bottom = true;
        }
        }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        // バネ復元力でゆらゆら揺れ続けるのを防ぐため、angularDragを大きめにしておく
        rigidbody.angularDrag = 20.0f;
        
    }

    void FixedUpdate()
    {
        NotOffScreen();
        

        this.multiplay_x = Input.GetAxis("Horizontal");
        this.multiplay_y = Input.GetAxis("Vertical");
        float x = this.multiplay_x;
        float y = this.multiplay_y ;

        
        // xとyにspeedを掛ける
        rigidbody.AddForce(x * speed, y * speed, 0);

        Vector3 moveVector = Vector3.zero;

        rigidbody.AddForce(moveForceMultiplier * (moveVector - rigidbody.velocity));
        
        this.rigidbody.drag = 2;

        // プレイヤーの入力に応じて姿勢をひねろうとするトルク
        Vector3 rotationTorque = new Vector3(-y * pitchTorqueMagnitude, x * yawTorqueMagnitude, -x * rollTorqueMagnitude);

        // 現在の姿勢のずれに比例した大きさで逆方向にひねろうとするトルク
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Vector3 forward = transform.forward;
        Vector3 restoringTorque = new Vector3(forward.y - up.z, right.z - forward.x, up.x - right.y) * restoringTorqueMagnitude;

        // 機体にトルクを加える
        rigidbody.AddTorque(rotationTorque + restoringTorque);


    }

    void NotOffScreen(){
        if (rb_freezepos_top){
            rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            if (Input.GetKey("down") && !Input.GetKey("up")  && !Input.GetKey("left")&& !Input.GetKey("right")){
                rb_freezepos_top = false;
                rigidbody.constraints = RigidbodyConstraints.None;
            }
        }
        if (rb_freezepos_bottom){
            rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            if (Input.GetKey("up") && !Input.GetKey("down")){
                rb_freezepos_bottom = false;
                rigidbody.constraints = RigidbodyConstraints.None;
            }
        }
        if (rb_freezepos_left){
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            if (Input.GetKey("right") && !Input.GetKey("left")&& !Input.GetKey("down")&& !Input.GetKey("up") ){
                rb_freezepos_left = false;
                rigidbody.constraints = RigidbodyConstraints.None;
            }
        }
        if (rb_freezepos_right){
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            if (Input.GetKey("left") && !Input.GetKey("right")){
                rb_freezepos_right = false;
                rigidbody.constraints = RigidbodyConstraints.None;
            }
        }
        
    }

}
