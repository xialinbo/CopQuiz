import java.util.*; 

public class CharArrayHelper
{
    public static LinkedList<Map.Entry<Character, Integer>>GetHistogram(char[] charArray)
    {
        var histogramMap = new HashMap<Character, Integer>();
        for (var c : charArray)
        {
            var count = 0;
            if (histogramMap.containsKey(c))
            {
                count = histogramMap.get(c);
            }
            
            histogramMap.put(c, count+1);
        }

        var histogramList = new LinkedList<Map.Entry<Character, Integer>>();
        histogramList.addAll(histogramMap.entrySet());

        Collections.sort(histogramList,
            new Comparator<Map.Entry<Character, Integer>>()
            {
                public int compare(Map.Entry<Character, Integer> x, Map.Entry<Character, Integer> y)
                {
                    return x.getValue().compareTo(y.getValue());
                }
            });

        return histogramList;
    }
}