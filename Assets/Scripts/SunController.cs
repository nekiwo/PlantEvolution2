using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
    private void Start() {
        for (float deg = 0; deg < 360; deg += 360 / Main.RayCount) {
            float rayRange = Main.RayRange;

            Vector2 target = new Vector2(
                Mathf.Cos(deg * Mathf.Deg2Rad) * rayRange,
                Mathf.Sin(deg * Mathf.Deg2Rad) * rayRange
            );

            ShootRay(transform.position, target, rayRange, null, 0);
        }
    }

    private void ShootRay(Vector2 start, Vector2 dir, float range, GameObject ignoreWall, int bounces)
    {
        RaycastHit2D hit = Physics2D.Raycast(start, dir, range, 1 << 6);
        if (ignoreWall != null)
        {
            ignoreWall.layer = 6;
        }

        if (hit.collider != null)
        {
            Debug.DrawRay(start, hit.point - start, Color.yellow, 100);
            //Debug.DrawRay(hit.point, hit.normal, Color.blue, 100);

            if (hit.collider.tag == "plant")
            {
                range = 0;
            }
            else if (hit.collider.tag == "wall")
            {
                range = range - hit.distance;
                if (range > 0 && bounces < Main.MaxBounces)
                {
                    bounces++;

                    hit.collider.gameObject.layer = 7;

                    Vector2 reflectedRay = ReflectRay(hit.point - start, hit.normal, range);
                    ShootRay(hit.point, reflectedRay, range, hit.collider.gameObject, bounces);
                }
            }
        }
        else
        {
            Debug.DrawRay(start, dir, Color.red, 100);
        }
    }

    private Vector2 ReflectRay(Vector2 dir, Vector2 normal, float mag)
    {
        return (Vector2)Vector3.Normalize(dir - 2 * Vector2.Dot(dir, normal) * normal) * mag;
    }
}