using Microsoft.AspNetCore.SignalR;
using SolidWorks.Interop.swconst;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteModuleBuilder
{
    public class ModelBuilderHub : Hub<IModelBuilderClient>
    {   
        private int m_TotalModelsBuilt;

        public ModelBuilderHub()
        {
        }

        public void Build(double width, double length, double height)
        {
            double mass;
            
            using (var builder = new SwModelBuilder()) 
            {
                mass = builder.Build(width, height, length);
            }

            m_TotalModelsBuilt++;
            Clients.All.UpdateStatus(m_TotalModelsBuilt);
            Clients.Caller.SendResult(mass);
        }
    }
}
