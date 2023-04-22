using System.Runtime.InteropServices;
using UnityEngine;

public struct TwoStrings
{
    public string string1;
    public string string2;
    public string concatenated;
}


public class TestoScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Concatenate(ref TwoStrings strings);

    private void Start()
    {
        TestoMethod();
    }

    private void TestoMethod()
    {
        TwoStrings twoStrings = new TwoStrings();
        twoStrings.string1 = "Testo";
        twoStrings.string2 = "Tato";
        
        Concatenate(ref twoStrings);
        
        Debug.Log($"Original values {twoStrings.string1} and {twoStrings.string2} concatenated {twoStrings.concatenated}");
    }
}
