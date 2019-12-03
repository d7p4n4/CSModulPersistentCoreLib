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
    public class TablaOszlopEntityMethods : TablaOszlopAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public TablaOszlopEntityMethods() { }

        public TablaOszlopEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public TablaOszlop findFirstById(int id)
        {
            TablaOszlop t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.TablaOszlops
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<TablaOszlop>();

                t = query;
            }
            return t;
        }
		
		public TablaOszlop LoadXmlById(int id)
        {
			TablaOszlop t = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.TablaOszlops
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<TablaOszlop>();

                t = query;
            }

            string xml = t.serialization;

            TablaOszlop tResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(TablaOszlop));

            StringReader reader = new StringReader(xml);
            tResult = (TablaOszlop)serializer.Deserialize(reader);
            reader.Close();

            return tResult;
        }
		
	public void addNew(TablaOszlop _TablaOszlop)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.TablaOszlops.Add(_TablaOszlop);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(TablaOszlop _TablaOszlop)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(TablaOszlop));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _TablaOszlop);

            xml = stringWriter.ToString();

            _TablaOszlop.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.TablaOszlops.Add(_TablaOszlop);

                ctx.SaveChanges();
            }
        }
    }
}
