using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed = 10f;

    private void Update()
    {
        transform.Translate(
            Vector3.left * speed * Time.deltaTime);
    }
}
