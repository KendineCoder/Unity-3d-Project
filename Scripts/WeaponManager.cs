using System.Collections;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public Transform swordSocket;
    public GameObject swordPrefab;

    public Vector3 swordPositionOffset = Vector3.zero;
    public Vector3 swordRotationOffset = new Vector3(140f, 30f, 0f);
    public Vector3 swordScaleOffset = Vector3.one;

    private GameObject currentSword;

    void Start()
    {
        StartCoroutine(EquipSwordAfterAnimation());
    }

    IEnumerator EquipSwordAfterAnimation()
    {
        yield return new WaitForEndOfFrame();
        EquipSword();
    }

    void EquipSword()
    {
        if (swordSocket == null || swordPrefab == null)
        {
            Debug.LogError("Sword socket or prefab is not assigned!");
            return;
        }

        currentSword = Instantiate(swordPrefab);
        currentSword.transform.SetParent(swordSocket);

        currentSword.transform.localPosition = swordPositionOffset;
        currentSword.transform.localRotation = Quaternion.Euler(swordRotationOffset);
        currentSword.transform.localScale = swordScaleOffset;
    }

    public void UnequipSword()
    {
        if (currentSword != null)
        {
            Destroy(currentSword);
            currentSword = null;
        }
    }

    public void EquipNewSword(GameObject newSwordPrefab)
    {
        UnequipSword();
        swordPrefab = newSwordPrefab;
        EquipSword();
    }
}