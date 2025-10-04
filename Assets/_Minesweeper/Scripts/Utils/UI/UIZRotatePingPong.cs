using UnityEngine;

namespace Utils
{
    [DisallowMultipleComponent]
    public sealed class UIZRotatePingPong : MonoBehaviour
    {
        [Header("Rotation range (degrees, Z axis)")]
        [SerializeField] private float fromDegrees = -10f;
        [SerializeField] private float toDegrees = 10f;

        [Header("Timing and curve")]
        [Tooltip("Seconds it takes to go from 'from' to 'to'.")]
        [SerializeField] private float cycleDuration = 1.5f;
        [Tooltip("Interpolation curve between 'from' and 'to'. 0→start, 1→end.")]
        [SerializeField] private AnimationCurve easing = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Behavior")]
        [Tooltip("If true, adds the movement to the object's initial angle.")]
        [SerializeField] private bool relativeToInitial = true;
        [Tooltip("Use Time.unscaledDeltaTime (useful for UI when the game is paused).")]
        [SerializeField] private bool useUnscaledTime = true;
        [Tooltip("Start automatically when the object is enabled.")]
        [SerializeField] private bool playOnEnable = true;

        private bool isPlaying;
        private float time;
        private float baseZ;
        private Transform tr;

        private void Awake()
        {
            tr = transform;
            baseZ = tr.localEulerAngles.z;
        }

        private void OnEnable()
        {
            baseZ = tr.localEulerAngles.z;
            time = 0f;

            if (playOnEnable)
            {
                Play();
            }
            else
            {
                ApplyAngle(GetCurrentAngle(0f));
            }
        }

        private void OnDisable()
        {
            isPlaying = false;
        }

        private void Update()
        {
            if (!isPlaying) return;

            float dt = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            if (cycleDuration <= 0f) cycleDuration = 0.0001f;

            time += dt;

            float phase = Mathf.PingPong(time / cycleDuration, 1f);
            float eased = Mathf.Clamp01(easing.Evaluate(phase));

            float angle = Mathf.LerpAngle(fromDegrees, toDegrees, eased);
            ApplyAngle(angle);
        }

        public void Play() => isPlaying = true;

        public void Pause() => isPlaying = false;

        public void ResetPhase()
        {
            time = 0f;
            ApplyAngle(GetCurrentAngle(0f));
        }

        public void SetRange(float fromDeg, float toDeg)
        {
            fromDegrees = fromDeg;
            toDegrees = toDeg;
            ResetPhase();
        }

    #if UNITY_EDITOR
        private void OnValidate()
        {
            if (cycleDuration < 0f) cycleDuration = 0f;
            if (easing == null || easing.length == 0)
                easing = AnimationCurve.EaseInOut(0, 0, 1, 1);
        }
    #endif

        private float GetCurrentAngle(float t01)
        {
            float eased = Mathf.Clamp01(easing.Evaluate(Mathf.Clamp01(t01)));
            return Mathf.LerpAngle(fromDegrees, toDegrees, eased);
        }

        private void ApplyAngle(float targetZ)
        {
            var e = tr.localEulerAngles;
            float finalZ = relativeToInitial ? baseZ + targetZ : targetZ;
            tr.localEulerAngles = new Vector3(e.x, e.y, finalZ);
        }
    }
}
