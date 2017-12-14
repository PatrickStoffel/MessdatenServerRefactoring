using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessdatenServer.services
{
    public class ReadWriteException : Exception
    {
        public ReadWriteException(string message)
            : base(message)
        {
        }

        public ReadWriteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}