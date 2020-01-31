namespace VisaProject
{
    public struct VisaInfoFilter
    {
        public VisaInfoFilter(string city)
        {
            City = city;
        }

        public string City { get; }
    }
}