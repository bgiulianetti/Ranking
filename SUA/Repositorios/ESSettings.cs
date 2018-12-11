using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SUA.Repositorios
{
    public class ESSettings
    {
        public const string INVALID_URI_EXCEPTION = "Invalid Uri";
        public const string INVALID_PORT_EXCEPTION = "Invalid Port";

        public ESSettings(UriBuilder node)
        {
            if (node == null)
                throw new Exception(INVALID_URI_EXCEPTION);

            Node = node;
        }

        public UriBuilder Node { get; protected set; }
    }
}