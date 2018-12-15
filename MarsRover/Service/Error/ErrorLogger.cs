using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MarsRover.Service.Error
{
    public class ErrorLogger : ILogger
    {
        public void WriteErrorLog(string message)
        {
            var logDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
            var logFile = $"{logDirectory}\\logs.json";

            //If we have a file than we can go ahead and log
            if (File.Exists(logFile))
            {
                var file = File.ReadAllText(logFile);
                var errors = JsonConvert.DeserializeObject<List<ErrorMessage>>(file);

                //Lets double check that we have logs, before we attempt to grab the last logId
                if (errors != null && errors.Any())
                {
                    errors.Add(new ErrorMessage()
                    {
                        Id = errors.LastOrDefault().Id + 1,
                        CreatedDate = DateTime.Now,
                        Message = message
                    });

                    string output = JsonConvert.SerializeObject(errors, Formatting.Indented);
                    File.WriteAllText(logFile, output);
                }
                else
                {
                    //since we have a file but no logs lets delete the file and recreate
                    //it with this log as the first entry.
                    File.Delete(logFile);
                    WriteErrorLog(message);
                }
            }
            else
            {
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                var errorList = new List<ErrorMessage>
                {
                    new ErrorMessage()
                    {
                        Id = 1,
                        CreatedDate = DateTime.Now,
                        Message = message,
                    }
                };

                string output = JsonConvert.SerializeObject(errorList, Formatting.Indented);
                File.WriteAllText(logFile, output);
            }
        }
    }
}
