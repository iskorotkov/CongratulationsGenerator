using System;

namespace CongratulationsGenerator.Core
{
    public class OutputFileSavingException : Exception
    {
        public OutputFileSavingException() :
            base("Can't save output file. Delete previous output files before proceeding.")
        {
        }
    }
}
