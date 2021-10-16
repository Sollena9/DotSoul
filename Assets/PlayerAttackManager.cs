using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    public GameObject attAngle;
    public GameObject weapon;
    public GameObject shield;

    private void FixedUpdate()
    {
        Vector2 len = Camera.main.ScreenToWorldPoint(Input.mousePosition) - attAngle.transform.position;

        float z = Mathf.Atan2(len.y, len.x) * Mathf.Rad2Deg;
        attAngle.transform.rotation = Quaternion.Euler(0, 0, z);

    }


    //검모드: 아래 베기 후 위 베기
    //무기별 특성 만들어야됨.
    //
    // 무기 아이템 습득 방식: 드롭 / 구매(재료 사용)
    //인벤토리 만들어야됨
    


}
