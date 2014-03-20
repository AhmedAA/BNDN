package iseen.client.Model;

import com.google.gson.Gson;
import iseen.client.Entities.Media;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.URL;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;

/**
 * Created by SebastianDybdal on 18-03-2014.
 */
public class HttpCommunication {

    private static String USER_AGENT = "Mozilla/5.0";

    public static ArrayList<Media> GetMedia() throws IOException, ParseException, GeneralError {
        //get request
        String requestResult = sendGet("media");

        //create object from json
        return JsonToListMedia(requestResult);
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
            Media newMedia = new Media();
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

            medias.add(newMedia);
        }

        return medias;
    }

    // HTTP POST request
    private static String sendPost() throws Exception {
        String url = "http://rentit.itu.dk/rentit02/media";
        URL obj = new URL(url);
        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        con.setRequestMethod("POST");
        con.setRequestProperty("User-Agent", USER_AGENT);
        con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
        String urlParameters = "media";
        con.setDoOutput(true);
        DataOutputStream wr = new DataOutputStream(con.getOutputStream());
        wr.writeBytes(urlParameters);
        wr.flush();
        wr.close();
        int responseCode = con.getResponseCode();
        System.out.println("\nSending 'POST' request to URL : " + url);
        System.out.println("Post parameters : " + urlParameters);
        System.out.println("Response Code : " + responseCode);
        BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
        String inputLine;
        StringBuffer response = new StringBuffer();
        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }
        in.close();
        return response.toString();
    }

    // HTTP GET request
    private static String sendGet(String service) throws IOException {
        String url = "http://rentit.itu.dk/rentit02/" + service;
        URL obj = new URL(url);
        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        con.setRequestMethod("GET");
        con.setRequestProperty("User-Agent", USER_AGENT);
        int responseCode = con.getResponseCode();
        System.out.println("\nSending 'GET' request to URL : " + url);
        System.out.println("Response Code : " + responseCode);
        BufferedReader in = new BufferedReader(new InputStreamReader(con.getInputStream()));
        String inputLine;
        StringBuffer response = new StringBuffer();
        while ((inputLine = in.readLine()) != null) {
            response.append(inputLine);
        }
        in.close();
        return response.toString();
    }

}
