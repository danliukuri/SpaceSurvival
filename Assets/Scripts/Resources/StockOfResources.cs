using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class StockOfResources
{
    #region Properties
    public ReadOnlyCollection<Resource> Resources => new ReadOnlyCollection<Resource>(resources);
    public int MaxWeight { get; }
    public int CurrentWeight => resources.Select(r => r.Weight).Sum();
    public bool HasFreeSpace => CurrentWeight < MaxWeight;
    #endregion

    #region Fields
    readonly List<Resource> resources = new List<Resource>();
    #endregion

    #region Methods
    public StockOfResources(int maxWeight)
    {
        MaxWeight = maxWeight;
    }
    public void Add(Resource resource)
    {
        if(HasFreeSpace)
            resources.Add(resource);
    }
    public void Remove(Resource resource) => resources.Remove(resource);
    public void Clear() => resources.Clear();
    #endregion
}