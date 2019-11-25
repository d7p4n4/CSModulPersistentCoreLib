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
    public class RAMetaObjektumEntityMethods : RAMetaObjektumAlgebra
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }

        public RAMetaObjektumEntityMethods() { }

        public RAMetaObjektumEntityMethods(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;

            AllContext context = new AllContext(serverName, baseName, userName, password);
            context.Database.EnsureCreated();
        }

        public RAMetaObjektum findFirstById(int id)
        {
            RAMetaObjektum r = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.RAMetaObjektums
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<RAMetaObjektum>();

                r = query;
            }
            return r;
        }
		
		public RAMetaObjektum LoadXmlById(int id)
        {
			RAMetaObjektum r = null;

            using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                var query = ctx.RAMetaObjektums
                                .Where(ss => ss.id == id)
                                .FirstOrDefault<RAMetaObjektum>();

                r = query;
            }

            string xml = r.serialization;

            RAMetaObjektum rResult = null;

            XmlSerializer serializer = new XmlSerializer(typeof(RAMetaObjektum));

            StringReader reader = new StringReader(xml);
            rResult = (RAMetaObjektum)serializer.Deserialize(reader);
            reader.Close();

            return rResult;
        }

        
		
	public void addNew(RAMetaObjektum _RAMetaObjektum)
	{
		using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.RAMetaObjektums.Add(_RAMetaObjektum);

                ctx.SaveChanges();
            }
	}
	
	    public void SaveWithXml(RAMetaObjektum _RAMetaObjektum)
        {
            string xml = "";

            XmlSerializer serializer = new XmlSerializer(typeof(RAMetaObjektum));
            StringWriter stringWriter = new StringWriter();
            XmlWriter xmlWriter = XmlWriter.Create(stringWriter);

            serializer.Serialize(xmlWriter, _RAMetaObjektum);

            xml = stringWriter.ToString();

            _RAMetaObjektum.serialization = xml;

			using (var ctx = new AllContext(serverName, baseName, userName, password))
            {
                ctx.RAMetaObjektums.Add(_RAMetaObjektum);

                ctx.SaveChanges();
            }
        }
    }
}
