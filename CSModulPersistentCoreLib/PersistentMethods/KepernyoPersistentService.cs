
using System;
using System.Collections.Generic;
using System.Linq;
using CSAc4yObjectService.Class;
using CSAc4yService.Class; 
using d7p4n4Namespace.EFMethods.Class;
using d7p4n4Namespace.Final.Class;

namespace d7p4n4Namespace.PersistentService.Class
{
    public class KepernyoPersistentService
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
		private KepernyoEntityMethods _KepernyoEntityMethods { get; set; }
		
		public KepernyoPersistentService() { }

        public KepernyoPersistentService(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;
            _KepernyoEntityMethods = new KepernyoEntityMethods(sName, newBaseName, uName, pwd);
        }

        public GetObjectResponse GetFirstById(int id)
        {
            var response = new GetObjectResponse();
            try
            {
                response.Object = (_KepernyoEntityMethods.findFirstById(id));
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
                response.Object = (_KepernyoEntityMethods.LoadXmlById(id));
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
		}
		
		public GetObjectResponse SaveWithXml(Kepernyo _Kepernyo)
        {
            var response = new GetObjectResponse();
            try
            {
                _KepernyoEntityMethods.SaveWithXml(_Kepernyo);
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
        }
		
		public GetObjectResponse Save(Kepernyo _Kepernyo)
        {
            var response = new GetObjectResponse();
            try
            {
                _KepernyoEntityMethods.addNew(_Kepernyo);
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
