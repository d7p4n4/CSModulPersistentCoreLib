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
    public class KepernyoEntityMethods : KepernyoAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public KepernyoEntityMethods() { }

        public KepernyoEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public Kepernyo findFirstById(int id)
        {
            Kepernyo k = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Kepernyos
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Kepernyo>();

                k = query;
            }
            return k;
        }
		
		public Kepernyo LoadXmlById(int id)
        {
			Kepernyo k = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Kepernyos
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Kepernyo>();

                k = query;
            }

            string xml = k.serialization;

            Kepernyo kResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Kepernyo));

            StringReader reader = new StringReader(xml);
            kResult = (Kepernyo)serializer.Deserialize(reader);
            reader.Close();

            return kResult;
        }
		
	public void addNew(Kepernyo _Kepernyo)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Kepernyos.Add(_Kepernyo);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(Kepernyo _Kepernyo)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(Kepernyo));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _Kepernyo);

            xml = stringWriter.ToString();

            _Kepernyo.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Kepernyos.Add(_Kepernyo);

                ctx.SaveChanges();
            }
        }
    }
}
