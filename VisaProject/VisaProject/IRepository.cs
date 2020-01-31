namespace VisaProject
{
    public interface IRepository
    {
        VisaInfo[] Read(VisaInfoFilter filter);
    }
}