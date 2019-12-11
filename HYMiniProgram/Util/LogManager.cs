using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYMiniProgram
{
    public class LogManager
    {
        private static ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }
    }
}