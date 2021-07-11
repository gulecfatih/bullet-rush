using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    GameControl gameControl;


   [SerializeField] GameObject kursunPrefab = default;


    
    // Angular speed in radians per sec.
    public float speed = 5f;
    [SerializeField] float time;
    // Start is called before the first frame update
    void Start()
    {
        gameControl = Camera.main.GetComponent<GameControl>();
        StartCoroutine(SpawnObject(time));
    }
 
    // Update is called once per frame
    void Update()
    {
        int index = 0;
        int i = 0;
        float mesafe = 1000;
        if (gameControl.enemyList.Count > 0) { 
        foreach (GameObject enemy in gameControl.enemyList)
        {
            
            float deger;
            deger = Vector3.Distance(transform.position, enemy.transform.position);
            if (deger < mesafe)
            {
               
                mesafe = deger;
                index = i;
            }

            i++;
        }
        
        Vector3 Direction = gameControl.enemyList[index].transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);


        

        }
       

    }


    public IEnumerator SpawnObject(float time) // belli zaman aralýklarla merminin atýlmasý
    {
        while (gameControl.enemyList.Count > 0)
        {
            GameObject yeniTop = Instantiate(kursunPrefab, transform.position, transform.rotation) as GameObject;   // Merminin objeye atýlmasý
            yeniTop.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1000);
            yield return new WaitForSeconds(time);
        }

    }



}
