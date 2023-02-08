using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Documents;
using Xarial.XCad.Examples.Sw.SqlPropertiesImporter.Properties;
using Xarial.XCad.SolidWorks.UI.PropertyPage;

namespace Xarial.XCad.Examples.Sw.SqlPropertiesImporter
{
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.icon_database))]
    [Title("Import Properties From Database")]
    public class SqlImportData : SwPropertyManagerPageHandler
    {
        public class ConnectionGroup
        {
            [Description("Connection String")]
            [Icon(typeof(Resources), nameof(Resources.connection_string_icon))]
            public string ConnectionString { get; set; }

            [Description("Table Name")]
            [Icon(typeof(Resources), nameof(Resources.icon_table))]
            public string TableName { get; set; }
        }

        public class PropertiesGroup 
        {
            [Description("Source Property Name")]
            [Icon(typeof(Resources), nameof(Resources.src_prp_name))]
            public string SourcePropertyName { get; set; }

            [Description("Source Column Name")]
            [Icon(typeof(Resources), nameof(Resources.src_column_name))]
            public string SourceColumnName { get; set; }

            [Description("Target Property Name")]
            [Icon(typeof(Resources), nameof(Resources.targ_prp_name))]
            public string TargetPropertyName { get; set; }
            
            [Description("Target Column Name")]
            [Icon(typeof(Resources), nameof(Resources.targ_column_name))]
            public string TargetColumnName { get; set; }
        }

        public class InputGroup
        {
            [Description("Components Scope")]
            public List<IXComponent> Components { get; set; }
        }

        public ConnectionGroup Connection { get; }
        public PropertiesGroup Properties { get; }
        public InputGroup Input { get; }

        public SqlImportData() 
        {
            Connection = new ConnectionGroup();
            Properties = new PropertiesGroup();
            Input = new InputGroup();
        }
    }
}
