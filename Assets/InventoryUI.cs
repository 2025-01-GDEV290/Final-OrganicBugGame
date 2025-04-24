using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject emptyBucketImage;
    public GameObject milkBucketImage;

    public void ShowEmptyBucket()
    {
        emptyBucketImage.SetActive(true);
        milkBucketImage.SetActive(false);
    }

    public void ShowMilkBucket()
    {
        emptyBucketImage.SetActive(false);
        milkBucketImage.SetActive(true);
    }

    public void ClearInventory()
    {
        emptyBucketImage.SetActive(false);
        milkBucketImage.SetActive(false);
    }
}
