using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Application.Interfaces
{
    public interface IReadService
    {
        public StreamReader ReadUsersFromFile();
        public void WriteUserInFile(string user);
    }
}
