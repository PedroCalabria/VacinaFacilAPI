﻿using System.Collections;
using System.Net;

namespace VacinaFacil.Utils.PatientContext
{
    /// <summary>
    /// Informações da origem da requisição HTTP.
    /// </summary>
    public class SourceInfo : ISourceInfo
    {
        /// <summary>
        /// Contém HEADERS da requisição.
        /// </summary>
        public Hashtable Data { get; set; }

        /// <summary>
        /// Origem da requisição.
        /// </summary>
        public IPAddress IP { get; set; }
    }
}