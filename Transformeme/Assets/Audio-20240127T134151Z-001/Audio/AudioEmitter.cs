using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MJGame.Library
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioEmitter : MonoBehaviour
    {
        private enum State
        {
            OnEnable,
            OnDisable,
            None
        }

        [SerializeField] private State startAction = State.None;
        [SerializeField] private State stopAction = State.None;

        [SerializeField] private MJGameAudioClipData audioClip;
        private AudioSource audioSource;

        private void OnEnable()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
            CheckState(State.OnEnable);
        }

        private void OnDisable()
        {
            CheckState(State.OnDisable);
        }

        private void CheckState(State currentState)
        {
            if (startAction == currentState)
            {
                PlayClip();
            }
            if (stopAction == currentState)
            {
                StopClip();
            }
        }

        public void Play()
        {
            PlayClip();
        }

        public void Stop()
        {
            StopClip();
        }

        public void PlayOneShot()
        {
            SetAudioClipAndPitch();
            audioSource.PlayOneShot(audioSource.clip);
        }

        public void PlayOneShot(MJGameAudioClipData audioClip)
        {
            this.audioClip = audioClip;
            PlayOneShot();
        }

        private void PlayClip()
        {
            SetAudioClipAndPitch();
            audioSource.Play();
        }

        private void SetAudioClipAndPitch()
        {
            audioSource.clip = audioClip.GetAudioClip();
            audioSource.pitch = audioClip.GetPitchOffset();
        }

        private void StopClip()
        {
            audioSource.Stop();
        }
    }
}