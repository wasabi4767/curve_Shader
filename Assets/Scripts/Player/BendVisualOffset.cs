using UnityEngine;

public class BendVisualOffset : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bendSpace;      // Plane(地面)のTransform
    [SerializeField] private Transform physicsRoot;    // PlayerRoot（物理本体）
    [SerializeField] private Transform visual;         // PlayerVisual（見た目）

    [Header("Bend Params (must match shader)")]
    [SerializeField] private float bendStrength = 0.01f;
    [SerializeField] private float bendStartZ = 0f;

    [Header("Options")]
    [SerializeField] private bool rotateToTangent = true;

    void LateUpdate()
    {
        // 物理Rootの位置（曲げ前）から、曲げ後の見た目座標を計算
        Vector3 rootWorld = physicsRoot.position;

        // bendSpace基準のローカルにして曲げる
        Vector3 local = bendSpace.InverseTransformPoint(rootWorld);

        float z = Mathf.Max(0f, local.z - bendStartZ);
        float deltaY = z * z * bendStrength;

        // 見た目はRootに対してYだけ持ち上げる（※物理を壊さない）
        visual.position = rootWorld + bendSpace.up * deltaY;

        // 傾きも合わせたい場合（簡易：Z方向ベンドの接線）
        if (rotateToTangent)
        {
            float slope = 2f * z * bendStrength; // dy/dz
            float pitchDeg = Mathf.Rad2Deg * Mathf.Atan(slope);

            // bendSpace基準で前後に傾ける
            Quaternion tangentRot = bendSpace.rotation * Quaternion.Euler(pitchDeg, 0f, 0f);
            visual.rotation = tangentRot;
        }
    }

    // Sliderから呼べる
    public void SetBendStrength(float value) => bendStrength = value;
}