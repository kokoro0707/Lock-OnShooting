
using UnityEngine;



public class BulletVisual : MonoBehaviour

{

    private Vector3 target;

    private float speed;



    public void SetTarget(Vector3 targetPos, float bulletSpeed)

    {

        target = targetPos;

        speed = bulletSpeed;



        Vector3 dir = target - transform.position;

        transform.rotation = Quaternion.LookRotation(dir);

    }



    void Update()

    {

        transform.position = Vector3.MoveTowards(

        transform.position,

        target,

        speed * Time.deltaTime

        );



        if (Vector3.Distance(transform.position, target) < 0.1f)

        {

            Destroy(gameObject);

        }

    }

}


