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
    public class TaroltEljarasEntityMethods : TaroltEljarasAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public TaroltEljarasEntityMethods() { }

        public TaroltEljarasEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public TaroltEljaras findFirstById(int id)
        {
            TaroltEljaras t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.TaroltEljarass
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<TaroltEljaras>();

                t = query;
            }
            return t;
        }
		
		public TaroltEljaras LoadXmlById(int id)
        {
			TaroltEljaras t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.TaroltEljarass
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<TaroltEljaras>();

                t = query;
            }

            string xml = t.serialization;

            TaroltEljaras tResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(TaroltEljaras));

            StringReader reader = new StringReader(xml);
            tResult = (TaroltEljaras)serializer.Deserialize(reader);
            reader.Close();

            return tResult;
        }
		
	public void addNew(TaroltEljaras _TaroltEljaras)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.TaroltEljarass.Add(_TaroltEljaras);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(TaroltEljaras _TaroltEljaras)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(TaroltEljaras));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _TaroltEljaras);

            xml = stringWriter.ToString();

            _TaroltEljaras.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.TaroltEljarass.Add(_TaroltEljaras);

                ctx.SaveChanges();
            }
        }
    }
}
