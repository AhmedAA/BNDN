using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ISeeNIIS
{
    [ServiceContract]
    public interface ISeenservice
    {
        [WebInvoke(Method = "OPTIONS", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{*end}")]
        [OperationContract]
        Stream CORSOptions(string end);

        #region SearchOp

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Search/{searchParam}")]
        [OperationContract]
        Stream SearchMedia(string searchParam);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Recent")]
        [OperationContract]
        Stream RecentSearchParams();
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

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Account/Login")]
        [OperationContract]
        Stream AccountLogin(Stream streamdata);

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

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Rent")]
        [OperationContract]
        Stream RentMediaById(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/New")]
        [OperationContract]
        Stream CreateNewMedia(Stream streamdata);

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Edit")]
        [OperationContract]
        Stream EditMedia(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Media/Delete")]
        [OperationContract]
        Stream DeleteMedia(Stream streamdata);

        #endregion

        #region RentalOp

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Rent/File/Get")]
        [OperationContract]
        Stream GetFileForRental(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Rent/User/All")]
        [OperationContract]
        Stream GetRentalsForUser(Stream streamdata);

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Rent/User/HasRented")]
        [OperationContract]
        Stream CheckUserRented(Stream streamdata);

        #endregion
    }
}