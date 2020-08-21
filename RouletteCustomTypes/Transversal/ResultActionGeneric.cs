using System;
using System.Collections.Generic;
using System.Text;

namespace RouletteCustomTypes.Transversal
{
    public class ResultActionGeneric<T>:ResultAction
    {
        public T Result { get; set; }
    }
}
