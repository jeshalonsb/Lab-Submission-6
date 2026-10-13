using UnityEngine;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ManagedSortPlugin;

public class SortingComparison : MonoBehaviour
{
    [DllImport("NativeSortPlugin")]
    private static extern void SortArray(int[] arr, int length);

    void Start()
    {
        int[] numbers = new int[10000];
        System.Random random = new System.Random();

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i + 1;
        }
        for (int i = 0; i < numbers.Length; i++)
        {
            int j = random.Next(i, numbers.Length);

            int temp = numbers[i];
            numbers[i] = numbers[j];
            numbers[j] = temp;
        }

        Stopwatch stopwatch = new Stopwatch();

        int[] nativeArray = (int[])numbers.Clone();

        stopwatch.Start();
        SortArray(nativeArray, nativeArray.Length);
        stopwatch.Stop();

        UnityEngine.Debug.Log("C++ Native: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

        int[] managedArray = (int[])numbers.Clone();

        stopwatch.Restart();
        Sorter.SortArray(managedArray);
        stopwatch.Stop();

        UnityEngine.Debug.Log("C# Managed: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

        int[] unityArray = (int[])numbers.Clone();

        stopwatch.Restart();
        Array.Sort(unityArray);
        stopwatch.Stop();

        UnityEngine.Debug.Log("Unity C#: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
    }
}