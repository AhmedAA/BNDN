using System;
using System.Collections.Generic;
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

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Account")]
        [OperationContract]
        Report<Potato> CreateAccount(User newUser);

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

        #endregion

        #region ReminderOp

        #endregion
    }
}
