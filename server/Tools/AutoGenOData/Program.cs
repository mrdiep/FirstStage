
using RazorEngine.Templating;
using RazorEngine;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using System.Net;
using System.Collections.Generic;

namespace AutoGenOData
{
    class Program
    {
        static void Main(string[] args)
        {
            var types= GetTypesInNamespace(
                Assembly.LoadFrom(
                    @"C:\Users\diepnguyenv\Desktop\App\server\Core\DataAccessLayer\bin\Debug\netcoreapp2.2\DataAccessLayer.dll"),
                "DataAccessLayer.Models");

            foreach(var type in types)
            {
                if (type.BaseType != typeof(System.Object)) continue;
                if (!type.IsPublic) return;

                Console.WriteLine("\r\n" + type.Name);
                var props = type.GetProperties();
                var idType = props.Where(x => x.Name == "Id").Select(x => x.PropertyType.Name).First();
                var l = props.Where(x => x.Name == "AppUserRoles").FirstOrDefault();
              
                var model = new
                {
                    TableName = type.Name,
                    ControllerName = type.Name +"Controller",
                    IdType= idType,
                    PrimativeProps = props.Select(x => GenTemplate(x.Name, type.Name, idType, 
                    !PrimitiveTypes.Test(x.PropertyType),
                    x.PropertyType.IsGenericType ? x.PropertyType.GetGenericTypeDefinition().IsAssignableFrom(typeof(ICollection<>)) : false)).ToArray(),
                };
                var controllerTemplate = File.ReadAllText(@"Templates\controller.cshtml.template");
                var controllerTemplate2 = File.ReadAllText(@"Templates\controller-custom.cshtml.template");
                var code = WebUtility.HtmlDecode(Engine.Razor.RunCompile(controllerTemplate, Guid.NewGuid().ToString(), null, (object)model, null));
                var code2 = WebUtility.HtmlDecode(Engine.Razor.RunCompile(controllerTemplate2, Guid.NewGuid().ToString(), null, (object)model, null));

                if (!File.Exists(@"C:\Users\diepnguyenv\Desktop\App\server\Api\WebApi\Controllers\OData\" + type.Name + "Controller.CustomExtension.cs")) {
                    File.WriteAllText(@"C:\Users\diepnguyenv\Desktop\App\server\Api\WebApi\Controllers\OData\" + type.Name + "Controller.CustomExtension.cs", code2);
                }

                File.WriteAllText(@"C:\Users\diepnguyenv\Desktop\App\server\Api\WebApi\Controllers\OData\" + type.Name + "Controller.cs", code);
            }
        }

        private static HtmlString GenTemplate(string name, string table, string idType, bool needInclude, bool isArray)
        {
            var text = $@"
        [EnableQuery]
        public IActionResult Get{name}([FromODataUri] {idType} key)
        {{
            var result = databaseContext.{table}.Where(m => m.Id == key){(needInclude ? $".Include(x => x.{name})" : string.Empty)};
            if (result == null) return NotFound();

            return Ok(result{(isArray ? ".SelectMany" : ".Select")}(x => x.{name}){(!isArray ? ".FirstOrDefault()" : string.Empty)});
        }}
";

            return new HtmlString(text);
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        static class PrimitiveTypes
        {
            public static readonly Type[] List;

            static PrimitiveTypes()
            {
                var types = new[]
                               {
                              typeof (Enum),
                              typeof (String),
                              typeof (Char),
                              typeof (Guid),

                              typeof (Boolean),
                              typeof (Byte),
                              typeof (Int16),
                              typeof (Int32),
                              typeof (Int64),
                              typeof (Single),
                              typeof (Double),
                              typeof (Decimal),

                              typeof (SByte),
                              typeof (UInt16),
                              typeof (UInt32),
                              typeof (UInt64),

                              typeof (DateTime),
                              typeof (DateTimeOffset),
                              typeof (TimeSpan),
                          };


                var nullTypes = from t in types
                                where t.IsValueType
                                select typeof(Nullable<>).MakeGenericType(t);

                List = types.Concat(nullTypes).ToArray();
            }

            public static bool Test(Type type)
            {
                if (List.Any(x => x.IsAssignableFrom(type)))
                    return true;

                var nut = Nullable.GetUnderlyingType(type);
                return nut != null && nut.IsEnum;
            }
        }
    }
}
