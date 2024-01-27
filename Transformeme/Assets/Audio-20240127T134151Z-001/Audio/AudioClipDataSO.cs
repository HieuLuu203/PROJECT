using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.Library
{
    [CreateAssetMenu(fileName = "AudioClip", menuName = "Data/AudioClip")]
    public class MJGameAudioClipData : ScriptableObject
    {
        [SerializeField] private List<AudioClip> _audioClips;
        [SerializeField] private float _pitchBase = 1f;
        [SerializeField] private float _pitchVariation = 0f;

        public AudioClip GetAudioClip()
        {
            return _audioClips[Random.Range(0, _audioClips.Count)];
        }

        public float GetPitchOffset()
        {
            float pitchVariationHalf = _pitchVariation / 2f;
            return _pitchBase + Random.Range(-pitchVariationHalf, pitchVariationHalf);
        }
    }
}