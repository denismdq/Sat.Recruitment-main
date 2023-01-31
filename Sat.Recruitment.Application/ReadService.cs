using Sat.Recruitment.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Application
{
    public class ReadService : IReadService
    {
        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }

        public void WriteUserInFile(string user)
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
            StreamWriter writer = new StreamWriter(path,true);
            writer.WriteLine(user,Environment.NewLine);
            writer.Close();
        }
    }
}
