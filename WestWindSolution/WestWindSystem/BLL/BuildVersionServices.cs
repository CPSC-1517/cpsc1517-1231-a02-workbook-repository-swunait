using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWindSystem.DAL;
using WestWindSystem.Entities;

namespace WestWindSystem.BLL
{
    public class BuildVersionServices
    {
        private WestWindContext _westWindContext;

        internal BuildVersionServices(WestWindContext westWindContext)
        {
            _westWindContext = westWindContext;
        }

        public BuildVersion GetBuildVersion()
        {
            return _westWindContext.BuildVersions.First();
        }
    }
}
