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
    public class MuveletEntityMethods : MuveletAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public MuveletEntityMethods() { }

        public MuveletEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public Muvelet findFirstById(int id)
        {
            Muvelet m = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Muvelets
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Muvelet>();

                m = query;
            }
            return m;
        }
		
		public Muvelet LoadXmlById(int id)
        {
			Muvelet m = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Muvelets
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Muvelet>();

                m = query;
            }

            string xml = m.serialization;

            Muvelet mResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Muvelet));

            StringReader reader = new StringReader(xml);
            mResult = (Muvelet)serializer.Deserialize(reader);
            reader.Close();

            return mResult;
        }
		
	public void addNew(Muvelet _Muvelet)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Muvelets.Add(_Muvelet);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(Muvelet _Muvelet)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(Muvelet));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _Muvelet);

            xml = stringWriter.ToString();

            _Muvelet.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Muvelets.Add(_Muvelet);

                ctx.SaveChanges();
            }
        }
    }
}
