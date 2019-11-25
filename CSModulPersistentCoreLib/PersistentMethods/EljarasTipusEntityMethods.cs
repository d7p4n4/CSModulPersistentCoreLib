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
    public class EljarasTipusEntityMethods : EljarasTipusAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public EljarasTipusEntityMethods() { }

        public EljarasTipusEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public EljarasTipus findFirstById(int id)
        {
            EljarasTipus e = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.EljarasTipuss
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<EljarasTipus>();

                e = query;
            }
            return e;
        }
		
		public EljarasTipus LoadXmlById(int id)
        {
			EljarasTipus e = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.EljarasTipuss
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<EljarasTipus>();

                e = query;
            }

            string xml = e.serialization;

            EljarasTipus eResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(EljarasTipus));

            StringReader reader = new StringReader(xml);
            eResult = (EljarasTipus)serializer.Deserialize(reader);
            reader.Close();

            return eResult;
        }
		
	public void addNew(EljarasTipus _EljarasTipus)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.EljarasTipuss.Add(_EljarasTipus);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(EljarasTipus _EljarasTipus)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(EljarasTipus));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _EljarasTipus);

            xml = stringWriter.ToString();

            _EljarasTipus.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.EljarasTipuss.Add(_EljarasTipus);

                ctx.SaveChanges();
            }
        }
    }
}
