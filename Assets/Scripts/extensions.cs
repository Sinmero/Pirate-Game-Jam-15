using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class extensions
{
    public static string GetFullKey<T>(this Dictionary<string, T> dictionary, string partialKey)
    {
        IEnumerable<string> fullMatchingKeys = dictionary.Keys.Where(currentKey => currentKey.Contains(partialKey));

        // List<T> returnedValues = new List<T>();
        string result = "|";

        foreach (string currentKey in fullMatchingKeys)
        {
            // returnedValues.Add(dictionary[currentKey]);
            result = currentKey;
        }

        // result = dictionary.FirstOrDefault(x => x.Value == returnedValues[0]).Key;

        return result;
    }
}
