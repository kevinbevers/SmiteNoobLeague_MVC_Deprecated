using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SNL_InterfaceLayer.CustomExceptions
{
    public class ContextErrorException : Exception
    {
        public ContextErrorException()
        {

        }

        public ContextErrorException(Exception ex)
            : base(String.Format($"Something went wrong at {ex.Source}. {ex.Message}"))
        {

        }

    }
}
