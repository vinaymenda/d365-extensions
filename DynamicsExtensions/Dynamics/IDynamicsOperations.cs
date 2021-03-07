using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicsExtensions.Dynamics
{
    public interface IDynamicsOperations
    {
        void CreatePersonalView(string name, QueryExpression qe);

        EntityMetadata[] GetEntities();

        AttributeMetadata[] GetAttributes(string entity);
    }
}