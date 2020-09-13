using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Data.Exceptions;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;
using Xarial.XCad.Base;
using Xarial.XCad.Data;
using SqlDbEfNetCore.Properties;

namespace SqlDbEfNetCore
{
    [ComVisible(true)]
    [Guid("F08C4EF7-CEB4-4E6B-8EA7-E033FACD7D19")]
    public partial class SqlPrpsLoaderAddIn : SwAddInEx
    {
        private const string PART_NO_PRP = "PartNo";
        private const string SQL_CONNECTION_STRING = "Server=localhost;Database=data;Trusted_Connection=True;";

        [Title("SQL Properties Loader")]
        public enum AddInCommands_e
        {
            [CommandItemInfo(WorkspaceTypes_e.Part)]
            [Title("Load Properties From Db")]
            [Description("Loads description, vendor and type properties from SQL database and adds to the custom properties")]
            [Icon(typeof(Resources), nameof(Resources.load_db_icon))]
            LoadPropertiesFromDb
        }

        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            SwAddInEx.RegisterFunction(t);
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            SwAddInEx.UnregisterFunction(t);
        }

        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<AddInCommands_e>().CommandClick += OnCommandClick;
        }
        
        private void OnCommandClick(AddInCommands_e spec)
        {
            switch (spec)
            {
                case AddInCommands_e.LoadPropertiesFromDb:
                    LoadProperties();
                    break;
            }
        }

        private void LoadProperties()
        {
            try
            {
                var doc = Application.Documents.Active;

                var partNmb = doc.Properties[PART_NO_PRP].Value as string;

                using (var dbContext = new DataContext(SQL_CONNECTION_STRING))
                {
                    var part = dbContext.Parts.FirstOrDefault(p => p.PartNumber == partNmb);

                    if (part != null)
                    {
                        doc.Properties.Set("Description", part.Description);
                        doc.Properties.Set("Vendor", part.Vendor);
                        doc.Properties.Set("Type", part.Type);
                    }
                    else
                    {
                        Application.ShowMessageBox($"Part Number: {partNmb} is not present in the database");
                    }
                }
            }
            catch (CustomPropertyMissingException)
            {
                Application.ShowMessageBox($"{PART_NO_PRP} property is not found in this part", MessageBoxIcon_e.Error);
            }
        }
    }
}
