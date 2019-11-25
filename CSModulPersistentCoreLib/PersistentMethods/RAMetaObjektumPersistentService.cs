
using System;
using System.Collections.Generic;
using System.Linq;
using CSAc4yObjectService.Class;
using CSAc4yService.Class; 
using d7p4n4Namespace.EFMethods.Class;
using d7p4n4Namespace.Final.Class;

namespace d7p4n4Namespace.PersistentService.Class
{
    public class RAMetaObjektumPersistentService
    {
		public string serverName { get; set; }
		public string baseName { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
		private RAMetaObjektumEntityMethods _RAMetaObjektumEntityMethods { get; set; }
		
		public RAMetaObjektumPersistentService() { }

        public RAMetaObjektumPersistentService(string sName, string newBaseName, string uName, string pwd)
        {
			serverName = sName;
            baseName = newBaseName;
            userName = uName;
            password = pwd;
            _RAMetaObjektumEntityMethods = new RAMetaObjektumEntityMethods(sName, newBaseName, uName, pwd);
        }

        public GetObjectResponse GetFirstById(int id)
        {
            var response = new GetObjectResponse();
            try
            {
                response.Object = (_RAMetaObjektumEntityMethods.findFirstById(id));
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
        }
        public GetObjectResponse LoadXmlByGuid(string guid)
        {
            var response = new GetObjectResponse();
            try
            {
                response.Object = (_RAMetaObjektumEntityMethods.LoadXmlByGuid(guid));
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
                response.Object = (_RAMetaObjektumEntityMethods.LoadXmlById(id));
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
		}
		
		public GetObjectResponse SaveWithXml(RAMetaObjektum _RAMetaObjektum)
        {
            var response = new GetObjectResponse();
            try
            {
                _RAMetaObjektumEntityMethods.SaveWithXml(_RAMetaObjektum);
                response.Result = new Ac4yProcessResult() { Code = "1" };
            }
            catch (Exception exception)
            {
                response.Result = (new Ac4yProcessResult() { Code = "-1", Message = exception.Message });
            }

            return response;
        }
		
		public GetObjectResponse Save(RAMetaObjektum _RAMetaObjektum)
        {
            var response = new GetObjectResponse();
            try
            {
                _RAMetaObjektumEntityMethods.addNew(_RAMetaObjektum);
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
