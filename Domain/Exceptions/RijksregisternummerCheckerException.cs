﻿using System;

namespace DomainLayer.Exceptions
{
    public class RijksregisternummerCheckerException : Exception
    {
        public RijksregisternummerCheckerException()
        {
            
        }

        public RijksregisternummerCheckerException(string message): base(message)
        {
            
        }
        public RijksregisternummerCheckerException(string message, Exception innerException): base(message, innerException)
        {
            
        }
    }
}