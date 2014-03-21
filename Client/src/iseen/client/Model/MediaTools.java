package iseen.client.Model;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.internal.LinkedTreeMap;
import com.google.gson.reflect.TypeToken;
import iseen.client.Entities.MediaFormats.Media;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;
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

    public static List<Media> GetAllMedia () throws Exception {
        return JsonReportOfListOfMedia_To_ListOfMedia(HttpCommunication.sendGet(PATH_TEST1));
    }

    public static Media GetMediaById (int id) {
        throw new NotImplementedException();
    }

    public static Media RentMedia (int id) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
    }

    public static Media CreateNewMedia (Media media, byte[] file) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
    }

    public static Media EditMedia (Media media) {
        return EditMedia(media, null, false);
    }

    public static Media EditMedia (Media media, byte[] file) {
        return EditMedia(media, file, true);
    }

    private static Media EditMedia (Media media, byte[] file, boolean FileWasEdited) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
    }

    public static Media DeleteMedia (int id) {

        //Should send the potato in the message body
        PotatoTools.Potato_To_Json();

        throw new NotImplementedException();
    }

    private static Media JsonReportOfMedia_To_Media (String Json) {
        //Forslag: udpak Report til en Rapport indeholdende Json
        //Derefter return Media.FromJson (STRENG);

        throw new NotImplementedException();
    }

    private static List<Media> JsonReportOfListOfMedia_To_ListOfMedia (String Json) {
        System.out.println(Json);

        return null;
    }

    public static String File_To_Json (byte[] file) {
        throw new NotImplementedException();
    }


    //TODO: BELOW SHALL NOT PASS (on in the next milestone)!!!

    public static ArrayList<Media> GetMedia(String service) throws IOException, ParseException, GeneralError {
        //get request
        String requestResult = HttpCommunication.sendGet("media/byid/1");

        //REFACTOR
        Gson gson = new Gson();
        Report fromJson = gson.fromJson(requestResult, Report.class);
        com.google.gson.internal.LinkedTreeMap LTM = (com.google.gson.internal.LinkedTreeMap)fromJson.Data;

        return SingleMedia(LTM);

        //create object from json
        //return JsonToListMedia(requestResult);
    }

    //TODO: Better implementation
    private static ArrayList<Media> JsonToListMedia(String JSONString) throws ParseException, GeneralError {
        //make object from response (JSON)
        Gson gson = new Gson();
        Report<ArrayList> fromJson = gson.fromJson(JSONString, Report.class);
        //List to be used for medias
        ArrayList<Media> medias = new ArrayList<Media>();

        //check if there is an error
        //TODO: GENERAL ERROR
        if (fromJson.Error!=0)
            throw new GeneralError(Integer.toString(fromJson.Error));

        //Iterate through report now consisting of LinkedTreeMaps
        for (int i = 0; i<fromJson.Data.size(); i++) {
            com.google.gson.internal.LinkedTreeMap LTM = (com.google.gson.internal.LinkedTreeMap)fromJson.Data.get(i);

            //parse to media
            Media newMedia = JSONtoMedia(LTM);
            medias.add(newMedia);
        }

        return medias;
    }

    private static ArrayList<Media> SingleMedia (com.google.gson.internal.LinkedTreeMap LTM) throws ParseException {
        ArrayList<Media> medias = new ArrayList<Media>();
        Media newMedia = JSONtoMedia(LTM);
        medias.add(newMedia);
        return medias;
    }

    private static Media JSONtoMedia (LinkedTreeMap LTM) throws ParseException {
        Media newMedia = new Media();

        //parse to media
        if (LTM.containsKey("Title"))
            newMedia.Title = (String)LTM.get("Title");
        if (LTM.containsKey("Description"))
            newMedia.Description = (String)LTM.get("Description");
        if (LTM.containsKey("Id"))
            newMedia.Id = ((Double)LTM.get("Id")).intValue();
        if (LTM.containsKey("Type"))
            newMedia.Type = ((Double)LTM.get("Type")).intValue();

        DateFormat df = new SimpleDateFormat("dd-MM-yyyy kk:mm:ss");
        if (LTM.containsKey("ReleaseDate"))
            newMedia.ReleaseDate = df.parse((String)LTM.get("ReleaseDate"));

        return newMedia;
    }
}
