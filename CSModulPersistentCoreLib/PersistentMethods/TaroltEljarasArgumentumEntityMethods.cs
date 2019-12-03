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
    public class TaroltEljarasArgumentumEntityMethods : TaroltEljarasArgumentumAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public TaroltEljarasArgumentumEntityMethods() { }

        public TaroltEljarasArgumentumEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public TaroltEljarasArgumentum findFirstById(int id)
        {
            TaroltEljarasArgumentum t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.TaroltEljarasArgumentums
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<TaroltEljarasArgumentum>();

                t = query;
            }
            return t;
        }
		
		public TaroltEljarasArgumentum LoadXmlById(int id)
        {
			TaroltEljarasArgumentum t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.TaroltEljarasArgumentums
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<TaroltEljarasArgumentum>();

                t = query;
            }

            string xml = t.serialization;

            TaroltEljarasArgumentum tResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(TaroltEljarasArgumentum));

            StringReader reader = new StringReader(xml);
            tResult = (TaroltEljarasArgumentum)serializer.Deserialize(reader);
            reader.Close();

            return tResult;
        }
		
	public void addNew(TaroltEljarasArgumentum _TaroltEljarasArgumentum)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.TaroltEljarasArgumentums.Add(_TaroltEljarasArgumentum);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(TaroltEljarasArgumentum _TaroltEljarasArgumentum)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(TaroltEljarasArgumentum));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _TaroltEljarasArgumentum);

            xml = stringWriter.ToString();

            _TaroltEljarasArgumentum.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.TaroltEljarasArgumentums.Add(_TaroltEljarasArgumentum);

                ctx.SaveChanges();
            }
        }
    }
}
