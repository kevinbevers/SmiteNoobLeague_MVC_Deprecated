using System;
using System.Collections.Generic;
using System.Text;

namespace SNL_InterfaceLayer.CustomExceptions
{
    public class ContextErrorException : Exception
    {
        public ContextErrorException()
        {

        }

        public ContextErrorException(string contextName, Exception ex)
            : base(String.Format($"Something went wrong in the {contextName}. \n{ex.Message} \n{ex.StackTrace}"))
        {

        }
    }
}
