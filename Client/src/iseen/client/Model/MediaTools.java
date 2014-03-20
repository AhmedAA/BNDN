package iseen.client.Model;

import com.google.gson.Gson;
import com.google.gson.internal.LinkedTreeMap;
import iseen.client.Entities.Media;
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

    public static List<Media> GetAllMedia () {
        throw new NotImplementedException();
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

    public static Media JsonReportOfMedia_To_Media (String Json) {
        throw new NotImplementedException();
    }

    public static List<Media> JsonReportOfListOfMedia_To_ListOfMedia (String Json) {
        throw new NotImplementedException();
    }

    public static String Media_To_Json (Media media) {
        throw new NotImplementedException();
    }

    public static String File_To_Json (byte[] file) {
        throw new NotImplementedException();
    }


    //TODO: BELOW SHALL NOT PASS (on in the next milestone)!!!

    public static ArrayList<iseen.client.Entities.Media> GetMedia(String service) throws IOException, ParseException, GeneralError {
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
    private static ArrayList<iseen.client.Entities.Media> JsonToListMedia(String JSONString) throws ParseException, GeneralError {
        //make object from response (JSON)
        Gson gson = new Gson();
        Report<ArrayList> fromJson = gson.fromJson(JSONString, Report.class);
        //List to be used for medias
        ArrayList<iseen.client.Entities.Media> medias = new ArrayList<iseen.client.Entities.Media>();

        //check if there is an error
        //TODO: GENERAL ERROR
        if (fromJson.Error!=0)
            throw new GeneralError(Integer.toString(fromJson.Error));

        //Iterate through report now consisting of LinkedTreeMaps
        for (int i = 0; i<fromJson.Data.size(); i++) {
            com.google.gson.internal.LinkedTreeMap LTM = (com.google.gson.internal.LinkedTreeMap)fromJson.Data.get(i);

            //parse to media
            iseen.client.Entities.Media newMedia = JSONtoMedia(LTM);
            medias.add(newMedia);
        }

        return medias;
    }

    private static ArrayList<iseen.client.Entities.Media> SingleMedia (com.google.gson.internal.LinkedTreeMap LTM) throws ParseException {
        ArrayList<iseen.client.Entities.Media> medias = new ArrayList<iseen.client.Entities.Media>();
        iseen.client.Entities.Media newMedia = JSONtoMedia(LTM);
        medias.add(newMedia);
        return medias;
    }

    private static iseen.client.Entities.Media JSONtoMedia (LinkedTreeMap LTM) throws ParseException {
        iseen.client.Entities.Media newMedia = new iseen.client.Entities.Media();

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