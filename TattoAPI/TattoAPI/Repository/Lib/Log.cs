using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TattoAPI.Repository.Lib
{
    public class Log
    {

        public void logInfoMessage(string th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logDebugMessage(string th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
        public void logErrorMessage(int th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(int? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(string th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(Int64 th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(Int64? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(Boolean th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(DateTime th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(Boolean? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }
        public void logErrorMessage(DateTime? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("ErrorLog");
            logger.Info(th);
        }

        ///
        public void logDebugMessage(int th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
        public void logDebugMessage(int? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }

        public void logDebugMessage(Int64 th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
        public void logDebugMessage(Int64? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
        public void logDebugMessage(Boolean th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
        public void logDebugMessage(DateTime th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
        public void logDebugMessage(Boolean? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }
        public void logDebugMessage(DateTime? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("DebugLog");
            logger.Info(th);
        }

        //
        public void logInfoMessage(int th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logInfoMessage(int? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }

        public void logInfoMessage(Int64 th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logInfoMessage(Int64? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logInfoMessage(Boolean th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logInfoMessage(DateTime th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logInfoMessage(Boolean? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
        public void logInfoMessage(DateTime? th)
        {
            log4net.ILog logger = log4net.LogManager.GetLogger("InfoLog");
            logger.Info(th);
        }
    }
}