using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRFactory
{
    public class DRFactory
    {
        public IDataRepo.IDataRepo DRConnection()
        {
            IDataRepo.IDataRepo newDRConnection = new DataRepo.DataRepo();
            return newDRConnection;

        }
    }
}
