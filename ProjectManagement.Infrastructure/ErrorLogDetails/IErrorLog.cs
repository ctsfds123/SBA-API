using System;

namespace ProjecManagement.ErrorHandler
{
    public interface IErrorLog
    {
        void Informations(string message);
        void InformationsFormat(string format, params object[] values);

        Guid ErrorLogstr(string message);
        Guid ErrorLogException(Exception ex);
    }
}
