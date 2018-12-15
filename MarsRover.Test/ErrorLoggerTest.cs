using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MarsRover.Service.Error;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MarsRover.Test
{
    [TestClass]
    public class ErrorLoggerTest
    {
        [TestMethod]
        public void ErrorLog_Directory_Was_Created_When_Logging_For_The_FirstTime()
        {
            //Arrange
            var logger = new ErrorLogger();
            var errorMessage = "Something, went terrible wrong";

            var logDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}logs";

            //Act
            logger.WriteErrorLog(errorMessage);

            //Assert
            Assert.IsTrue(Directory.Exists(logDirectory));

            // Cleanup
            Directory.Delete(logDirectory, true);
        }

        [TestMethod]
        public void ErrorLog_File_Was_Created_When_Logging_For_The_FirstTime()
        {
            var logger = new ErrorLogger();
            var errorMessage = "Something, went terrible wrong";

            var logDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
            var logFile = $"{logDirectory}\\logs.json";

            //Act
            logger.WriteErrorLog(errorMessage);

            //Assert
            Assert.IsTrue(File.Exists(logFile));

            //Cleanup
            Directory.Delete(logDirectory, true);
        }

        [TestMethod]
        public void Can_Log_ExceptionMessage_To_File()
        {
            //Arrange
            var logger = new ErrorLogger();
            var errorMessage = "Something, went terrible wrong";

            var logDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
            var logFile = $"{logDirectory}\\logs.json";

            //Act
            logger.WriteErrorLog(errorMessage);

            var file = File.ReadAllText(logFile);
            var errors = JsonConvert.DeserializeObject<List<ErrorMessage>>(file);

            //Assert
            Assert.AreEqual(errorMessage, errors.FirstOrDefault().Message);

            //Cleanup
            Directory.Delete(logDirectory, true);
        }

        [TestMethod]
        public void Can_Add_Multiple_ExceptionMessage_To_File()
        {
            //Arrange
            var logger = new ErrorLogger();
            var errorMessage = "Something, went terrible wrong";
            var secondErrorMessage = "This is a new exception Log";
            var finalErrorMessage = "This is a new exception Log";

            var logDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
            var logFile = $"{logDirectory}\\logs.json";

            //Act
            logger.WriteErrorLog(errorMessage);
            logger.WriteErrorLog(secondErrorMessage);
            logger.WriteErrorLog(finalErrorMessage);

            var file = File.ReadAllText(logFile);
            var errors = JsonConvert.DeserializeObject<List<ErrorMessage>>(file);

            //Assert
            Assert.IsTrue(errors.Count() == 3);

            //Cleanup
            Directory.Delete(logDirectory, true);
        }

        [TestMethod]
        public void Can_Add_First_Error_Even_If_The_File_Is_Empty()
        {
            //Arrange
            var logger = new ErrorLogger();
            var errorMessage = "Something, went terrible wrong";


            var logDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
            var logFile = $"{logDirectory}\\logs.json";

            Directory.CreateDirectory(logDirectory);
            File.Create(logFile).Close();

            //Act
            logger.WriteErrorLog(errorMessage);

            var file = File.ReadAllText(logFile);
            var errors = JsonConvert.DeserializeObject<List<ErrorMessage>>(file);

            //Assert
            Assert.IsTrue(errors.First().Id == 1);

            //Cleanup
            Directory.Delete(logDirectory, true);
        }
    }
}
