using UnityEngine;

public static class BendMath
{
    public static Vector3 BendPos(Vector3 worldPos, Transform bendSpace, float bendStrength, float bendStartZ)
    {
        // bendSpace基準のローカルにしてから曲げる（地面のTransformに追従させたい場合に重要）
        var local = bendSpace.InverseTransformPoint(worldPos);

        float z = Mathf.Max(0f, local.z - bendStartZ);
        local.y += z * z * bendStrength;

        return bendSpace.TransformPoint(local);
    }

    public static float BendDeltaY(Vector3 worldPos, Transform bendSpace, float bendStrength, float bendStartZ)
    {
        var local = bendSpace.InverseTransformPoint(worldPos);
        float z = Mathf.Max(0f, local.z - bendStartZ);
        return z * z * bendStrength;
    }
}