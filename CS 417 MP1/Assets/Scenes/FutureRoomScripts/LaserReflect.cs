using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserReflect : MonoBehaviour
{
    public int maxReflections = 10;
    public float maxDistance = 100f;

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        Vector3 position = transform.position;
        Vector3 direction = transform.forward;

        line.positionCount = 1;
        line.SetPosition(0, position);

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(position, direction, out hit, maxDistance))
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, hit.point);

                // If hit mirror → reflect
                if (hit.collider.CompareTag("Mirror"))
                {
                    direction = Vector3.Reflect(direction, hit.normal);
                    position = hit.point + direction * 0.01f; // avoid self-hit
                }
                else
                {
                    break; // stop at non-mirror
                }
            }
            else
            {
                // no hit → draw straight line
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, position + direction * maxDistance);
                break;
            }
        }
    }
}
