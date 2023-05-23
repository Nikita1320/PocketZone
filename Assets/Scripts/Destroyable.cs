using System.Collections;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private float delay;

    private void Start()
    {
        StartCoroutine(DestroyWithDelay());
    }
    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
