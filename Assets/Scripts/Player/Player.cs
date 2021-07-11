using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Touch touch;
    private float speedModifier;
    public Text winLose;
    public Button resbutton;

    private Rigidbody rb;



    private float horizontalInput = 0;
    private float verticalInput = 0;

    private Vector3 kameraForward;
    private Vector3 kameraRight;

    public float hareketHizi = 7f;
    public float donmeHizi = 360f;


  
    void Start()
    {
        Time.timeScale = 1;

        resbutton.gameObject.SetActive(false);


        speedModifier = 0.05f;// mobil h�z

        // Player'�n sahip oldu�u �nemli component'leri de�i�kenlerde depola
        rb = GetComponent<Rigidbody>();
       

        // Kameraya g�re ileri (forward) ve sa� (right) y�nleri hesapla
        // Bu vekt�rleri, karakteri kameran�n bakt��� y�nde hareket
        // ettirmek i�in kullanaca��z
        kameraForward = Camera.main.transform.forward;
        kameraRight = Camera.main.transform.right;

        // Kameran�n a�a��-yukar� e�imini yoksay
        kameraForward.y = 0f;
        kameraRight.y = 0f;

        // Y�n vekt�rlerinin uzunluklar�n�n 1 olmas�n� sa�la
        kameraForward.Normalize();
        kameraRight.Normalize();

    }
    
    void MobilMove()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * speedModifier, +
                    transform.position.y, transform.position.z + touch.deltaPosition.y * speedModifier);
            }

        }
    }


    void FixedUpdate()
    {
        // E�er hareket tu�lar�ndan en az birine bas�l�yorsa
        if (horizontalInput != 0 || verticalInput != 0)
        {
          

            // Karakterin hareket edece�i y�n� hesapla
            Vector3 hareketYonu = (kameraForward * verticalInput + kameraRight * horizontalInput).normalized * hareketHizi * Time.deltaTime;

            // Karakteri hareket y�n�ne do�ru yumu�ak bir �ekilde d�nd�r
            Quaternion hedefRotation = Quaternion.LookRotation(hareketYonu, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, hedefRotation, donmeHizi * Time.deltaTime);

            // Karakteri rigidbody vas�tas�yla hareket ettir
            rb.MovePosition(rb.position + hareketYonu);
        }
        else
        {
          
            
        }
    }



    void Update()
    {
#if UNITY_EDITOR
        // Klavyeden input al
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");


#else
        MobilMove();
#endif


    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            resbutton.gameObject.SetActive(true);
            winLose.text = "Kaybettin";
            Time.timeScale = 0;
            winLose.gameObject.SetActive(true);
        }
    }


}
