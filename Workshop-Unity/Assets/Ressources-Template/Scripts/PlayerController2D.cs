using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController2D : MonoBehaviour
{
    public string gauche = "left";    //Possible de paramétrer directement dans l'éditeur
    public string droite = "right";
    public string saut = "space";
    public float speed = 5f;
    public float jumpSpeed = 5f;

    private int ground = 1;
    private Rigidbody2D rigid;
    private SpriteRenderer render;
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim.Play("idle");
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(saut) && ground > 0)
        {
            ground -= 1;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                anim.SetTrigger("Jump");
            rigid.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }
    
    private void FixedUpdate() // Ce code est largement améliorable mais propose une base de test
    {
        if (Input.GetKey(gauche))
        {
            render.flipX = true;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") && ground > 0)
                anim.SetTrigger("Walk");
            transform.position -= transform.right * speed * 0.01f;
        }
        else if (Input.GetKey(droite))
        {
            render.flipX = false;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") && ground > 0)
                anim.SetTrigger("Walk");
            transform.position += transform.right * speed * 0.01f;
        }
        else
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && ground > 0)
                anim.SetTrigger("Idle");
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground") && transform.position.y >= coll.transform.position.y)
            ground = 1;
    }
}