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
    public class ModulEntityMethods : ModulAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public ModulEntityMethods() { }

        public ModulEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public Modul findFirstById(int id)
        {
            Modul m = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Moduls
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Modul>();

                m = query;
            }
            return m;
        }
		
		public Modul LoadXmlById(int id)
        {
			Modul m = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.Moduls
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<Modul>();

                m = query;
            }

            string xml = m.serialization;

            Modul mResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(Modul));

            StringReader reader = new StringReader(xml);
            mResult = (Modul)serializer.Deserialize(reader);
            reader.Close();

            return mResult;
        }
		
	public void addNew(Modul _Modul)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Moduls.Add(_Modul);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(Modul _Modul)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(Modul));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _Modul);

            xml = stringWriter.ToString();

            _Modul.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.Moduls.Add(_Modul);

                ctx.SaveChanges();
            }
        }
    }
}
