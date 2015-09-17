using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using Alicization.Model.Information;

namespace Alicization.Model.GlobalObjects
{
    public class InformationOwnerships : IGlobalObject
    {
        private static InformationOwnerships instance = null;

        private ConcurrentDictionary<EntityInformation, double> dict { get; set; }

        private InformationOwnerships()
        {
            ; //singleton
        }

        internal static InformationOwnerships GetInstance()
        {
            return (instance == null) ? instance = new InformationOwnerships() : instance;
        }

        internal void Initialize()
        {
            dict = new ConcurrentDictionary<EntityInformation, double>();
        }

        public int Count()
        {
            return dict.Count();
        }
    }
}
