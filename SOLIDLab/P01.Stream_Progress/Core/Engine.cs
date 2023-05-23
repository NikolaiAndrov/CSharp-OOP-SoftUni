using P01.Stream_Progress.Core.Interfaces;
using P01.Stream_Progress.IO.Interfaces;
using P01.Stream_Progress.Models;
using P01.Stream_Progress.Models.Interfaces;
using System;
using System.Collections.Generic;
namespace P01.Stream_Progress.Core
{
    public class Engine : IEngine
    {
        private readonly IWriter writer;
        private readonly IFile file;
        StreamProgressInfo streamProgressInfo;

        public Engine(IWriter writer, IFile file)
        {
            this.writer = writer;
            this.file = file;
            streamProgressInfo = new StreamProgressInfo(this.file);
        }

        public void Run()
        {
            writer.WriteLine(streamProgressInfo.CalculateCurrentPercent());
        }
    }
}
