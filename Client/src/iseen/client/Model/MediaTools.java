package iseen.client.Model;

import com.google.gson.*;
import com.google.gson.internal.LinkedTreeMap;
import com.google.gson.reflect.TypeToken;
import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.MediaFormats.Movie;
import iseen.client.Entities.MediaFormats.Music;
import iseen.client.Entities.MediaFormats.Picture;
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

    private static String PATH_TEST1 = "Test/1";

    private static Gson gson = new Gson();

    public static List<Media> GetAllMedia() throws Exception {
        //TODO: IMPLEMENT GET ALL MEDIA (venter på rasmus fixer migration)
        //Kaldet giver:
        //
        //HTTP/1.1 200 OK
        //Content-Length: 370
        //Content-Type: application/octet-stream
        //Server: Microsoft-HTTPAPI/2.0
        //Date: Fri, 21 Mar 2014 12:04:49 GMT
        //
        //{"Data":[{"Director":"Al Pacino","Id":0,"Title":"Die hard","Type":1,"ReleaseDate":"01-01-0001 00:00:00","Description":null},{"Artist":"The piano man","Id":0,"Title":"Ghetto gospel","Type":2,"ReleaseDate":"01-01-0001 00:00:00","Description":null},{"Author":"Göbels","Id":0,"Title":"The Scream","Type":3,"ReleaseDate":"01-01-0001 00:00:00","Description":null}],"Error":0}
        //
        //Json object kan bruges som streng direkte til at test :-)
        
        return JsonReportOfListOfMedia_To_ListOfMedia(HttpCommunication.sendGet(PATH_TEST1));
    }

    public static Media GetMediaById(int id) throws MediaTypeNotMatchedException, GeneralError {
        //TODO Actually use http communication
        return JsonReportOfMedia_To_Media("{\"Data\":{\"Id\":1,\"Title\":\"Once upon a time in America\",\"Type\":0,\"ReleaseDate\":\"01-01-1993 00:00:00\",\"Description\":\"Movie with Robert Di Niro\"},\"Error\":0}");
    }

    public static Media RentMedia(int id) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
    }

    public static Media CreateNewMedia(Media media, byte[] file) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
    }

    public static Media EditMedia(Media media) {
        return EditMedia(media, null, false);
    }

    public static Media EditMedia(Media media, byte[] file) {
        return EditMedia(media, file, true);
    }

    public static Media DeleteMedia(int id) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
    }

    private static Media EditMedia(Media media, byte[] file, boolean FileWasEdited) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
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

        System.out.println( ((Movie)medias.get(0)).Title + " " + ((Movie)medias.get(0)).Director );
        System.out.println( ((Music)medias.get(1)).Title + " " + ((Music)medias.get(1)).Artist );
        System.out.println( ((Picture)medias.get(2)).Title + " " + ((Picture)medias.get(2)).Author );

        return medias;
    }
}

