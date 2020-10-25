using System;
using System.Text;

namespace VoiceAssistant
{
    public class AssistantCommands
    {

        public AssistantCommands()
        {
        }

        public StringBuilder Commands(StringBuilder text, Form1 _form1)
        {
            // Figure out how to use a dictionary for this.
            //Form1.commandDictionary.Add("Hello", "Hello, my name is Ozzie. How can I help you?");
            //Form1.speech.Append
            
            
            switch (Convert.ToString(text))
            {
                case "Logan.":
                    Manager.speechSB.Clear();
                    Manager.assistant.SpeakAsyncCancelAll();
                    Manager.speechSB.Append("Yes?");
                    break;
                case "What time is it?":
                    Manager.speechSB.Clear();
                    Manager.speechSB.Append("It is currently " + DateTime.Now.ToShortTimeString());
                    break;
                case "Open Google.":
                    Manager.speechSB.Clear();
                    System.Diagnostics.Process.Start("https://www.google.com");
                    Manager.speechSB.Append("Opening Google");
                    break;
                case "Close Google.":
                    Manager.speechSB.Clear();
                    System.Diagnostics.Process[] close = System.Diagnostics.Process.GetProcessesByName("chrome");
                    foreach (System.Diagnostics.Process process in close) process.Kill();
                    Manager.speechSB.Append("Closing Chrome");
                    break;
                case "Faded.":
                    Manager.speechSB.Clear();
                    Manager.music.SoundLocation = "faded.wav";
                    Manager.music.Play();
                    Manager.speechSB.Append("");
                    break;
                case "Stop.":
                    Manager.speechSB.Clear();
                    int rndResponse;
                    Manager.assistant.SpeakAsyncCancelAll();
                    Manager.music.Stop();
                    rndResponse = Manager.random.Next(1,2);
                    if (rndResponse == 1) Manager.speechSB.Append("Okay. If you need me just ask");
                    else if (rndResponse == 2) Manager.speechSB.Append("Will do. If you need me just ask");
                    Manager.recognitionEngine.RecognizeAsyncCancel(); // Turn off
                    Manager.listeningEngine.RecognizeAsync(System.Speech.Recognition.RecognizeMode.Multiple); // Turn on
                    break;
                case "Show commands.":
                    Manager.speechSB.Clear();
                    _form1.showCommands_lstBx.Items.Clear();
                    _form1.showCommands_lstBx.SelectionMode = System.Windows.Forms.SelectionMode.None;
                    _form1.showCommands_lstBx.Visible = true;
                    foreach (string commands in Manager.grammerCommands) _form1.showCommands_lstBx.Items.Add(commands);
                    break;
                case "Hide commands.":
                    Manager.speechSB.Clear();
                    _form1.showCommands_lstBx.Visible = false;
                    break;
                default:
                    Manager.speechSB.Clear();
                    Manager.speechSB.Append("Should I repeat that for you?");
                    // Branch this out to answer the yes or no

                    break;
            }
            return text;
        }
    }
}
