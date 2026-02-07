using UnityEngine;

public class BendFollower : MonoBehaviour
{
    [SerializeField] private Transform bendSpace;     // 地面(Plane)のTransformなど
    [SerializeField] private float bendStrength = 0.01f;
    [SerializeField] private float bendStartZ = 0f;

    [SerializeField] private bool affectRotation = true;

    void LateUpdate()
    {
        // 位置を曲げた結果に置き換え
        Vector3 bent = BendMath.BendPos(transform.position, bendSpace, bendStrength, bendStartZ);
        transform.position = bent;

        // 回転も“接線方向”へ寄せたい場合（簡易版）
        if (affectRotation)
        {
            // Z方向ベンドなら、Zに沿った傾き = dY/dZ = 2*z*bendStrength
            var local = bendSpace.InverseTransformPoint(transform.position);
            float z = Mathf.Max(0f, local.z - bendStartZ);
            float slope = 2f * z * bendStrength; // dy/dz

            // X軸回転で前後に傾ける（bendSpace基準）
            Quaternion targetLocalRot = Quaternion.Euler(Mathf.Rad2Deg * Mathf.Atan(slope), 0f, 0f);
            transform.rotation = bendSpace.rotation * targetLocalRot;
        }
    }

    // UIから更新できるように
    public void SetBend(float strength) => bendStrength = strength;
}