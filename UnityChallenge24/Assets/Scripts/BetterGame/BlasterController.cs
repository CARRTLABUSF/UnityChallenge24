using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BlasterController : MonoBehaviour
{
    //Number of rounds in the "clip"
    public int rounds = 10;
    //Cooldown between shots
    public bool shotCoolDown = false;
    //Check if we are currently reloading
    public bool reloading = false;
    //Object hit by raycast
    private RaycastHit hitObject;
    //Raycast length
    public float rayLength = 9.0f;
    //Layer for meteors
    public LayerMask meteorLayer;
    //Forward direction of starship (blaster)
    private Vector3 blasterForward;
    //Text object to display ammo capacity
    public Text uiText;
    //Reloading progress bar
    public GameObject progressBar;
    //Image of progress bar
    private Image bar; 
    //Blast projectile prefab
    public GameObject blastPrefab;
    
    void Start()
    {
        bar = progressBar.ConvertTo<Image>();
        blasterForward = -transform.up;
    }

    void Update()
    {
        if (rounds > 0){
            uiText.text = rounds + " / 10";
            if (Input.GetMouseButtonDown(0)){
                Shoot();
            }
        }

        if (rounds == 0 && !reloading){
            StartCoroutine("FillProgressBar", 3.0f);
            StartCoroutine("Reload");
        }
    }

    void Shoot(){
        if (!shotCoolDown){
            Instantiate(blastPrefab, transform.position, Quaternion.identity);
            StartCoroutine("Blast");
            if (Physics.Raycast(transform.position, blasterForward, out hitObject, rayLength, meteorLayer)){
                IHittable hittableObject = hitObject.transform.gameObject.GetComponent<IHittable>();
                hittableObject.OnHit(); 
            }
        }
    }

    IEnumerator Blast(){
        shotCoolDown = true;
        rounds -= 1;
        yield return new WaitForSeconds(0.3f);
        shotCoolDown = false;
    }

    IEnumerator Reload(){
        shotCoolDown = false;
        progressBar.SetActive(true);
        reloading = true;
        uiText.text = "Reloading...";
        yield return new WaitForSeconds(3.0f);
        reloading = false;
        progressBar.SetActive(false);
        rounds = 10;
    }

    IEnumerator FillProgressBar(float duration)
    {
        bar.fillAmount = 0.0f;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            bar.fillAmount = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        bar.fillAmount = 1.0f;
    }
}
