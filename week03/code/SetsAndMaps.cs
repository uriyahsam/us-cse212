using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Linq;

public static class SetsAndMaps
{
    public static string[] FindPairs(string[] words)
    {
        List<string> result = new List<string>();
        HashSet<string> seenWords = new HashSet<string>();
        
        foreach (string word in words)
        {
            if (word[0] == word[1])
                continue;
            
            string reversedWord = new string(new char[] { word[1], word[0] });
            
            if (seenWords.Contains(reversedWord))
            {
                result.Add($"{reversedWord} & {word}");
                seenWords.Remove(reversedWord);
            }
            else
            {
                seenWords.Add(word);
            }
        }
        
        return result.ToArray();
    }

    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        Dictionary<string, int> degreeCounts = new Dictionary<string, int>();
        
        try
        {
            string[] lines = File.ReadAllLines(filename);
            
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');
                
                if (columns.Length >= 4)
                {
                    string degree = columns[3].Trim();
                    
                    if (!string.IsNullOrEmpty(degree))
                    {
                        if (degreeCounts.ContainsKey(degree))
                        {
                            degreeCounts[degree]++;
                        }
                        else
                        {
                            degreeCounts[degree] = 1;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error reading file: {ex.Message}");
        }
        
        return degreeCounts;
    }

    public static bool IsAnagram(string word1, string word2)
    {
        string cleanWord1 = word1.Replace(" ", "").ToLower();
        string cleanWord2 = word2.Replace(" ", "").ToLower();
        
        if (cleanWord1.Length != cleanWord2.Length)
            return false;
        
        Dictionary<char, int> charFrequency = new Dictionary<char, int>();
        
        foreach (char c in cleanWord1)
        {
            if (charFrequency.ContainsKey(c))
                charFrequency[c]++;
            else
                charFrequency[c] = 1;
        }
        
        foreach (char c in cleanWord2)
        {
            if (!charFrequency.ContainsKey(c))
                return false;
            
            charFrequency[c]--;
            
            if (charFrequency[c] < 0)
                return false;
        }
        
        return charFrequency.Values.All(count => count == 0);
    }

    public static string[] EarthquakeDailySummary()
    {
        const string url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string jsonData = client.GetStringAsync(url).Result;
                
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                
                EarthquakeFeatureCollection features = JsonSerializer.Deserialize<EarthquakeFeatureCollection>(jsonData, options);
                
                List<string> results = new List<string>();
                
                if (features?.Features != null)
                {
                    foreach (EarthquakeFeature feature in features.Features)
                    {
                        if (feature?.Properties != null)
                        {
                            string place = feature.Properties.Place ?? "Unknown location";
                            double magnitude = feature.Properties.Mag;
                            results.Add($"{place} - Mag {magnitude}");
                        }
                    }
                }
                
                return results.ToArray();
            }
            catch (Exception ex)
            {
                return new string[] { $"Error: {ex.Message}" };
            }
        }
    }
}

// Earthquake JSON Classes with unique names to avoid conflicts
public class EarthquakeFeatureCollection
{
    public EarthquakeFeature[] Features { get; set; }
}

public class EarthquakeFeature
{
    public EarthquakeProperties Properties { get; set; }
}

public class EarthquakeProperties
{
    public double Mag { get; set; }
    public string Place { get; set; }
}