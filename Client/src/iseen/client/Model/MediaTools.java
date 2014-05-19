package iseen.client.Model;

import com.google.gson.*;
import com.google.gson.internal.LinkedTreeMap;
import com.google.gson.reflect.TypeToken;
import iseen.client.Entities.MediaFormats.*;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;
import iseen.client.Exceptions.MediaTypeNotMatchedException;
import iseen.client.Storage.Memory;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

import java.io.IOException;
import java.lang.reflect.Type;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by ahmed on 20/03/14.
 */
public class MediaTools {

    private static String PATH_TEST1 = "test/1";
    private static String PATH_ALL_MEDIA = "media";
    private static String PATH_MEDIA_BY_ID = "media/byid/";
    private static String PATH_MEDIA_STATS = "media/stats/";
    private static String PATH_MEDIA_RENT = "media/rent";
    private static String PATH_NEW_MEDIA = "media/new";
    private static String PATH_EDIT_MEDIA = "media/edit";
    private static String PATH_DELETE_MEDIA = "media/delete";
    private static String PATH_CHECK_RENT = "rent/user/hasRented";
    private static String PATH_GET_FILE = "Rent/File/Get";

    private static Gson gson = new Gson();

    public static List<Media> GetAllMedia() throws Exception {
        return JsonReportOfListOfMedia_To_ListOfMedia(HttpCommunication.sendGet(PATH_ALL_MEDIA));
    }

    public static Media GetMediaById(int id) throws MediaTypeNotMatchedException, GeneralError, IOException {
        return JsonReportOfMedia_To_Media(HttpCommunication.sendGet(PATH_MEDIA_BY_ID + id));
    }

    public static Media RentMedia(int id) throws Exception {

        String response = HttpCommunication.sendPostPut(PATH_MEDIA_RENT,"[\"" + id + "\"," + PotatoTools.Potato_To_Json()+ "]",true);

        return JsonReportOfMedia_To_Media(response);
    }

    public static Media CreateNewMedia(Media media) throws Exception {
        //build the json string
        StringBuilder sb = new StringBuilder();
        sb.append('[');
        sb.append(PotatoTools.Potato_To_Json());
        sb.append(',');
        sb.append(media.ToJson(gson));
        sb.append(',');
        sb.append(GeneralTools.File_To_Base64());
        sb.append(']');

        String response = HttpCommunication.sendPostPut(PATH_NEW_MEDIA, sb.toString(), true);

        return JsonReportOfMedia_To_Media(response);
    }

    //TODO: Not sure this method will work.
    public static Media DeleteMedia(int id) throws Exception {
        String response = HttpCommunication.sendPostPut(PATH_DELETE_MEDIA,"[\"" + id + "\",\"" + PotatoTools.Potato_To_Json()+ "\"]",true);

        return JsonReportOfMedia_To_Media(response);
    }

    private static Media EditMedia(Media media, boolean FileWasEdited) throws Exception {
        //build the json string
        StringBuilder sb = new StringBuilder();
        sb.append('[');
        sb.append(PotatoTools.Potato_To_Json());
        sb.append(',');
        sb.append(media.ToJson(gson));

        if (FileWasEdited == true) {
            sb.append(',');
            sb.append(GeneralTools.File_To_Base64());
        }

        sb.append(']');

        String response = HttpCommunication.sendPostPut(PATH_EDIT_MEDIA,sb.toString(),false);

        return JsonReportOfMedia_To_Media(response);
    }

    public static boolean CheckRented() throws Exception {
        //build the json string
        StringBuilder sb = new StringBuilder();
        sb.append('[');
        sb.append(Memory.CurrentMedia.Id);
        sb.append(',');
        sb.append(PotatoTools.Potato_To_Json());
        sb.append(']');

        String response = HttpCommunication.sendPostPut(PATH_CHECK_RENT, sb.toString(), true);

        //Unpack report into data and error JSON strings
        JsonObject JsonReport = GeneralTools.JsonReport_To_JsonObject(response);

        //Separated into Data and Error
        JsonPrimitive data = JsonReport.getAsJsonPrimitive("Data");

        return data.getAsBoolean();
    }

    public static void UseRent() throws Exception {
        //build the json string
        StringBuilder sb = new StringBuilder();
        sb.append('[');
        sb.append(Memory.CurrentMedia.Id);
        sb.append(',');
        sb.append(PotatoTools.Potato_To_Json());
        sb.append(']');

        String response = HttpCommunication.sendPostPut(PATH_GET_FILE, sb.toString(), true);

        //Unpack report into data and error JSON strings
        JsonObject JsonReport = GeneralTools.JsonReport_To_JsonObject(response);

        //Separated into Data and Error
        JsonPrimitive data = JsonReport.getAsJsonPrimitive("Data");

        //Construct filename
        sb = new StringBuilder();
        sb.append(Memory.CurrentMedia.Id);

        int type = Memory.CurrentMedia.Type;

        if (type == MediaTypes.Types.MapEnum(MediaTypes.Types.MOVIE))
            sb.append(".avi");
        else if (type == MediaTypes.Types.MapEnum(MediaTypes.Types.MUSIC))
            sb.append(".mp3");
        else if (type == MediaTypes.Types.MapEnum(MediaTypes.Types.PICTURE))
            sb.append(".jpg");

        //File handling
        GeneralTools.SaveFile(data.getAsString(),sb.toString());
    }

    private static Media JsonReportOfMedia_To_Media(String Json) throws GeneralError, MediaTypeNotMatchedException {
        System.out.println(Json);
        //Get data from json report
        JsonObject data = GeneralTools.JsonReport_To_DataJsonObject(Json);

        Media media = Media.FromJson(data, gson);

        System.out.println(media.Title);

        return Media.FromJson(data,gson);
    }

    private static List<Media> JsonReportOfListOfMedia_To_ListOfMedia(String Json) throws GeneralError {
        System.out.println(Json);
        //Get data from json report
        JsonArray data = GeneralTools.JsonReport_To_DataJsonArray(Json);

        //To list of media
        List<Media> medias = new ArrayList<Media>();
        for (int i = 0; i<data.size(); i++) {
            JsonObject elem = data.get(i).getAsJsonObject();

            //try to parse into media
            try {
                medias.add(Media.FromJson(elem, gson));
            } catch (MediaTypeNotMatchedException e) {
                e.printStackTrace();
            }

        }

        return medias;
    }

}

