using System.Collections.Generic;
using System.Linq;

namespace GilGoblin.Batcher;

public class Batcher<T>(int pageSize = 100) : IBatcher<T>
{
    protected readonly int PageSize = pageSize;

    public List<List<T>> SplitIntoBatchJobs(List<T> entries)
    {
        if (!entries.Any() || PageSize < 1)
            return [];

        var cumulativeList = new List<List<T>>();
        var tempList = new List<T>();
        var queue = new Queue<T>(entries);
        while (queue.Any())
        {
            var entry = queue.Dequeue();
            tempList.Add(entry);

            if (tempList.Count != PageSize)
                continue;

            cumulativeList.Add(tempList);
            tempList = [];
        }

        if (tempList.Any())
            cumulativeList.Add(tempList);

        return cumulativeList;
    }
}