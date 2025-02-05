using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class SphereController : MonoBehaviour
{
    private int _health = 5;
    private bool _canTakeDamage = true;

    public void TakeDamage()
    {
        if (_canTakeDamage)
        {
            _health -= 1;

            if (_health <= 0)
            {
                GameManager.Instance.Restart();
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(DamageCooldown()); 
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(1f);
        _canTakeDamage = true;
    }


}