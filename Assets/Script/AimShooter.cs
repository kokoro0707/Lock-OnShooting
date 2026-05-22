using UnityEngine;



public class AimShooter : MonoBehaviour

{

    [Header("ÉJÉÅÉâ")]

    public Camera mainCamera;



    [Header("èeå˚")]

    public Transform firePoint;



    [Header("éÀåÇê›íË")]

    public float shootRange = 100f;

    public int damage = 10;

    public LayerMask hitLayer;



    [Header("íeÇÃå©ÇΩñ⁄")]

    public GameObject bulletVisualPrefab;

    public float bulletSpeed = 80f;



    void Update()

    {

        Aim();



        if (Input.GetMouseButtonDown(0))

        {

            Shoot();

        }

    }



    void Aim()

    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);



        if (Physics.Raycast(ray, out RaycastHit hit, shootRange))

        {

            Vector3 targetPos = hit.point;



            Vector3 dir = targetPos - firePoint.position;

            firePoint.rotation = Quaternion.LookRotation(dir);

        }

    }



    void Shoot()

    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);



        Vector3 targetPoint;



        if (Physics.Raycast(ray, out RaycastHit hit, shootRange, hitLayer))

        {

            targetPoint = hit.point;



            EnemyHP enemy = hit.collider.GetComponent<EnemyHP>();



            if (enemy != null)

            {

                enemy.TakeDamage(damage);

            }

        }

        else

        {

            targetPoint = ray.origin + ray.direction * shootRange;

        }



        if (bulletVisualPrefab != null)

        {

            GameObject bullet = Instantiate(

            bulletVisualPrefab,

            firePoint.position,

            Quaternion.identity

            );



            bullet.GetComponent<BulletVisual>().SetTarget(targetPoint, bulletSpeed);

        }

    }

}
