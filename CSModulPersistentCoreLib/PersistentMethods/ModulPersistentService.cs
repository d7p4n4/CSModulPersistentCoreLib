
using System;
using System.Collections.Generic;
using System.Linq;
using CSAc4yObjectService.Class;
using CSAc4yService.Class; 
using d7p4n4Namespace.EFMethods.Class;
using d7p4n4Namespace.Final.Class;

namespace d7p4n4Namespace.PersistentService.Class
{
    public class ModulPersistentService
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
		private ModulEntityMethods _ModulEntityMethods { get; set; }
		
		public ModulPersistentService() { }

        public ModulPersistentService(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;
            _ModulEntityMethods = new ModulEntityMethods(sName, newBaseName, uName, pwd);
        }

        public GetObjectResponse GetFirstById(int id)
        {
            var response = new GetObjectResponse();
            try
            {
                response.Object = (_ModulEntityMethods.findFirstById(id));
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
                response.Object = (_ModulEntityMethods.LoadXmlById(id));
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
		}
		
		public GetObjectResponse SaveWithXml(Modul _Modul)
        {
            var response = new GetObjectResponse();
            try
            {
                _ModulEntityMethods.SaveWithXml(_Modul);
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
        }
		
		public GetObjectResponse Save(Modul _Modul)
        {
            var response = new GetObjectResponse();
            try
            {
                _ModulEntityMethods.addNew(_Modul);
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
