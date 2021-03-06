package iseen.client.Model;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

/**
 * Created by SebastianDybdal on 18-03-2014.
 *
 * Used for basic http communication with the server ISeen server, placed on ITUs rentit server.
 * The other classes use this send put and get requests.
 */
public class HttpCommunication {

    private static String USER_AGENT = "Mozilla/5.0";
    private static String RENT_ITU_PATH = "http://rentit.itu.dk/rentit02/Seenservice.svc/";


    // HTTP POST request
    public static String sendPostPut(String path, String Json, boolean Post) throws Exception {
        String method;

        if (Post)
            method = "POST";
        else
            method = "PUT";

        String url = RENT_ITU_PATH + path;
        URL obj = new URL(url);
        HttpURLConnection con = (HttpURLConnection) obj.openConnection();
        con.setRequestMethod(method);
        con.setRequestProperty("User-Agent", USER_AGENT);
        con.setRequestProperty("Accept-Language", "en-US,en;q=0.5");
        con.setRequestProperty("Content-Type", "text/plain");
        con.setDoOutput(true);
        DataOutputStream wr = new DataOutputStream(con.getOutputStream());
        wr.writeBytes(Json);
        wr.flush();
        wr.close();
        int responseCode = con.getResponseCode();
        System.out.println("\nSending '" + method + "' request to URL : " + url);
        System.out.println(method + " parameters : " + Json);
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
    public static String sendGet(String path) throws IOException {
        String url = RENT_ITU_PATH + path;
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
