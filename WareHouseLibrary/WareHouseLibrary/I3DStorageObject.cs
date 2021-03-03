namespace WareHouseLibrary
{
    public interface I3DStorageObject
    {
        string ID { get; }

        string Description { get; }

        short Weight { get; }

        double Volume { get; }

        double Area { get; }

        bool IsFragile { get; }

        short MaxDimension { get; }

        int InsuranceValue { get; }
    }
}
