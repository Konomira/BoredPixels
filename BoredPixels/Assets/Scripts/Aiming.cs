using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private GameObject arm;
    float rotateSpeed = 10.0f;
    private void Start()
    {
        arm = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        Vector2 target;

        if (transform.localScale.x > 0)
            target = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        else
            target = new Vector2(transform.position.x - mousePos.x, transform.position.y - mousePos.y);

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        var q = Quaternion.AngleAxis(angle, Vector3.forward);

        arm.transform.rotation = Quaternion.Slerp(arm.transform.rotation, q, Time.deltaTime * rotateSpeed);
    }
}
