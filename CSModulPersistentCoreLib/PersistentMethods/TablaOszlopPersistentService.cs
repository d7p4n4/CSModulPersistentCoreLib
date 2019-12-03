
using System;
using System.Collections.Generic;
using System.Linq;
using CSAc4yObjectService.Class;
using CSAc4yService.Class; 
using d7p4n4Namespace.EFMethods.Class;
using d7p4n4Namespace.Final.Class;

namespace d7p4n4Namespace.PersistentService.Class
{
    public class TablaOszlopPersistentService
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
		private TablaOszlopEntityMethods _TablaOszlopEntityMethods { get; set; }
		
		public TablaOszlopPersistentService() { }

        public TablaOszlopPersistentService(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;
            _TablaOszlopEntityMethods = new TablaOszlopEntityMethods(sName, newBaseName, uName, pwd);
        }

        public GetObjectResponse GetFirstById(int id)
        {
            var response = new GetObjectResponse();
            try
            {
                response.Object = (_TablaOszlopEntityMethods.findFirstById(id));
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
        }

        public GetObjectResponse GetFirstWithXML(int id)
        {
            var response = new GetObjectResponse();
            try
            {
                response.Object = (_TablaOszlopEntityMethods.LoadXmlById(id));
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
		}
		
		public GetObjectResponse SaveWithXml(TablaOszlop _TablaOszlop)
        {
            var response = new GetObjectResponse();
            try
            {
                _TablaOszlopEntityMethods.SaveWithXml(_TablaOszlop);
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
        }
		
		public GetObjectResponse Save(TablaOszlop _TablaOszlop)
        {
            var response = new GetObjectResponse();
            try
            {
                _TablaOszlopEntityMethods.addNew(_TablaOszlop);
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
        }
    }
}
