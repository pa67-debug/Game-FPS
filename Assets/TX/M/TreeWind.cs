using UnityEngine;

public class TreeWind : MonoBehaviour
{
    public float speed = 1.5f;     // ความเร็วลม
    public float strength = 2f;    // แรงลม (ต้นไม้ใช้เบาๆ)

    float offset;
    Quaternion startRot;

    void Start()
    {
        startRot = transform.rotation;
        offset = Random.Range(0f, 10f); // ทำให้แต่ละต้นไม่เหมือนกัน
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * speed + offset) * strength;

        // แกว่งเล็กๆ เหมือนลม
        Quaternion rot = Quaternion.Euler(0, sway, sway * 0.3f);

        transform.rotation = startRot * rot;
    }
}