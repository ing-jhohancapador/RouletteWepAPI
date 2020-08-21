using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteWebAPI.Transversal
{
    public class ResultAction
    {

        public bool Error { get; set; }

        public string Message { get; set; }

        public ResultAction()
        {
            Error = false;
            Message = string.Empty;
        }

        public ResultAction(bool error, string message)
        {
            Error = error;
            Message = message;
        }

        public ResultAction(string message)
        {
            Error = !string.IsNullOrWhiteSpace(message);
            Message = message;
        }
    }
}
