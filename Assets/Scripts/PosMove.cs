using UnityEngine;
using System.Linq.Expressions;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PosMove : MonoBehaviour
{
    [SerializeField]
    public Vector3 StartPos;
    [SerializeField]
    public Vector3 EndPos;
    [HideInInspector]
    public Ease ease;
    public enum Ease
    {
        Linear,
        easeInSine,
        easeOutSine,
        easeInOutSine,
        easeInCubic,
        easeOutCubic,
        easeInOutCubic,
        easeInQuint,
        easeOutQuint,
        easeInOutQuint,
        easeInCirc,
        easeOutCirc,
        easeInOutCirc,
        easeInElastic,
        easeOutElastic,
        easeInOutElastic,
        easeInQuad,
        easeOutQuad,
        easeInOutQuad,
        easeInQuart,
        easeOutQuart,
        easeInOutQuart,
        easeInExpo,
        easeOutExpo,
        easeInOutExpo,
        easeInBack,
        easeOutBack,
        easeInOutBack,
        easeInBounce,
        easeOutBounce,
        easeInOutBounce
    }
    public float Duration;
    public float Delay;
    public bool Delete = false;
    private float ElapsedTime = 0f;
    private float DelayedTime = 0f;
    void Start()
    {
        if (StartPos == null) StartPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (DelayedTime <= Delay)
        {
            DelayedTime += Time.deltaTime;
        }
        else
        {
            if (ElapsedTime < Duration)
            {
                ElapsedTime += Time.deltaTime;
                float t = ElapsedTime / Duration;
                t = Mathf.Clamp(t, 0f, 1f);
                switch (ease)
                {
                    case Ease.Linear:
                        t = Linear(t);
                        break;
                    case Ease.easeInSine:
                        t = easeInSine(t);
                        break;
                    case Ease.easeOutSine:
                        t = easeOutSine(t);
                        break;
                    case Ease.easeInOutSine:
                        t = easeInOutSine(t);
                        break;
                    case Ease.easeInCubic:
                        t = easeInCubic(t);
                        break;
                    case Ease.easeOutCubic:
                        t = easeOutCubic(t);
                        break;
                    case Ease.easeInOutCubic:
                        t = easeInOutCubic(t);
                        break;
                    case Ease.easeInQuint:
                        t = easeInQuint(t);
                        break;
                    case Ease.easeOutQuint:
                        t = easeOutQuint(t);
                        break;
                    case Ease.easeInOutQuint:
                        t = easeInOutQuint(t);
                        break;
                    case Ease.easeInCirc:
                        t = easeInCirc(t);
                        break;
                    case Ease.easeOutCirc:
                        t = easeOutCirc(t);
                        break;
                    case Ease.easeInOutCirc:
                        t = easeInOutCirc(t);
                        break;
                    case Ease.easeInElastic:
                        t = easeInElastic(t);
                        break;
                    case Ease.easeOutElastic:
                        t = easeOutElastic(t);
                        break;
                    case Ease.easeInOutElastic:
                        t = easeInOutElastic(t);
                        break;
                    case Ease.easeInQuad:
                        t = easeInQuad(t);
                        break;
                    case Ease.easeOutQuad:
                        t = easeOutQuad(t);
                        break;
                    case Ease.easeInOutQuad:
                        t = easeInOutQuad(t);
                        break;
                    case Ease.easeInQuart:
                        t = easeInQuart(t);
                        break;
                    case Ease.easeOutQuart:
                        t = easeOutQuart(t);
                        break;
                    case Ease.easeInOutQuart:
                        t = easeInOutQuart(t);
                        break;
                    case Ease.easeInExpo:
                        t = easeInExpo(t);
                        break;
                    case Ease.easeOutExpo:
                        t = easeOutExpo(t);
                        break;
                    case Ease.easeInOutExpo:
                        t = easeInOutExpo(t);
                        break;
                    case Ease.easeInBack:
                        t = easeInBack(t);
                        break;
                    case Ease.easeOutBack:
                        t = easeOutBack(t);
                        break;
                    case Ease.easeInOutBack:
                        t = easeInOutBack(t);
                        break;
                    case Ease.easeInBounce:
                        t = easeInBounce(t);
                        break;
                    case Ease.easeOutBounce:
                        t = easeOutBounce(t);
                        break;
                    case Ease.easeInOutBounce:
                        t = easeInOutBounce(t);
                        break;
                }
                transform.position = Vector3.Lerp(StartPos, EndPos, t);
            }
            else
            {
                if (Delete) Destroy(gameObject);
                PosMove PM = GetComponent<PosMove>();
                Destroy(PM);
            }
        }
    }
    public float Linear(float x)
    {
        return x;
    }

    public float easeOutSine(float x)
    {
        return Mathf.Sin((x * Mathf.PI) / 2);
    }

    public float easeInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }

    public float easeInOutSine(float x)
    {
        return -(Mathf.Cos(Mathf.PI * x) - 1) / 2;
    }

    public float easeInCubic(float x)
    {
        return x * x * x;
    }

    public float easeOutCubic(float x)
    {
        return 1 - Mathf.Pow(1 - x, 3);
    }

    public float easeInOutCubic(float x)
    {
        return x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;
    }

    public float easeInQuint(float x)
    {
        return x * x * x * x * x;
    }

    public float easeOutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }

    public float easeInOutQuint(float x)
    {
        return x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;
    }

    public float easeInCirc(float x)
    {
        return 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
    }

    public float easeOutCirc(float x)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
    }

    public float easeInOutCirc(float x)
    {
        return x < 0.5
            ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
            : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;
    }

    public float easeInElastic(float x)
    {
        float c4 = (2 * Mathf.PI) / 3;
        return x == 0 ? 0 : x == 1 ? 1 : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * c4);
    }

    public float easeOutElastic(float x)
    {
        float c4 = (2 * Mathf.PI) / 3;
        return x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
    }

    public float easeInOutElastic(float x)
    {
        float c5 = (2 * Mathf.PI) / 4.5f;
        return x == 0 ? 0 : x == 1 ? 1 : x < 0.5
            ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2
            : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * c5)) / 2 + 1;
    }

    public float easeInQuad(float x)
    {
        return x * x;
    }

    public float easeOutQuad(float x)
    {
        return 1 - (1 - x) * (1 - x);
    }

    public float easeInOutQuad(float x)
    {
        return x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
    }

    public float easeInQuart(float x)
    {
        return x * x * x * x;
    }

    public float easeOutQuart(float x)
    {
        return 1 - Mathf.Pow(1 - x, 4);
    }

    public float easeInOutQuart(float x)
    {
        return x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
    }

    public float easeInExpo(float x)
    {
        return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
    }

    public float easeOutExpo(float x)
    {
        return x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
    }

    public float easeInOutExpo(float x)
    {
        return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
    }

    public float easeInBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
        return c3 * x * x * x - c1 * x * x;
    }

    public float easeOutBack(float x)
    {
        float c1 = 1.70158f;
        float c3 = c1 + 1;
        return 1 + c3 * Mathf.Pow(x - 1, 3) + c1 * Mathf.Pow(x - 1, 2);
    }

    public float easeInOutBack(float x)
    {
        float c1 = 1.70158f;
        float c2 = c1 * 1.525f;
        return x < 0.5
            ? (Mathf.Pow(2 * x, 2) * ((c2 + 1) * 2 * x - c2)) / 2
            : (Mathf.Pow(2 * x - 2, 2) * ((c2 + 1) * (x * 2 - 2) + c2) + 2) / 2;
    }

    public float easeInBounce(float x)
    {
        return 1 - easeOutBounce(1 - x);
    }

    public float easeOutBounce(float x)
    {
        if (x < 1 / 2.75f)
        {
            return 7.5625f * x * x;
        }
        else if (x < 2 / 2.75f)
        {
            return 7.5625f * (x -= 1.5f / 2.75f) * x + 0.75f;
        }
        else if (x < 2.5 / 2.75)
        {
            return 7.5625f * (x -= 2.25f / 2.75f) * x + 0.9375f;
        }
        else
        {
            return 7.5625f * (x -= 2.625f / 2.75f) * x + 0.984375f;
        }
    }

    public float easeInOutBounce(float x)
    {
        return x < 0.5
            ? (1 - easeOutBounce(1 - 2 * x)) / 2
            : (1 + easeOutBounce(2 * x - 1)) / 2;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(PosMove))]
public class EaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        PosMove PM = (PosMove)target;

        DrawDefaultInspector();
        PM.ease = (PosMove.Ease)EditorGUILayout.EnumPopup("Ease Type", PM.ease);
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(PM);
    }
}
#endif
