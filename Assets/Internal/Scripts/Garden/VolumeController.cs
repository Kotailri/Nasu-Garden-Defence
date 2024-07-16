using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum VolumeType
{
    Music,
    Effect
}

public class VolumeController : MonoBehaviour
{
    public VolumeType volumeType;
    public SpriteRenderer incrementIcon;
    public SpriteRenderer decrementIcon;
    public TextMeshProUGUI volumeText;
    private float _volumeIncrement = 0.1f;

    private void Awake()
    {
        volumeText.text = "50%";
    }

    public void HoverIncrement(bool hovered)
    {
        incrementIcon.color = hovered ? Color.blue : Color.white;
    }

    public void HoverDecrement(bool hovered)
    {
        decrementIcon.color = hovered ? Color.blue : Color.white;
    }

    public void IncrementVolume()
    {
        if (volumeType == VolumeType.Music)
        {
            if (GlobalAudio.MusicVolume < 1f)
            {
                GlobalAudio.MusicVolume += _volumeIncrement;
                AudioManager.instance.AdjustMusicVolume(GlobalAudio.MusicVolume);
            }
            volumeText.text = Mathf.RoundToInt(GlobalAudio.MusicVolume * 100f).ToString() + "%";
        }

        if (volumeType == VolumeType.Effect)
        {
            if (GlobalAudio.SoundVolume < 1f)
            {
                GlobalAudio.SoundVolume += _volumeIncrement;
                AudioManager.instance.PlaySound(AudioEnum.EnemyDamaged);
            }
            volumeText.text = Mathf.RoundToInt(GlobalAudio.SoundVolume * 100f).ToString() + "%";
        }
    }

    public void DecrementVolume()
    {
        if (volumeType == VolumeType.Music)
        {
            if (GlobalAudio.MusicVolume > 0f)
            {
                GlobalAudio.MusicVolume -= _volumeIncrement;
                AudioManager.instance.AdjustMusicVolume(GlobalAudio.MusicVolume);
            }
            volumeText.text = Mathf.RoundToInt(GlobalAudio.MusicVolume * 100f).ToString() + "%";
        }

        if (volumeType == VolumeType.Effect)
        {
            if (GlobalAudio.SoundVolume > 0f)
            {
                GlobalAudio.SoundVolume -= _volumeIncrement;
                AudioManager.instance.PlaySound(AudioEnum.EnemyDamaged);
            }
            volumeText.text = Mathf.RoundToInt(GlobalAudio.SoundVolume * 100f).ToString() + "%";
        }
    }
}
