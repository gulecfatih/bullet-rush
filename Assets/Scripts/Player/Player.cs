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


        speedModifier = 0.05f;// mobil hýz

        // Player'ýn sahip olduðu önemli component'leri deðiþkenlerde depola
        rb = GetComponent<Rigidbody>();
       

        // Kameraya göre ileri (forward) ve sað (right) yönleri hesapla
        // Bu vektörleri, karakteri kameranýn baktýðý yönde hareket
        // ettirmek için kullanacaðýz
        kameraForward = Camera.main.transform.forward;
        kameraRight = Camera.main.transform.right;

        // Kameranýn aþaðý-yukarý eðimini yoksay
        kameraForward.y = 0f;
        kameraRight.y = 0f;

        // Yön vektörlerinin uzunluklarýnýn 1 olmasýný saðla
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
        // Eðer hareket tuþlarýndan en az birine basýlýyorsa
        if (horizontalInput != 0 || verticalInput != 0)
        {
          

            // Karakterin hareket edeceði yönü hesapla
            Vector3 hareketYonu = (kameraForward * verticalInput + kameraRight * horizontalInput).normalized * hareketHizi * Time.deltaTime;

            // Karakteri hareket yönüne doðru yumuþak bir þekilde döndür
            Quaternion hedefRotation = Quaternion.LookRotation(hareketYonu, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, hedefRotation, donmeHizi * Time.deltaTime);

            // Karakteri rigidbody vasýtasýyla hareket ettir
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
