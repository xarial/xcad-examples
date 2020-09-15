using System;
using System.IO;
using System.Threading.Tasks;
using Xarial.XCad.Documents.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Enums;

namespace model_generator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using(var app = await SwApplication.Start(SwVersion_e.Sw2020, "/b"))
            {
                var doc = app.Documents.Open(new DocumentOpenArgs()
                {
                    Path = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), @"template\model1.SLDPRT"),
                    ReadOnly = true
                });

                Console.WriteLine("Enter width in meters");
                var widthStr = Console.ReadLine();

                if(!string.IsNullOrEmpty(widthStr))
                {
                    doc.Dimensions["Width@Base"].SetValue(double.Parse(widthStr));
                }

                Console.WriteLine("Enter height in meters");
                var heightStr = Console.ReadLine();

                if(!string.IsNullOrEmpty(heightStr))
                {
                    doc.Dimensions["Height@Boss"].SetValue(double.Parse(heightStr));
                }

                Console.WriteLine("Enter length in meters");
                var lengthStr = Console.ReadLine();

                if(!string.IsNullOrEmpty(lengthStr))
                {
                    doc.Dimensions["Length@Base"].SetValue(double.Parse(lengthStr));
                }
                
                Console.WriteLine("Enter output file path");
                var outFilePath = Console.ReadLine();

                int errs = -1;
                int warns = -1;

                if(!doc.Model.Extension.SaveAs(outFilePath, 
                    (int)SolidWorks.Interop.swconst.swSaveAsVersion_e.swSaveAsCurrentVersion,
                    (int)SolidWorks.Interop.swconst.swSaveAsOptions_e.swSaveAsOptions_Silent, null, ref errs, ref warns))
                    {
                        throw new Exception("Failed to save document");
                    }

                app.Close();
            }
        }
    }
}
