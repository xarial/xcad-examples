using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.Data;
using Xarial.XCad.Examples.Sw.SqlPropertiesImporter.Properties;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Enums;
using Xarial.XCad.UI.PropertyPage.Structures;

namespace Xarial.XCad.Examples.Sw.SqlPropertiesImporter
{
    [ComVisible(true)]
    [Guid("BE31A1FD-8265-4BAE-B2F4-4B404C20690C")]
    [Title("SQL Link")]
    public class SqlPropertiesImporterSwAddIn : SwAddInEx
    {
        [Title("SQL Link")]
        [Icon(typeof(Resources), nameof(Resources.icon_database))]
        private enum Commands_e 
        {
            [Title("Import Properties")]
            [Description("Imports properties from SQL table")]
            [Icon(typeof(Resources), nameof(Resources.icon_database))]
            ImportPropertiesFromSql
        }

        private SqlImportData m_SqlImportData;
        private IXPropertyPage<SqlImportData> m_SqlImportPage;

        public override void OnConnect()
        {
            m_SqlImportData = new SqlImportData();
            m_SqlImportPage = this.CreatePage<SqlImportData>();

            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;

            m_SqlImportPage.Closing += OnSqlImportPageClosing;
            m_SqlImportPage.Closed += OnSqlImportPageClosed;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.ImportPropertiesFromSql:
                    m_SqlImportData.Input.Components = null;
                    m_SqlImportPage.Show(m_SqlImportData);
                    break;
            }
        }

        private void OnSqlImportPageClosing(PageCloseReasons_e reason, PageClosingArg arg)
        {
            if (reason == PageCloseReasons_e.Okay) 
            {
                try
                {
                    ValidateData();
                }
                catch (Exception ex)
                {
                    arg.Cancel = true;
                    arg.ErrorMessage = ex.Message;
                }
            }
        }

        private void ValidateData() 
        {
            if (string.IsNullOrEmpty(m_SqlImportData.Connection.ConnectionString)) 
            {
                throw new Exception("Connection string is not specified");
            }

            if (string.IsNullOrEmpty(m_SqlImportData.Connection.TableName))
            {
                throw new Exception("Table name is not specified");
            }

            if (string.IsNullOrEmpty(m_SqlImportData.Properties.SourceColumnName))
            {
                throw new Exception("Source column name is not specified");
            }

            if (string.IsNullOrEmpty(m_SqlImportData.Properties.SourcePropertyName))
            {
                throw new Exception("Source property name is not specified");
            }

            if (string.IsNullOrEmpty(m_SqlImportData.Properties.TargetColumnName))
            {
                throw new Exception("Target column name is not specified");
            }

            if (string.IsNullOrEmpty(m_SqlImportData.Properties.TargetPropertyName))
            {
                throw new Exception("Target property name is not specified");
            }

            if (m_SqlImportData.Input.Components?.Any() != true)
            {
                throw new Exception("Components are not selected");
            }
        }

        private void OnSqlImportPageClosed(PageCloseReasons_e reason)
        {
            if (reason == PageCloseReasons_e.Okay)
            {
                try
                {
                    LoadPropertiesFromSql();
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                    Application.ShowMessageBox(ex.Message, MessageBoxIcon_e.Error);
                }
            }
        }

        private void LoadPropertiesFromSql() 
        {
            using (var conn = new SqlConnection(m_SqlImportData.Connection.ConnectionString)) 
            {
                conn.Open();

                foreach (var comp in m_SqlImportData.Input.Components)
                {
                    if (comp.ReferencedDocument.IsCommitted)
                    {
                        if (!comp.ReferencedConfiguration.Properties.TryGet(m_SqlImportData.Properties.SourcePropertyName, out var srcPrp))
                        {
                            comp.ReferencedDocument.Properties.TryGet(m_SqlImportData.Properties.SourcePropertyName, out srcPrp);
                        }

                        var sqlCmd = conn.CreateCommand();

                        var srcPrpVal = srcPrp?.Value?.ToString();

                        if (!string.IsNullOrEmpty(srcPrpVal))
                        {
                            sqlCmd.CommandText = $"SELECT {m_SqlImportData.Properties.TargetColumnName} FROM {m_SqlImportData.Connection.TableName} WHERE CONVERT(VARCHAR, {m_SqlImportData.Properties.SourceColumnName}) = '{srcPrpVal}'";

                            var targPrpVal = "";

                            using (var reader = sqlCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    targPrpVal = reader.GetString(0);
                                }
                            }

                            if (!string.IsNullOrEmpty(targPrpVal))
                            {
                                var targPrp = comp.ReferencedConfiguration.Properties.GetOrPreCreate(m_SqlImportData.Properties.TargetPropertyName);
                                targPrp.Value = targPrpVal;

                                if (!targPrp.IsCommitted)
                                {
                                    targPrp.Commit();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
