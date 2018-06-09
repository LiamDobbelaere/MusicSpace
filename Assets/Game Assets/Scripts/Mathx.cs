using UnityEngine;

public static class Mathx {
    public static int Mod(float a, float b)
    {
        return (int)(a - b * Mathf.Floor(a / b));
    }
}
