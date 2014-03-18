using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ISeeN_DB;
using ISeeN_DB.Entities;

namespace ISeenService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISeeNService" in both code and config file together.
    [ServiceContract()]
    public interface ISeeNService
    {
        #region SearchOp

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Search/{searchParam}")]
        [OperationContract]
        Report<IList<Media>> SearchMedia(string searchParam);

        #endregion

        #region AccountOp

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/New")]
        [OperationContract]
        Report<Potato> CreateAccount(Stream streamdata);

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Edit")]
        [OperationContract]
        Report<User> EditAccount(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account")]
        [OperationContract]
        Report<User> GetAccount(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Login/{email}")]
        [OperationContract]
        Report<Potato> AccountLogin(string email, Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Delete")]
        [OperationContract]
        Report<int> DeleteAccount(Stream streamdata);

        #endregion

        #region MediaOp

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media")]
        [OperationContract]
        Report<IList<Media>> GetAllMedia();

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/ById/{id}")]
        [OperationContract]
        Report<Media> GetMediaForId(string id);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Stats/{id}")]
        [OperationContract]
        Report<Statistic> GetStatsForId(string id);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Rent/{mediaId}")]
        [OperationContract]
        Report<Media> RentMediaById(string mediaId, Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/New")]
        [OperationContract]
        Report<Media> CreateNewMedia(Stream streamdata);

        #endregion

        #region ReminderOp

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Reminder/Check")]
        [OperationContract]
        Report<IList<Reminder>> CheckReminders(Stream streamdata);

        #endregion
    }
}
