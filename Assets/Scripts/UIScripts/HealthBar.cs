using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Health health;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        slider.gameObject.SetActive(false);
        health.ChangedHP += RenderHP;
    }
    private void FixedUpdate()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
    private void RenderHP()
    {
        slider.gameObject.SetActive(true);
        slider.value = health.CurrentHP / health.MaxHP;
    }
}
