﻿using System.Collections;
using System.Net;

namespace VacinaFacil.Utils.PatientContext
{
    /// <summary>
    /// Interface com informações da origem da requisição HTTP.
    /// </summary>
    public interface ISourceInfo
    {
        /// <summary>
        /// Contém HEADERS da requisição.
        /// </summary>
        Hashtable Data { get; set; }

        /// <summary>
        /// Origem da requisição.
        /// </summary>
        IPAddress IP { get; set; }
    }
}