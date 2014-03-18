/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

package iseenclient;

import com.google.gson.Gson;
import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.net.ssl.HttpsURLConnection;
import javax.swing.JFrame;

/**
 *
 * @author neo
 */
public class ISeenClient {
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        // TODO code application logic here
        MainWindow frame = new MainWindow();
//        try {
//            frame.testerTextField.setText(sendPost());
//        } catch (Exception ex) {
//            Logger.getLogger(ISeenClient.class.getName()).log(Level.SEVERE, null, ex);
//        }
        try {
            frame.testerTextField.setText(httpCommunication.sendGet("media"));
        } catch (Exception ex) {
            Logger.getLogger(ISeenClient.class.getName()).log(Level.SEVERE, null, ex);
        }
        frame.show();
    }
    
}
