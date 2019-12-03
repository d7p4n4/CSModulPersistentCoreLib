using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using d7p4n4Namespace.Algebra.Class;
using d7p4n4Namespace.Final.Class;
using d7p4n4Namespace.Context.Class;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace d7p4n4Namespace.EFMethods.Class
{
    public class TablaEntityMethods : TablaAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public TablaEntityMethods() { }

        public TablaEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public Tabla findFirstById(int id)
        {
            Tabla t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Tablas
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Tabla>();

                t = query;
            }
            return t;
        }
		
		public Tabla LoadXmlById(int id)
        {
			Tabla t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Tablas
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Tabla>();

                t = query;
            }

            string xml = t.serialization;

            Tabla tResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Tabla));

            StringReader reader = new StringReader(xml);
            tResult = (Tabla)serializer.Deserialize(reader);
            reader.Close();

            return tResult;
        }
		
	public void addNew(Tabla _Tabla)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Tablas.Add(_Tabla);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(Tabla _Tabla)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(Tabla));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _Tabla);

            xml = stringWriter.ToString();

            _Tabla.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Tablas.Add(_Tabla);

                ctx.SaveChanges();
            }
        }
    }
}
