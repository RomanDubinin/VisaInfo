namespace VisaProject
{
    public interface IRepository
    {
        void Write(VisaInfo info);
        VisaInfo[] Read(VisaInfoFilter filter);
    }
}