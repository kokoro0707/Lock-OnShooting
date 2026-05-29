
using System.Collections;

using UnityEngine;



public class AimShooter : MonoBehaviour

{

    [Header("カメラ")]

    public Camera mainCamera;



    [Header("銃口")]

    public Transform firePoint;



    [Header("射撃設定")]

    public float shootRange = 100f;

    public int damage = 10;

    public LayerMask hitLayer;



    [Header("弾の見た目")]

    public GameObject bulletVisualPrefab;

    public float bulletSpeed = 80f;



    [Header("弾数管理")]

    public int maxAmmo = 30;

    public int currentAmmo;

    public float reloadTime = 1.5f;



    [Header("連射設定")]

    public float fireRate = 0.1f;



    private bool isReloading = false;

    private float nextFireTime = 0f;



    void Start()

    {

        currentAmmo = maxAmmo;

    }



    void Update()

    {

        Aim();



        // 左クリック長押しで連射

        if (Input.GetMouseButton(0))

        {

            TryShoot();

        }



        // Rキーでリロード

        if (Input.GetKeyDown(KeyCode.R))

        {

            StartReload();

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



    void TryShoot()

    {

        if (isReloading) return;

        if (Time.time < nextFireTime) return;



        if (currentAmmo <= 0)

        {

            StartReload();

            return;

        }



        nextFireTime = Time.time + fireRate;

        currentAmmo--;



        Shoot();



        Debug.Log("残弾：" + currentAmmo + " / " + maxAmmo);

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



            BulletVisual bulletVisual = bullet.GetComponent<BulletVisual>();



            if (bulletVisual != null)

            {

                bulletVisual.SetTarget(targetPoint, bulletSpeed);

            }

        }

    }



    void StartReload()

    {

        if (isReloading) return;

        if (currentAmmo == maxAmmo) return;



        StartCoroutine(ReloadCoroutine());

    }



    IEnumerator ReloadCoroutine()

    {

        isReloading = true;

        Debug.Log("リロード中...");



        yield return new WaitForSeconds(reloadTime);



        currentAmmo = maxAmmo;

        isReloading = false;



        Debug.Log("リロード完了！");

    }

}

