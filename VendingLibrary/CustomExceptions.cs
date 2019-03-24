using System;
using System.Collections.Generic;
using System.Text;

namespace VendingLibrary
{
    public class OutOfOrderException : Exception
    {
        public OutOfOrderException(string message) : base(message)
        {

        }
    }
}
