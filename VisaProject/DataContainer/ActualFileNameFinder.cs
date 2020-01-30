namespace DataContainer
{
    public class ActualFileNameFinder : IFileNameFinder
    {
        private readonly string directory;
        public ActualFileNameFinder(string directory)
        {
            this.directory = directory;
        }

        public string FindName()
        {
            return null;
        }
    }
}