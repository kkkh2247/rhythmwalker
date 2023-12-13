using UnityEngine;
using System.Collections;

public class CurvedControls : MonoBehaviour
{

    public static CurvedControls inst = null;

    [HideInInspector]
    public Vector2 Offset;

    const float lerpSpeed = 0.01f;

    private Vector2 targetVec;
    //float camPos = -20;
    public Material[] Mats;
    //public Transform cam;

    private void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(this.gameObject);

        Offset = Vector2.zero;
        targetVec = Vector2.zero;
    }

    public void CurveMap(int x, int y)
    {
        targetVec = new Vector2(x, y);
    }

    private void Update()
    {
        
        Offset = Vector2.Lerp(Offset, targetVec, lerpSpeed);
        foreach (Material M in Mats)
        {
            M.SetVector("_QOffset", Offset);
        }
    }
    //void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(5, 5, Screen.width - 10, Screen.height - 10));

    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("xOffset", GUILayout.Width(100));
    //    Offset.x = GUILayout.HorizontalSlider(Offset.x, -1000, 1000);
    //    if (GUILayout.Button("0", GUILayout.Width(30)))
    //        Offset.x = 0;
    //    GUILayout.EndHorizontal();

    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("yOffset", GUILayout.Width(100));
    //    Offset.y = GUILayout.HorizontalSlider(Offset.y, -200, 200);
    //    if (GUILayout.Button("0", GUILayout.Width(30)))
    //        Offset.y = 0;

    //    GUILayout.EndHorizontal();

    //    GUILayout.EndArea();

    //    foreach (Material M in Mats)
    //    {
    //        M.SetVector("_QOffset", Offset);
    //    }
    //}
}