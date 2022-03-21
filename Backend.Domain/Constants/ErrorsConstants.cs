using System;
using System.Runtime.Serialization;

namespace Backend.Domain
{
    public class ErrorsConstants  
    {
        public static string S_APPLICATION_DBCONTEXT_NULL_MSG = "ApplicationDBContext is null";
        public static string S_REPO_MSG_ERROR = "Repository Error";
        public static string S_SERVICE_MSG_ERROR = "Service Error";
        public static string S_NOT_EXIST_MSG_ERROR = "Object Not exist";
        public static string S_HTTP_ERROR_5XX = "Internal Error";
        public static string S_HTTP_ERROR_4XX = "Bad request";
        public static string S_REPO_MSG_ERROR_SAVE ="Save Change in database Error";
        public static string S_REPO_MSG_ERROR_UPDATE = "Update object in database context Error";
        public static string S_REPO_MSG_ERROR_ADD = "Add object in database context Error";
        public static string S_REPO_MSG_ERROR_REMOVE = "Add object in database context Error";
    }
}
