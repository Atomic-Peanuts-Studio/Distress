using UnityEngine;

public class PostProcessingEffectsManager : MonoBehaviour
{
    public Blit blit;

    public void SetEffectEnabled(bool isEnabled)
    {
        blit.SetActive(isEnabled);
    }
}
