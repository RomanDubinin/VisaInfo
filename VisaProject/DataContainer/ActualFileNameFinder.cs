﻿using System.IO;
using System.Linq;

namespace DataContainer
{
    public class ActualFileNameFinder : IFileNameFinder
    {
        private readonly string directory;
        public ActualFileNameFinder(string directory)
        {
            this.directory = directory;
        }

        public string FindName(string subDirectory)
        {
            var dir = new DirectoryInfo(Path.Combine(directory, subDirectory));
            return dir.GetFiles("*.txt").OrderBy(x => x.Name).Last().FullName;
        }
    }
}