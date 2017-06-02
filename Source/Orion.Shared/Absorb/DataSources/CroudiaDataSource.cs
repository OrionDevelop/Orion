﻿using System;

using Orion.Shared.Absorb.Clients;

namespace Orion.Shared.Absorb.DataSources
{
    internal class CroudiaDataSource : BaseDataSource
    {
        private readonly CroudiaClientWrapper _croudiaClient;

        public CroudiaDataSource(CroudiaClientWrapper croudiaClient)
        {
            _croudiaClient = croudiaClient;
        }

        protected override string NormalizedSource(string source)
        {
            throw new NotImplementedException();
        }

        protected override void ConnectImpl(string sourceStr)
        {
            throw new NotImplementedException();
        }
    }
}