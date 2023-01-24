using System;
using System.Collections.Generic;

namespace EmcureNPD.Business.Models
{
    public class APIResponseEntity<T> where T : class
    {
        public T _object { get; set; }

        public String _Message { get; set; }

        public Boolean _Success { get; set; }

        public string _ReturnToUrl { get; set; }


        public List<string> _errorMessages = new List<string>();
    }
}