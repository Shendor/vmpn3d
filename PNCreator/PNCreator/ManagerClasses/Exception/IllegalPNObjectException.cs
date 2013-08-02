using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses.Exception
{
    public class IllegalPNObjectException  : System.Exception
    {
        private string message;

        public IllegalPNObjectException()
        {
            message = base.Message;
        }

        public IllegalPNObjectException(string message)
        {
            this.message = message;
        }

        public override string Message
        {
            get
            {
                return message;
            }
        }
    }
}
