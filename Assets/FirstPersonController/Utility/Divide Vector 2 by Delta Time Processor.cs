using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif


public class DivideVector2byDeltaTimeProcessor : InputProcessor<Vector2> {

#if UNITY_EDITOR
    static DivideVector2byDeltaTimeProcessor() {
        Initialize();
    }
#endif

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize() {
        InputSystem.RegisterProcessor<DivideVector2byDeltaTimeProcessor>();
    }

    public override Vector2 Process(Vector2 value, InputControl control)
    {
        return value / Time.deltaTime;
    }

}
