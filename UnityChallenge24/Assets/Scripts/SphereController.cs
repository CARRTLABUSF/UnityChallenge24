//License sound: https://opengameart.org/content/41-random-sound-effects
//CC BY 3.0 DEED - Attribution 3.0 Unported

//License model: https://www.turbosquid.com/3d-models/super-mario-blooper-1316383
//Editorial Uses Only

//License skybox: https://polyhaven.com/a/kloofendal_43d_clear_puresky

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [Header("Sphere hit")]
    [SerializeField] int lives;

    [Space(10)]
    [SerializeField] GameObject explosion;
    [SerializeField] Material flashHitMaterial;
    [SerializeField] AudioClip hitAudioClip;
    [SerializeField] AudioClip deathAudioClip;

    //Components
    private Renderer renderer;
    private AudioSource audioSource;

    //Materials
    private Material originalMaterial;
    
    void Start() {
        renderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();

        originalMaterial = renderer.material;
    }

    public void OnRaycastHit() {
        if (lives > 0) {
            if (lives > 1) {
                FlashWhenHit();
                audioSource.PlayOneShot(hitAudioClip);
            }
            lives--;
        }
        Debug.Log("Hit! Lives left: " + lives.ToString());
    }

    //Change material to flashing when sphere is hit
    private void FlashWhenHit() {
        renderer.material = flashHitMaterial;
        Invoke("ResetMaterialToOriginal", 0.2f);
    }

    private void ResetMaterialToOriginal() {
        renderer.material = originalMaterial;
    }

    public bool IsDead() {
        return lives <= 0;
    }

    public IEnumerator Dies() {
        Debug.Log("Sphere dead!");

        //play death audio and wait while clip is playing
        audioSource.PlayOneShot(deathAudioClip);
        yield return new WaitForSeconds(deathAudioClip.length + 0.1f);

        //play explosion FX and self-destruct
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
