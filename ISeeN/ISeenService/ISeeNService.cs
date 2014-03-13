using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ISeeN.Entities;

namespace ISeenService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISeeNService" in both code and config file together.
    [ServiceContract()]
    public interface ISeeNService
    {
        #region SearchOp

        /// <summary>
        /// Seach for medias where the name contains the searchParam
        /// </summary>
        /// <param name="searchParam">Search param to look for in media name</param>
        /// <returns>A report containing either a list of matches based on best-hit
        /// or an error int</returns>
        [OperationContract]
        Report<IList<IMedia>> SearchMediaByName(string searchParam);

        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "test/{te}")]
        //[WebGet(UriTemplate = "Test?id={t}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string Test(string te);

        ///// <summary>
        ///// Seach for medias where the name contains the searchParam and type is equal to type
        ///// </summary>
        ///// <param name="type">type of media. TODO: create translation int to media type</param>
        ///// <returns>A report containing either a list of matches based on best-hit
        ///// or an error int</returns>
        //[OperationContract]
        //Report<IList<IMedia>> SearchMediaByType(int type);

        ///// <summary>
        ///// Seach for medias where the name contains the searchParam
        ///// </summary>
        ///// <param name="searchParam">Search param to look for in media name</param>
        ///// <param name="type">type of media</param>
        ///// <returns>A report containing either a list of matches based on best-hit
        ///// or an error int</returns>
        //[OperationContract]
        //Report<IList<IMedia>> SearchMediaByNameType(string searchParam, int type);

        #endregion

        #region AccountOp

        ///// <summary>
        ///// Creates a new account in the service
        ///// </summary>
        ///// <param name="newUser">User object which contains user information for the new account. 
        ///// NOTE:Id and IsAdmin is not used</param>
        ///// <returns>A report containing a potato valid for the created user
        ///// or an error int</returns>
        //[OperationContract]
        //Report<Potato> CreateAccount(User newUser);

        ///// <summary>
        ///// Logs in to the server using the username and password
        ///// </summary>
        ///// <param name="email">email for user login</param>
        ///// <param name="password">password for user login</param>
        ///// <returns>A report containing a potato valid for the created user
        ///// or an error int</returns>
        //[OperationContract]
        //Report<Potato> Login(string email, string password);

        ///// <summary>
        ///// Returns a user object containing account info for the user corresponding to the potato
        ///// </summary>
        ///// <param name="potato">potato to get user for</param>
        ///// <returns>A report containing either the user of the potato
        ///// or an error int</returns>
        //[OperationContract]
        //Report<User> GetAccountInfo(Potato potato);

        ///// <summary>
        ///// Sets the account information in the user object
        ///// </summary>
        ///// <param name="potato">potato of user to set information to</param>
        ///// <param name="editedUser">edited user to be saved in the service</param>
        ///// <returns>A report containing either the user saved
        ///// or an error int</returns>
        //[OperationContract]
        //Report<User> SetAccountInfo(Potato potato, User editedUser);

        #endregion

        #region MediaOp

        ///// <summary>
        ///// Rent the media corresponding to the id for the user in the potato
        ///// </summary>
        ///// <param name="id">id of media to rent</param>
        ///// <param name="potato">potato to rent the media with</param>
        ///// <returns>A report containing either a rented media
        ///// or an error int</returns>
        //[OperationContract]
        //Report<IMedia> RentMedia(int id, Potato potato);

        ///// <summary>
        ///// Creates the media with the information in the media object and file in array of bytes.
        ///// </summary>
        ///// <param name="media">Media to create.
        ///// NOTE: Id is ignored. Id is set by server</param>
        ///// <param name="file">File for the media</param>
        ///// <returns>A report containing either the new media 
        ///// or an error int</returns>
        //[OperationContract]
        //Report<IMedia> NewMedia(IMedia media, byte[] file);

        ///// <summary>
        ///// Returns the media corresponding to the id
        ///// </summary>
        ///// <param name="id">id of media</param>
        ///// <returns>A report containing either the media requested
        ///// or an error int</returns>
        //[OperationContract]
        //Report<IMedia> GetMediaById(int id);

        ///// <summary>
        ///// Returns statistics for a media corresponding to the id
        ///// </summary>
        ///// <param name="id">id of media</param>
        ///// <returns>A report containing either the statistic for the media
        ///// or an error int</returns>
        //[OperationContract]
        //Report<Statistic> GetStatisticsForMedia(int id);

        #endregion

        #region ReminderOp

        ///// <summary>
        ///// Checks whether new reminders are available for the user of the potato
        ///// </summary>
        ///// <param name="potato">potato to check with</param>
        ///// <returns>A report either containing a list of Reminders
        ///// or an error int</returns>
        //[OperationContract]
        //Report<IList<Reminder>> CheckReminders(Potato potato);

        #endregion
    }
}
