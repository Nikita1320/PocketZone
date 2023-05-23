using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private TMP_Text ammountText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button cellButton;
    [SerializeField] private Button removedButton;
    [SerializeField] private float lifeTimeRemoveButton;
    private Coroutine delayCoroutine;
    private Item item;

    public Item ContainedItem { get => item; }
    public Button RemovedButton { get => removedButton; }
    private void Start()
    {
        cellButton.onClick.AddListener(RenderRemovedButton);
    }
    public void Init(Item item)
    {
        this.item = item;
        RenderItemInfo();
    }
    private void RenderItemInfo()
    {
        itemImage.sprite = item.Data.ItemIcon;
        RenderAmmount();
    }
    private void RenderAmmount()
    {
        if (item != null)
        {
            if (item.Ammount > 1)
            {
                ammountText.text = item.Ammount.ToString();
            }
            else
            {
                ammountText.text = "";
            }
        }
    }
    private void Subcribe()
    {
        if (item != null)
        {
            item.ChangedAmmount += RenderAmmount;
        }
    }
    private void UnSubcribe()
    {
        if (item != null)
        {
            item.ChangedAmmount -= RenderAmmount;
        }
    }
    private void RenderRemovedButton()
    {
        if (delayCoroutine == null && item != null)
        {
            removedButton.gameObject.SetActive(true);
            delayCoroutine = StartCoroutine(DelayDisactiveButton());
        }
    }
    private IEnumerator DelayDisactiveButton()
    {
        var time = lifeTimeRemoveButton;
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
        }
        removedButton.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Subcribe();
        RenderAmmount();
    }
    private void OnDisable()
    {
        UnSubcribe();
    }
}
