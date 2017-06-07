using System;
using System.Collections.Generic;
using System.Text;

namespace Ersin.RestClient
{
    public static class Helper
    {

        /// <summary> 
        /// Helper method to create full rest url
        /// </summary> 
        /// <param name="baseUrl">Rest service base address</param> 
        /// <param name="path">Rest service path</param> 
        /// <returns>Rest service address</returns> 
        public static string GetAddress(string baseUrl, string path)
        {
            baseUrl = baseUrl.TrimEnd('/');
            path = path.TrimStart('/');
            return $"{baseUrl}/{path}";
        }

    }
}
