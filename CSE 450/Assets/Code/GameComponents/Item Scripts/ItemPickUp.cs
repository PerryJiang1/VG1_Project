using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickUp : MonoBehaviour
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;
    public TextMeshProUGUI itemCollectedText;
    private CircleCollider2D myCollider;
    private float displayTime = 2f;

    private void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;

        if (itemCollectedText != null)
        {
            itemCollectedText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var inventory = other.transform.GetComponent<InventoryHolder>();
        if (!inventory) return;

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            StartCoroutine(ShowItemCollectedText(myCollider.gameObject.name));
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ShowItemCollectedText(string itemName)
    {
        if (itemCollectedText != null)
        {
            // Display text
            itemCollectedText.text = itemName + " collected";
            itemCollectedText.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1f);
            itemCollectedText.gameObject.SetActive(false);
        }
    }
}
