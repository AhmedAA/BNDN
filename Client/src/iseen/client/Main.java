package iseen.client;

import iseen.client.Entities.Media;
import iseen.client.Entities.Report;
import iseen.client.Exceptions.GeneralError;
import iseen.client.Helpers.HttpCommunication;

import java.io.IOException;
import java.text.ParseException;
import java.util.ArrayList;

public class Main {

    public static void main(String[] args) throws ParseException, IOException, GeneralError {

        ArrayList<Media> medias = HttpCommunication.GetMedia();
        System.out.println("===========================================");
        System.out.println("Medias received");
        System.out.println("===========================================");
        for (int i = 0; i<medias.size(); i++) {
            System.out.println("Title: " + medias.get(i).Title);
            System.out.println("Id: " + medias.get(i).Id);
            System.out.println("Type: " + medias.get(i).Type);
            System.out.println("Release Date: " + medias.get(i).ReleaseDate.toString());
            System.out.println("Description: " + medias.get(i).Description);
            System.out.println("======================");
        }

    }
}
