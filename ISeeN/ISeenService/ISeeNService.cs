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
        Stream Test1();

        #endregion

        #region SearchOp

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Search/{searchParam}")]
        [OperationContract]
        Stream SearchMedia(string searchParam);

        #endregion

        #region AccountOp

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/New")]
        [OperationContract]
        Stream CreateAccount(Stream streamdata);

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Edit")]
        [OperationContract]
        Stream EditAccount(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account")]
        [OperationContract]
        Stream GetAccount(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Login/{email}")]
        [OperationContract]
        Stream AccountLogin(string email, Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Delete")]
        [OperationContract]
        Stream DeleteAccount(Stream streamdata);

        #endregion

        #region MediaOp

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media")]
        [OperationContract]
        Stream GetAllMedia();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/ById/{id}")]
        [OperationContract]
        Stream GetMediaForId(string id);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Stats/{id}")]
        [OperationContract]
        Stream GetStatsForId(string id);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Rent/{mediaId}")]
        [OperationContract]
        Stream RentMediaById(string mediaId, Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/New")]
        [OperationContract]
        Stream CreateNewMedia(Stream streamdata);

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Edit")]
        [OperationContract]
        Stream EditMedia(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Delete/{id}")]
        [OperationContract]
        Stream DeleteMedia(string id, Stream streamdata);

        #endregion

        #region ReminderOp

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Reminder/Check")]
        [OperationContract]
        Stream CheckReminders(Stream streamdata);

        #endregion
    }
}
