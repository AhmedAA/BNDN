package iseen.client.Model;

import com.google.gson.*;
import com.google.gson.internal.LinkedTreeMap;
import com.google.gson.reflect.TypeToken;
import iseen.client.Entities.MediaFormats.*;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;
import iseen.client.Exceptions.MediaTypeNotMatchedException;
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

    private static Gson gson = new Gson();

    public static List<Media> GetAllMedia() throws Exception {
        return JsonReportOfListOfMedia_To_ListOfMedia(HttpCommunication.sendGet(PATH_ALL_MEDIA));
    }

    public static Media GetMediaById(int id) throws MediaTypeNotMatchedException, GeneralError, IOException {
        return JsonReportOfMedia_To_Media(HttpCommunication.sendGet(PATH_MEDIA_BY_ID + id));
    }

    public static Media RentMedia(int id) throws Exception {

        String response = HttpCommunication.sendPostPut(PATH_MEDIA_RENT,"[\"" + id + "\",\"" + PotatoTools.Potato_To_Json()+ "\"]",true);

        return JsonReportOfMedia_To_Media(response);
    }

    public static Media CreateNewMedia(Media media, byte[] file) throws Exception {
        //build the json string
        StringBuilder sb = new StringBuilder();
        sb.append('[');
        sb.append(PotatoTools.Potato_To_Json());
        sb.append(',');
        sb.append(media.ToJson(gson));
        sb.append(',');
        sb.append(GeneralTools.File_To_Json(file));
        sb.append(']');

        String response = HttpCommunication.sendPostPut(PATH_NEW_MEDIA, sb.toString(), true);

        return JsonReportOfMedia_To_Media(response);
    }

    public static Media EditMedia(Media media) throws Exception {
        return EditMedia(media, null, false);
    }

    public static Media EditMedia(Media media, byte[] file) throws Exception {
        return EditMedia(media, file, true);
    }

    //TODO: Not sure this method will work.
    public static Media DeleteMedia(int id) throws Exception {
        String response = HttpCommunication.sendPostPut(PATH_DELETE_MEDIA,"[\"" + id + "\",\"" + PotatoTools.Potato_To_Json()+ "\"]",true);

        return JsonReportOfMedia_To_Media(response);
    }

    private static Media EditMedia(Media media, byte[] file, boolean FileWasEdited) throws Exception {
        //build the json string
        StringBuilder sb = new StringBuilder();
        sb.append('[');
        sb.append(PotatoTools.Potato_To_Json());
        sb.append(',');
        sb.append(media.ToJson(gson));

        if (FileWasEdited == true) {
            sb.append(',');
            sb.append(GeneralTools.File_To_Json(file));
        }

        sb.append(']');

        String response = HttpCommunication.sendPostPut(PATH_EDIT_MEDIA,sb.toString(),false);

        return JsonReportOfMedia_To_Media(response);
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

