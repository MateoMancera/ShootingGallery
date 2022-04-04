using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class armasPersonaje : MonoBehaviour
{
    public GameObject arma1, arma2, arma3, bulletSpawnPoint;
    [SerializeField]
    private List<GameObject> armasRecogidas;

    [SerializeField]
    private int armaActual, armasTotales;

    [SerializeField]
    private bool canSwitchWeapon;

    [SerializeField]
    public ajustesArmas ajustesArma1, ajustesArma2, ajustesArma3;
    [SerializeField]
    private List<ajustesArmas> ajustesArmas;
    private float nextFire;
    [SerializeField]
    private Camera playerCamera;

    private bool canShoot;

    public int municionArma1, municionArma2, municionArma3;
    [SerializeField]
    private List<int> municiones;

    [SerializeField]
    private Text ammoText;

    [SerializeField]
    private GameObject alertaFinal, scoreManagerObj;
    [SerializeField]
    private Text finalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        canSwitchWeapon = false;
        canShoot = true;

        municionArma1 = ajustesArma1.ammo;
        municionArma2 = ajustesArma2.ammo;
        municionArma3 = ajustesArma3.ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && canSwitchWeapon)
        {
            Debug.Log("Can change");
            if (Input.mouseScrollDelta.y > 0)
            {
                Debug.Log("Positive change");

                changeWeapon(1);
            }
            else
            {
                Debug.Log("Negative change");
                changeWeapon(-1);
            }
        }

        if (Time.time > nextFire)
        {
            canShoot = true;
        }

        if (Input.GetButton("Fire1") && armasRecogidas.Count != 0 && canShoot)
        {
            canShoot = false;
            nextFire = Time.time + ajustesArmas[armaActual].fireRate;
            shoot();
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Arma1 - Parabolica PICKUP":
                arma1.SetActive(true);
                arma2.SetActive(false);
                arma3.SetActive(false);
                armasRecogidas.Add(arma1);
                ajustesArmas.Add(ajustesArma1);
                municiones.Add(municionArma1);
                armasTotales++;
                armaActual = armasRecogidas.Count - 1;
                Destroy(other.gameObject);
                ammoText.gameObject.SetActive(true);
                canSwitchWeapon = true;
                refreshWeapon();
                break;
            case "Arma2 - Gravitacion PICKUP":
                arma1.SetActive(false);
                arma2.SetActive(true);
                arma3.SetActive(false);
                armasRecogidas.Add(arma2);
                ajustesArmas.Add(ajustesArma2);
                municiones.Add(municionArma2);
                armasTotales++;
                armaActual = armasRecogidas.Count - 1;
                Destroy(other.gameObject);
                ammoText.gameObject.SetActive(true);
                canSwitchWeapon = true;
                refreshWeapon();
                break;
            case "Arma3 - Custom PICKUP":
                arma1.SetActive(false);
                arma2.SetActive(false);
                arma3.SetActive(true);
                armasRecogidas.Add(arma3);
                ajustesArmas.Add(ajustesArma3);
                municiones.Add(municionArma3);
                armasTotales++;
                armaActual = armasRecogidas.Count - 1;
                Destroy(other.gameObject);
                ammoText.gameObject.SetActive(true);
                canSwitchWeapon = true;
                refreshWeapon();
                break;
            default:
                break;
        }


    }

    private void changeWeapon(int sentido)
    {
        Debug.Log("Ingreso a changeWeapon");
        canSwitchWeapon = false;
        if (sentido > 0)
        {
            armaActual++;
        }
        else
        {
            armaActual--;
        }

        if (armaActual> armasTotales-1)
        {
            armaActual = 0;
        }else if (armaActual < 0)
        {
            armaActual = armasTotales - 1;
        }

        refreshWeapon();
        StartCoroutine(switchCooldown());
    }

    private void refreshWeapon()
    {
        Debug.Log("Ingreso a refreshWeapon");

        for (int i = 0; i<armasRecogidas.Count; i++)
        {
            if (i!=armaActual)
            {
                armasRecogidas[i].SetActive(false);
            }
            else
            {
                armasRecogidas[i].SetActive(true);
            }
        }

        nextFire = Time.time + ajustesArmas[armaActual].fireRate;
        canShoot = true;
        refreshAmmoText();

    }

    IEnumerator switchCooldown()
    {
        Debug.Log("Ingreso a switchCooldown");

        yield return new WaitForSeconds(0.5f);
        canSwitchWeapon = true;
    }

    private void shoot()
    {
        Debug.Log("ajustesArmas[armaActual] = " + ajustesArmas[armaActual]);
        Debug.Log("ajustesArmas[armaActual].ammo = " + ajustesArmas[armaActual].ammo);

        if (municiones[armaActual] != 0)
        {
            GameObject newBullet = Instantiate(ajustesArmas[armaActual].bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().velocity = playerCamera.transform.forward * ajustesArmas[armaActual].bulletSpeed;
            newBullet.GetComponent<bulletScript>().lifesPan = ajustesArmas[armaActual].lifeSpan;

            municiones[armaActual]--;
            refreshAmmoText();
        }

        int municionTotalActual = 0;
        for (int i = 0; i<municiones.Count;i++)
        {
            municionTotalActual += municiones[i];
        }

        if (municionTotalActual == 0 && armasTotales==3)
        {
            endAction();
        }

    }

    private void refreshAmmoText()
    {
        if (municiones[armaActual] != 0)
        {

            ammoText.text = "AMMO: " + municiones[armaActual];
        }
        else
        {
            ammoText.text = "SIN MUNICIÓN";
        }
    }

    private void endAction()
    {
        alertaFinal.SetActive(true);
        gameObject.GetComponent<armasPersonaje>().enabled = false;
        gameObject.GetComponent<movimientoPersonaje>().enabled = false;
        gameObject.GetComponentInChildren<mouseLook>().enabled = false;
        finalScoreText.text = scoreManagerObj.GetComponent<scoreManager>().actualScore + "";
    }

}
