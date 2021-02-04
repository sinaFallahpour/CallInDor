using Service.Interfaces.Resource;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ResourceServices : IResourceServices
    {
        public override string SetKeyName(string keyName, object id)
        {
            var key = keyName + id;
            return key;
        }


    }
}
