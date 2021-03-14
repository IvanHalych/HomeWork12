using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DepsWebApp.Models
{
    /// <summary>
    /// return if exception was throwed
    /// </summary>
    public class ExceptionModel
    {
#pragma warning disable 1591
        public ExceptionModel(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public ExceptionModel()
        {
        }
#pragma warning restore 1591
        /// <summary>
        /// Code of exception 
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public int Code { get;set; }
        /// <summary>
        /// Massage was in exception
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        public string Message { get; set; }
    }
}
