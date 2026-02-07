using UnityEngine;
using UnityEngine.UI;

public class GroundBendController : MonoBehaviour
{
    [SerializeField] private Material groundMaterial;
    [SerializeField] private Slider bendSlider;

    private static readonly int BendStrengthID =
        Shader.PropertyToID("_BendStrength");

    void Start()
    {
        bendSlider.onValueChanged.AddListener(OnSliderChanged);
        OnSliderChanged(bendSlider.value);
    }

    void OnSliderChanged(float value)
    {
        groundMaterial.SetFloat(BendStrengthID, value);
    }
}

