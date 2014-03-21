using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ISeeN_DB;

namespace ISeenService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISeeNService" in both code and config file together.
    [ServiceContract()]
    public interface ISeeNService
    {
        #region Tests

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Test/1")]
        [OperationContract]
        string Test1();

        #endregion

        #region SearchOp

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Search/{searchParam}")]
        [OperationContract]
        string SearchMedia(string searchParam);

        #endregion

        #region AccountOp

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/New")]
        [OperationContract]
        string CreateAccount(Stream streamdata);

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Edit")]
        [OperationContract]
        string EditAccount(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account")]
        [OperationContract]
        string GetAccount(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Login/{email}")]
        [OperationContract]
        string AccountLogin(string email, Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Delete")]
        [OperationContract]
        string DeleteAccount(Stream streamdata);

        #endregion

        #region MediaOp

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media")]
        [OperationContract]
        string GetAllMedia();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/ById/{id}")]
        [OperationContract]
        string GetMediaForId(string id);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Stats/{id}")]
        [OperationContract]
        string GetStatsForId(string id);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Rent/{mediaId}")]
        [OperationContract]
        string RentMediaById(string mediaId, Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/New")]
        [OperationContract]
        string CreateNewMedia(Stream streamdata);

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Edit")]
        [OperationContract]
        string EditMedia(Stream streamdata);

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Delete/{id}")]
        [OperationContract]
        string DeleteMedia(string id, Stream streamdata);

        #endregion

        #region ReminderOp

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Reminder/Check")]
        [OperationContract]
        string CheckReminders(Stream streamdata);

        #endregion
    }
}
