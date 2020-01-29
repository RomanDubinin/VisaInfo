namespace VisaProject
{
    public interface IRepository
    {
        void RewriteAll(VisaInfo[] infos);
        void Write(VisaInfo info);
        VisaInfo[] ReadAll();
    }
}